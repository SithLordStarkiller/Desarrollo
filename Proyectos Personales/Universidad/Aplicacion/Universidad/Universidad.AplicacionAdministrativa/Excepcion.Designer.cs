namespace Universidad.AplicacionAdministrativa
{
    partial class Excepcion
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
            this.txb_MensageExcepcion = new System.Windows.Forms.TextBox();
            this.btn_Cerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txb_MensageExcepcion
            // 
            this.txb_MensageExcepcion.Location = new System.Drawing.Point(12, 21);
            this.txb_MensageExcepcion.Multiline = true;
            this.txb_MensageExcepcion.Name = "txb_MensageExcepcion";
            this.txb_MensageExcepcion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txb_MensageExcepcion.Size = new System.Drawing.Size(559, 114);
            this.txb_MensageExcepcion.TabIndex = 0;
            // 
            // btn_Cerrar
            // 
            this.btn_Cerrar.Location = new System.Drawing.Point(232, 149);
            this.btn_Cerrar.Name = "btn_Cerrar";
            this.btn_Cerrar.Size = new System.Drawing.Size(75, 23);
            this.btn_Cerrar.TabIndex = 1;
            this.btn_Cerrar.Text = "Cerrar";
            this.btn_Cerrar.UseVisualStyleBackColor = true;
            this.btn_Cerrar.Click += new System.EventHandler(this.btn_Cerrar_Click);
            // 
            // Excepcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 184);
            this.Controls.Add(this.btn_Cerrar);
            this.Controls.Add(this.txb_MensageExcepcion);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Excepcion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excepcion";
            this.Load += new System.EventHandler(this.Excepcion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_MensageExcepcion;
        private System.Windows.Forms.Button btn_Cerrar;
    }
}