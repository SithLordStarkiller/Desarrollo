namespace DALCTester
{
    partial class Help
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
            this.txtaDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConstructorInfo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtaDescription
            // 
            this.txtaDescription.Location = new System.Drawing.Point(12, 54);
            this.txtaDescription.Multiline = true;
            this.txtaDescription.Name = "txtaDescription";
            this.txtaDescription.ReadOnly = true;
            this.txtaDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtaDescription.Size = new System.Drawing.Size(510, 167);
            this.txtaDescription.TabIndex = 0;
            this.txtaDescription.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Description:";
            // 
            // lblConstructorInfo
            // 
            this.lblConstructorInfo.AutoSize = true;
            this.lblConstructorInfo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConstructorInfo.Location = new System.Drawing.Point(12, 10);
            this.lblConstructorInfo.Name = "lblConstructorInfo";
            this.lblConstructorInfo.Size = new System.Drawing.Size(98, 14);
            this.lblConstructorInfo.TabIndex = 2;
            this.lblConstructorInfo.Text = "Constructor Info";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(447, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "&Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 273);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblConstructorInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtaDescription);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Help";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtaDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblConstructorInfo;
        private System.Windows.Forms.Button button1;
    }
}