using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;


namespace ucp2
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void btnAtlet_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                FormAtlet formAtlet = new FormAtlet();
                formAtlet.ShowDialog(); // Menampilkan form FormAtlet
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening FormAtlet: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrestasi_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                FormPrestasi formPrestasi = new FormPrestasi();
                formPrestasi.ShowDialog(); // Menampilkan form FormPrestasi
                this.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening FormPrestasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKeuangan_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                FormKeuangan formKeuangan = new FormKeuangan();
                formKeuangan.ShowDialog(); // Menampilkan form FormKeuangan
                this.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening FormKeuangan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEvent_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                FormEvent formEvent = new FormEvent();
                formEvent.ShowDialog(); // Menampilkan form FormEvent
                this.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening FormEvent: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                FormDashboard formDashboard = new FormDashboard();
                formDashboard.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening FormDashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
