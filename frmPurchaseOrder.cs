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
    public partial class frmPurchaseOrder : Form
    {
        public frmPurchaseOrder()
        {
            InitializeComponent();
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
            txtBarcode.Focus();

        }
        private void frmPurchaseOrder_Load(object sender, EventArgs e)
        {
            pro.sqlselect = "SELECT OrderDate,p.Barcode,ProductName,Description,Category,p.OriginalPrice,OrderQty,Unit,OrderTotal FROM tblcategory c, tblproduct p,tblorder o WHERE p.CategoryId=c.CategoryId AND p.Barcode=o.Barcode";
            pro.LoadData(pro.sqlselect, dtgList);
            txtBarcode.MaxLength = 11;
            pro.FillComboBox("SELECT * FROM tblsupplier", "SupplierId", "Supplier", comboBox1);
            clearStockin();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text != "")
                {
                    if (txtBarcode.Text.Length >= 10)
                    {

                        pro.sqlselect = "SELECT * FROM tblcategory c, tblproduct p  WHERE p.CategoryId=c.CategoryId And Barcode Like '%" + txtBarcode.Text + "%'";
                        pro.Single_Select(pro.sqlselect);

                        if (pro.dt.Rows.Count > 0)
                        {
                            decimal price;
                            txtProduct.Text = pro.dt.Rows[0].Field<string>("ProductName");
                            txtDescription.Text = pro.dt.Rows[0].Field<string>("Description");
                            txtCategory.Text = pro.dt.Rows[0].Field<string>("Category");
                            price = pro.dt.Rows[0].Field<decimal>("OriginalPrice");
                            txtPrice.Text = price.ToString("N2");
                        }
                        else
                        {
                            txtProduct.Clear();
                            txtDescription.Clear();
                            txtPrice.Clear();
                            txtCategory.Clear();
                            txtQty.Clear();
                            txtBarcode.Focus();

                        }

                    }
                    else
                    {

                        txtProduct.Clear();
                        txtDescription.Clear();
                        txtPrice.Clear();
                        txtCategory.Clear();
                        txtQty.Clear();
                        txtBarcode.Focus();

                    }

                }
                else
                {
                    txtProduct.Clear();
                    txtDescription.Clear();
                    txtPrice.Clear();
                    txtCategory.Clear();
                    txtQty.Clear();
                    txtBarcode.Focus();

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
                DateTime today = DateTime.Now;
                double price = double.Parse(txtPrice.Text.ToString());
                int qty = int.Parse(txtQty.Text.ToString());
                double tot;

                tot = price * qty;



                //pro.sql = "Select * From tblorder WHERE ";
                //pro.Single_Select(pro.sql);
                //if (pro.dt.Rows.Count > 0)
                //{
                //}
                //else
                //{
                pro.sqladd = "INSERT INTO tblorder (OrderDate,Barcode,OrderQty,OrderTotal,SupplierId,Rem) " +
                    " VALUES ('" + today + "','" + txtBarcode.Text + "'," + txtQty.Text + ",'" + tot.ToString("n2") + "','" + comboBox1.SelectedValue + "','Ordered')";
                pro.SaveDataMsg(pro.sqladd, "New order has been saved in the database.");
                //}
                frmPurchaseOrder_Load(sender, e);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            frmPurchaseOrder_Load(sender, e);
        }
    }
}
