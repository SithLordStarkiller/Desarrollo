SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/02/03
-- Description:	Actualiza el valor de un registro del Catalogo General
-- =============================================
CREATE PROCEDURE InsUpdCatalogoGeneral (@Llave varchar(500),@Valor varchar(500),@Descripcion varchar(2000)='')
	
AS
BEGIN
	SET NOCOUNT ON;

	   IF((SELECT 1 FROM catalogoGeneral WITH(NOLOCK)  WHERE llave=@Llave)=1)
	BEGIN
		UPDATE catalogoGeneral SET valor=@Valor,Descripcion=CASE WHEN @Descripcion!='' THEN @Descripcion ELSE Descripcion END  WHERE llave=@Llave
		SELECT id FROM catalogoGeneral WHERE llave=@Llave
	END
	ELSE
	BEGIN
		INSERT INTO catalogoGeneral (Llave,Valor,Descripcion) VALUES (@Llave,@Valor,@Descripcion)
		SELECT SCOPE_IDENTITY() AS id
	END


END
GO
