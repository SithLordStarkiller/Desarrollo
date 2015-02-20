namespace Universidad.AplicacionAdministrativa.Controles.ControPersonas
{
    partial class EditarPersona
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNombreCompleto = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tbxClave = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblInicioFechaRegistro = new System.Windows.Forms.Label();
            this.lblFinalFechaRegistro = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.lblRol = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNombreCompleto
            // 
            this.lblNombreCompleto.AutoSize = true;
            this.lblNombreCompleto.Location = new System.Drawing.Point(4, 47);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.Size = new System.Drawing.Size(91, 13);
            this.lblNombreCompleto.TabIndex = 1;
            this.lblNombreCompleto.Text = "Nombre Completo";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(101, 44);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(152, 20);
            this.textBox2.TabIndex = 2;
            // 
            // tbxClave
            // 
            this.tbxClave.Location = new System.Drawing.Point(797, 44);
            this.tbxClave.Name = "tbxClave";
            this.tbxClave.Size = new System.Drawing.Size(168, 20);
            this.tbxClave.TabIndex = 3;
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(716, 47);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(34, 13);
            this.lblClave.TabIndex = 4;
            this.lblClave.Text = "Clave";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(667, 83);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // lblInicioFechaRegistro
            // 
            this.lblInicioFechaRegistro.AutoSize = true;
            this.lblInicioFechaRegistro.Location = new System.Drawing.Point(527, 85);
            this.lblInicioFechaRegistro.Name = "lblInicioFechaRegistro";
            this.lblInicioFechaRegistro.Size = new System.Drawing.Size(121, 13);
            this.lblInicioFechaRegistro.TabIndex = 6;
            this.lblInicioFechaRegistro.Text = "Fecha de registro desde";
            // 
            // lblFinalFechaRegistro
            // 
            this.lblFinalFechaRegistro.AutoSize = true;
            this.lblFinalFechaRegistro.Location = new System.Drawing.Point(154, 85);
            this.lblFinalFechaRegistro.Name = "lblFinalFechaRegistro";
            this.lblFinalFechaRegistro.Size = new System.Drawing.Size(118, 13);
            this.lblFinalFechaRegistro.TabIndex = 8;
            this.lblFinalFechaRegistro.Text = "Fecha de registro hasta";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(280, 83);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 7;
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Location = new System.Drawing.Point(361, 47);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(23, 13);
            this.lblRol.TabIndex = 10;
            this.lblRol.Text = "Rol";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(431, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 11;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(301, 132);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // EditarPersona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.lblFinalFechaRegistro);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.lblInicioFechaRegistro);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.tbxClave);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.lblNombreCompleto);
            this.Name = "EditarPersona";
            this.Size = new System.Drawing.Size(980, 580);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tbxClave;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblInicioFechaRegistro;
        private System.Windows.Forms.Label lblFinalFechaRegistro;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnBuscar;

    }
}
