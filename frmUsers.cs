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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        SQLConfig user = new SQLConfig();
        UsableFunction useFunc = new UsableFunction();

        private void frmUsers_Load(object sender, EventArgs e)
        {
            btnNew_Click(sender, e);

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            useFunc.clearTxt(this);

            cboRole.Text = "Administrator";

            user.FillAutonumber(2, lblUserId);

            user.sqlselect = "Select UserId,Fullname,User_Name as 'Username',UserRole as 'Role' FROM tbluser";
            user.LoadData(user.sqlselect, dtgList);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            user.sqlselect = "SELECT * FROM tbluser WHERE UserId =" + lblUserId.Text;

            user.sqladd = "INSERT INTO tbluser (UserId,Fullname,User_name,Pass,UserRole) VALUES " +
                " (" + lblUserId.Text + ",'" + txtName.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + cboRole.Text + "')";

            user.msgadd = "New User has been saved in the database.";

            user.sqledit = "UPDATE tbluser SET Fullname='" + txtName.Text +
                "',User_name='" + txtUsername.Text +
                "',Pass='" + txtPassword.Text +
                "',UserRole='" + cboRole.Text +
                "' WHERE UserId=" + lblUserId.Text;

            user.msgedit = "User has been updated in the database.";

            user.SaveUpdate(user.sqlselect, user.sqladd, user.msgadd, user.sqledit, user.msgedit);


            btnNew_Click(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            user.sql = "DELETE FROM tbluser WHERE UserId = " + dtgList.CurrentRow.Cells[0].Value;

            user.SaveDataMsg(user.sql, "User has been deleted in the database.");

            btnNew_Click(sender, e);
        }

        private void dtgList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblUserId.Text = dtgList.CurrentRow.Cells[0].Value.ToString();
                txtName.Text = dtgList.CurrentRow.Cells[1].Value.ToString();
                txtUsername.Text = dtgList.CurrentRow.Cells[2].Value.ToString();
                cboRole.Text = dtgList.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
