using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.TCControlVL.Model
{
    /// <summary>
    /// Catalogo de estados de tarjetas en CREDE
    /// </summary>
    public class EstadosTarjetas
    {
        /// <summary>
        /// Las Tarjetas se originan con éste Estado, sean Personalizadas o de Stock
        /// Puede transicionar a 01, 21, 23
        /// </summary>
        public const string Inactiva = "2N";
        
        /// <summary>
        /// Las Tarjetas pasarán a este Estado, cuando Broxel las habilite, o las que operen regularmente
        /// Puede transicionar a 21, 23, 28, 29, 2F, 2T
        /// </summary>
        public const string Operativa = "01";
        
        /// <summary>
        /// Al cancelar la cuenta o bien por archivo PC puede cancelar una tarjeta adicional
        /// Estado final 
        /// </summary>
        public const string BolCancelada = "03";
        
        /// <summary>
        ///  Se boletina ante la denuncia por robo del Tarjetahabiente 
        /// Estado Final
        /// </summary>
        public const string BolRobo = "21";
        
        /// <summary>
        /// Se boletina ante la denuncia por extravío del Tarjetahabiente
        /// Estado Final
        /// </summary>
        public const string BolExtravio = "23";

        /// <summary>
        /// Se boletina ante la denuncia porque la tarjeta está dañada y el Cliente  no la puede utilizar
        /// Puede transicionar a 01, 21, 23, 29, 2F
        /// </summary>
        public const string BolOtros = "28";

        /// <summary>
        /// Broxel (Prev. Fraudes) puede boletinar Tarjetas por este motivo.
        /// Puede transicionar a 01, 21, 23,28,29,2F
        /// </summary>
        public const string BolBloqueoTemporal = "2T";

        /// <summary>
        /// Este estado lo ingresa el SPC cuando repone la tarjeta (es la tarjeta repuesta)
        /// Puede trancisionar a 01, 21, 23, 28, 29, 2F, 2T
        /// </summary>
        public const string BolEnRep = "2R";

        /// <summary>
        /// Se boletina automáticamente ante el intento de 3 Pines erróneos
        /// Puede trancisionar a 01, 21, 23
        /// </summary>
        public const string Bol3PinErr = "2P";

    }
}