using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjemploTreeView.Modelos;

namespace EjemploTreeView.Clases
{
    public class GestionTabPages
    {
        private List<CatTabPages> _listaCatTabPages;

        public List<CatTabPages> ListaCatTabPages
        {
            get
            {
                return _listaCatTabPages;
            }
            set
            {
                _listaCatTabPages = value;
            }
        }

        public GestionTabPages()
        {
            LlenarLista();
        }

        private void LlenarLista()
        {
            ListaCatTabPages = new List<CatTabPages>()
            {
                new CatTabPages
                {
                    IdTabPage = 1, NombreTabPage = @"Suma", IdComboBox = 1, RutaTabPage = @"Vistas\Operaciones Aritmeticas\Suma.cs"
                }
            };
        }

        public List<CatTabPages> ObtenTabPages(int idComboBox)
        {
            return ListaCatTabPages.Where(r => r.IdComboBox == idComboBox).ToList();
        }

    }
}
