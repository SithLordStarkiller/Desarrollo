
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/01/28
-- Description:	Se regresa el credito al dominio al que pertenecia anteriormente si no porcede la reasignacion por que la gestion es un convenio
-- =============================================
CREATE PROCEDURE CreditosAMover
	@Creditos nvarchar (2000)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @CreditosTemp nvarchar(3000) ='''';
	DECLARE @sql nvarchar(4000)

	SET @CreditosTemp+= REPLACE(@Creditos,',',''',''')
	
	SET @CreditosTemp+='''';
	
    
	SET @sql='UPDATE c'
	SET @sql+=' SET c.TX_NOMBRE_DESPACHO =d.nom_corto'
	SET @sql+=' FROM Ordenes o WITH (HOLDLOCK, ROWLOCK)'
	SET @sql+=' LEFT JOIN Dominio d WITH(NOLOCK) ON d.idDominio=o.idDominio'
	SET @sql+=' LEFT JOIN Creditos c WITH (HOLDLOCK, ROWLOCK) ON c.CV_CREDITO=o.num_Cred'
	SET @sql+=' LEFT JOIN Respuestas r WITH(NOLOCK) ON r.idOrden=o.idOrden LEFT JOIN  CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo=r.idCampo'
	SET @sql+=' WHERE d.nom_corto<>c.TX_NOMBRE_DESPACHO AND (cr.Nombre LIKE ''Dictamen%'' OR cr.Nombre IS NULL)'
	SET @sql+=' AND cr.Nombre IN (''DictamenSiAceptaSTM'',''DictamenBCN'',''DictamenFPP'',''DictamenDCP'',''DictamenFPPMen'',''DictamenSTM'',''DictamenFPPTOM'')'
	SET @sql+=' AND CV_CREDITO IN('
	SET @sql+=@CreditosTemp
	SET @sql+=')'


	EXECUTE sp_executesql @sql

END
GO