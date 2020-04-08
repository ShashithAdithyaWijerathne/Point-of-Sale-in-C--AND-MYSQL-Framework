using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSBunifu
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random(); 
            bunifuCircleProgressbar1.Value = r.Next(0, 100);
            bunifuCircleProgressbar2.Value = r.Next(0, 100);
            bunifuCircleProgressbar3.Value = r.Next(0, 100); 
            bunifuProgressBar1.Value = r.Next(0, 100);
            bunifuProgressBar2.Value = r.Next(0, 100);

        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
