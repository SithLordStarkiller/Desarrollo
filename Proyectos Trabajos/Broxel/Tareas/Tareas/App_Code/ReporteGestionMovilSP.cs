using System;
using PubliPayments.Entidades;

namespace PubliPayments
{
    class ReporteGestionMovilSp : Tarea
    {
        public override int Ejecutar()
        {
            var entidad = new EntReporteGestionMovilSp();
            entidad.ActualizarTabla();
            return 1;
        }

        public override int Terminar()
        {
            return 0;
        }
    }
}
