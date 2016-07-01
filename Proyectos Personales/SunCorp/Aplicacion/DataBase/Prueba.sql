SELECT 
	PP.NombreProducto 
	,PCMA.NombreMarca
FROM ProProducto AS PP
INNER JOIN ProCatMarca AS PCMA ON PP.IdMarca = PCMA.IdMarca