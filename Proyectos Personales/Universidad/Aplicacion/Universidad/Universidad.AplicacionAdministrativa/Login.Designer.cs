namespace Universidad.AplicacionAdministrativa
{
    partial class FORM_Login
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.LBL_Titulo = new System.Windows.Forms.Label();
            this.BTN_Login = new System.Windows.Forms.Button();
            this.BTN_Salir = new System.Windows.Forms.Button();
            this.LBL_Usuario = new System.Windows.Forms.Label();
            this.LBL_Contraseña = new System.Windows.Forms.Label();
            this.TXB_Usuario = new System.Windows.Forms.TextBox();
            this.TXB_Contrasena = new System.Windows.Forms.TextBox();
            this.ckbRecordarSesion = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LBL_Titulo
            // 
            this.LBL_Titulo.AutoSize = true;
            this.LBL_Titulo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Titulo.Location = new System.Drawing.Point(217, 9);
            this.LBL_Titulo.Name = "LBL_Titulo";
            this.LBL_Titulo.Size = new System.Drawing.Size(43, 19);
            this.LBL_Titulo.TabIndex = 1;
            this.LBL_Titulo.Text = "Login";
            // 
            // BTN_Login
            // 
            this.BTN_Login.Location = new System.Drawing.Point(112, 163);
            this.BTN_Login.Name = "BTN_Login";
            this.BTN_Login.Size = new System.Drawing.Size(75, 23);
            this.BTN_Login.TabIndex = 2;
            this.BTN_Login.Text = "Login";
            this.BTN_Login.UseVisualStyleBackColor = true;
            this.BTN_Login.Click += new System.EventHandler(this.BTN_Login_Click);
            // 
            // BTN_Salir
            // 
            this.BTN_Salir.Location = new System.Drawing.Point(286, 163);
            this.BTN_Salir.Name = "BTN_Salir";
            this.BTN_Salir.Size = new System.Drawing.Size(75, 23);
            this.BTN_Salir.TabIndex = 3;
            this.BTN_Salir.Text = "Salir";
            this.BTN_Salir.UseVisualStyleBackColor = true;
            this.BTN_Salir.Click += new System.EventHandler(this.BTN_Salir_Click);
            // 
            // LBL_Usuario
            // 
            this.LBL_Usuario.AutoSize = true;
            this.LBL_Usuario.Location = new System.Drawing.Point(57, 56);
            this.LBL_Usuario.Name = "LBL_Usuario";
            this.LBL_Usuario.Size = new System.Drawing.Size(43, 13);
            this.LBL_Usuario.TabIndex = 4;
            this.LBL_Usuario.Text = "Usuario";
            // 
            // LBL_Contraseña
            // 
            this.LBL_Contraseña.AutoSize = true;
            this.LBL_Contraseña.Location = new System.Drawing.Point(57, 89);
            this.LBL_Contraseña.Name = "LBL_Contraseña";
            this.LBL_Contraseña.Size = new System.Drawing.Size(61, 13);
            this.LBL_Contraseña.TabIndex = 5;
            this.LBL_Contraseña.Text = "Contraseña";
            // 
            // TXB_Usuario
            // 
            this.TXB_Usuario.Location = new System.Drawing.Point(145, 56);
            this.TXB_Usuario.Name = "TXB_Usuario";
            this.TXB_Usuario.Size = new System.Drawing.Size(236, 20);
            this.TXB_Usuario.TabIndex = 6;
            // 
            // TXB_Contrasena
            // 
            this.TXB_Contrasena.Location = new System.Drawing.Point(145, 86);
            this.TXB_Contrasena.Name = "TXB_Contrasena";
            this.TXB_Contrasena.Size = new System.Drawing.Size(236, 20);
            this.TXB_Contrasena.TabIndex = 7;
            this.TXB_Contrasena.UseSystemPasswordChar = true;
            // 
            // ckbRecordarSesion
            // 
            this.ckbRecordarSesion.AutoSize = true;
            this.ckbRecordarSesion.Location = new System.Drawing.Point(276, 129);
            this.ckbRecordarSesion.Name = "ckbRecordarSesion";
            this.ckbRecordarSesion.Size = new System.Drawing.Size(105, 17);
            this.ckbRecordarSesion.TabIndex = 8;
            this.ckbRecordarSesion.Text = "Recordar Sesion";
            this.ckbRecordarSesion.UseVisualStyleBackColor = true;
            // 
            // FORM_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 214);
            this.Controls.Add(this.ckbRecordarSesion);
            this.Controls.Add(this.TXB_Contrasena);
            this.Controls.Add(this.TXB_Usuario);
            this.Controls.Add(this.LBL_Contraseña);
            this.Controls.Add(this.LBL_Usuario);
            this.Controls.Add(this.BTN_Salir);
            this.Controls.Add(this.BTN_Login);
            this.Controls.Add(this.LBL_Titulo);
            this.Name = "FORM_Login";
            this.Text = "Universidad";
            this.Load += new System.EventHandler(this.FORM_Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_Titulo;
        private System.Windows.Forms.Button BTN_Login;
        private System.Windows.Forms.Button BTN_Salir;
        private System.Windows.Forms.Label LBL_Usuario;
        private System.Windows.Forms.Label LBL_Contraseña;
        private System.Windows.Forms.TextBox TXB_Usuario;
        private System.Windows.Forms.TextBox TXB_Contrasena;
        private System.Windows.Forms.CheckBox ckbRecordarSesion;


    }
}

