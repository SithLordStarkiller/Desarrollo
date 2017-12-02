using System;

namespace Mcs.Entities.Sicer
{
    /// <summary>
    /// Clase entidad que representa una persona física.
    /// </summary>
    public class BePersonaFisica
    {
        /// <summary>
        /// Valor del Estandar de competencia de Conec II.
        /// </summary>
        public int ClaveEtandardComp { get; set; }
        /// <summary>
        /// Foto de la persona a certificarse.
        /// </summary>
        public Archivo Foto { get; set; }
        /// <summary>
        /// Id de la foto.
        /// </summary>
        public int FotoId { get; set; }
        /// <summary>
        /// Fecha de registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// Nombre de la persona física.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Apellido paterno de la persona física.
        /// </summary>
        public string ApPaterno { get; set; }
        /// <summary>
        /// Apellido materno de la persona física.
        /// </summary>
        public string ApMaterno { get; set; }
        /// <summary>
        /// Lugar de nacimiento de la persona física..
        /// </summary>
        public string LugarNacimiento { get; set; }
        /// <summary>
        /// Fecha de nacimiento de la persona física.
        /// </summary>
        public DateTime FechaNacimiento { get; set; }
        /// <summary>
        /// Identificador de la nacionalidad (clave del país) del la persona física.
        /// </summary>
        public int Nacionalidad { get; set; }
        /// <summary>
        /// CURP de la persona física.
        /// </summary>
        public string Curp { get; set; }
        /// <summary>
        /// Úiltimo grado de estudios de la persona física.
        /// </summary>
        public string UltimoGradoEstudio { get; set; }
        /// <summary>
        /// Clave de la entidad federativa.
        /// </summary>
        public int IdEntidadFederativa { get; set; }
        /// <summary>
        /// Clave del municipio.
        /// </summary>
        public int IdMunicipio { get; set; }
        /// <summary>
        /// Colonia donde vive la persona física.
        /// </summary>
        public string Colonia { get; set; }
        /// <summary>
        /// Calle donde vive la persona física.
        /// </summary>
        public string Calle { get; set; }
        /// <summary>
        /// Número exterior de su casa.
        /// </summary>
        public string NumeroExterior { get; set; }
        /// <summary>
        /// Número interior de su casa.
        /// </summary>
        public string NumeroInterior { get; set; }
        /// <summary>
        /// Código postal
        /// </summary>
        public int CodigoPostal { get; set; }
        /// <summary>
        /// Años de servicio prestados.
        /// </summary>
        public int AniosServicio { get; set; }
        /// <summary>
        /// Nombre del trabajo anterior.
        /// </summary>
        public string TrabajoAnterior { get; set; }
        /// <summary>
        /// Clave generada por el Registro Nacional de Personal de Seguridad Pública a 
        /// todas las personas que trabajen en institución o corporaciones relacionadas 
        /// con la seguridad pública.
        /// </summary>
        public string Cuip { get; set; }
        /// <summary>
        /// Edad de la persona física.
        /// </summary>
        public int Edad { get; set; }
        /// <summary>
        /// Femenino (F), Masculino (M).
        /// </summary>
        public string Sexo { get; set; }
        /// <summary>
        /// Estructura de RFC.
        ///  Primeras dos letras del apellido paterno, seguido de la primera letra del apellido materno, seguido de la primera letra del nombre, seguido del Año de nacimiento, Mes de nacimiento y Día de nacimiento, seguido de Homo clave(Asignada por el SAT).
        /// Consta de 4 letras seguida por 6 dígitos y 3 caracteres alfanuméricos.
        /// </summary>
        public string Rfc { get; set; }
        /// <summary>
        /// Ultimo grado en que estubo la persona física.
        /// </summary>
        public string Grado  { get; set; }
        /// <summary>
        /// Ultimo cargo que obtuvo la persona física.
        /// </summary>
        public string Cargo { get; set; }
        /// <summary>
        /// Teléfono de casa.
        /// </summary>
        public int TelefonoCasa { get; set; }
        /// <summary>
        /// Teléfono del celular.
        /// </summary>
        public int TelefonoMovil { get; set; }
        /// <summary>
        /// Teléfono laboral.
        /// </summary>
        public int TelefonoLaboral { get; set; }
        /// <summary>
        /// Teléfono del trabajo con extensión
        /// </summary>
        public int TelefonoLaboralExt { get; set; }
        /// <summary>
        /// Correo personal.
        /// </summary>
        public string EmailPersonal { get; set; }
        /// <summary>
        /// Correo del trabajo.
        /// </summary>
        public string EmailLaboral{ get; set; }
        /// <summary>
        /// Licencia Oficial Colectiva.
        /// </summary>
        public string Loc { get; set; }
        /// <summary>
        /// Ultimos 3 digitos del RCF
        /// </summary>
        public string Homoclave { get; set; }

        /// <summary>
        /// Id del Estado
        /// </summary>
        public int IdEstado { get; set; }
    }
}
