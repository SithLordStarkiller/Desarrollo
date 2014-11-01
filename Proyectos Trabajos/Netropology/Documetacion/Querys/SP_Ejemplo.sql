USE [PAEEEM_DESARROLLO_05]
GO
/****** Object:  StoredProcedure [dbo].[get_Historico_LOG]    Script Date: 02/10/2014 06:22:37 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[get_Historico_LOG]
	-- Add the parameters for the stored procedure here
	@Count int output,
	@SortBy nvarchar(50),
	@PageIndex int,
	@PageSize int,
	@FECHA_INICIO NVARCHAR(10),
	@FECHA_FIN NVARCHAR(10),
	@ID_ROL INT,
	@NAMEUSER NVARCHAR(200),
	@ID_TAREA INT,
	@IDPROCESO INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @spSql nvarchar(max)
	Declare @spWhere nvarchar(1000)
	Declare @spOrder nvarchar(500)

	SET @spWhere = N' WHERE 1=1'

	BEGIN
	  IF @ID_ROL  <> 0
		SET @spWhere = @spWhere + ' AND ID_ROL = @IDROL ' 
	  IF @NAMEUSER  <> ''
		SET @spWhere = @spWhere + ' AND nombre_usuario LIKE @NAMEUSER '		
	  IF @ID_TAREA  <> 0
		SET @spWhere = @spWhere + ' AND IDTAREA = @ID_TAREA '
	  IF @IDPROCESO  <> 0
		SET @spWhere = @spWhere + ' AND IDTIPOPROCESO = @IDPROCESO '
	  IF @FECHA_INICIO <> ''
		SET @spWhere = @spWhere + ' AND FECHA BETWEEN @FECHA_INICIO AND @FECHA_FIN'
	END
	
	
	SET @spOrder=N''

	IF(LEN(@SortBy)>0)
		SET @spOrder = N' ORDER BY '+ @SortBy 
	ELSE
	SET @spOrder=N' ORDER BY idSecuencia'
	
	SET @spSql='
	WITH tempList  AS (
		SELECT	rowNum = ROW_NUMBER() OVER ('+@spOrder+N'),
				IDSECUENCIA,
				FECHA,
				Nombre_Usuario,
				ID_ROL,
				IDTIPOPROCESO,
				IDTAREA,
				Nombre_Rol,
				PROCESO,
				empresa,
				region,
				zona,
				TAREA,
				DATOMODIFICADO,
				MOTIVO,
				NOTA
		   from dbo.Datos_LOG
		'+@spWhere+'
	)
	
	SELECT  rowNum,
			IDSECUENCIA,
			FECHA,
			Nombre_Usuario,
			ID_ROL,
			IDTIPOPROCESO,
			IDTAREA,
			Nombre_Rol,
			PROCESO,
			empresa,
			region,
			zona,
			TAREA,
			DATOMODIFICADO,
			MOTIVO,
			NOTA
	   FROM templist
	WHERE rowNum BETWEEN (@PageIndex-1) *@PageSize +1 AND @PageIndex *@PageSize
   
	SELECT @Count=count(1) FROM dbo.Datos_LOG '+@spWhere

	execute sp_executesql @spSql,
	N'@Count int output, 
	@SortBy nvarchar(50),
	@PageIndex int ,
	@PageSize int,
	@FECHA_INICIO NVARCHAR(10),
	@FECHA_FIN NVARCHAR(10),
	@ID_ROL INT,
	@NAMEUSER NVARCHAR(200),
	@ID_TAREA INT,
	@IDPROCESO INT',
	@Count=@Count output,
	@SortBy =@SortBy,
	@PageIndex=@PageIndex,
	@PageSize=@PageSize,
	@FECHA_INICIO = @FECHA_INICIO,
	@FECHA_FIN = @FECHA_FIN,
	@ID_ROL =@ID_ROL,
	@NAMEUSER =@NAMEUSER,
	@ID_TAREA =@ID_TAREA,
	@IDPROCESO =@IDPROCESO;
END
