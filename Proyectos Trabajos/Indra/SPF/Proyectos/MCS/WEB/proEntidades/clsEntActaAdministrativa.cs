using System;

namespace SICOGUA.Entidades
{
    public class clsEntActaAdministrativa
    {
        private int _idRevDocumento1ra;
        private string _fechaActaPr;
        private string _NoOficioPr;
        private string _fechaOficioPr;



        private byte[] _docAdjuntoPr;

        private string _observaciones;
        private string _fechaCancelacion;   
        private Guid _idEmpleado;
        private int _idInstalacion;
        private int _idServicio;
        private int _idRevision;

        private int _idCancelacion;
        private string _desServicio;
        private string _desInstalacion;
        private bool _revVigente;
        private int _tipoBusquedaProceso;
        private int _acta;
        private int _proceso;
        private int _movimiento;
        private bool _columnaReporte;
        private string _rdObservaciones;

        private clsEntRenuncia _objRenuncia = new clsEntRenuncia();


        public bool columnaReporte
        {
            get { return _columnaReporte; }
            set { _columnaReporte = value; }
        }

        public int acta
        {
            get { return _acta; }
            set { _acta = value; }
        }
        public int proceso
        {
            get { return _proceso; }
            set { _proceso = value; }
        }
        public int movimiento
        {
            get { return _movimiento; }
            set { _movimiento = value; }
        }

        public clsEntRenuncia objRenuncia
        {
            get { return _objRenuncia; }
            set { _objRenuncia = value; }
        }


        public int tipoBusquedaProceso
        {
            get { return _tipoBusquedaProceso; }
            set { _tipoBusquedaProceso = value; }
        }

        public bool revVigente
        {
            get { return _revVigente; }
            set { _revVigente = value; }
        }



        public int idCancelacion
        {
            get { return _idCancelacion; }
            set { _idCancelacion = value; }
        }



        public int idRevDocumento1ra
        {
            get { return _idRevDocumento1ra; }
            set { _idRevDocumento1ra = value; }
        }

        public int idRevision
        {
            get { return _idRevision; }
            set { _idRevision = value; }
        }

        public string desServicio
        {
            get { return _desServicio; }
            set { _desServicio = value; }
        }

        public string desInstalacion
        {
            get { return _desInstalacion; }
            set { _desInstalacion = value; }
        }

        public int idServicio
        {
            get { return _idServicio; }
            set { _idServicio= value; }
        }

        public int idInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }

        public Guid idEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public string fechaActaPr
        {
            get { return _fechaActaPr; }
            set { _fechaActaPr = value; }
        }

        public string NoOficioPr
        {
            get { return _NoOficioPr; }
            set { _NoOficioPr = value; }
        }

        public string fechaOficioPr
        {
            get { return _fechaOficioPr; }
            set { _fechaOficioPr = value; }
        }

       

        

       

        public byte[] docAdjuntoPr
        {
            get { return _docAdjuntoPr; }
            set { _docAdjuntoPr = value; }
        }

       

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        public string fechaCancelacion
        {
            get { return _fechaCancelacion; }
            set { _fechaCancelacion = value; }
        }

        public string rdObservaciones
        {
            get { return _rdObservaciones; }
            set { _rdObservaciones = value; }
        }

    }
}
