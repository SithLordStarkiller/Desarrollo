using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace wsBroxel.App_Code
{
    [Serializable]
    public class ComisionTransferencia
    {
        private Entities _entities;
        private productos_broxel ProductoEnvio { get; set; }
        private DetalleClientesBroxel DetalleClientesBroxelEnvio { get; set; }
        private String NumCuentaEnvio { get; set; }
        private productos_broxel ProductoRecepcion { get; set; }
        private DetalleClientesBroxel DetalleClientesBroxelRecepcion { get; set; }
        private String NumCuentaRecepcion { get; set; }
        private Decimal MontoTransferencia { get; set; }
        private String Tipo { get; set; }

        public Int32 TipoComision
        {
            get
            {
                //if (DetalleClientesBroxelEnvio.CuentaConcentradora == NumCuentaEnvio || DetalleClientesBroxelRecepcion.CuentaConcentradora == NumCuentaRecepcion)
                //JAVG //20150917 //Cambio solicitado por AGM: Se agrega el tipo OneToOne
                if (Tipo == "ConcentradoraACuentas".ToUpper() || Tipo == "OneToOne".ToUpper() || Tipo == "ACuentaConcentradora".ToUpper())
                    return 5;
                return DetalleClientesBroxelEnvio.TipoComisionTransferencia;
            }
        }

        public Int32 TipoConceptoComision
        {
            get
            {
             //   if (DetalleClientesBroxelEnvio.CuentaConcentradora == NumCuentaEnvio || DetalleClientesBroxelRecepcion.CuentaConcentradora == NumCuentaRecepcion)
                //JAVG //20150917 //Cambio solicitado por AGM: Se agrega el tipo OneToOne
                if (Tipo == "ConcentradoraACuentas".ToUpper() || Tipo == "OneToOne".ToUpper() || Tipo == "ACuentaConcentradora".ToUpper())
                    return 5;
                switch (DetalleClientesBroxelEnvio.TipoComisionTransferencia)
                {
                    case 1:
                    case 2:
                        return DetalleClientesBroxelEnvio.TipoConceptoComisionTransferencia;
                    case 3:
                    case 4:
                        return ProductoEnvio.TipoConceptoComisionTransferencia;
                    default:
                        return 0;
                }
            }
        }

        public Decimal MontoComision
        {
            get
            {
                //if (DetalleClientesBroxelEnvio.CuentaConcentradora == NumCuentaEnvio || DetalleClientesBroxelRecepcion.CuentaConcentradora == NumCuentaRecepcion)
                //JAVG //20150917 //Cambio solicitado por AGM: Se agrega el tipo OneToOne
                if (Tipo == "ConcentradoraACuentas".ToUpper() || Tipo == "OneToOne".ToUpper() || Tipo == "ACuentaConcentradora".ToUpper())
                    return 0;
                switch (DetalleClientesBroxelEnvio.TipoComisionTransferencia)
                {
                    case 1:
                        return MontoTransferencia * Convert.ToDecimal(DetalleClientesBroxelEnvio.ComisionTransferencia) + (.16m * MontoTransferencia * Convert.ToDecimal(DetalleClientesBroxelEnvio.ComisionTransferencia));
                    case 2:
                        return Convert.ToDecimal(DetalleClientesBroxelEnvio.ComisionTransferencia) + (.16m * Convert.ToDecimal(DetalleClientesBroxelEnvio.ComisionTransferencia));
                    case 3:
                        return MontoTransferencia * Convert.ToDecimal(ProductoEnvio.ComisionTransferencia) + (.16m * MontoTransferencia * Convert.ToDecimal(ProductoEnvio.ComisionTransferencia));
                    case 4:
                        return Convert.ToDecimal(ProductoEnvio.ComisionTransferencia) + (.16m * Convert.ToDecimal(ProductoEnvio.ComisionTransferencia));
                    default:
                        return 0;
                }
            }
        }

        public Boolean TransferenciaEnvioValida
        {
            get
            {
                try
                {
                    //if (DetalleClientesBroxelEnvio.CuentaConcentradora == NumCuentaEnvio || DetalleClientesBroxelRecepcion.CuentaConcentradora == NumCuentaRecepcion)
                    //JAVG //20150917 //Cambio solicitado por AGM: Se agrega tipo OneToOne
                    if (Tipo == "ConcentradoraACuentas".ToUpper() || Tipo == "OneToOne".ToUpper() || Tipo == "ACuentaConcentradora".ToUpper())
                        return true;

                    switch (DetalleClientesBroxelEnvio.TipoComisionTransferencia)
                    {
                        case 0:
                            return false;
                        case 1:
                        case 2:
                            return true;
                        case 3:
                        case 4:
                            return ProductoEnvio.TipoComisionTransferencia != 0;
                        default:
                            return false;
                    }

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Boolean TransferenciaRecepcionValida
        {
            get
            {
                //if (DetalleClientesBroxelEnvio.CuentaConcentradora == NumCuentaEnvio || DetalleClientesBroxelRecepcion.CuentaConcentradora == NumCuentaRecepcion)
                //JAVG //20150917 //Cambio solicitado por AGM: Se agrega tipo OneToOne
                if (Tipo == "ConcentradoraACuentas".ToUpper() || Tipo == "OneToOne".ToUpper() || Tipo == "ACuentaConcentradora".ToUpper())
                        return true;
                if (DetalleClientesBroxelRecepcion.RecibeTransferencia == 0 && ProductoRecepcion.RecibeTransferencia == 0)
                    return false;
                if (DetalleClientesBroxelRecepcion.RecibeTransferencia == 1 || (DetalleClientesBroxelRecepcion.RecibeTransferencia == 2 &&
                    ProductoRecepcion.RecibeTransferencia == 1))
                    return true;
                return false;
            }
        }

        public ComisionTransferencia(){}

        public ComisionTransferencia(string numCuentaEnvio, decimal montoTransferencia, string numCuentaRecepcion,string tipo)
        {
            NumCuentaEnvio = numCuentaEnvio;
            NumCuentaRecepcion = numCuentaRecepcion;
            MontoTransferencia = montoTransferencia;
            Tipo = tipo;
        }

        public void InitializeComponents()
        {
            _entities = Entities.Instance;
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var m = broxelcoRdgEntities.maquila.SingleOrDefault(x => x.num_cuenta == NumCuentaEnvio);
            Debug.Assert(m != null, " No hay maquila para NumCuenta" + NumCuentaEnvio + " - Envio");
            ProductoEnvio = _entities.ProductosBroxel.SingleOrDefault(x => x.codigo == m.producto);
            DetalleClientesBroxelEnvio = _entities.DetalleClientesBroxel.SingleOrDefault(x => x.Producto == m.producto && x.ClaveCliente == m.clave_cliente);

            var m2 = broxelcoRdgEntities.maquila.SingleOrDefault(x => x.num_cuenta == NumCuentaRecepcion);
            Debug.Assert(m2 != null, " No hay maquila para NumCuenta" + NumCuentaRecepcion + " - Recepcion");
            ProductoRecepcion = _entities.ProductosBroxel.SingleOrDefault(x => x.codigo == m2.producto);
            DetalleClientesBroxelRecepcion = _entities.DetalleClientesBroxel.SingleOrDefault(x => x.Producto == m2.producto && x.ClaveCliente == m2.clave_cliente);
        }
    }

    public sealed class Entities
    {
        private static Entities _instance;
        private static DateTime _expiry = DateTime.MinValue;
        private static readonly ObjectIDGenerator Obj = new ObjectIDGenerator();
        private static readonly object Lock = new object();
        public String GetObjectCode { get { return Obj.GetHashCode().ToString(CultureInfo.InvariantCulture); } }

        public readonly List<productos_broxel> ProductosBroxel;
        public readonly List<DetalleClientesBroxel> DetalleClientesBroxel;
        public readonly List<bancos_stp> BancosStp;
        public readonly List<Monedas> Monedas;
        public readonly List<CodigosTransaccionWebService> CodsTranWeb;
        public readonly List<Comercio11> Comercios;

        private Entities()
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            ProductosBroxel = broxelcoRdgEntities.productos_broxel.ToList();
            DetalleClientesBroxel = broxelcoRdgEntities.DetalleClientesBroxel.ToList();
            BancosStp = broxelcoRdgEntities.bancos_stp.ToList();
            Monedas = broxelcoRdgEntities.Monedas.ToList();
            CodsTranWeb = broxelcoRdgEntities.CodigosTransaccionWebService.ToList();
            Comercios = broxelcoRdgEntities.Comercio.ToList();
        }

        public void RefreshData()
        {
            lock (Lock) { _expiry = DateTime.Now.AddHours(-1); }
        }

        public static bool HasExpired
        {
            get
            {
                lock (Lock) { return (_expiry < DateTime.Now); }
            }
        }

        public static Entities Instance
        {
            get
            {
                lock (Lock)
                {
                    if (!HasExpired)
                        return _instance;
                    // dispose and reconstrusct _intance
                    _instance = null;
                    _instance = new Entities();
                    var t = ConfigurationManager.AppSettings["ChacheMinutes"];
                    var minutes = Regex.IsMatch(t, @"^\d+$") ? Convert.ToInt32(t) : 5;
                    _expiry = DateTime.Now.AddMinutes(minutes);
                    return _instance;
                }
            }
        }

    }
}