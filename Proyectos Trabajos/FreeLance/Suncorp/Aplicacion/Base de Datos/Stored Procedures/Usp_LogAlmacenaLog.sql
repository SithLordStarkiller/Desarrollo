-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 09/05/2017
-- Description:	Se encargara de almacenar un Log
-- =============================================
CREATE PROCEDURE [dbo].[Usp_LogAlmacenaLog] 
	@tipoLog int,
    @proyecto VarChar(MAX),
    @clase VarChar(MAX),
    @metodo VarChar(MAX),
    @mensage varchar(MAX),
    @log VarChar(MAX),
    @excepcion varchar(MAX),
    @auxiliar VarChar(MAX)
AS
BEGIN

	INSERT INTO LOGLOGGER (IdTipoLog, Proyecto, Clase, Metodo,Mensage,Log,Excepcion,Auxiliar,FechaCreacion)  
	VALUES (@tipoLog , @proyecto, @clase,@metodo,@mensage,@log ,@Excepcion,@auxiliar, GETDATE());

	SELECT @@IDENTITY
END
