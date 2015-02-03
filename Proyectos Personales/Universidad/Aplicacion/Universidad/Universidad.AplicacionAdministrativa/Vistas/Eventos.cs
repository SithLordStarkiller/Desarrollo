using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Universidad.AplicacionAdministrativa.Vistas
{
    public class EventosArgs : EventArgs
    {
        private int _noCheckBox;

        public EventosArgs(int noCheckBox)
        {
            _noCheckBox = noCheckBox;
        }

        // This is a straightforward implementation for 
        // declaring a public field
        public int NoCheckBox
        {
            set
            {
                _noCheckBox = value;
            }
            get
            {
                return _noCheckBox;
            }
        }
    }

    public class  Evento : EventArgs
    {
        public int numero;

        public delegate void EventoHandler(int cbx);
        public event EventoHandler AlfinalizarActualizacion;

        public void Inicia(int no)
        {
            numero = no;
        }

        public void Finalizado()
        {
            var arg = numero;

            AlfinalizarActualizacion(arg);
        }
    }
}
