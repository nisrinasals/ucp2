using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
