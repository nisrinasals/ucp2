using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ucp2
{
    public partial class reportEvent : Form
    {
        public reportEvent()
        {
            InitializeComponent();
        }

        private void reportEvent_Load(object sender, EventArgs e)
        {
            SetupReportViewer();
            this.rvEvent.RefreshReport();
        }

        private void SetupReportViewer()
        {
            string connectionString = "Server=LAPTOP-I0H7METT\\CHESTAYURCEL;Database=keuangan2;Integrated Security=True";
            string query =@"
            SELECT
                            E.id_event,
                            E.nama_event,
                            E.jenis_event,
                            E.keterangan,
                            E.tanggal,
                            PA.nim,
                            PA.peran_partisipasi,
                            PA.id_partisipasi
                        FROM
                            Event E
                        INNER JOIN
                            Partisipasi_Atlet PA ON E.id_event = PA.id_event";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            rvEvent.LocalReport.DataSources.Clear();
            rvEvent.LocalReport.DataSources.Add(rds);

            rvEvent.LocalReport.ReportPath = @"D:\kuliah\4\PABD\pabd\ucp2\ReportEvent.rdlc";
            rvEvent.RefreshReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
