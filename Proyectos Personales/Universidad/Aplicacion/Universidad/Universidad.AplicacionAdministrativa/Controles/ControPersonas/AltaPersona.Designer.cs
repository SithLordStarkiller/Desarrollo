namespace Universidad.AplicacionAdministrativa.Controles.ControPersonas
{
    partial class AltaPersona
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpDatosPersonales = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblApellidoP = new System.Windows.Forms.Label();
            this.tbxApellidoP = new System.Windows.Forms.TextBox();
            this.tbxApellidoM = new System.Windows.Forms.TextBox();
            this.lblApellidoM = new System.Windows.Forms.Label();
            this.lblFechaNac = new System.Windows.Forms.Label();
            this.mcrFechaNacimiento = new System.Windows.Forms.MonthCalendar();
            this.tabControl1.SuspendLayout();
            this.tbpDatosPersonales.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpDatosPersonales);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 107);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(977, 411);
            this.tabControl1.TabIndex = 0;
            // 
            // tbpDatosPersonales
            // 
            this.tbpDatosPersonales.Controls.Add(this.mcrFechaNacimiento);
            this.tbpDatosPersonales.Controls.Add(this.lblFechaNac);
            this.tbpDatosPersonales.Controls.Add(this.tbxApellidoM);
            this.tbpDatosPersonales.Controls.Add(this.lblApellidoM);
            this.tbpDatosPersonales.Controls.Add(this.tbxApellidoP);
            this.tbpDatosPersonales.Controls.Add(this.lblApellidoP);
            this.tbpDatosPersonales.Controls.Add(this.textBox1);
            this.tbpDatosPersonales.Controls.Add(this.lblNombre);
            this.tbpDatosPersonales.Location = new System.Drawing.Point(4, 22);
            this.tbpDatosPersonales.Name = "tbpDatosPersonales";
            this.tbpDatosPersonales.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDatosPersonales.Size = new System.Drawing.Size(969, 385);
            this.tbpDatosPersonales.TabIndex = 0;
            this.tbpDatosPersonales.Text = "Datos Personales";
            this.tbpDatosPersonales.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(341, 17);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(305, 24);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Registro de personal y alumnos";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(764, 536);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(75, 23);
            this.btnRegistrar.TabIndex = 2;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(881, 536);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(25, 31);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(55, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre(s)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(149, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(245, 20);
            this.textBox1.TabIndex = 1;
            // 
            // lblApellidoP
            // 
            this.lblApellidoP.AutoSize = true;
            this.lblApellidoP.Location = new System.Drawing.Point(25, 80);
            this.lblApellidoP.Name = "lblApellidoP";
            this.lblApellidoP.Size = new System.Drawing.Size(84, 13);
            this.lblApellidoP.TabIndex = 2;
            this.lblApellidoP.Text = "Apellido Paterno";
            // 
            // tbxApellidoP
            // 
            this.tbxApellidoP.Location = new System.Drawing.Point(149, 77);
            this.tbxApellidoP.Name = "tbxApellidoP";
            this.tbxApellidoP.Size = new System.Drawing.Size(245, 20);
            this.tbxApellidoP.TabIndex = 3;
            // 
            // tbxApellidoM
            // 
            this.tbxApellidoM.Location = new System.Drawing.Point(149, 130);
            this.tbxApellidoM.Name = "tbxApellidoM";
            this.tbxApellidoM.Size = new System.Drawing.Size(245, 20);
            this.tbxApellidoM.TabIndex = 5;
            // 
            // lblApellidoM
            // 
            this.lblApellidoM.AutoSize = true;
            this.lblApellidoM.Location = new System.Drawing.Point(25, 133);
            this.lblApellidoM.Name = "lblApellidoM";
            this.lblApellidoM.Size = new System.Drawing.Size(86, 13);
            this.lblApellidoM.TabIndex = 4;
            this.lblApellidoM.Text = "Apellido Materno";
            // 
            // lblFechaNac
            // 
            this.lblFechaNac.AutoSize = true;
            this.lblFechaNac.Location = new System.Drawing.Point(25, 180);
            this.lblFechaNac.Name = "lblFechaNac";
            this.lblFechaNac.Size = new System.Drawing.Size(108, 13);
            this.lblFechaNac.TabIndex = 6;
            this.lblFechaNac.Text = "Fecha de Nacimiento";
            // 
            // mcrFechaNacimiento
            // 
            this.mcrFechaNacimiento.Location = new System.Drawing.Point(146, 180);
            this.mcrFechaNacimiento.MaxSelectionCount = 1;
            this.mcrFechaNacimiento.Name = "mcrFechaNacimiento";
            this.mcrFechaNacimiento.TabIndex = 7;
            // 
            // AltaPersona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.tabControl1);
            this.Name = "AltaPersona";
            this.Size = new System.Drawing.Size(980, 580);
            this.Load += new System.EventHandler(this.AltaPersona_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbpDatosPersonales.ResumeLayout(false);
            this.tbpDatosPersonales.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpDatosPersonales;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox tbxApellidoP;
        private System.Windows.Forms.Label lblApellidoP;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MonthCalendar mcrFechaNacimiento;
        private System.Windows.Forms.Label lblFechaNac;
        private System.Windows.Forms.TextBox tbxApellidoM;
        private System.Windows.Forms.Label lblApellidoM;

    }
}
