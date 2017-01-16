SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes - Alberto Rojas
-- Create date: 2016/01/28
-- DescriptiON:	Obtiene ordenes que son necesarios mover para realizar una reasignaciON
-- =============================================
CREATE PROCEDURE ObtenerOrdenesAMover
AS
BEGIN
	
	SET NOCOUNT ON;
	    
		-----------------------------------------ORDENES a mover ----------------------------------------------
		SELECT  r.valor,o.estatus,CAST(o.idOrden AS VARCHAR(12)) as idorden,num_Cred,d.nom_corto,c.TX_NOMBRE_DESPACHO 
		FROM Ordenes o WITH(NOLOCK) 
		LEFT JOIN Dominio d WITH(NOLOCK) ON d.idDominio=o.idDominio 
		LEFT JOIN Creditos c WITH(NOLOCK) ON c.CV_CREDITO=o.num_Cred
		LEFT JOIN Respuestas r WITH(NOLOCK) ON r.idOrden=o.idOrden 
		LEFT JOIN  CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo=r.idCampo
		WHERE d.nom_corto<>c.TX_NOMBRE_DESPACHO AND (cr.Nombre IS NULL)
		UNION ALL
		SELECT r.valor,o.estatus,CAST(o.idOrden AS VARCHAR(12)),num_Cred,d.nom_corto,c.TX_NOMBRE_DESPACHO 
		FROM Ordenes o WITH(NOLOCK) 
		LEFT JOIN Dominio d WITH(NOLOCK) ON d.idDominio=o.idDominio 
		LEFT JOIN Creditos c WITH(NOLOCK) ON c.CV_CREDITO=o.num_Cred
		LEFT JOIN Respuestas r WITH(NOLOCK) ON r.idOrden=o.idOrden 
		LEFT JOIN  CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo=r.idCampo
		WHERE d.nom_corto<>c.TX_NOMBRE_DESPACHO AND (cr.Nombre like 'Dictamen%' or cr.Nombre IS NULL)
		AND cr.Nombre NOT IN ('DictamenSiAceptaSTM','DictamenBCN','DictamenFPP','DictamenDCP','DictamenFPPMen','DictamenSTM','DictamenFPPTOM')
		
		END
GO
