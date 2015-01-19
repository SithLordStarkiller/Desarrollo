using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Universidad.AplicacionAdministrativa.Controles.ControPersonas
{
    public partial class AltaPersona : UserControl
    {
        public AltaPersona()
        {
            InitializeComponent();
        }

        private void AltaPersona_Load(object sender, EventArgs e)
        {
            mcrFechaNacimiento.MaxDate = DateTime.Now;
            mcrFechaNacimiento.MinDate = new DateTime(1850,1,1);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var fecha = Convert.ToDateTime(mcrFechaNacimiento.SelectionStart);
            tbxApellidoM.Text = fecha.ToString("d");
        }
    }
}
