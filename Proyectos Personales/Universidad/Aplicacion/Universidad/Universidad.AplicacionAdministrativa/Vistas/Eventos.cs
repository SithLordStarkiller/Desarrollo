using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Universidad.AplicacionAdministrativa.Vistas
{
    public class  Evento : EventArgs
    {
        public int Numero;

        public delegate void EventoHandler(int cbx);
        public event EventoHandler AlfinalizarActualizacion;

        public void Inicia(int numero)
        {
            Numero = numero;
        }

        public void Finalizado()
        {
            var arg = Numero;

            AlfinalizarActualizacion(arg);
        }
    }
}
