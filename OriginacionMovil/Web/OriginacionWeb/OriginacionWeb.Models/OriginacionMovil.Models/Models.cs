namespace OriginacionMovil.Models
{
    public class CatEmpresa
    {
        public int Id { get; set; }
	    public string Nombre { get; set; }
	    public string IdExterno { get; set; }
	    public string IdPadre { get; set; }
    }

    public class CatLugar
    {
        public int Id { get; set; }
	    public string Nombre { get; set; }
	    public string IdExterno { get; set; }
	    public string IdEmpresa { get; set; }
    }

    public class CatEstados
    {
        public int Id { get; set; }
        public string EstadoDescripcion { get; set; }
        public string IdEstado { get; set; }
    }

    public class Respose
    {
        public int Codigo { get; set; }

        public string Mensaje { get; set; }
    }

    public class RequestLoginModel
    {
        public string Usuario { get; set; }

        public string Password { get; set; }
    }

    public class RequestPrecalificacionModel
    {
        public string OficinaCesi { get; set; }
        public string Nss { get; set; }
        public string Usuario { get; set; }
    }

    public class RequestRegistraCuenta
    {
        public string Nss { get; set; }
        public string Tc { get; set; }
        public string Usuario { get; set; }
    }

    public class RequestValidaCreditoModel
    {
        public string Nss { get; set; }
        public string Usuario { get; set; }
    }

    public class RequestRegistroCreditoModel
    {
        public string EstadoCesi { get; set; }
        public string OficinaCesi { get; set; }
        public string Nss { get; set; }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string Curp { get; set; }
        public string Plazo { get; set; }
        public string Nombres { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string GeneroCb { get; set; }
        public string FechaNacimiento { get; set; }
        public string CorreoElectronico { get; set; }
        public string CorreoElectronicoCorrabora { get; set; }
        public string LadaCelular { get; set; }
        public string Telefono1Cte { get; set; }
        public string Lada { get; set; }
        public string Telefono2Cte { get; set; }
        public string CentObre { get; set; }
        public string LadaEmpresa { get; set; }
        public string TelEmpresa { get; set; }
        public string ExtEmpresa { get; set; }
        public string Estado { get; set; }
        public string Delegacion { get; set; }
        public string DelegacionId { get; set; }//
        public string Cp { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumeroExt { get; set; }
        public string NumeroInt { get; set; }
        public string ManzanaCb { get; set; }
        public string LoteCb { get; set; }
        public string EntreCalle1Cb { get; set; }
        public string EntreCalle2Cb { get; set; }
        public string EdoCivil { get; set; }
        public string RegimenCony { get; set; }
        public string NombreRef1 { get; set; }
        public string APaternoRef1 { get; set; }
        public string AMaternoRef1 { get; set; }
        public string TipoTelR1 { get; set; }
        public string LadaR1 { get; set; }
        public string Telefono1Ref1 { get; set; }
        public string NombreRef2 { get; set; }
        public string APaternoRef2 { get; set; }
        public string AMaternoRef2 { get; set; }
        public string TipoTelR2 { get; set; }
        public string ValidacionTel2 { get; set; }
        public string LadaR2 { get; set; }
        public string Telefono1Ref2 { get; set; }
        public string NombreBeneficiario { get; set; }
        public string APaternoBen { get; set; }
        public string AMaternoBen { get; set; }
        public string DomNomDh { get; set; }
        public string Cuenta { get; set; }
        public string ListaDocumentos { get; set; }
        public string NumIdentificacionIne { get; set; }
        public string NumIdentigicacionPas { get; set; }
        public string FechaVigenciaIdentificacion { get; set; }
        public string LocalidadCb { get; set; }
        public string Usuario { get; set; }
    }
}
