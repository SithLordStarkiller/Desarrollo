using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Policy;
using System.Text;
using Sincronizar.BackEndFormiik;

namespace Sincronizar
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Comienza");

            Console.WriteLine("Obteniendo ordenes a consultar...");
            var ent = new ConexionSql();
            var os = ent.ObtenerOrdenes();

            Console.WriteLine("Se encontraron " + os.Count + " ordenes");


            //Parallel.For(0, os.Count / 100, i =>
            //{
            for (int i = 0; i < Math.Ceiling((double)os.Count / 100); i++)
            {
                var bloque = os.Skip(i * 100).Take(100).ToList();
                string ordenesConsulta = string.Join(",", bloque.Select(x => x.IdOrden));
                var listaProveedor = ObtenerOrdenesProveedor(ordenesConsulta);

                ProcesarOrdenes(bloque, listaProveedor);
            }
            //);

            Console.WriteLine("Escribiendo archivo...");
            var sb = new StringBuilder();
            //Cabecero
            sb.Append("idOden,Estatus,EstatusExterno,Usuario,UsuarioExterno,FechaEnvio,FechaRecepcion,FechaExterno,Accion");
            sb.Append(Environment.NewLine);
            foreach (OrdenSincronizada o in os)
            {
                sb.Append("\"");
                sb.Append(o.IdOrden);
                sb.Append("\",\"");
                sb.Append(o.Estatus);
                sb.Append("\",\"");
                sb.Append(o.EstatusExterno);
                sb.Append("\",\"");
                sb.Append(o.Usuario);
                sb.Append("\",\"");
                sb.Append(o.UsuarioExterno);
                sb.Append("\",\"");
                sb.Append(o.FechaEnvio);
                sb.Append("\",\"");
                sb.Append(o.FechaRecepcion);
                sb.Append("\",\"");
                sb.Append(o.FechaExterno);
                sb.Append("\",\"");
                sb.Append(o.Accion);
                sb.Append("\"");
                sb.Append(Environment.NewLine);
            }
            if (File.Exists("ReporteSincronizacion.csv")) File.Delete("ReporteSincronizacion.csv");
            File.WriteAllText("ReporteSincronizacion.csv", sb.ToString());

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


        private static void ProcesarOrdenes(List<OrdenSincronizada> bloque, List<OrderHistory> listaProveedor)
        {
            if (bloque != null)
            {

                foreach (OrdenSincronizada os in bloque)
                {
                    OrdenSincronizada os1 = os;
                    var lista = (from x in listaProveedor
                        where x.ExternalId == os1.IdOrden
                              && x.OrderStatus == "Recibida"
                        select new
                        {
                            idProveedor = x.FormiikId,
                            fecha =
                                DateTime.ParseExact(x.OrderStatusDate, "M/d/yyyy h:m:ss tt",
                                    CultureInfo.InvariantCulture)
                        }).ToList()
                        ;

                    if (lista.Any())
                    {
                        var encontrado = lista.OrderByDescending(z => z.fecha).FirstOrDefault();

                        if (encontrado != null)
                        {
                            var historia = (from x in listaProveedor
                                where x.FormiikId == encontrado.idProveedor
                                select new
                                {
                                    registro = x,
                                    fecha =
                                        DateTime.ParseExact(x.OrderStatusDate, "M/d/yyyy h:m:ss tt",
                                            CultureInfo.InvariantCulture)
                                }).ToList();


                            if (historia.Any())
                            {
                                var ultimoEstatus = historia.OrderByDescending(z => z.fecha).ThenBy(n => n.registro.OrderStatus).FirstOrDefault();

                                if (ultimoEstatus != null)
                                {
                                    Console.WriteLine("id:{0} - fecha:{1} - Est:{2}", ultimoEstatus.registro.ExternalId,
                                        ultimoEstatus.registro.OrderStatusDate,
                                        ultimoEstatus.registro.OrderStatus);

                                    os.UsuarioExterno = ultimoEstatus.registro.User;
                                    os.FechaExterno = ultimoEstatus.fecha;
                                    os.EstatusExterno = ultimoEstatus.registro.OrderStatus;
                                    os.FormularioExterno = ultimoEstatus.registro.FormatType;

                                    if (os.UsuarioExterno != os.Usuario )
                                    {
                                        if (os.Usuario != "No asignado" && os.Estatus == 1)
                                            os.Accion += "Differente Usuario,";
                                    }
                                    switch (os.Estatus)
                                    {
                                        case 1:
                                            if (os.Usuario == "No asignado")
                                            {
                                                if (os.EstatusExterno == "Cancelada"
                                                    || os.EstatusExterno == "Enviada al cliente")
                                                {
                                                    os.Accion += "Estatus OK,";
                                                }
                                                else
                                                {
                                                    os.Accion += "Revisar,";
                                                }
                                            }
                                            else
                                            {
                                                if (os.EstatusExterno == "A disposición" ||
                                                    os.EstatusExterno == "En dispositivo" ||
                                                    os.EstatusExterno == "Recibida")
                                                {
                                                    os.Accion += "Estatus OK,";
                                                }
                                                else
                                                {
                                                    if (os.EstatusExterno == "Respuesta")
                                                        os.Accion += "Respuesta no enviada,";
                                                    else
                                                        os.Accion += "Diferente Estatus,";
                                                }
                                            }
                                            break;
                                        case 3:
                                        case 4:

                                            if (os.EstatusExterno == "Enviada al cliente")
                                            {
                                                os.Accion += "Estatus OK,";
                                            }
                                            else
                                            {
                                                if (os.EstatusExterno == "Cancelada completa")
                                                {
                                                    // Si esta cancelada es porque puede ser una captura web
                                                    //Todo: Consulta a Captura Web
                                                    os.Accion += "Revisar no se tendria que hacer nada,";
                                                }
                                                else
                                                {
                                                    if (os.EstatusExterno == "Respuesta completa")
                                                    {
                                                        os.Accion += "Revisar nuevamente en unos minutos,";
                                                    }
                                                    else
                                                        os.Accion += "Revisar caso,";
                                                }
                                            }
                                            break;
                                        default:
                                            os.Accion += "Estatus no encontrado,";
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error...");
                                }
                            }
                        }
                        else
                        {
                            os.Accion = os.Estatus != 1
                                ? "No se encontro en el proveedor, Revisar, No se necesita reasignar"
                                : "No se encontro en el proveedor, reasignar";
                        }
                    }
                }
            }
        }

        private static
            List<OrderHistory> ObtenerOrdenesProveedor(string idOrden)
        {
            var ordenesStatus = new List<OrderHistory>();
            var watch = Stopwatch.StartNew();
            try
            {
                using (var ms = new MemoryStream())
                {
                    var ser = new DataContractJsonSerializer(typeof(List<OrderHistory>));
                    var sw = new StreamWriter(ms);

                    var backEndClient = new BackEndClient("BasicHttpBinding_IBackEnd");

                    //Produccion
                    //string resultado = backEndClient.GetWorkOrdersStateByExternalIds(
                    //    "DBD4DCBD-27F8-42D6-91CF-0902F5B96421",
                    //    idOrden);
                    //Test:
                    //string resultado = backEndClient.GetWorkOrderHistoryByExternalIds(
                    //    "114269FA-61E0-4A3D-87A0-3C8107F3F61C",
                    //    idOrden);
                    //Produccion
                    string resultado = backEndClient.GetWorkOrderHistoryByExternalIds(
                        "DBD4DCBD-27F8-42D6-91CF-0902F5B96421",
                        idOrden);

                    sw.Write(resultado);
                    sw.Flush();
                    ms.Position = 0;

                    watch.Stop();

                    ordenesStatus = (List<OrderHistory>)ser.ReadObject(ms);

                    var conexion = new ConexionSql();
                    foreach (OrderHistory oh in ordenesStatus)
                    {
                        conexion.GuardarHistoria(oh);
                    }
                }

                Console.WriteLine("Tiempo de ejecución ws: " + watch.ElapsedMilliseconds + " milisegundos");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service Error:" + ex.Message);

            }
            return ordenesStatus;
        }
    }
}
