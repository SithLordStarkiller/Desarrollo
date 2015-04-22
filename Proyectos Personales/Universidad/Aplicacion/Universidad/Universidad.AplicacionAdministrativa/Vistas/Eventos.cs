using System;

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
