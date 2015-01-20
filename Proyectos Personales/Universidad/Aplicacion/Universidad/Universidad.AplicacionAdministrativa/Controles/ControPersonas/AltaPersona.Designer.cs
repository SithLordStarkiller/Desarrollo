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
            this.mcrFechaNacimiento = new System.Windows.Forms.MonthCalendar();
            this.lblFechaNac = new System.Windows.Forms.Label();
            this.tbxApellidoM = new System.Windows.Forms.TextBox();
            this.lblApellidoM = new System.Windows.Forms.Label();
            this.tbxApellidoP = new System.Windows.Forms.TextBox();
            this.lblApellidoP = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblSexo = new System.Windows.Forms.Label();
            this.cbxSexo = new System.Windows.Forms.ComboBox();
            this.lblCurp = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblRfc = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.lblNss = new System.Windows.Forms.Label();
            this.cbxNacionalidad = new System.Windows.Forms.ComboBox();
            this.lblNacionalidad = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tbpDatosPersonales.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpDatosPersonales);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(977, 441);
            this.tabControl1.TabIndex = 0;
            // 
            // tbpDatosPersonales
            // 
            this.tbpDatosPersonales.Controls.Add(this.lblNacionalidad);
            this.tbpDatosPersonales.Controls.Add(this.cbxNacionalidad);
            this.tbpDatosPersonales.Controls.Add(this.lblNss);
            this.tbpDatosPersonales.Controls.Add(this.textBox4);
            this.tbpDatosPersonales.Controls.Add(this.textBox3);
            this.tbpDatosPersonales.Controls.Add(this.lblRfc);
            this.tbpDatosPersonales.Controls.Add(this.textBox2);
            this.tbpDatosPersonales.Controls.Add(this.lblCurp);
            this.tbpDatosPersonales.Controls.Add(this.cbxSexo);
            this.tbpDatosPersonales.Controls.Add(this.lblSexo);
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
            this.tbpDatosPersonales.Size = new System.Drawing.Size(969, 415);
            this.tbpDatosPersonales.TabIndex = 0;
            this.tbpDatosPersonales.Text = "Datos Personales";
            this.tbpDatosPersonales.UseVisualStyleBackColor = true;
            // 
            // mcrFechaNacimiento
            // 
            this.mcrFechaNacimiento.Location = new System.Drawing.Point(146, 187);
            this.mcrFechaNacimiento.MaxSelectionCount = 1;
            this.mcrFechaNacimiento.Name = "mcrFechaNacimiento";
            this.mcrFechaNacimiento.TabIndex = 7;
            // 
            // lblFechaNac
            // 
            this.lblFechaNac.AutoSize = true;
            this.lblFechaNac.Location = new System.Drawing.Point(25, 187);
            this.lblFechaNac.Name = "lblFechaNac";
            this.lblFechaNac.Size = new System.Drawing.Size(108, 13);
            this.lblFechaNac.TabIndex = 6;
            this.lblFechaNac.Text = "Fecha de Nacimiento";
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
            // tbxApellidoP
            // 
            this.tbxApellidoP.Location = new System.Drawing.Point(149, 77);
            this.tbxApellidoP.Name = "tbxApellidoP";
            this.tbxApellidoP.Size = new System.Drawing.Size(245, 20);
            this.tbxApellidoP.TabIndex = 3;
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(149, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(245, 20);
            this.textBox1.TabIndex = 1;
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(969, 385);
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
            this.btnRegistrar.Location = new System.Drawing.Point(859, 536);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(75, 23);
            this.btnRegistrar.TabIndex = 2;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(761, 536);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Location = new System.Drawing.Point(557, 31);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(31, 13);
            this.lblSexo.TabIndex = 8;
            this.lblSexo.Text = "Sexo";
            // 
            // cbxSexo
            // 
            this.cbxSexo.FormattingEnabled = true;
            this.cbxSexo.Items.AddRange(new object[] {
            "Masculino",
            "Femenino"});
            this.cbxSexo.Location = new System.Drawing.Point(706, 27);
            this.cbxSexo.Name = "cbxSexo";
            this.cbxSexo.Size = new System.Drawing.Size(154, 21);
            this.cbxSexo.TabIndex = 9;
            // 
            // lblCurp
            // 
            this.lblCurp.AutoSize = true;
            this.lblCurp.Location = new System.Drawing.Point(557, 80);
            this.lblCurp.Name = "lblCurp";
            this.lblCurp.Size = new System.Drawing.Size(37, 13);
            this.lblCurp.TabIndex = 10;
            this.lblCurp.Text = "CURP";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(651, 77);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(276, 20);
            this.textBox2.TabIndex = 11;
            // 
            // lblRfc
            // 
            this.lblRfc.AutoSize = true;
            this.lblRfc.Location = new System.Drawing.Point(559, 133);
            this.lblRfc.Name = "lblRfc";
            this.lblRfc.Size = new System.Drawing.Size(28, 13);
            this.lblRfc.TabIndex = 12;
            this.lblRfc.Text = "RFC";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(651, 129);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(276, 20);
            this.textBox3.TabIndex = 13;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(651, 184);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(276, 20);
            this.textBox4.TabIndex = 14;
            // 
            // lblNss
            // 
            this.lblNss.AutoSize = true;
            this.lblNss.Location = new System.Drawing.Point(559, 187);
            this.lblNss.Name = "lblNss";
            this.lblNss.Size = new System.Drawing.Size(29, 13);
            this.lblNss.TabIndex = 15;
            this.lblNss.Text = "NSS";
            // 
            // cbxNacionalidad
            // 
            this.cbxNacionalidad.FormattingEnabled = true;
            this.cbxNacionalidad.Location = new System.Drawing.Point(706, 241);
            this.cbxNacionalidad.Name = "cbxNacionalidad";
            this.cbxNacionalidad.Size = new System.Drawing.Size(154, 21);
            this.cbxNacionalidad.TabIndex = 16;
            // 
            // lblNacionalidad
            // 
            this.lblNacionalidad.AutoSize = true;
            this.lblNacionalidad.Location = new System.Drawing.Point(559, 244);
            this.lblNacionalidad.Name = "lblNacionalidad";
            this.lblNacionalidad.Size = new System.Drawing.Size(69, 13);
            this.lblNacionalidad.TabIndex = 17;
            this.lblNacionalidad.Text = "Nacionalidad";
            // 
            // AltaPersona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLimpiar);
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
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox tbxApellidoP;
        private System.Windows.Forms.Label lblApellidoP;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MonthCalendar mcrFechaNacimiento;
        private System.Windows.Forms.Label lblFechaNac;
        private System.Windows.Forms.TextBox tbxApellidoM;
        private System.Windows.Forms.Label lblApellidoM;
        private System.Windows.Forms.Label lblNacionalidad;
        private System.Windows.Forms.ComboBox cbxNacionalidad;
        private System.Windows.Forms.Label lblNss;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label lblRfc;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblCurp;
        private System.Windows.Forms.ComboBox cbxSexo;
        private System.Windows.Forms.Label lblSexo;

    }
}
