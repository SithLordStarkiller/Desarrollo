namespace DALCTester
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnHelpConstructor1 = new System.Windows.Forms.Button();
            this.btnConstructor3 = new System.Windows.Forms.Button();
            this.btnConstructor2 = new System.Windows.Forms.Button();
            this.btnConstructor1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnExecScalar4 = new System.Windows.Forms.Button();
            this.btnExecScalar6 = new System.Windows.Forms.Button();
            this.btnExecScalar5 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnExecScalar1 = new System.Windows.Forms.Button();
            this.btnExecScalar2 = new System.Windows.Forms.Button();
            this.btnExecScalar3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblDBName = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExecNonQuery5 = new System.Windows.Forms.Button();
            this.btnExecNonQuery4 = new System.Windows.Forms.Button();
            this.btnExecNonQuery3 = new System.Windows.Forms.Button();
            this.btnExecNonQuery1 = new System.Windows.Forms.Button();
            this.btnExecNonQuery2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnExecDataTable1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExecReader1 = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnMisc1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnHelpConstructor1);
            this.groupBox1.Controls.Add(this.btnConstructor3);
            this.groupBox1.Controls.Add(this.btnConstructor2);
            this.groupBox1.Controls.Add(this.btnConstructor1);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DALC4NET Constructors";
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(314, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 26);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(314, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 26);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnHelpConstructor1
            // 
            this.btnHelpConstructor1.Image = ((System.Drawing.Image)(resources.GetObject("btnHelpConstructor1.Image")));
            this.btnHelpConstructor1.Location = new System.Drawing.Point(314, 13);
            this.btnHelpConstructor1.Name = "btnHelpConstructor1";
            this.btnHelpConstructor1.Size = new System.Drawing.Size(30, 26);
            this.btnHelpConstructor1.TabIndex = 3;
            this.btnHelpConstructor1.UseVisualStyleBackColor = true;
            this.btnHelpConstructor1.Click += new System.EventHandler(this.btnHelpConstructor1_Click);
            // 
            // btnConstructor3
            // 
            this.btnConstructor3.Location = new System.Drawing.Point(36, 71);
            this.btnConstructor3.Name = "btnConstructor3";
            this.btnConstructor3.Size = new System.Drawing.Size(277, 25);
            this.btnConstructor3.TabIndex = 2;
            this.btnConstructor3.Text = "Connection string and provider name as parameter";
            this.btnConstructor3.UseVisualStyleBackColor = true;
            this.btnConstructor3.Click += new System.EventHandler(this.btnConstructor3_Click);
            // 
            // btnConstructor2
            // 
            this.btnConstructor2.Location = new System.Drawing.Point(38, 43);
            this.btnConstructor2.Name = "btnConstructor2";
            this.btnConstructor2.Size = new System.Drawing.Size(276, 25);
            this.btnConstructor2.TabIndex = 1;
            this.btnConstructor2.Text = "Connection key as parameter";
            this.btnConstructor2.UseVisualStyleBackColor = true;
            this.btnConstructor2.Click += new System.EventHandler(this.btnConstructor2_Click);
            // 
            // btnConstructor1
            // 
            this.btnConstructor1.Location = new System.Drawing.Point(38, 14);
            this.btnConstructor1.Name = "btnConstructor1";
            this.btnConstructor1.Size = new System.Drawing.Size(276, 25);
            this.btnConstructor1.TabIndex = 0;
            this.btnConstructor1.Text = "No parameter";
            this.btnConstructor1.UseVisualStyleBackColor = true;
            this.btnConstructor1.Click += new System.EventHandler(this.btnConstructor1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Location = new System.Drawing.Point(13, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 222);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ExecuteScalar";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnExecScalar4);
            this.groupBox7.Controls.Add(this.btnExecScalar6);
            this.groupBox7.Controls.Add(this.btnExecScalar5);
            this.groupBox7.Location = new System.Drawing.Point(18, 116);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(311, 96);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Stored Procedure";
            // 
            // btnExecScalar4
            // 
            this.btnExecScalar4.Location = new System.Drawing.Point(19, 15);
            this.btnExecScalar4.Name = "btnExecScalar4";
            this.btnExecScalar4.Size = new System.Drawing.Size(283, 25);
            this.btnExecScalar4.TabIndex = 4;
            this.btnExecScalar4.Text = "Execute Stored Procedure (Without Parameter)";
            this.btnExecScalar4.UseVisualStyleBackColor = true;
            this.btnExecScalar4.Click += new System.EventHandler(this.btnExecScalar4_Click);
            // 
            // btnExecScalar6
            // 
            this.btnExecScalar6.Location = new System.Drawing.Point(19, 69);
            this.btnExecScalar6.Name = "btnExecScalar6";
            this.btnExecScalar6.Size = new System.Drawing.Size(283, 25);
            this.btnExecScalar6.TabIndex = 6;
            this.btnExecScalar6.Text = "Execute Stored Procedure (Multiple Parameter)";
            this.btnExecScalar6.UseVisualStyleBackColor = true;
            this.btnExecScalar6.Click += new System.EventHandler(this.btnExecScalar6_Click);
            // 
            // btnExecScalar5
            // 
            this.btnExecScalar5.Location = new System.Drawing.Point(19, 43);
            this.btnExecScalar5.Name = "btnExecScalar5";
            this.btnExecScalar5.Size = new System.Drawing.Size(283, 25);
            this.btnExecScalar5.TabIndex = 5;
            this.btnExecScalar5.Text = "Execute Stored Procedure (Single Parameter)";
            this.btnExecScalar5.UseVisualStyleBackColor = true;
            this.btnExecScalar5.Click += new System.EventHandler(this.btnExecScalar5_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnExecScalar1);
            this.groupBox6.Controls.Add(this.btnExecScalar2);
            this.groupBox6.Controls.Add(this.btnExecScalar3);
            this.groupBox6.Location = new System.Drawing.Point(18, 17);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(311, 97);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Sql Command Text";
            // 
            // btnExecScalar1
            // 
            this.btnExecScalar1.Location = new System.Drawing.Point(19, 19);
            this.btnExecScalar1.Name = "btnExecScalar1";
            this.btnExecScalar1.Size = new System.Drawing.Size(283, 25);
            this.btnExecScalar1.TabIndex = 1;
            this.btnExecScalar1.Text = "Execute Sql Command (Without Parameter)";
            this.btnExecScalar1.UseVisualStyleBackColor = true;
            this.btnExecScalar1.Click += new System.EventHandler(this.btnExecScalar1_Click);
            // 
            // btnExecScalar2
            // 
            this.btnExecScalar2.Location = new System.Drawing.Point(19, 46);
            this.btnExecScalar2.Name = "btnExecScalar2";
            this.btnExecScalar2.Size = new System.Drawing.Size(283, 25);
            this.btnExecScalar2.TabIndex = 2;
            this.btnExecScalar2.Text = "Execute Sql Command (Single Parameter)";
            this.btnExecScalar2.UseVisualStyleBackColor = true;
            this.btnExecScalar2.Click += new System.EventHandler(this.btnExecScalar2_Click);
            // 
            // btnExecScalar3
            // 
            this.btnExecScalar3.Location = new System.Drawing.Point(19, 73);
            this.btnExecScalar3.Name = "btnExecScalar3";
            this.btnExecScalar3.Size = new System.Drawing.Size(283, 25);
            this.btnExecScalar3.TabIndex = 3;
            this.btnExecScalar3.Text = "Execute Sql Command (Multiple Parameter)";
            this.btnExecScalar3.UseVisualStyleBackColor = true;
            this.btnExecScalar3.Click += new System.EventHandler(this.btnExecScalar3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(828, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.contactToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem1.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // contactToolStripMenuItem
            // 
            this.contactToolStripMenuItem.Name = "contactToolStripMenuItem";
            this.contactToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.contactToolStripMenuItem.Text = "&Contact";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDBName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 658);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(828, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblDBName
            // 
            this.lblDBName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblDBName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new System.Drawing.Size(4, 17);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExecNonQuery5);
            this.groupBox2.Controls.Add(this.btnExecNonQuery4);
            this.groupBox2.Controls.Add(this.btnExecNonQuery3);
            this.groupBox2.Controls.Add(this.btnExecNonQuery1);
            this.groupBox2.Controls.Add(this.btnExecNonQuery2);
            this.groupBox2.Location = new System.Drawing.Point(16, 374);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 158);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ExecuteNonQuery";
            // 
            // btnExecNonQuery5
            // 
            this.btnExecNonQuery5.Location = new System.Drawing.Point(37, 127);
            this.btnExecNonQuery5.Name = "btnExecNonQuery5";
            this.btnExecNonQuery5.Size = new System.Drawing.Size(283, 25);
            this.btnExecNonQuery5.TabIndex = 6;
            this.btnExecNonQuery5.Text = "Execute Stored Proc (Multiple Params && Transaction)";
            this.btnExecNonQuery5.UseVisualStyleBackColor = true;
            this.btnExecNonQuery5.Click += new System.EventHandler(this.btnExecNonQuery5_Click);
            // 
            // btnExecNonQuery4
            // 
            this.btnExecNonQuery4.Location = new System.Drawing.Point(37, 100);
            this.btnExecNonQuery4.Name = "btnExecNonQuery4";
            this.btnExecNonQuery4.Size = new System.Drawing.Size(283, 25);
            this.btnExecNonQuery4.TabIndex = 5;
            this.btnExecNonQuery4.Text = "Execute Sql Command (Multiple Params && Transaction)";
            this.btnExecNonQuery4.UseVisualStyleBackColor = true;
            this.btnExecNonQuery4.Click += new System.EventHandler(this.btnExecNonQuery4_Click);
            // 
            // btnExecNonQuery3
            // 
            this.btnExecNonQuery3.Location = new System.Drawing.Point(37, 72);
            this.btnExecNonQuery3.Name = "btnExecNonQuery3";
            this.btnExecNonQuery3.Size = new System.Drawing.Size(283, 25);
            this.btnExecNonQuery3.TabIndex = 4;
            this.btnExecNonQuery3.Text = "Execute Stored Procedure (Multiple Parameter)";
            this.btnExecNonQuery3.UseVisualStyleBackColor = true;
            this.btnExecNonQuery3.Click += new System.EventHandler(this.btnExecNonQuery3_Click);
            // 
            // btnExecNonQuery1
            // 
            this.btnExecNonQuery1.Location = new System.Drawing.Point(37, 16);
            this.btnExecNonQuery1.Name = "btnExecNonQuery1";
            this.btnExecNonQuery1.Size = new System.Drawing.Size(283, 25);
            this.btnExecNonQuery1.TabIndex = 1;
            this.btnExecNonQuery1.Text = "Execute Sql Command (Without Parameter)";
            this.btnExecNonQuery1.UseVisualStyleBackColor = true;
            this.btnExecNonQuery1.Click += new System.EventHandler(this.btnExecNonQuery1_Click);
            // 
            // btnExecNonQuery2
            // 
            this.btnExecNonQuery2.Location = new System.Drawing.Point(37, 44);
            this.btnExecNonQuery2.Name = "btnExecNonQuery2";
            this.btnExecNonQuery2.Size = new System.Drawing.Size(283, 25);
            this.btnExecNonQuery2.TabIndex = 3;
            this.btnExecNonQuery2.Text = "Execute Sql Command (Multiple Parameter)";
            this.btnExecNonQuery2.UseVisualStyleBackColor = true;
            this.btnExecNonQuery2.Click += new System.EventHandler(this.btnExecNonQuery2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.btnExecDataTable1);
            this.groupBox4.Location = new System.Drawing.Point(415, 494);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(367, 101);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ExecuteDataTable";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(30, 46);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(325, 42);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "Execution of Stored Proc and Sql Commands with parameter are similar to ExecuteNo" +
                "nQuery.\r\n";
            // 
            // btnExecDataTable1
            // 
            this.btnExecDataTable1.Location = new System.Drawing.Point(30, 15);
            this.btnExecDataTable1.Name = "btnExecDataTable1";
            this.btnExecDataTable1.Size = new System.Drawing.Size(251, 25);
            this.btnExecDataTable1.TabIndex = 4;
            this.btnExecDataTable1.Text = "Execute Sql Command (Without Parameter)";
            this.btnExecDataTable1.UseVisualStyleBackColor = true;
            this.btnExecDataTable1.Click += new System.EventHandler(this.btnExecDataTable1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(418, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(287, 450);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.btnExecReader1);
            this.groupBox5.Location = new System.Drawing.Point(18, 535);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(350, 89);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ExecuteReader";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(325, 42);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Execution of Stored Proc and Sql Commands with parameter and Transaction are simi" +
                "lar to ExecuteNonQuery.\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 14);
            this.label1.TabIndex = 6;
            // 
            // btnExecReader1
            // 
            this.btnExecReader1.Location = new System.Drawing.Point(37, 13);
            this.btnExecReader1.Name = "btnExecReader1";
            this.btnExecReader1.Size = new System.Drawing.Size(283, 25);
            this.btnExecReader1.TabIndex = 5;
            this.btnExecReader1.Text = "Execute Sql Command (Without Parameter)";
            this.btnExecReader1.UseVisualStyleBackColor = true;
            this.btnExecReader1.Click += new System.EventHandler(this.btnExecReader1_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnMisc1);
            this.groupBox8.Location = new System.Drawing.Point(415, 601);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(311, 48);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Miscellaneous";
            // 
            // btnMisc1
            // 
            this.btnMisc1.Location = new System.Drawing.Point(30, 14);
            this.btnMisc1.Name = "btnMisc1";
            this.btnMisc1.Size = new System.Drawing.Size(251, 24);
            this.btnMisc1.TabIndex = 1;
            this.btnMisc1.Text = "Execute Stored Proc and Retrieve Output";
            this.btnMisc1.UseVisualStyleBackColor = true;
            this.btnMisc1.Click += new System.EventHandler(this.btnMisc1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 680);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DALC4NET Tester";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnConstructor2;
        private System.Windows.Forms.Button btnConstructor1;
        private System.Windows.Forms.Button btnConstructor3;
        private System.Windows.Forms.Button btnHelpConstructor1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnExecScalar6;
        private System.Windows.Forms.Button btnExecScalar5;
        private System.Windows.Forms.Button btnExecScalar4;
        private System.Windows.Forms.Button btnExecScalar3;
        private System.Windows.Forms.Button btnExecScalar2;
        private System.Windows.Forms.Button btnExecScalar1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExecDataTable1;
        private System.Windows.Forms.Button btnExecNonQuery1;
        private System.Windows.Forms.Button btnExecNonQuery2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExecNonQuery3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnMisc1;
        private System.Windows.Forms.Button btnExecReader1;
        private System.Windows.Forms.Button btnExecNonQuery5;
        private System.Windows.Forms.Button btnExecNonQuery4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripStatusLabel lblDBName;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contactToolStripMenuItem;


    }
}

