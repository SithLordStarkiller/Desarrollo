-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/12
-- Description:	Retorna los registros en lo que no se ha enviado mensaje SMS
-- =============================================
CREATE PROCEDURE ObtenerTelefonosEnvioSMS
AS
BEGIN
	 select a.num_Cred,a.idOrden,a.Telefono,a.Clave,rr.Valor as Dictamen from (select r.idOrden,r.idDominio,r.idUsuarioPadre,r.Valor from respuestas r WITH (NOLOCK) inner join camposrespuesta cr 
	on r.idcampo=cr.idCampo and r.idformulario=cr.idformulario where cr.nombre like 'Dictamen%') as rr inner join AutorizacionSMS a on rr.idOrden=a.idOrden where a.FechaEnvio is null

END
GO
