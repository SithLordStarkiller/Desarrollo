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

-------------------------------------------------Inserta Registros Modelos -------------------------------------------------------------------

	INSERT INTO ProCatModelo (IdModelo, IdMarca, NombreModelo, Descripcion, Creador, FechaCreacion, Borrado)
	SELECT
	ROW_NUMBER() OVER(order by O.PR_MODELO) AS IdTable
	, (SELECT IdMarca FROM ProCatMarca WHERE NombreMarca = M.MA_NOMBRE) AS IdModel
	, O.PR_MODELO
	, O.TITULO
	, 'ETL'
	, GETDATE()
	, 0
	FROM SSISTABLEPRODUCTOS AS O
    INNER JOIN SSISTABLEMARCAS AS M ON O.ID_MARCAS = M.ID_MARCAS
	GROUP BY O.PR_MODELO,M.MA_NOMBRE, O.PR_NOMBRE, O.TITULO

-------------------------------------------------Inserta Registros Diviciones -------------------------------------------------------------------

	INSERT INTO ProDiviciones(IdDivicion, NombreDivicion, Descripcion, Creador, FechaCreacion, Borrado)
	SELECT
	ROW_NUMBER() OVER(order by O.DIV_NOMBRE) AS IdTable
	, DIV_NOMBRE
	, TITULO
	, 'ETL'
	, GETDATE()
	, 0
	FROM SSISTABLEDIVICIONES AS O
	GROUP BY O.DIV_NOMBRE, O.TITULO

------------------------------------------------Inserta Registro de Productos ---------------------------------------------------------------
	
	SET IDENTITY_INSERT ProProducto ON

	INSERT INTO ProProducto (IdProducto, IdModelo, IdMarca, IdFamilia, IdDivicion, NombreProducto, Descripcion
	, CodigoBarras, Detalles, EsReparable, HaySeries, Borrado, Creador, FechaCreacion)
	SELECT 
	ROW_NUMBER() OVER (ORDER BY SP.PR_NOMBRE)
	,(SELECT MO.IdModelo FROM ProCatModelo AS MO INNER JOIN ProCatMarca MA ON MO.IdMarca = MA.IdMarca WHERE MO.NombreModelo = SP.PR_MODELO GROUP BY MO.IdModelo)
	,(SELECT MA.IdMarca FROM ProCatMarca AS MA WHERE MA.NombreMarca = SM.MA_NOMBRE)
	,(SELECT PF.IdFamilia FROM ProFamilias AS PF  WHERE pf.NombreFamilia = SF.FA_NOMBRE)
	,(SELECT PD.IdDivicion FROM ProDiviciones AS PD WHERE PD.NombreDivicion = SD.DIV_NOMBRE)
	,SP.PR_NOMBRE
	,SP.TITULO
	,SP.PR_CODBAR
	,SP.PR_OBSERVACIONES
	,SP.PR_REPARABLE
	,SP.PR_HAYSERIES
	,0
	,'ETL'
	,GETDATE()
	FROM SSISTABLEPRODUCTOS SP
	INNER JOIN SSISTABLEMARCAS SM ON SP.ID_MARCAS = SM.ID_MARCAS
	INNER JOIN SSISTABLEDIVICIONES SD ON SP.ID_DIVISIONES = SD.ID_DIVICIONES
	INNER JOIN SSISTABLEFAMILIAS SF ON SP.ID_FAMILIAS = SF.ID_FAMILIAS
	GROUP BY SP.PR_NOMBRE,SP.PR_MODELO,sm.MA_NOMBRE, SD.DIV_NOMBRE, SP.TITULO, FA_NOMBRE,SP.PR_CODBAR, SP.PR_OBSERVACIONES, SP.PR_REPARABLE, SP.PR_HAYSERIES

	SET IDENTITY_INSERT ProProducto OFF

END
GO
