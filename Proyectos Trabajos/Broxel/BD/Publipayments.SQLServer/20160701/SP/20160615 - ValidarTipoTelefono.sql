
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/06/15
-- Description:	Valida el tipo de linea que corresponde al telefono que es mandado
-- =============================================
CREATE PROCEDURE ValidarTipoTelefono @Telefono VARCHAR (10)

AS
BEGIN
	
DECLARE @Serie VARCHAR(6),@Numeracion VARCHAR(4),@TIPO_RED VARCHAR(100),@POBLACION VARCHAR(100),@MUNICIPIO VARCHAR(100),@ESTADO VARCHAR(100)

	IF(LEN(@Telefono)!=10)
		SELECT  'INVALIDO' AS TIPO_RED,'' AS POBLACION,'' AS MUNICIPIO,'' AS ESTADO
	ELSE
		BEGIN
			SELECT @Serie =LEFT(@Telefono,6),@Numeracion= RIGHT(@Telefono,4) 

			SELECT @TIPO_RED=TIPO_RED,@POBLACION=POBLACION,@MUNICIPIO=MUNICIPIO,@ESTADO=ESTADO FROM CatTelefonos WITH(NOLOCK)
			WHERE
			NIR + SERIE= @Serie
			AND CAST(@NUMERACION  AS int)>=cast(NUMERACION_INICIAL AS int) 
			AND CAST(@NUMERACION AS int)<=cast(NUMERACION_FINAL AS int)


			IF(@TIPO_RED!='')
			SELECT  @TIPO_RED AS TIPO_RED,@POBLACION AS POBLACION,@MUNICIPIO AS MUNICIPIO,@ESTADO AS ESTADO
			ELSE
			SELECT  'INVALIDO' AS TIPO_RED,'' AS POBLACION,'' AS MUNICIPIO,'' AS ESTADO

		END

END
GO
