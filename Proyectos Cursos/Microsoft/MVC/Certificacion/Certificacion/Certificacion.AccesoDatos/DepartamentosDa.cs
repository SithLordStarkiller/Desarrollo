namespace Certificacion.AccesoDatos
{
    using Modelos;
    using System.Collections.Generic;
    using System.Linq;

    public class DepartamentosDa
    {
        public List<Departamentos> ObtenesListaDeparatamentos()
        {
            using (var contexto = new ModelDbEntities())
            {
                return contexto.Departamentos.ToList();
            }
        }
    }
}
