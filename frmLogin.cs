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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        SQLSelects config = new SQLSelects();
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            config.ValidatingAccounts(txtUsername, txtpassword);
            int maxrows = config.dt.Rows.Count;

            if (maxrows > 0)
            {
                MessageBox.Show("Welcome " + config.dt.Rows[0].Field<string>("UserRole"), "Accepted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (config.dt.Rows[0].Field<string>("UserRole") == "Administrator")
                {
                    Form frm = new Form1();
                    frm.Show();
                }
                else if (config.dt.Rows[0].Field<string>("UserRole") == "Cashier")
                {
                    Form frm = new frmStockout();
                    frm.Show();

                }

                txtUsername.Focus();
                txtUsername.Clear();
                txtpassword.Clear();
                this.Hide();
 
            }
            else
            {
                MessageBox.Show("Account does not exist! ", "Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //roundObject(this);
            //roundPanel(panel2);
        }

        private void roundObject(Form obj)
        {
              
            System.Drawing.Drawing2D.GraphicsPath DGP = new System.Drawing.Drawing2D.GraphicsPath();
            DGP.StartFigure();
            //'top left corner
            DGP.AddArc(new Rectangle(0, 0, 40, 40), 180, 90);
            DGP.AddLine(40, 0, obj.Width - 40, 0);


            //'top right corner
            DGP.AddArc(new Rectangle(obj.Width - 40, 0, 40, 40), -90, 90);
            DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40);


            //'buttom right corner
            DGP.AddArc(new Rectangle(obj.Width - 40, obj.Height - 40, 40, 40), 0, 90);
            DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height);


            //'buttom left corner
            DGP.AddArc(new Rectangle(0, obj.Height - 40, 40, 40), 90, 90);
            DGP.CloseFigure();
             
            obj.Region = new Region(DGP);
        }
        private void roundPanel(Panel obj)
        {

            System.Drawing.Drawing2D.GraphicsPath DGP = new System.Drawing.Drawing2D.GraphicsPath();
            DGP.StartFigure();
            //'top left corner
            DGP.AddArc(new Rectangle(0, 0, 40, 40), 180, 90);
            DGP.AddLine(40, 0, obj.Width - 40, 0);


            //'top right corner
            DGP.AddArc(new Rectangle(obj.Width - 40, 0, 40, 40), -90, 90);
            DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40);


            //'buttom right corner
            DGP.AddArc(new Rectangle(obj.Width - 40, obj.Height - 40, 40, 40), 0, 90);
            DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height);


            //'buttom left corner
            DGP.AddArc(new Rectangle(0, obj.Height - 40, 40, 40), 90, 90);
            DGP.CloseFigure();

            obj.Region = new Region(DGP);
        }
    }
}
