-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 10/12/2015
-- Description:	Se encargara de modificar registro de la tabla AUL_CAT_TIPO_AULA
-- =============================================
CREATE PROCEDURE Usp_ActualizaRegistroAUL_CAT_TIPO_AULA 
	@IdTipoAula int, 
	@TipoAula varchar,
	@Descripcion varchar
AS
BEGIN
	UPDATE AUL_CAT_TIPO_AULA SET TIPOAULA = @TipoAula, DESCRIPCION = @Descripcion WHERE IDTIPOAULA = IDTIPOAULA
END
GO
