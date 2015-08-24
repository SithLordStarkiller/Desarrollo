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
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnBuscarExcel = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.cbxInstacias = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxDataBase = new System.Windows.Forms.ComboBox();
            this.btnProbar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.clbListaExcel = new System.Windows.Forms.CheckedListBox();
            this.lbxSeleccionados = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            this.label2.Location = new System.Drawing.Point(58, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "URL Carpeta Excel";
            // 
            // txtUrlCarpetaExcel
            // 
            this.txtUrlCarpetaExcel.Location = new System.Drawing.Point(186, 310);
            this.txtUrlCarpetaExcel.Name = "txtUrlCarpetaExcel";
            this.txtUrlCarpetaExcel.Size = new System.Drawing.Size(309, 20);
            this.txtUrlCarpetaExcel.TabIndex = 5;
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(407, 479);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 6;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnBuscarExcel
            // 
            this.btnBuscarExcel.Location = new System.Drawing.Point(510, 308);
            this.btnBuscarExcel.Name = "btnBuscarExcel";
            this.btnBuscarExcel.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarExcel.TabIndex = 9;
            this.btnBuscarExcel.Text = "Buscar";
            this.btnBuscarExcel.UseVisualStyleBackColor = true;
            this.btnBuscarExcel.Click += new System.EventHandler(this.btnBuscarExcel_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(510, 479);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 11;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // cbxInstacias
            // 
            this.cbxInstacias.FormattingEnabled = true;
            this.cbxInstacias.Location = new System.Drawing.Point(158, 54);
            this.cbxInstacias.Name = "cbxInstacias";
            this.cbxInstacias.Size = new System.Drawing.Size(337, 21);
            this.cbxInstacias.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(158, 94);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(166, 20);
            this.txtUsuario.TabIndex = 14;
            this.txtUsuario.Leave += new System.EventHandler(this.txtUsuario_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(419, 94);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(166, 20);
            this.txtContrasena.TabIndex = 16;
            this.txtContrasena.UseSystemPasswordChar = true;
            this.txtContrasena.Leave += new System.EventHandler(this.txtContrasena_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Base de Datos";
            // 
            // cbxDataBase
            // 
            this.cbxDataBase.FormattingEnabled = true;
            this.cbxDataBase.Location = new System.Drawing.Point(158, 132);
            this.cbxDataBase.Name = "cbxDataBase";
            this.cbxDataBase.Size = new System.Drawing.Size(337, 21);
            this.cbxDataBase.TabIndex = 18;
            // 
            // btnProbar
            // 
            this.btnProbar.Location = new System.Drawing.Point(510, 130);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new System.Drawing.Size(75, 23);
            this.btnProbar.TabIndex = 19;
            this.btnProbar.Text = "Probar";
            this.btnProbar.UseVisualStyleBackColor = true;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "OLE DB Conection String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(61, 193);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(524, 72);
            this.txtConnectionString.TabIndex = 21;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(510, 52);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 22;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // clbListaExcel
            // 
            this.clbListaExcel.FormattingEnabled = true;
            this.clbListaExcel.Location = new System.Drawing.Point(61, 375);
            this.clbListaExcel.Name = "clbListaExcel";
            this.clbListaExcel.Size = new System.Drawing.Size(244, 79);
            this.clbListaExcel.TabIndex = 24;
            this.clbListaExcel.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbListaExcel_ItemCheck);
            // 
            // lbxSeleccionados
            // 
            this.lbxSeleccionados.FormattingEnabled = true;
            this.lbxSeleccionados.Location = new System.Drawing.Point(349, 372);
            this.lbxSeleccionados.Name = "lbxSeleccionados";
            this.lbxSeleccionados.Size = new System.Drawing.Size(236, 82);
            this.lbxSeleccionados.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 348);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Archivos encontrados";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(346, 348);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Archivos Seleccionados";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 520);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbxSeleccionados);
            this.Controls.Add(this.clbListaExcel);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnProbar);
            this.Controls.Add(this.cbxDataBase);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxInstacias);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnBuscarExcel);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.txtUrlCarpetaExcel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Principal";
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUrlCarpetaExcel;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnBuscarExcel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ComboBox cbxInstacias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxDataBase;
        private System.Windows.Forms.Button btnProbar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.CheckedListBox clbListaExcel;
        private System.Windows.Forms.ListBox lbxSeleccionados;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}