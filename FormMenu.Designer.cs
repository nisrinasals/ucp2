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
            this.SuspendLayout();
            // 
            // btnAtlet
            // 
            this.btnAtlet.Location = new System.Drawing.Point(53, 51);
            this.btnAtlet.Name = "btnAtlet";
            this.btnAtlet.Size = new System.Drawing.Size(188, 39);
            this.btnAtlet.TabIndex = 0;
            this.btnAtlet.Text = "Data Atlet";
            this.btnAtlet.UseVisualStyleBackColor = true;
            this.btnAtlet.Click += new System.EventHandler(this.btnAtlet_Click);
            // 
            // btnPrestasi
            // 
            this.btnPrestasi.Location = new System.Drawing.Point(53, 112);
            this.btnPrestasi.Name = "btnPrestasi";
            this.btnPrestasi.Size = new System.Drawing.Size(188, 39);
            this.btnPrestasi.TabIndex = 1;
            this.btnPrestasi.Text = "Data Prestasi";
            this.btnPrestasi.UseVisualStyleBackColor = true;
            this.btnPrestasi.Click += new System.EventHandler(this.btnPrestasi_Click);
            // 
            // btnKeuangan
            // 
            this.btnKeuangan.Location = new System.Drawing.Point(53, 174);
            this.btnKeuangan.Name = "btnKeuangan";
            this.btnKeuangan.Size = new System.Drawing.Size(188, 39);
            this.btnKeuangan.TabIndex = 2;
            this.btnKeuangan.Text = "Data Keuangan";
            this.btnKeuangan.UseVisualStyleBackColor = true;
            this.btnKeuangan.Click += new System.EventHandler(this.btnKeuangan_Click);
            // 
            // btnEvent
            // 
            this.btnEvent.Location = new System.Drawing.Point(53, 235);
            this.btnEvent.Name = "btnEvent";
            this.btnEvent.Size = new System.Drawing.Size(188, 39);
            this.btnEvent.TabIndex = 3;
            this.btnEvent.Text = "Data Event";
            this.btnEvent.UseVisualStyleBackColor = true;
            this.btnEvent.Click += new System.EventHandler(this.btnEvent_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Crimson;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEvent);
            this.Controls.Add(this.btnKeuangan);
            this.Controls.Add(this.btnPrestasi);
            this.Controls.Add(this.btnAtlet);
            this.Name = "FormMenu";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAtlet;
        private System.Windows.Forms.Button btnPrestasi;
        private System.Windows.Forms.Button btnKeuangan;
        private System.Windows.Forms.Button btnEvent;
    }
}

