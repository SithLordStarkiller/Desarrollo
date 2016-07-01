CREATE PROCEDURE Usp_ETLLoadTableCatalogsProduct 
AS
BEGIN
---------------------------------------------Inserta Registros ProFamilias-------------------------------------------------------------------

	INSERT INTO ProFamilias (IdFamilia, NombreFamilia, Descripcion, Creador,FechaCreacion,Borrado) 
	SELECT  
	ROW_NUMBER() OVER(order by o.FA_NOMBRE) AS IdTable,
	FA_NOMBRE,
	TITULO,
	'ETL',
	GETDATE(),
	0
	FROM SSISTABLEFAMILIAS AS o
	GROUP BY
	FA_NOMBRE,TITULO

------------------------------------------------Inserta Registros MARCAS --------------------------------------------------------------------
	
	INSERT INTO ProCatMarca (IdMarca, NombreMarca, Descripcion, Creador, FechaCreacion,Borrado) 
	SELECT
	ROW_NUMBER() OVER(order by o.MA_NOMBRE) AS IdTable
	, MA_NOMBRE
	, TITULO
	, 'ETL'
	, GETDATE()
	, 0
	FROM SSISTABLEMARCAS AS o
	Group By 
	MA_NOMBRE,TITULO

END
GO
