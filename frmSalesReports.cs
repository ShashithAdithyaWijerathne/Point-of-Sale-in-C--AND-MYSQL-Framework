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
    public partial class frmSalesReports : Form
    {
        public frmSalesReports()
        {
            InitializeComponent();
        }

        public string sql = "";
        SQLConfig pro = new SQLConfig();
        private void SalesReports(string sql, string rptname)
        {
            try
            {
                pro.strcon.Open();

                string reportname = rptname;

                pro.cmd = new SqlCommand();
                pro.cmd.Connection = pro.strcon;
                pro.cmd.CommandText = sql;
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
                MessageBox.Show(ex.Message + "No crystal reports installed. Pls. contact administrator.");
            }
            finally
            {
                pro.da.Dispose();
                pro.strcon.Close();
            }
        }

        private void rdoDaily_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                sql = "SELECT * FROM tblcategory c, tblproduct p , tbltransaction t,tblsummary s " +
                    " WHERE c.CategoryId=p.CategoryId AND p.Barcode = t.Barcode AND t.InvoiceNo = s.InvoiceNo " +
                    " AND  day(s.TransactionDate) = day(GETDATE())";
                SalesReports(sql, "DailySales");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdoWeekly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                sql = "SELECT * FROM tblcategory c, tblproduct p , tbltransaction t,tblsummary s " +
                    " WHERE c.CategoryId=p.CategoryId AND p.Barcode = t.Barcode AND t.InvoiceNo = s.InvoiceNo " +
                    " AND  datepart(ww, s.TransactionDate) = datepart(ww, getdate())";
                SalesReports(sql, "WeeklySales");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdoMonthly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                sql = "SELECT * FROM tblcategory c, tblproduct p , tbltransaction t,tblsummary s " +
                    " WHERE c.CategoryId=p.CategoryId AND p.Barcode = t.Barcode AND t.InvoiceNo = s.InvoiceNo " +
                    " AND datepart(month, s.TransactionDate) = datepart(month, GETDATE())";
                SalesReports(sql, "MonthlySales");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
