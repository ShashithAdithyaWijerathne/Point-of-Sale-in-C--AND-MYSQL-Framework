using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSBunifu.Config;

namespace POSBunifu
{
    public partial class frmTransaction : Form
    {
        public frmTransaction()
        {
            InitializeComponent();
        }
        //initialize the validating method 
        static Regex validNumbers = NumbersOnly();



        //Method for numbers validation only
        private static Regex NumbersOnly()
        {
            string StringAndNumber_Pattern = "^[0-9]*$";

            return new Regex(StringAndNumber_Pattern, RegexOptions.IgnoreCase);
        }

        SQLConfig pro = new SQLConfig();
        UsableFunction useFunc = new UsableFunction();

        private void clearStockin()
        {
            txtBarcode.Clear();
            txtProduct.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtCategory.Clear();
            txtQty.Clear();


        }
        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text != "")
                {
                    if (txtBarcode.Text.Length >= 10)
                    {

                        pro.sqlselect = "SELECT OrderId,Supplier,OrderDate,p.Barcode,ProductName,Description,Category,p.OriginalPrice,OrderQty,Unit,OrderTotal " +
                            " FROM tblcategory c, tblproduct p,tblorder o , tblsupplier s " +
                            " WHERE p.CategoryId=c.CategoryId AND p.Barcode=o.Barcode AND o.SupplierId=s.SupplierId AND Rem='Ordered' And p.Barcode = '" + txtBarcode.Text + "'";
                        pro.LoadData(pro.sqlselect, dtgOrderlist);
                        dtgOrderlist.Columns[0].Visible = false;

                        //pro.sqlselect = "SELECT * FROM tblcategory c, tblproduct p  WHERE p.CategoryId=c.CategoryId And Barcode Like '%" + txtBarcode.Text + "%'";
                        //pro.Single_Select(pro.sqlselect);

                        //if (pro.dt.Rows.Count > 0)
                        //{
                        //    decimal price;
                        //    txtProduct.Text = pro.dt.Rows[0].Field<string>("ProductName");
                        //    txtDescription.Text = pro.dt.Rows[0].Field<string>("Description");
                        //    txtCategory.Text = pro.dt.Rows[0].Field<string>("Category");
                        //    price = pro.dt.Rows[0].Field<decimal>("OriginalPrice");
                        //    txtPrice.Text = price.ToString("N2"); 
                        //}
                        //else
                        //{ 
                        //    clearStockin();
                        //}


                    }
                    else
                    {

                        txtProduct.Clear();
                        txtDescription.Clear();
                        txtPrice.Clear();
                        txtCategory.Clear();
                        txtQty.Clear();
                    }

                }
                else
                {
                    txtProduct.Clear();
                    txtDescription.Clear();
                    txtPrice.Clear();
                    txtCategory.Clear();
                    txtQty.Clear();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //double total;
                string today = DateTime.Now.ToString("yyyy-MM-dd");

                if (txtBarcode.Text == "" || dtgOrderlist.Rows.Count <= 0)
                {
                    MessageBox.Show("Fields are required.", "Filled up", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //total = double.Parse(txtPrice.Text) * Int32.Parse(txtQty.Text);

                    //pro.sqledit = "UPDATE tblproduct Set ProductQty = ProductQty + " + txtQty.Text  + "  WHERE Barcode = '" + txtBarcode.Text  + "'";
                    //pro.SaveData(pro.sqledit);


                    foreach (DataGridViewRow r in dtgOrderlist.Rows)
                    {
                        pro.sqledit = "UPDATE tblproduct Set ProductQty = ProductQty + " + r.Cells[8].Value + "  WHERE Barcode = '" + txtBarcode.Text + "'";
                        pro.SaveData(pro.sqledit);

                        pro.sqledit = "UPDATE tblorder Set Rem = 'Settled'  WHERE OrderId = " + r.Cells[0].Value;
                        pro.SaveData(pro.sqledit);


                        pro.sqladd = "INSERT INTO tblstockin (Barcode,DateReceived,Price,ReceivedQty,SubTotal,UserId,OrderId) " +
                            " Values('" + txtBarcode.Text + "','" + today + "','" + txtPrice.Text +
                            "'," + r.Cells[8].Value + ",'" + r.Cells[10].Value +
                            "',1," + r.Cells[0].Value + ")";
                        pro.SaveData(pro.sqladd);
                    }


                    MessageBox.Show("Product has been added to the inventory.");
                    frmTransaction_Load(sender, e);
                    clearStockin();
                    txtBarcode.Clear();
                    txtBarcode.Focus();


                    //pro.sqledit = "UPDATE tblorder Set Rem = 'Settled'  WHERE OrderId = " + dtgOrderlist.CurrentRow.Cells[0].Value;
                    //pro.SaveData(pro.sqledit);


                    ////pro.sqladd = "INSERT INTO tblstockin (Barcode,DateReceived,Price,ReceivedQty,SubTotal,UserId) " +
                    ////    " Values('" + txtBarcode.Text + "','" + today + "','" + txtPrice.Text + "'," + txtQty.Text + ",'" + total + "',1)";

                    //pro.sqladd = "INSERT INTO tblstockin (Barcode,DateReceived,Price,ReceivedQty,SubTotal,UserId,OrderId) " +
                    //    " Values('" + txtBarcode.Text + "','" + today + "','" + txtPrice.Text +
                    //    "'," + dtgOrderlist.CurrentRow.Cells[8].Value + ",'" + dtgOrderlist.CurrentRow.Cells[10].Value +
                    //    "',1," + dtgOrderlist.CurrentRow.Cells[0].Value + ")";

                    //pro.SaveDataMsg(pro.sqladd, "Product has been added to the inventory.");

                    //frmTransaction_Load(sender, e);

                    //clearStockin();
                    //txtBarcode.Clear();
                    //txtBarcode.Focus();
                    //pro.sqlselect = "SELECT OrderId,Supplier,OrderDate,p.Barcode,ProductName,Description,Category,p.OriginalPrice,OrderQty,Unit,OrderTotal " +
                    //        " FROM tblcategory c, tblproduct p,tblorder o , tblsupplier s " +
                    //        " WHERE p.CategoryId=c.CategoryId AND p.Barcode=o.Barcode AND o.SupplierId=s.SupplierId AND Rem='Ordered' And p.Barcode = '" + txtBarcode.Text + "'";
                    //pro.LoadData(pro.sqlselect, dtgOrderlist);
                    //dtgOrderlist.Columns[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void frmTransaction_Load(object sender, EventArgs e)
        {
            txtBarcode.MaxLength = 11;

            pro.sqlselect = "SELECT p.Barcode,ProductName,Category,p.OriginalPrice,DateReceived,ReceivedQty,Unit FROM tblcategory c, tblproduct p,tblstockin s  WHERE p.CategoryId=c.CategoryId AND p.Barcode=s.Barcode ORDER BY DateReceived Desc";
            pro.LoadData(pro.sqlselect, dtgList);

            pro.sqlselect = "SELECT OrderId,Supplier,OrderDate,p.Barcode,ProductName,Description,Category,p.OriginalPrice,OrderQty,Unit,OrderTotal " +
                            " FROM tblcategory c, tblproduct p,tblorder o , tblsupplier s " +
                            " WHERE p.CategoryId=c.CategoryId AND p.Barcode=o.Barcode AND o.SupplierId=s.SupplierId AND Rem='Ordered' And p.Barcode = '" + txtBarcode.Text + "'";
            pro.LoadData(pro.sqlselect, dtgOrderlist);
            dtgOrderlist.Columns[0].Visible = false;


            pro.ResponsiveDtg(dtgOrderlist);
        }
    }
}
