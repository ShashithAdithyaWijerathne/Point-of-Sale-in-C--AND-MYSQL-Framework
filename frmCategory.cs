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
    public partial class frmCategory : Form
    {
        public frmCategory()
        {
            InitializeComponent();
        }
        int categoryid = 0;
        SQLConfig config = new SQLConfig();
        private void frmCategory_Load(object sender, EventArgs e)
        {

            config.sqlselect = "SELECT c.CategoryId,Category,CategoryType,Unit FROM tblcategory c,tblautonumber a WHERE c.CategoryId=a.CategoryId";
            config.LoadData(config.sqlselect, dtglist);
            txtcategory.Clear();
            txtUnit.Clear();
            txtType.Clear();
            categoryid = 0;
            dtglist.Columns[0].Visible = false;

            config.FillAutonumber(4, lblCategoryId);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ////SQLConfig config = new SQLConfig();


            config.sqlselect = "SELECT * FROM tblcategory WHERE CategoryId=" + lblCategoryId.Text;
            config.sqladd = "INSERT INTO tblcategory (CategoryId,Category,CategoryType,Unit) VALUES ('" + lblCategoryId.Text + "','" + txtcategory.Text + "','" + txtType.Text + "','" + txtUnit.Text + "')";
            config.sqledit = "UPDATE tblcategory SET Category='" + txtcategory.Text + "',CategoryType='" + txtType.Text + "',Unit='" + txtUnit.Text + "' WHERE CategoryId=" + lblCategoryId.Text;
            config.msgadd = "New Category has been saved in the database.";
            config.msgedit = "Category has been updated in the database.";
            config.SaveUpdate(config.sqlselect, config.sqladd, config.msgadd, config.sqledit, config.msgedit);

            

            string autoStart;
            autoStart = txtcategory.Text.Substring(0, 3) + "-" + txtType.Text.Substring(0, 3) + "-";

            if (categoryid == 0)
            {

                config.sqladd = "INSERT INTO tblautonumber (AutoStart,AutoEnd,AutoIncrement,CategoryId) VALUES ('" + autoStart + "',1,1," + lblCategoryId.Text + ")";
                config.SaveData(config.sqladd);

            }
            else
            {
                config.sqledit = "UPDATE tblautonumber SET AutoStart ='" + autoStart + "' WHERE CategoryId=" + lblCategoryId.Text;
                config.SaveData(config.sqledit);
            }


            btnNew_Click(sender, e);
            //frmCategory_Load(sender, e);

            //MessageBox.Show(txtcategory.Text.Substring(0, 3));
            //MessageBox.Show(txtType.Text.Substring(0, 3)); 


        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            config.sqlselect = "SELECT c.CategoryId,Category,CategoryType,Unit FROM tblcategory c,tblautonumber a WHERE c.CategoryId=a.CategoryId";
            config.LoadData(config.sqlselect, dtglist);
            txtcategory.Clear();
            txtUnit.Clear();
            txtType.Clear();
            categoryid = 0;
            dtglist.Columns[0].Visible = false;

            config.FillAutonumber(4, lblCategoryId);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //SQLConfig config = new SQLConfig();
            config.sqlselect = "SELECT c.CategoryId,Category,CategoryType,Unit FROM tblcategory c,tblautonumber a WHERE c.CategoryId=a.CategoryId AND Category Like '%" + txtSearch.Text + "%'";
            config.LoadData(config.sqlselect, dtglist);

            txtcategory.Clear();
            txtUnit.Clear();
            txtType.Clear();
            categoryid = 0;
            dtglist.Columns[0].Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            { //SQLConfig config = new SQLConfig();
                config.sql = "DELETE FROM tblcategory WHERE CategoryId=" + dtglist.CurrentRow.Cells[0].Value;
                config.SaveDataMsg(config.sql, "Category has been deleted in the database.");
                btnNew_Click(sender, e);
            }
            catch
            {
            }
        }

        private void dtglist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                categoryid = (int)(dtglist.CurrentRow.Cells[0].Value);
                lblCategoryId.Text = dtglist.CurrentRow.Cells[0].Value.ToString();
                txtcategory.Text = dtglist.CurrentRow.Cells[1].Value.ToString();
                txtType.Text = dtglist.CurrentRow.Cells[2].Value.ToString();
                txtUnit.Text = dtglist.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                this.Text = ex.Message;
            }
        }
    }
}
