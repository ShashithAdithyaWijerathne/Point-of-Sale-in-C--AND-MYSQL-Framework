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
    public partial class frmSupplier : Form
    {
        public frmSupplier()
        {
            InitializeComponent();
        }
        SQLConfig sup = new SQLConfig();
        int supplierid = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
             
            try
            {
                sup.sqlselect = "SELECT * From tblsupplier WHERE SupplierId=" + supplierid;
                sup.Single_Select(sup.sqlselect);
                if (sup.dt.Rows.Count > 0)
                {
                    sup.sqledit = "UPDATE tblsupplier SET Supplier='" + txtSupplier.Text +
                      "',ContactNo='" + txtContactNo.Text + "',Company='" + txtCompany.Text +
                      "',CompanyAddress = '" + txtCompanyAddress.Text + "' WHERE SupplierId=" + supplierid;
                    sup.SaveDataMsg(sup.sqledit, "Supplier has been updated in the database.");
                }
                else
                {
                    sup.sqladd = "INSERT INTO tblsupplier (Supplier,ContactNo,Company,CompanyAddress) " +
                          " VALUES ('" + txtSupplier.Text + "','" + txtContactNo.Text
                          + "','" + txtCompany.Text + "','" + txtCompanyAddress.Text + "')";
                    sup.SaveDataMsg(sup.sqladd, "New Supplier has been saved in the database.");
                }
                frmSupplier_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            sup.sqlselect = "SELECT * From tblsupplier";
            sup.LoadData(sup.sqlselect, dtgList);

            txtSupplier.Clear();
            txtContactNo.Clear();
            txtCompanyAddress.Clear();
            txtCompany.Clear();
            txtSupplier.Focus();
            supplierid = 0;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmSupplier_Load(sender, e);
        }

        private void dtgList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                supplierid = Int32.Parse(dtgList.CurrentRow.Cells[0].Value.ToString());
                txtSupplier.Text = dtgList.CurrentRow.Cells[1].Value.ToString();
                txtContactNo.Text = dtgList.CurrentRow.Cells[2].Value.ToString();
                txtCompany.Text = dtgList.CurrentRow.Cells[3].Value.ToString();
                txtCompanyAddress.Text = dtgList.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
