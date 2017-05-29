using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace ComCredencial
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
        public readonly List<cat_incremento> CatIncrementos;

        private Entities()
        {
            var broxelcoRdgEntities = new broxelco_rdgEntities();
            ProductosBroxel = broxelcoRdgEntities.productos_broxel.ToList();
            DetalleClientesBroxel = broxelcoRdgEntities.DetalleClientesBroxel.ToList();
            BancosStp = broxelcoRdgEntities.bancos_stp.ToList();
            Monedas = broxelcoRdgEntities.Monedas.ToList();
            CatIncrementos = broxelcoRdgEntities.cat_incremento.ToList();
        }
    }
}
