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
            this.btn_ListarArchivos = new System.Windows.Forms.Button();
            this.btn_CreaDirectorio = new System.Windows.Forms.Button();
            this.btn_GuardaTextoArchivo = new System.Windows.Forms.Button();
            this.btn_LeerArchivo = new System.Windows.Forms.Button();
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
            // btn_ListarArchivos
            // 
            this.btn_ListarArchivos.Location = new System.Drawing.Point(769, 41);
            this.btn_ListarArchivos.Name = "btn_ListarArchivos";
            this.btn_ListarArchivos.Size = new System.Drawing.Size(223, 23);
            this.btn_ListarArchivos.TabIndex = 1;
            this.btn_ListarArchivos.Text = "Listar Archivos";
            this.btn_ListarArchivos.UseVisualStyleBackColor = true;
            this.btn_ListarArchivos.Click += new System.EventHandler(this.btn_ListarArchivos_Click);
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
            // btn_GuardaTextoArchivo
            // 
            this.btn_GuardaTextoArchivo.Location = new System.Drawing.Point(769, 70);
            this.btn_GuardaTextoArchivo.Name = "btn_GuardaTextoArchivo";
            this.btn_GuardaTextoArchivo.Size = new System.Drawing.Size(223, 23);
            this.btn_GuardaTextoArchivo.TabIndex = 3;
            this.btn_GuardaTextoArchivo.Text = "Guarda texto en archivo";
            this.btn_GuardaTextoArchivo.UseVisualStyleBackColor = true;
            this.btn_GuardaTextoArchivo.Click += new System.EventHandler(this.btn_GuardaTextoArchivo_Click);
            // 
            // btn_LeerArchivo
            // 
            this.btn_LeerArchivo.Location = new System.Drawing.Point(769, 99);
            this.btn_LeerArchivo.Name = "btn_LeerArchivo";
            this.btn_LeerArchivo.Size = new System.Drawing.Size(223, 23);
            this.btn_LeerArchivo.TabIndex = 4;
            this.btn_LeerArchivo.Text = "Leer Archivo";
            this.btn_LeerArchivo.UseVisualStyleBackColor = true;
            this.btn_LeerArchivo.Click += new System.EventHandler(this.btn_LeerArchivo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 513);
            this.Controls.Add(this.btn_LeerArchivo);
            this.Controls.Add(this.btn_GuardaTextoArchivo);
            this.Controls.Add(this.btn_CreaDirectorio);
            this.Controls.Add(this.btn_ListarArchivos);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_ListarArchivos;
        private System.Windows.Forms.Button btn_CreaDirectorio;
        private System.Windows.Forms.Button btn_GuardaTextoArchivo;
        private System.Windows.Forms.Button btn_LeerArchivo;
    }
}

