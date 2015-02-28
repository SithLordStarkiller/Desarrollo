namespace OperacionesExcel
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
            this.btnCargarExcelOLDB = new System.Windows.Forms.Button();
            this.btnCargarArchivo = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fodCargaExcel = new System.Windows.Forms.OpenFileDialog();
            this.tbpVistaTablas = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // btnCargarExcelOLDB
            // 
            this.btnCargarExcelOLDB.Location = new System.Drawing.Point(21, 263);
            this.btnCargarExcelOLDB.Name = "btnCargarExcelOLDB";
            this.btnCargarExcelOLDB.Size = new System.Drawing.Size(134, 23);
            this.btnCargarExcelOLDB.TabIndex = 1;
            this.btnCargarExcelOLDB.Text = "Cargar Excel OLE DB";
            this.btnCargarExcelOLDB.UseVisualStyleBackColor = true;
            this.btnCargarExcelOLDB.Click += new System.EventHandler(this.btnCargarExcelOLDB_Click);
            // 
            // btnCargarArchivo
            // 
            this.btnCargarArchivo.Location = new System.Drawing.Point(21, 26);
            this.btnCargarArchivo.Name = "btnCargarArchivo";
            this.btnCargarArchivo.Size = new System.Drawing.Size(114, 23);
            this.btnCargarArchivo.TabIndex = 2;
            this.btnCargarArchivo.Text = "Cargar Archivo";
            this.btnCargarArchivo.UseVisualStyleBackColor = true;
            this.btnCargarArchivo.Click += new System.EventHandler(this.btnCargarArchivo_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(141, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(812, 20);
            this.textBox1.TabIndex = 3;
            // 
            // tbpVistaTablas
            // 
            this.tbpVistaTablas.Location = new System.Drawing.Point(21, 56);
            this.tbpVistaTablas.Name = "tbpVistaTablas";
            this.tbpVistaTablas.SelectedIndex = 0;
            this.tbpVistaTablas.Size = new System.Drawing.Size(932, 201);
            this.tbpVistaTablas.TabIndex = 4;
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 554);
            this.Controls.Add(this.tbpVistaTablas);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCargarArchivo);
            this.Controls.Add(this.btnCargarExcelOLDB);
            this.Name = "Inicio";
            this.Text = "Inicio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCargarExcelOLDB;
        private System.Windows.Forms.Button btnCargarArchivo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog fodCargaExcel;
        private System.Windows.Forms.TabControl tbpVistaTablas;
    }
}