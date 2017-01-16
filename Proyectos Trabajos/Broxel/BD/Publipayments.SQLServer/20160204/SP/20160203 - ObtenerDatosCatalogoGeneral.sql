
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/02/03
-- Description:	Obtiene los datos que contiene un registro de Catalogo General
-- =============================================
CREATE PROCEDURE ObtenerDatosCatalogoGeneral (@id int=0, @Llave varchar(500)='')

AS
BEGIN
	SET NOCOUNT ON;
	IF(@id>0)
		SELECT id,Valor,llave,Descripcion  FROM catalogoGeneral WITH(NOLOCK) WHERE id=@id
   ELSE IF(@Llave!='')
		SELECT id,Valor,llave,Descripcion  FROM catalogoGeneral WITH(NOLOCK) WHERE llave=@Llave
END
GO





