namespace EscrituraArchivos.APP
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_CreaArchivo = new System.Windows.Forms.Button();
            this.btn_CreaDirectorio = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(740, 489);
            this.textBox1.TabIndex = 0;
            // 
            // btn_CreaArchivo
            // 
            this.btn_CreaArchivo.Location = new System.Drawing.Point(769, 41);
            this.btn_CreaArchivo.Name = "btn_CreaArchivo";
            this.btn_CreaArchivo.Size = new System.Drawing.Size(223, 23);
            this.btn_CreaArchivo.TabIndex = 1;
            this.btn_CreaArchivo.Text = "Listar Archivos Docs";
            this.btn_CreaArchivo.UseVisualStyleBackColor = true;
            // 
            // btn_CreaDirectorio
            // 
            this.btn_CreaDirectorio.Location = new System.Drawing.Point(769, 12);
            this.btn_CreaDirectorio.Name = "btn_CreaDirectorio";
            this.btn_CreaDirectorio.Size = new System.Drawing.Size(223, 23);
            this.btn_CreaDirectorio.TabIndex = 2;
            this.btn_CreaDirectorio.Text = "Crea Directorio";
            this.btn_CreaDirectorio.UseVisualStyleBackColor = true;
            this.btn_CreaDirectorio.Click += new System.EventHandler(this.btn_CreaDirectorio_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 513);
            this.Controls.Add(this.btn_CreaDirectorio);
            this.Controls.Add(this.btn_CreaArchivo);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_CreaArchivo;
        private System.Windows.Forms.Button btn_CreaDirectorio;
    }
}

