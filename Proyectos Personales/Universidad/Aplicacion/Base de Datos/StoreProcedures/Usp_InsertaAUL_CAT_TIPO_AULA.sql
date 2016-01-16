-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 29/12/2015
-- Description:	se encargara de insertar un registro a AUL_CAT_TIPO_AULA
-- =============================================
CREATE PROCEDURE Usp_InsertaAUL_CAT_TIPO_AULA 
	@IdTipoAula integer, 
	@TipoAula varchar(50),
	@Descripcion varchar(50)
AS
BEGIN
	 INSERT INTO AUL_CAT_TIPO_AULA (IDTIPOAULA, TIPOAULA,DESCRIPCION) VALUES (@IdTipoAula,@TipoAula,@Descripcion);
END


