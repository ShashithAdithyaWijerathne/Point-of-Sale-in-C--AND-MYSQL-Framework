using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSBunifu.Config;

namespace POSBunifu
{
    public partial class frmStockout : Form
    {
        public frmStockout()
        {
            InitializeComponent();
        }
        SQLConfig pro = new SQLConfig();
        private void frmStockout_Load(object sender, EventArgs e)
        {
            txtBarcode.MaxLength = 11;
            txtBarcode.Focus();

            pro.FillAutonumber(11, lblTransactionId);

            txtChange.Visible = false;
            txtAmountTender.Visible = false;
            lblamountTender.Visible = false;
            lblchange.Visible = false;
            txtTotalAmount.Size = new Size(150, 76);
            timer1.Start();
            pro.ResponsiveDtg(dtgList);
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //int maxrow = 0;
                //string itemid = txtBarcode.Text;
                double vat = 0.0;
                double totvat = 0.0;
                double tot = 0.0;

                if (txtBarcode.Text.Length == 11)
                {
                    pro.sqlselect = "SELECT * FROM tblcategory c, tblproduct p  WHERE p.CategoryId=c.CategoryId AND ProductQty > 0 And Barcode Like '%" + txtBarcode.Text + "%'";
                    pro.Single_Select(pro.sqlselect);
                    if (pro.dt.Rows.Count > 0)
                    {
                        decimal markup = pro.dt.Rows[0].Field<decimal>("MarkupPrice");
                        string[] r = new string[] {pro.dt.Rows[0].Field<string>("Barcode"),
                        pro.dt.Rows[0].Field<string>("ProductName"),
                        pro.dt.Rows[0].Field<string>("Description"),
                        markup.ToString("N2"),
                        "1", markup.ToString("N2")};


                        dtgList.Rows.Add(r);

                        txtBarcode.Clear();
                        txtBarcode.Focus();

                        for (int i = 0; i < dtgList.Rows.Count; i++)
                        {
                            tot += double.Parse(dtgList.Rows[i].Cells[5].Value.ToString());

                        }

                        txtSubTotal.Text = tot.ToString("N2");

                        vat = tot * 0.12;

                        //totvat =  tot - vat;
                        totvat = tot;
                        txtTotalAmount.Text = totvat.ToString("N2");




                        //        for(int i = 0;i<0;i++){

                        //        }

                        //For i = 0 To dtgCart.Rows.Count - 1
                        //    tot += dtgCart.Rows(i).Cells(5).Value
                        //Next

                    }
                }


            }
            catch (Exception ex)
            {
            }
        }

        private void tenderAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmTenderAmount frm = new frmTenderAmount(txtTotalAmount.Text);
            //frm.ShowDialog();
            //frm.Focus();
            if (dtgList.Rows.Count > 0 && txtBarcode.Enabled == true && pnlQty.Visible == false)
            {
                pnltenderAmount.Visible = true;
                txtTenderAmount.Focus();
                txtTenderAmount.Text = "0.00";
                txtTenderAmount.SelectAll();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (pnlQty.Visible == false && pnltenderAmount.Visible == false)
            {
                dtgList.Rows.Clear();
                txtSubTotal.Text = "0.00";
                txtTotalAmount.Text = "0.00";
                txtAmountTender.Text = "0.00";
                txtChange.Text = "0.00";
                txtChange.Visible = false;
                txtAmountTender.Visible = false;
                lblamountTender.Visible = false;
                lblchange.Visible = false;
                txtTotalAmount.Size = new Size(150, 76);
                pro.FillAutonumber(11, lblTransactionId);

                dtgList.Enabled = true;
                txtBarcode.Enabled = true;
                checkBox1.Enabled = true;
                btnVoid.Enabled = true;
                btnTender.Enabled = true;
                txtBarcode.Focus();
            }
        }

