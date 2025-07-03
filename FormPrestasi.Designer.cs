namespace ucp2
{
    partial class FormPrestasi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrestasi));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrestasi = new System.Windows.Forms.TextBox();
            this.txtTingkat = new System.Windows.Forms.TextBox();
            this.txtTahun = new System.Windows.Forms.TextBox();
            this.dgvPrestasi = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.txtNIM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestasi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nama Prestasi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tingkat";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tahun";
            // 
            // txtPrestasi
            // 
            this.txtPrestasi.Location = new System.Drawing.Point(28, 179);
            this.txtPrestasi.Name = "txtPrestasi";
            this.txtPrestasi.Size = new System.Drawing.Size(170, 22);
            this.txtPrestasi.TabIndex = 4;
            // 
            // txtTingkat
            // 
            this.txtTingkat.Location = new System.Drawing.Point(28, 252);
            this.txtTingkat.Name = "txtTingkat";
            this.txtTingkat.Size = new System.Drawing.Size(170, 22);
            this.txtTingkat.TabIndex = 5;
            // 
            // txtTahun
            // 
            this.txtTahun.Location = new System.Drawing.Point(28, 318);
            this.txtTahun.Name = "txtTahun";
            this.txtTahun.Size = new System.Drawing.Size(170, 22);
            this.txtTahun.TabIndex = 6;
            // 
            // dgvPrestasi
            // 
            this.dgvPrestasi.BackgroundColor = System.Drawing.Color.Tan;
            this.dgvPrestasi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrestasi.Location = new System.Drawing.Point(223, 83);
            this.dgvPrestasi.Name = "dgvPrestasi";
            this.dgvPrestasi.RowHeadersWidth = 51;
            this.dgvPrestasi.RowTemplate.Height = 24;
            this.dgvPrestasi.Size = new System.Drawing.Size(664, 331);
            this.dgvPrestasi.TabIndex = 7;
            this.dgvPrestasi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPrestasi_CellContentClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(545, 433);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 29);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(388, 433);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 29);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(307, 433);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 29);
            this.btnHapus.TabIndex = 20;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(223, 433);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(78, 29);
            this.btnTambah.TabIndex = 21;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Geoform", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(240, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(635, 38);
            this.label6.TabIndex = 23;
            this.label6.Text = "DATA PRESTASI ATLET FAKULTAS TEKNIK";
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(1, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(48, 40);
            this.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnBack.TabIndex = 22;
            this.btnBack.TabStop = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtNIM
            // 
            this.txtNIM.Location = new System.Drawing.Point(28, 110);
            this.txtNIM.Name = "txtNIM";
            this.txtNIM.Size = new System.Drawing.Size(170, 22);
            this.txtNIM.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "NIM Mahasiswa";
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(468, 433);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(2);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(72, 29);
            this.BtnImport.TabIndex = 25;
            this.BtnImport.Text = "Import";
            this.BtnImport.UseVisualStyleBackColor = true;
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click_1);
            // 
            // FormPrestasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(899, 474);
            this.Controls.Add(this.BtnImport);
            this.Controls.Add(this.txtNIM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dgvPrestasi);
            this.Controls.Add(this.txtTahun);
            this.Controls.Add(this.txtTingkat);
            this.Controls.Add(this.txtPrestasi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormPrestasi";
            this.Text = "Data Prestasi";
            this.Load += new System.EventHandler(this.FormPrestasi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestasi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrestasi;
        private System.Windows.Forms.TextBox txtTingkat;
        private System.Windows.Forms.TextBox txtTahun;
        private System.Windows.Forms.DataGridView dgvPrestasi;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox btnBack;
        private System.Windows.Forms.TextBox txtNIM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnImport;
    }
}