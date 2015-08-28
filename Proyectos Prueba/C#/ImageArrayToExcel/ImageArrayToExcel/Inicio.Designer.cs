namespace ImageArrayToExcel
{
    partial class pan
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
            this.txtRutaImagen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscarImagen = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.pbxImagenBinario = new System.Windows.Forms.PictureBox();
            this.txtBinario = new System.Windows.Forms.TextBox();
            this.btnConvertir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenBinario)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRutaImagen
            // 
            this.txtRutaImagen.Location = new System.Drawing.Point(138, 34);
            this.txtRutaImagen.Name = "txtRutaImagen";
            this.txtRutaImagen.Size = new System.Drawing.Size(411, 20);
            this.txtRutaImagen.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ruta de imagen";
            // 
            // btnBuscarImagen
            // 
            this.btnBuscarImagen.Location = new System.Drawing.Point(555, 32);
            this.btnBuscarImagen.Name = "btnBuscarImagen";
            this.btnBuscarImagen.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarImagen.TabIndex = 6;
            this.btnBuscarImagen.Text = "Buscar";
            this.btnBuscarImagen.UseVisualStyleBackColor = true;
            this.btnBuscarImagen.Click += new System.EventHandler(this.btnBuscarImagen_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(393, 388);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(555, 388);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(15, 84);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(288, 220);
            this.pbxImagen.TabIndex = 9;
            this.pbxImagen.TabStop = false;
            // 
            // pbxImagenBinario
            // 
            this.pbxImagenBinario.Location = new System.Drawing.Point(342, 84);
            this.pbxImagenBinario.Name = "pbxImagenBinario";
            this.pbxImagenBinario.Size = new System.Drawing.Size(288, 220);
            this.pbxImagenBinario.TabIndex = 10;
            this.pbxImagenBinario.TabStop = false;
            // 
            // txtBinario
            // 
            this.txtBinario.Location = new System.Drawing.Point(16, 319);
            this.txtBinario.Multiline = true;
            this.txtBinario.Name = "txtBinario";
            this.txtBinario.Size = new System.Drawing.Size(615, 54);
            this.txtBinario.TabIndex = 11;
            // 
            // btnConvertir
            // 
            this.btnConvertir.Location = new System.Drawing.Point(474, 388);
            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.Size = new System.Drawing.Size(75, 23);
            this.btnConvertir.TabIndex = 12;
            this.btnConvertir.Text = "Convertir";
            this.btnConvertir.UseVisualStyleBackColor = true;
            this.btnConvertir.Click += new System.EventHandler(this.btnConvertir_Click);
            // 
            // pan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 425);
            this.Controls.Add(this.btnConvertir);
            this.Controls.Add(this.txtBinario);
            this.Controls.Add(this.pbxImagenBinario);
            this.Controls.Add(this.pbxImagen);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnBuscarImagen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRutaImagen);
            this.Name = "pan";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.pan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenBinario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRutaImagen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscarImagen;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.PictureBox pbxImagen;
        private System.Windows.Forms.PictureBox pbxImagenBinario;
        private System.Windows.Forms.TextBox txtBinario;
        private System.Windows.Forms.Button btnConvertir;
    }
}