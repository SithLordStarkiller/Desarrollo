/******
Autor: Maximiliano Silva 
Fecha: 04/02/2015 01:27:02 p. m. 
Descripcion: Inserta los registros base el catalogo de Respuestas del WS de London 
******/

INSERT INTO CatRespuestasWS
SELECT idCampo id
	,Nombre
	,'CSD' Ruta
	,'' Descripcion
FROM dbo.CamposRespuesta
