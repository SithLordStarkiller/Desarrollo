namespace Universidad.AplicacionAdministrativa
{
    partial class Wizard
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
            this.txb_RutaServicio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.brn_Aceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txb_RutaServicio
            // 
            this.txb_RutaServicio.Location = new System.Drawing.Point(105, 28);
            this.txb_RutaServicio.Name = "txb_RutaServicio";
            this.txb_RutaServicio.Size = new System.Drawing.Size(369, 20);
            this.txb_RutaServicio.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ruta del servidor";
            // 
            // brn_Aceptar
            // 
            this.brn_Aceptar.Location = new System.Drawing.Point(204, 65);
            this.brn_Aceptar.Name = "brn_Aceptar";
            this.brn_Aceptar.Size = new System.Drawing.Size(75, 23);
            this.brn_Aceptar.TabIndex = 2;
            this.brn_Aceptar.Text = "Aceptar";
            this.brn_Aceptar.UseVisualStyleBackColor = true;
            this.brn_Aceptar.Click += new System.EventHandler(this.brn_Aceptar_Click);
            // 
            // Wizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 110);
            this.Controls.Add(this.brn_Aceptar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_RutaServicio);
            this.Name = "Wizard";
            this.Text = "Wizard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_RutaServicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button brn_Aceptar;
    }
}