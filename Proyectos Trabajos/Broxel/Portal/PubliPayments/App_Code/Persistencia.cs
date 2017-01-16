using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public static class Persistencia
    {
        private static object _bloqueoRespuestaUsuario = new object();
        private static object _bloqueoObtenerOrdenes = new object();
        private static object _bloqueoObtenerCreditos = new object();
        private static bool _ejecutando = true;

        private static List<PesistenciaRespuestasUsuario> _listaRespuestasUsuarios = new List<PesistenciaRespuestasUsuario>();
        private static List<PesistenciaObtenerOrdenes> _listaObtenerOrdenes = new List<PesistenciaObtenerOrdenes>();
        private static List<PesistenciaObtenerCreditos> _listaObtenerCreditos = new List<PesistenciaObtenerCreditos>();

        static Persistencia()
        {
            var eliminarAntiguos = new Task(EliminarAntiguos);
            eliminarAntiguos.Start();
        }

        public static DataTable ObtenerRespuestasUsuario(int tipo, string idOrden, int reporte, int idUsuarioPadre,string estatus, string tipoFormulario,
            bool recargar)
        {
            var guidTiempos = Tiempos.Iniciar();

            var pesistente =
                _listaRespuestasUsuarios.FirstOrDefault(
                    x => x.IdOrden == idOrden && x.Tipo == tipo && x.IdUsuarioPadre == idUsuarioPadre && x.Estatus == estatus && x.TipoFormulario==tipoFormulario );

            if (pesistente != null)
            {
                if (recargar)
                {
                    lock (_bloqueoRespuestaUsuario)
                    {
                        _listaRespuestasUsuarios.Remove(pesistente);
                    }
                }
                else
                {
                    Tiempos.Terminar(guidTiempos);
                    return pesistente.Tabla;
                }
            }

            var persistencia = new PesistenciaRespuestasUsuario
            {
                Tipo = tipo,
                IdOrden = idOrden,
                Fecha = DateTime.Now,
                IdUsuarioPadre = idUsuarioPadre,
                Estatus = estatus,
                TipoFormulario = tipoFormulario
            };

            var sql = ConexionSql.Instance;

            var ds = sql.ObtenerRespuestasUsuario(tipo, idOrden, reporte, idUsuarioPadre, estatus, tipoFormulario);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                persistencia.Tabla = ds.Tables[0];
            }
            else
            {
                persistencia.Tabla = null;
            }

            lock (_bloqueoRespuestaUsuario)
            {
                _listaRespuestasUsuarios.Add(persistencia);
            }
            Tiempos.Terminar(guidTiempos);
            return persistencia.Tabla;
        }

        public static DataTable ObtenerOrdenes(int tipo, int idUsuarioPadre, string numCred, int idUsuario, string tipoFormulario,
           bool recargar)
        {
           var guidTiempos = Tiempos.Iniciar();

            var pesistente =
                _listaObtenerOrdenes.FirstOrDefault(
                    x =>
                        x.Tipo == tipo && x.IdUsuarioPadre == idUsuarioPadre && x.NumCred == numCred &&
                        x.IdUsuario == idUsuario && x.TipoFormulario == tipoFormulario);

            if (pesistente != null)
            {
                if (recargar) //En este caso la recaga tiene que ser inmediata
                {
                    lock (_bloqueoObtenerOrdenes)
                    {
                        _listaObtenerOrdenes.Remove(pesistente);
                    }
                }
                else
                {
                    Tiempos.Terminar(guidTiempos);
                    return pesistente.Tabla;
                }
            }

            var persistencia = new PesistenciaObtenerOrdenes
            {
                Tipo = tipo,
                IdUsuarioPadre = idUsuarioPadre,
                NumCred = numCred,
                IdUsuario = idUsuario,
                Fecha = DateTime.Now,
                TipoFormulario = tipoFormulario
            };

            var sql = ConexionSql.Instance;

            var ds = sql.ObtenerOrdenes(tipo, idUsuarioPadre, numCred, idUsuario, tipoFormulario);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                persistencia.Tabla = ds.Tables[0];
            }
            else
            {
                persistencia.Tabla = null;
            }

            lock (_bloqueoObtenerOrdenes)
            {
                _listaObtenerOrdenes.Add(persistencia);
            }
            Tiempos.Terminar(guidTiempos);
            return persistencia.Tabla;
        }

        public static DataTable ObtenerCreditos(string numCred, int usuarioPadre, int idDominio,string tipoFormulario,
          bool recargar)
        {
            var guidTiempos = Tiempos.Iniciar();
            var pesistente =
                _listaObtenerCreditos.FirstOrDefault(
                    x =>
                        x.NumCred == numCred && x.UsuarioPadre == usuarioPadre && x.IdDominio == idDominio && x.TipoFormulario == tipoFormulario);

            if (pesistente != null)
            {
                if (recargar) //En este caso la recaga tiene que ser inmediata
                {
                    lock (_bloqueoObtenerCreditos)
                    {
                        _listaObtenerCreditos.Remove(pesistente);
                    }
                }
                else
                {
                    Tiempos.Terminar(guidTiempos);
                    return pesistente.Tabla;
                }
            }

            var persistencia = new PesistenciaObtenerCreditos
            {
                NumCred = numCred,
                UsuarioPadre = usuarioPadre,
                IdDominio = idDominio,
                Fecha = DateTime.Now,
                TipoFormulario = tipoFormulario
            };

            var sql = ConexionSql.Instance;

            var ds = sql.ObtenerCreditos(numCred, usuarioPadre, idDominio, tipoFormulario);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                persistencia.Tabla = ds.Tables[0];
            }
            else
            {
                persistencia.Tabla = null;
            }

            lock (_bloqueoObtenerCreditos)
            {
                _listaObtenerCreditos.Add(persistencia);
            }
            Tiempos.Terminar(guidTiempos);
            return persistencia.Tabla;
        }

        private static void EliminarAntiguos()
        {
            while (_ejecutando)
            {            
                Thread.Sleep(10000); //Espero 10 segundos

                try
                {
                    var fecha = DateTime.Now.AddHours(-1.00); // Se considera antiguo despues de una hora
                    var respuestasUsuariosAntiguos = from l in _listaRespuestasUsuarios
                                                     where l.Fecha < fecha
                                                     select l;

                    lock (_bloqueoRespuestaUsuario)
                    {
                        var antiguos =  respuestasUsuariosAntiguos.ToList();
                        for (int i = 0; i < antiguos.Count; i++)
                        {
                            var antiguo = antiguos[i];
                            _listaRespuestasUsuarios.Remove(antiguo);
                        }
                    }

                    var fecha1 = DateTime.Now.AddMinutes(-15.00);// Se considera antiguo despues de 15 minutos
                    var respuestasObtenerOrdenes = from l in _listaObtenerOrdenes
                                                   where l.Fecha < fecha1
                                                   select l;

                    lock (_bloqueoObtenerOrdenes)
                    {
                        var antiguos = respuestasObtenerOrdenes.ToList();
                        for (int i = 0; i < antiguos.Count; i++)
                        {
                            var antiguo = antiguos[i];
                            _listaObtenerOrdenes.Remove(antiguo);
                        }
                    }

                    var fecha2 = DateTime.Now.AddMinutes(-15.00);// Se considera antiguo despues de 15 minutos
                    var respuestasObtenerCreditos = from l in _listaObtenerCreditos
                                                    where l.Fecha < fecha2
                                                    select l;

                    lock (_bloqueoObtenerCreditos)
                    {
                        var antiguos = respuestasObtenerCreditos.ToList();
                        for (int i = 0; i < antiguos.Count; i++)
                        {
                            var antiguo = antiguos[i];
                            _listaObtenerCreditos.Remove(antiguo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Persitencia", "Error: EliminarAntiguos - " + ex.Message);
                }

            }
        }

        public static void Detener()
        {
            _ejecutando = false;
        }
    }

    public class PesistenciaRespuestasUsuario
    {
        public int Tipo { get; set; }
        public string IdOrden { get; set; }
        public int IdUsuarioPadre { get; set; }
        public DateTime Fecha { get; set; }
        public DataTable Tabla { get; set; }
        public string Estatus { get; set; }
        public string TipoFormulario { get; set; }
    }

    public class PesistenciaObtenerOrdenes
    {
        public int Tipo { get; set; }
        public int IdUsuarioPadre { get; set; }
        public string NumCred { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public DataTable Tabla { get; set; }
        public string TipoFormulario { get; set; }
    }

    public class PesistenciaObtenerCreditos
    {
        public string NumCred { get; set; }
        public int UsuarioPadre { get; set; }
        public int IdDominio { get; set; }
        public DateTime Fecha { get; set; }
        public DataTable Tabla { get; set; }
        public string TipoFormulario { get; set; }
    }
}