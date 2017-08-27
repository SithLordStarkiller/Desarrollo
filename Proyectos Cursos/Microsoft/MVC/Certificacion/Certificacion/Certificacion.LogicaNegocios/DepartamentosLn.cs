using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Certificacion.AccesoDatos;
using Certificacion.Modelos;

namespace Certificacion.LogicaNegocios
{
    public class DepartamentosLn
    {
        public List<Departamentos> ObtenesListaDeparatamentos()
        {
            return new DepartamentosDa().ObtenesListaDeparatamentos();
        }
    }
}
