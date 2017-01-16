using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace PubliPayments
{

    public partial class PubliPaymentsService : ServiceBase
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        private readonly List<Proceso> _listaDeProcesos = new List<Proceso>();
        private EstatusServicio _estatus = EstatusServicio.Stopped;

        private ServiceStatus _serviceStatus;
        public PubliPaymentsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log("Iniciando Servicio");

            _estatus = EstatusServicio.StartPending;
            _serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_START_PENDING,
                dwWaitHint = 100000
            };
            SetServiceStatus(ServiceHandle, ref _serviceStatus);

            
            var tarea = new Task(TareaOnStart);
            tarea.Start();

            var tareaMonitoreo = new Task(MonitoreoDeProcesos);
            tareaMonitoreo.Start();
        }

        private void MonitoreoDeProcesos()
        {
            int intentos = 0;
            while (_estatus == EstatusServicio.StartPending && intentos < 120) //Espero 1 minuto como maximo
            {
                //Espero a que termine de ejecutarse ante de continuar
                Thread.Sleep(500);
                intentos++;
            }

            while (_estatus != EstatusServicio.Stopped && _estatus != EstatusServicio.StopPending)
            {
                if (_estatus == EstatusServicio.Running)
                {
                    Log("Monitor");
                    try
                    {
                        foreach (Proceso proc in _listaDeProcesos)
                        {
                            var ejec = Ejecutandose(proc.Nombre, proc.Archivo);
                            if (ejec > 0 && ejec != proc.Id)
                            {
                                // Se encontró otra instancia del proceso
                                if (ejec != proc.Id)
                                    Log(string.Format("Warning: Se encontró otra instancia del proceso - id {0} - proceso.id {1}", ejec,
                                        proc.Id));
                            }

                            if (ejec <= 0)
                            {
                                //Si no se esta ejecutando, lo ejecuto
                                EjecutarPrograma(proc);
                                ejec = Ejecutandose(proc.Nombre, proc.Archivo);
                                Log(ejec > 0 ? "Ejecutandose OK - Id del proceso: " + ejec : "Error al ejecutar tarea");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log("Error en MonitoreoDeProcesos - " + ex.Message);
                    }
                }
                Thread.Sleep(180000); //Monitoreo cada 3 minutos
            }
        }

        protected override void OnStop()
        {
            Log("Cerrando Servicio...");

            _estatus = EstatusServicio.StopPending;
            _serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            SetServiceStatus(ServiceHandle, ref _serviceStatus);

            foreach (Proceso proc in _listaDeProcesos)
            {
                try
                {
                    Log(string.Format("Cerrando el proceso: {0} - id: {1}", proc.Nombre, proc.Id));
                    Process.GetProcessById(proc.Id).Kill();
                }
                catch (Exception e)
                {
                    Log(string.Format("Fallo al intentar cerrar el proceso: {0} - id: {1} - Error: {2}", proc.Nombre,
                        proc.Id, e.Message));
                }
            }
            _estatus = EstatusServicio.Stopped;
            _serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(ServiceHandle, ref _serviceStatus);
        }

        protected override void OnPause()
        {
            Log("Pausando Servicio");

            if (_estatus == EstatusServicio.StartPending)
            {
                Log("El servicio aun no inicia completamente, intentando esperar...");
                int intentos = 0;
                while (_estatus == EstatusServicio.StartPending && intentos < 120) //Espero 1 minuto como maximo
                {
                    //Espero a que termine de ejecutarse ante de permitir la pausa
                    Thread.Sleep(500);
                    intentos++;
                }
            }

            if (_estatus == EstatusServicio.Running)
            {
                _estatus = EstatusServicio.PausePending;
                _serviceStatus.dwCurrentState = ServiceState.SERVICE_PAUSE_PENDING;
                SetServiceStatus(ServiceHandle, ref _serviceStatus);
                
                base.OnPause();

                _estatus = EstatusServicio.Paused;
                _serviceStatus.dwCurrentState = ServiceState.SERVICE_PAUSED;
                SetServiceStatus(ServiceHandle, ref _serviceStatus);
                Log("Servicio Pausado");
            }
        }

        protected override void OnContinue()
        {
            Log("Continuando Servicio");
            _estatus = EstatusServicio.ContinuePending;
            _serviceStatus.dwCurrentState = ServiceState.SERVICE_CONTINUE_PENDING;
            SetServiceStatus(ServiceHandle, ref _serviceStatus);

            
            base.OnContinue();

            _estatus = EstatusServicio.Running;
            _serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(ServiceHandle, ref _serviceStatus);
            Log("Servicio Ejecutandose");
        }

        private void Log(string texto)
        {
            Trace.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff"), texto));
        }

        private Proceso EjecutarPrograma(Proceso proc)
        {
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas",
                FileName = proc.Archivo
            };
            try
            {
                Process p = Process.Start(startInfo);
                if (p != null)
                    proc.Id = p.Id;
            }
            catch (Exception ex)
            {
                Log("Error en EjecutarPrograma - Error: " + ex.Message);
            }
            Log(proc.Archivo + " ejecutado...");
            return proc;
        }


        private int Ejecutandose(string nombreProceso, string archivo, bool cerrarOtrasInstancias = false)
        {
            int resultado = 0;
            try
            {
                const string wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
                using (var searcher = new ManagementObjectSearcher(wmiQueryString))
                using (var results = searcher.Get())
                {
                    var query = from p in Process.GetProcessesByName(nombreProceso)
                                join mo in results.Cast<ManagementObject>()
                                on p.Id equals (int)(uint)mo["ProcessId"]
                                select new
                                {
                                    p.Id,
                                    p.ProcessName,
                                    Path = (string)mo["ExecutablePath"],
                                    CommandLine = (string)mo["CommandLine"]
                                };
                    foreach (var item in query)
                    {
                        if (item.Path.Equals(archivo))
                        {
                            if (resultado > 0)
                            {
                                Log("Warning: Encontradas multiples instancias del proceso: " + item.Id);
                                if (cerrarOtrasInstancias)
                                {
                                    try
                                    {
                                        Log("Se está ejecutando " + item.ProcessName + ", matando el proceso : " + item.Id);
                                        Process.GetProcessById(item.Id).Kill();
                                    }
                                    catch (Exception e)
                                    {
                                        Log("No se pudo cerrar el proceso, error: " + e.Message);

                                    }
                                }
                            }
                            else
                                resultado = item.Id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log("Error en Ejecutandose - Error: " + ex.Message);
                return -1;
            }

            return resultado;
        }

        private void TareaOnStart()
        {
            Log("Ejecutando Tareas...");
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    var appArc = "appArchivo" + i.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                    var appNom = "appNombre" + i.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                    var appArchivo = ConfigurationManager.AppSettings[appArc];
                    var appNombre = ConfigurationManager.AppSettings[appNom];
                    if (appArchivo != null && appNombre != null)
                    {
                        Log("Encontrado en config proceso: " + appArchivo);

                        var ejec = Ejecutandose(appNombre, appArchivo, true);

                        if (ejec > 0)
                        {
                            try
                            {
                                Log("Se está ejecutando " + appNombre + ", matando el proceso : " + ejec);
                                Process.GetProcessById(ejec).Kill();
                            }
                            catch (Exception e)
                            {
                                Log("No se pudo cerrar el proceso, error: " + e.Message);

                            }

                            Thread.Sleep(1000);
                        }

                        var proc = new Proceso
                        {
                            Archivo = appArchivo,
                            Nombre = appNombre
                        };
                        proc = EjecutarPrograma(proc);
                        _listaDeProcesos.Add(proc);
                        Thread.Sleep(1000);

                        ejec = Ejecutandose(appNombre, appArchivo);

                        if (ejec != proc.Id)
                            Log(string.Format("Warning: Revisar por que el id {0} es distinto al del proceso {1}", ejec,
                                proc.Id));

                        Log(ejec > 0 ? "Ejecutandose OK - Id del proceso: " + ejec : "Error al ejecutar tarea");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log("Error en TareaOnStart - Error: " + ex.Message);
            }

            _estatus = EstatusServicio.Running;
            _serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(ServiceHandle, ref _serviceStatus);
        }
    }

    public class Proceso
    {
        public int Id { get; set; }
        public string Archivo { get; set; }
        public string Nombre { get; set; }
    }

    public enum EstatusServicio
    {
        Stopped = 0x00000001,
        StartPending = 0x00000002,
        StopPending = 0x00000003,
        Running = 0x00000004,
        ContinuePending = 0x00000005,
        PausePending = 0x00000006,
        Paused = 0x00000007
    }

    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public long dwServiceType;
        public ServiceState dwCurrentState;
        public long dwControlsAccepted;
        public long dwWin32ExitCode;
        public long dwServiceSpecificExitCode;
        public long dwCheckPoint;
        public long dwWaitHint;
    };
}
