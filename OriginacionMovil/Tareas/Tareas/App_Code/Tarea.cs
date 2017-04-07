using System;

namespace PubliPayments
{
    public abstract class Tarea
    {
        public TipoTarea Tipo;
        public int Estatus;
        public DateTime HoraEjecucion { get; set; }
        public abstract int Ejecutar();
        public abstract int Terminar();

        protected Tarea()
        {
            Estatus = 0;
        }
    }

    public enum TipoTarea
    {
        EjecucionUnica = 0,
        EjecucionContinua = 1
    }
}
