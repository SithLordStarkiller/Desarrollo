using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using wsBroxel.App_Code.SolicitudBL.Model;
using wsBroxel.App_Code.SolicitudBL.Operaciones;

namespace wsBroxel.App_Code.SolicitudBL
{
    public class MainSolicitudBL
    {
        private MySqlDataAccess _mySql;

        public MainSolicitudBL()
        {
            _mySql = new MySqlDataAccess();    
        }
        public DispResponse Execute(OperArguments a, int idOperation, string msgDefault, bool sync=true)
        {
            DispResponse res = null;
            try
            {
                a.Folio = InsertSolicitud(a.IdUser, a.NumeroTarjeta, (double) a.Monto, a.Token, a.IpFrom, a.IdTransacFrom, a.NumeroCuenta, a.Referencia);
                if (a.Folio != 0)
                {
                    // Codigo para QA
                    //return new RetornaDummy().Execute(a, _mySql);
                    if(sync)
                        EjecutaOperaciones(a, idOperation, msgDefault);
                    else
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            EjecutaOperaciones(a, idOperation, msgDefault);
                        });
                        return new DispResponse { msg = msgDefault, numTransac = a.Folio };
                    }
                }
                else throw new Exception("Imposible generar folio para esta solicitud: ");
            }
            catch (Exception e)
            {
                if (a.Folio != 0)
                {
                    InsertDispersionErr(a.Folio,e.Message);   
                }
                res = new DispResponse { msg = msgDefault, numTransac = 0 };   
            }
            return res;
        }

        public DispResponse EjecutaOperaciones(OperArguments a, int idOperation, string msgDefault)
        {
            DispResponse res = null;

            var operaciones = GetOperacionesSolicitud(idOperation);
            if (operaciones != null && operaciones.Count > 0)
            {
                foreach (var operacion in operaciones)
                {
                    var oper = (IOperacion)ImplementClass(operacion.Implementadora);
                    if (oper != null)
                    {
                        res = oper.Execute(a, _mySql);
                        if (res != null)
                            return res;
                    }
                    else throw new Exception("La clase " + operacion.Implementadora + " no pudo ser instanciada.");
                }
            }
            else throw new Exception("No existen operaciones definidas para el tipo: " + idOperation.ToString(CultureInfo.InvariantCulture));
            return res;
        }

        public CargoWSResponse ExecuteCargo(CargoArguments c)
        {
            //Validar los datos de tarjeta y obtener cuenta y fechavigencia
            return null;
        }
        private long InsertSolicitud(string idUser, string lastDigits, double amount, string token, string ipFrom, int idTransacFrom, string numCuenta, string referencia = null)
        {
            long folio = 0;
            var data = _mySql.GetFolio(idTransacFrom, numCuenta,idUser);
            if (data != null)
                folio = data.Folio;
            return folio==0 ? _mySql.InsertDispersionWs(idUser, lastDigits, amount, token, ipFrom, idTransacFrom, numCuenta,referencia) : folio;
        }
        private List<OperDb> GetOperacionesSolicitud(int id)
        {
            return _mySql.GetOperacionesSolicitud(id);
        }
        private void InsertDispersionErr(long folio, string desc)
        {
            _mySql.InsertDispersionErr(folio, desc);
        }
        private Object ImplementClass(string className)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var type = assembly.GetTypes().First(t => t.Name == className);
                return Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}