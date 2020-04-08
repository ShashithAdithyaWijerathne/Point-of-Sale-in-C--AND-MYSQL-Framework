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
using System.Data.SqlClient;

namespace POSBunifu
{
    public partial class frmInvoice : Form
    {
        SQLConfig pro = new SQLConfig();
        public frmInvoice(string txt)
        {
            InitializeComponent();

            try
            {
                pro.strcon.Open();
                string reportname = "Receipt";

                pro.sqlselect = "SELECT * FROM tblproduct p , tbltransaction t,tblsummary s WHERE p.Barcode = t.Barcode AND t.InvoiceNo = s.InvoiceNo AND s.InvoiceNo ='" + txt + "'";

                pro.cmd = new SqlCommand();
                pro.cmd.Connection = pro.strcon;
                pro.cmd.CommandText = pro.sqlselect;
                pro.da = new SqlDataAdapter();
                pro.da.SelectCommand = pro.cmd;
                pro.dt = new DataTable();
                pro.da.Fill(pro.dt);


                CrystalDecisions.CrystalReports.Engine.ReportDocument reportdoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument(); ;

                string strReportPath = Application.StartupPath + "\\report\\" + reportname + ".rpt";


                reportdoc.Load(strReportPath);
                reportdoc.SetDataSource(pro.dt);

                crystalReportViewer1.ReportSource = reportdoc;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                pro.da.Dispose();
                pro.strcon.Close();
            }

        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {

        }
    }
}
