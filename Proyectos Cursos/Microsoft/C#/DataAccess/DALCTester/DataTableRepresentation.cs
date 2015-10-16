using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DALCTester
{
    public partial class DataTableRepresentation : Form
    {
        public DataTableRepresentation(DataTable data)
        {
            _data = data;
            InitializeComponent();
            dataGridView1.DataSource = _data;
        }

        public DataTable _data;
        public DataTable Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        
    }
}
