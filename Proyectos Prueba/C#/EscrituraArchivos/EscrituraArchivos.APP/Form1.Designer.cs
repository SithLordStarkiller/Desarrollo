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
            this.btn_ListarDocs = new System.Windows.Forms.Button();
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
            // btn_ListarDocs
            // 
            this.btn_ListarDocs.Location = new System.Drawing.Point(769, 13);
            this.btn_ListarDocs.Name = "btn_ListarDocs";
            this.btn_ListarDocs.Size = new System.Drawing.Size(223, 23);
            this.btn_ListarDocs.TabIndex = 1;
            this.btn_ListarDocs.Text = "Listar Archivos Docs";
            this.btn_ListarDocs.UseVisualStyleBackColor = true;
            this.btn_ListarDocs.Click += new System.EventHandler(this.btn_ListarDocs_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 513);
            this.Controls.Add(this.btn_ListarDocs);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_ListarDocs;
    }
}

