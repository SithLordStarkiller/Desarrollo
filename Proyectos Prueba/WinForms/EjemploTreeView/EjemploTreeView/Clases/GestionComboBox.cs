using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjemploTreeView.Modelos;

namespace EjemploTreeView.Clases
{
    public class GestionComboBox
    {
        public List<CatComboBox> ListaCatComboBox
        {
            get { return _listaCatComboBox; }
            set { _listaCatComboBox = value; }
        }

        private List<CatComboBox> _listaCatComboBox;

        public GestionComboBox()
        {
            LlenaList();
        }

        private void LlenaList()
        {
            _listaCatComboBox = new List<CatComboBox>()
            {
                new CatComboBox {IdItem = 1, NombrePropiedad = "Opeaciones Arimeticas"},
                new CatComboBox {IdItem = 2, NombrePropiedad = "Buscar numero"},
                new CatComboBox {IdItem = 3, NombrePropiedad = "Enviar nombre"}
            };
        }
    }
}
