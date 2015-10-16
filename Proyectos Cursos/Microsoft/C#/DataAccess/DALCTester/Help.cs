using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DALCTester
{
    public partial class Help : Form
    {
        public Help()
        {
            
            InitializeComponent();
        }

        private string _description = "";
        public string Description
        {
            get
            {
                return txtaDescription.Text;
            }
            set
            {                
                txtaDescription.Text = value;
                
            }
        }

        private string _constructorInfo = string.Empty;
        public string ConstructorInfo
        {
            get
            {
                return lblConstructorInfo.Text;
            }
            set
            {
                lblConstructorInfo.Text = value;                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        
    }
}