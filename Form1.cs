using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSBunifu.Forms;

namespace POSBunifu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        } 
         private void addForm(Form frm)
        {
            pnlContainer.Controls.Clear();
            frm.TopLevel = false;
            frm.TopMost = true;
            //frm.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(frm);
            frm.Show();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Form frm = new frmLogin();
            frm.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        { 
            addForm(new frmProduct());
        }

        private void btnFindProduct_Click(object sender, EventArgs e)
        {
            addForm(new frmListProducts());
        }

        private void btnStockin_Click(object sender, EventArgs e)
        {
            addForm(new frmTransaction());
        }

        private void btnPurchasedOrder_Click(object sender, EventArgs e)
        {
            addForm(new frmPurchaseOrder());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            addForm(new frmCategory());
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            addForm(new frmSupplier());
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            addForm(new frmUsers());
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            addForm(new frmSalesReports());
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            addForm(new frmInnventoryReports());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            roundObject(this);
            btnHome.selected = true;

            addForm(new frmHome());
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

        private void btnHome_Click(object sender, EventArgs e)
        {
            addForm(new frmHome());
        }
    }
}
