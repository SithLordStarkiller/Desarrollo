
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes - Alberto Rojas
-- Create date: 2016/01/28
-- DescriptiON:	Obtiene creditos  que son necesarios mover tras realizar una reasignacion que no es permitida
-- =============================================
create PROCEDURE ObtenerCreditosAMover
AS
BEGIN
	SET NOCOUNT ON;

	SELECT r.valor,o.estatus,CAST(o.idOrden AS VARCHAR(12)),num_Cred,d.nom_corto,c.TX_NOMBRE_DESPACHO FROM Ordenes o WITH(NOLOCK) LEFT JOIN Dominio d WITH(NOLOCK) ON d.idDominio=o.idDominio LEFT JOIN Creditos c WITH(NOLOCK) ON c.CV_CREDITO=o.num_Cred
		LEFT JOIN Respuestas r WITH(NOLOCK) ON r.idOrden=o.idOrden LEFT JOIN  CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo=r.idCampo
		WHERE d.nom_corto<>c.TX_NOMBRE_DESPACHO AND (cr.Nombre like 'Dictamen%' or cr.Nombre IS NULL)
		AND cr.Nombre IN ('DictamenSiAceptaSTM','DictamenBCN','DictamenFPP','DictamenDCP','DictamenFPPMen','DictamenSTM','DictamenFPPTOM')
		order by  Valor,Estatus
END
GO
