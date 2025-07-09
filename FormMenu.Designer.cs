namespace ucp2
{
    partial class FormMenu
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
            this.btnAtlet = new System.Windows.Forms.Button();
            this.btnPrestasi = new System.Windows.Forms.Button();
            this.btnKeuangan = new System.Windows.Forms.Button();
            this.btnEvent = new System.Windows.Forms.Button();
            this.labelMenu = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAtlet
            // 
            this.btnAtlet.Location = new System.Drawing.Point(188, 208);
            this.btnAtlet.Name = "btnAtlet";
            this.btnAtlet.Size = new System.Drawing.Size(202, 49);
            this.btnAtlet.TabIndex = 0;
            this.btnAtlet.Text = "Data Atlet";
            this.btnAtlet.UseVisualStyleBackColor = true;
            this.btnAtlet.Click += new System.EventHandler(this.btnAtlet_Click);
            // 
            // btnPrestasi
            // 
            this.btnPrestasi.Location = new System.Drawing.Point(434, 208);
            this.btnPrestasi.Name = "btnPrestasi";
            this.btnPrestasi.Size = new System.Drawing.Size(202, 49);
            this.btnPrestasi.TabIndex = 1;
            this.btnPrestasi.Text = "Data Prestasi";
            this.btnPrestasi.UseVisualStyleBackColor = true;
            this.btnPrestasi.Click += new System.EventHandler(this.btnPrestasi_Click);
            // 
            // btnKeuangan
            // 
            this.btnKeuangan.Location = new System.Drawing.Point(188, 299);
            this.btnKeuangan.Name = "btnKeuangan";
            this.btnKeuangan.Size = new System.Drawing.Size(202, 49);
            this.btnKeuangan.TabIndex = 2;
            this.btnKeuangan.Text = "Data Keuangan";
            this.btnKeuangan.UseVisualStyleBackColor = true;
            this.btnKeuangan.Click += new System.EventHandler(this.btnKeuangan_Click);
            // 
            // btnEvent
            // 
            this.btnEvent.Location = new System.Drawing.Point(434, 299);
            this.btnEvent.Name = "btnEvent";
            this.btnEvent.Size = new System.Drawing.Size(202, 49);
            this.btnEvent.TabIndex = 3;
            this.btnEvent.Text = "Data Event";
            this.btnEvent.UseVisualStyleBackColor = true;
            this.btnEvent.Click += new System.EventHandler(this.btnEvent_Click);
            // 
            // labelMenu
            // 
            this.labelMenu.AutoSize = true;
            this.labelMenu.BackColor = System.Drawing.Color.Transparent;
            this.labelMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 40.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMenu.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelMenu.Location = new System.Drawing.Point(165, 77);
            this.labelMenu.Name = "labelMenu";
            this.labelMenu.Size = new System.Drawing.Size(492, 76);
            this.labelMenu.TabIndex = 4;
            this.labelMenu.Text = "MENU UTAMA";
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(309, 386);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(202, 49);
            this.btnDashboard.TabIndex = 5;
            this.btnDashboard.Text = "Grafik Keuangan";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(814, 483);
            this.Controls.Add(this.btnDashboard);
            this.Controls.Add(this.labelMenu);
            this.Controls.Add(this.btnEvent);
            this.Controls.Add(this.btnKeuangan);
            this.Controls.Add(this.btnPrestasi);
            this.Controls.Add(this.btnAtlet);
            this.Name = "FormMenu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAtlet;
        private System.Windows.Forms.Button btnPrestasi;
        private System.Windows.Forms.Button btnKeuangan;
        private System.Windows.Forms.Button btnEvent;
        private System.Windows.Forms.Label labelMenu;
        private System.Windows.Forms.Button btnDashboard;
    }
}

