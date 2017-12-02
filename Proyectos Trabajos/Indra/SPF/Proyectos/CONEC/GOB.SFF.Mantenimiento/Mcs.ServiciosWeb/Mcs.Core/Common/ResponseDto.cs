using Mcs.Core.Interfaces;


namespace Mcs.Core.Common
{
    public class ResponseDto<TResponse> : IResponse<TResponse>
    {
        /// <summary>
        /// Inidica si la respuesta fue satisfactoria.
        /// </summary>
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// Total de registros devueltos o encontrados.
        /// </summary>
        public int TotalRows
        {
            get;
            set;
        }
        /// <summary>
        /// Valores devueltos de la solicitud.
        /// </summary>
        public TResponse Value
        {
            get;
            set;
        }
        /// <summary>
        /// Mensaje de error en caso de haber ocurrido un error.
        /// </summary>
        public string Message { get; set; }

    }
}
