namespace Universidad.AplicacionAdministrativa.Vistas
{
    partial class Inicio
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslInformacion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspProgreso = new System.Windows.Forms.ToolStripProgressBar();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archviToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TRV_Menu = new System.Windows.Forms.TreeView();
            this.tbcContenido = new System.Windows.Forms.TabControl();
            this.LBL_Nombre_Usuario = new System.Windows.Forms.Label();
            this.LBL_Tipo_Usuario = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslInformacion,
            this.tspProgreso});
            this.statusStrip1.Location = new System.Drawing.Point(0, 666);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1228, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslInformacion
            // 
            this.tsslInformacion.Name = "tsslInformacion";
            this.tsslInformacion.Size = new System.Drawing.Size(0, 17);
            // 
            // tspProgreso
            // 
            this.tspProgreso.Name = "tspProgreso";
            this.tspProgreso.Size = new System.Drawing.Size(100, 16);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(216, 642);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archviToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1228, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archviToolStripMenuItem
            // 
            this.archviToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem,
            this.cerrarSesionToolStripMenuItem});
            this.archviToolStripMenuItem.Name = "archviToolStripMenuItem";
            this.archviToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archviToolStripMenuItem.Text = "Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // TRV_Menu
            // 
            this.TRV_Menu.Location = new System.Drawing.Point(0, 27);
            this.TRV_Menu.Name = "TRV_Menu";
            this.TRV_Menu.Size = new System.Drawing.Size(216, 636);
            this.TRV_Menu.TabIndex = 3;
            this.TRV_Menu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TRV_Menu_AfterSelect);
            // 
            // tbcContenido
            // 
            this.tbcContenido.Location = new System.Drawing.Point(222, 76);
            this.tbcContenido.Name = "tbcContenido";
            this.tbcContenido.SelectedIndex = 0;
            this.tbcContenido.Size = new System.Drawing.Size(989, 590);
            this.tbcContenido.TabIndex = 4;
            // 
            // LBL_Nombre_Usuario
            // 
            this.LBL_Nombre_Usuario.AutoSize = true;
            this.LBL_Nombre_Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Nombre_Usuario.Location = new System.Drawing.Point(1030, 44);
            this.LBL_Nombre_Usuario.Name = "LBL_Nombre_Usuario";
            this.LBL_Nombre_Usuario.Size = new System.Drawing.Size(0, 20);
            this.LBL_Nombre_Usuario.TabIndex = 5;
            // 
            // LBL_Tipo_Usuario
            // 
            this.LBL_Tipo_Usuario.AutoSize = true;
            this.LBL_Tipo_Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Tipo_Usuario.Location = new System.Drawing.Point(240, 44);
            this.LBL_Tipo_Usuario.Name = "LBL_Tipo_Usuario";
            this.LBL_Tipo_Usuario.Size = new System.Drawing.Size(0, 20);
            this.LBL_Tipo_Usuario.TabIndex = 6;
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1228, 688);
            this.Controls.Add(this.LBL_Tipo_Usuario);
            this.Controls.Add(this.LBL_Nombre_Usuario);
            this.Controls.Add(this.tbcContenido);
            this.Controls.Add(this.TRV_Menu);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Inicio_FormClosing);
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslInformacion;
        private System.Windows.Forms.ToolStripProgressBar tspProgreso;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archviToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.TreeView TRV_Menu;
        private System.Windows.Forms.TabControl tbcContenido;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.Label LBL_Nombre_Usuario;
        private System.Windows.Forms.Label LBL_Tipo_Usuario;
    }
}