        private void voidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pnllogin.Visible = true;
                txtUsername.Focus();
                txtBarcode.Text = "";
                txtBarcode.Enabled = false;
                //pnllogin.Visible = true;
                //txtUsername.Focus();
                //dtgList.Rows.Remove(dtgList.CurrentRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void qtyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlQty.Visible = true;
            txtAddQty.Text = dtgList.CurrentRow.Cells[4].Value.ToString();
            txtAddQty.Focus();
            txtAddQty.SelectAll();
            lblTotqtyProduct.Text = dtgList.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            if (dtgList.Rows.Count > 0)
            {
                pnltenderAmount.Visible = true;
                txtTenderAmount.Focus();
                txtTenderAmount.Text = "0.00";
                txtTenderAmount.SelectAll();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnllogin.Visible = true;
            txtUsername.Focus();
            txtBarcode.Text = "";
            txtBarcode.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            qtyToolStripMenuItem_Click(sender, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dtgList.Rows.Clear();
            txtSubTotal.Text = "0.00";
            txtTotalAmount.Text = "0.00";
            txtAmountTender.Text = "0.00";
            txtChange.Text = "0.00";
            txtChange.Visible = false;
            txtAmountTender.Visible = false;
            lblamountTender.Visible = false;
            lblchange.Visible = false;
            txtTotalAmount.Size = new Size(150, 76);
            pro.FillAutonumber(11, lblTransactionId);

            dtgList.Enabled = true;
            txtBarcode.Enabled = true;
            checkBox1.Enabled = true;
            btnVoid.Enabled = true;
            btnTender.Enabled = true;
            txtBarcode.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Form frm = new frmLogin();
            frm.Show();
        }

        private void txtAddQty_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double vat = 0.0;
                double totvat = 0.0;
                double tot = 0.0;


                double price = double.Parse(dtgList.CurrentRow.Cells[3].Value.ToString());
                double qty = double.Parse(txtAddQty.Text);
                double total;

                total = price * qty;

                if (e.KeyCode == Keys.Enter)
                {

                    dtgList.CurrentRow.Cells[4].Value = txtAddQty.Text;
                    txtAddQty.Text = "";
                    pnlQty.Visible = false;
                    txtBarcode.Focus();
                    dtgList.CurrentRow.Cells[5].Value = total.ToString("n2");


                    for (int i = 0; i < dtgList.Rows.Count; i++)
                    {
                        tot += double.Parse(dtgList.Rows[i].Cells[5].Value.ToString());

                    }

                    txtSubTotal.Text = tot.ToString("N2");

                    vat = tot * 0.12;

                    //totvat = tot - vat;
                    totvat = tot;
                    txtTotalAmount.Text = totvat.ToString("N2");



                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtTenderAmount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double subtot = double.Parse(txtTotalAmount.Text);
                double tender = double.Parse(txtTenderAmount.Text);
                DateTime today = DateTime.Now;
                int totqty = 0;

                if (e.KeyCode == Keys.Enter)
                {
                    //MessageBox.Show(subtot.ToString());
                    //MessageBox.Show(tender.ToString());


                    if (tender >= subtot)
                    {
                        pnltenderAmount.Visible = false;
                        txtTotalAmount.Size = new Size(150, 35);
                        txtChange.Visible = true;
                        txtAmountTender.Visible = true;
                        lblamountTender.Visible = true;
                        lblchange.Visible = true;


                        foreach (DataGridViewRow row in dtgList.Rows)
                        {

                            pro.sqladd = "INSERT INTO tbltransaction (InvoiceNo,Barcode,TransactionDate,Price,TransVat,TransDiscount,TransactionQty,SubTotal,UserId) " +
                                "Values (" + lblTransactionId.Text + ",'" + row.Cells[0].Value +
                                "','" + today + "','" + row.Cells[3].Value + "','12','" + txtDiscount.Text +
                                "','" + row.Cells[4].Value + "','" + row.Cells[5].Value + "','" + UserIdStatusStrip.Text + "')";
                            //pro.SaveDataMsg(pro.sqladd, "Transaction has been saved!");
                            pro.SaveData(pro.sqladd);

                            pro.sqledit = "UPDATE tblproduct Set ProductQty=ProductQty-" + row.Cells[4].Value + " WHERE Barcode='" + row.Cells[0].Value + "'";
                            pro.SaveData(pro.sqledit);

                            totqty += int.Parse(row.Cells[4].Value.ToString());




                        }

                        pro.sqladd = "INSERT INTO tblsummary (InvoiceNo,TransactionDate,TotalQty,TransDiscount,TotalAmount,AmountTendered,Change) " +
                            " VALUES (" + lblTransactionId.Text + ",'" + today + "','" + totqty + "','" + txtDiscount.Text + "','" + txtTotalAmount.Text +
                            "','" + txtAmountTender.Text + "','" + txtChange.Text + "')";
                        pro.SaveData(pro.sqladd);



                        pro.UpdateAutonumber(11);
                        dtgList.Enabled = false;
                        txtBarcode.Enabled = false;
                        checkBox1.Enabled = false;
                        btnVoid.Enabled = false;
                        btnTender.Enabled = false;
                        totqty = 0;


                        frmInvoice frm = new frmInvoice(lblTransactionId.Text);
                        frm.ShowDialog();




                    }
                    else
                    {
                        MessageBox.Show("The amount you tendered is not valid.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                //MessageBox.Show("The amount you tendered is not valid." +  ex.Message);
            }
        }

        private void txtTenderAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtTenderAmount.Text == "")
                {
                    txtChange.Text = "0.00";

                    txtAmountTender.Text = "0.00";
                    //this.AcceptButton = btnCancel;
                }
                else
                {
                    double subtotal = double.Parse(txtTotalAmount.Text);
                    double tendered = double.Parse(txtTenderAmount.Text);
                    double change;

                    change = tendered - subtotal;
                    txtAmountTender.Text = tendered.ToString("N2");
                    txtChange.Text = change.ToString("N2");


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {


                if (txtUsername.Text == "staff" && txtPass.Text == "1234")
                {
                    double vat = 0.0;
                    double totvat = 0.0;
                    double tot = 0.0;

                    dtgList.Rows.Remove(dtgList.CurrentRow);
                    txtBarcode.Clear();
                    txtBarcode.Focus();

                    for (int i = 0; i < dtgList.Rows.Count; i++)
                    {
                        tot += double.Parse(dtgList.Rows[i].Cells[5].Value.ToString());

                    }

                    txtSubTotal.Text = tot.ToString("N2");

                    vat = tot * 0.12;

                    //totvat = tot - vat;
                    totvat = tot;
                    txtTotalAmount.Text = totvat.ToString("N2");
                    pnllogin.Visible = false;
                }
                else
                {
                    MessageBox.Show("Account does not exist.", "Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


            }
            catch (Exception ex)
            {
            }
        }

        private void btnLoginClose_Click(object sender, EventArgs e)
        {
            pnllogin.Visible = false;
            txtPass.Text = "";
            txtUsername.Text = "";
            txtBarcode.Enabled = true;
            txtBarcode.Text = "";
            txtBarcode.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            DateTimeStatusStrip.Text = today.ToShortDateString();
            toolStripStatusLabel6.Text = today.ToLongTimeString();
        }
    }
}
