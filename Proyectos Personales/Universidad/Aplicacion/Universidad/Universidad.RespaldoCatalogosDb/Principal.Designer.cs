namespace Universidad.RespaldoCatalogosDb
{
    partial class Principal
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUrlCarpetaExcel = new System.Windows.Forms.TextBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtListaExcel = new System.Windows.Forms.TextBox();
            this.btnBuscarExcel = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cbxInstacias = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL DataBase";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "URL Carpeta Excel";
            // 
            // txtUrlCarpetaExcel
            // 
            this.txtUrlCarpetaExcel.Location = new System.Drawing.Point(186, 200);
            this.txtUrlCarpetaExcel.Name = "txtUrlCarpetaExcel";
            this.txtUrlCarpetaExcel.Size = new System.Drawing.Size(309, 20);
            this.txtUrlCarpetaExcel.TabIndex = 5;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(407, 358);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 6;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            // 
            // txtListaExcel
            // 
            this.txtListaExcel.Location = new System.Drawing.Point(61, 257);
            this.txtListaExcel.Multiline = true;
            this.txtListaExcel.Name = "txtListaExcel";
            this.txtListaExcel.Size = new System.Drawing.Size(524, 72);
            this.txtListaExcel.TabIndex = 7;
            // 
            // btnBuscarExcel
            // 
            this.btnBuscarExcel.Location = new System.Drawing.Point(510, 198);
            this.btnBuscarExcel.Name = "btnBuscarExcel";
            this.btnBuscarExcel.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarExcel.TabIndex = 9;
            this.btnBuscarExcel.Text = "Buscar";
            this.btnBuscarExcel.UseVisualStyleBackColor = true;
            this.btnBuscarExcel.Click += new System.EventHandler(this.btnBuscarExcel_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(510, 358);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 11;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(61, 99);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(524, 72);
            this.textBox2.TabIndex = 8;
            // 
            // cbxInstacias
            // 
            this.cbxInstacias.FormattingEnabled = true;
            this.cbxInstacias.Location = new System.Drawing.Point(169, 54);
            this.cbxInstacias.Name = "cbxInstacias";
            this.cbxInstacias.Size = new System.Drawing.Size(416, 21);
            this.cbxInstacias.TabIndex = 12;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 393);
            this.Controls.Add(this.cbxInstacias);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnBuscarExcel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txtListaExcel);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.txtUrlCarpetaExcel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Principal";
            this.Text = "Principal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUrlCarpetaExcel;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtListaExcel;
        private System.Windows.Forms.Button btnBuscarExcel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox cbxInstacias;
    }
}