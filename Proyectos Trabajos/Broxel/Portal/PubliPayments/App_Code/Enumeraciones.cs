/****************************************************************************************************************
* Desarrrollador: Israel Salinas Contreras
* Proyecto:	London-PubliPayments-Formiik
* Fecha de Creación: 29/04/2014
* Descripción de Creacion: Clase Generica para controlar enumeradores
* Ultima Fecha de Modificaciòn: 29/04/2014
* Descripciòn de ultima modificacion: ----
****************************************************************************************************************/

namespace PubliPayments.App_Code
{
    public class Enumeraciones
    {
    }

    public enum filtroDashBoard {
        Sin_Asignacion = 0,
        Administrador = 1,
        Despacho = 2,
        Supervisor = 3,
        Gestor = 4,
        Delegacion = 5,
        Direccion = 6
    }

    public enum tipoGrafica
    {
        SinAsignacion = 0,
        StatusGestionDomiciliaria = 1,
        AsignacionPorEstado = 2,
        AsignacionDeCreditoXMes = 3,
        GestionMensual = 4,
        RankingDespXMes = 5,
        RankingDespX___ = 6,
        AsigCarteraMensualMesCorri = 7,
        EjecGestionDomicilio = 8,
        PagosGlobalMesCorriente = 9,
        SituacionNoPagados = 10,
        SolucionesXDiaMesCorriente = 11,
        MapaPorEstado = 12
    }

}