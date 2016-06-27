--SELECT * FROM FAMILIAS
--SELECT * FROM MARCAS
--SELECT * FROM DIVISIONES
SELECT 
ID_PRODUCTOS,
CASE WHEN (TITULO IS NULL) OR (TITULO = '') THEN 'NULL' ELSE TITULO END AS TITULO,
CASE WHEN (CREADOR IS NULL) OR (CREADOR = '') THEN 'NULL' ELSE CREADOR END AS CREADOR,
CASE WHEN (FECHA_CREACION IS NULL) OR (FECHA_CREACION = '') THEN 'NULL' ELSE FECHA_CREACION END AS FECHA_CREACION,
CASE WHEN (PR_CODBAR IS NULL) OR (PR_CODBAR = '') THEN 'NULL' ELSE PR_CODBAR END AS PR_CODBAR,
CASE WHEN (PR_NOMBRE IS NULL) OR (PR_NOMBRE = '') THEN 'NULL' ELSE PR_NOMBRE END AS PR_NOMBRE,
CASE WHEN (PR_MODELO IS NULL) OR (PR_MODELO = '') THEN 'NULL' ELSE PR_MODELO END AS PR_MODELO,
CASE WHEN (PR_OBSERVACIONES IS NULL) OR (PR_OBSERVACIONES = '') THEN 'NULL' ELSE PR_OBSERVACIONES END AS PR_OBSERVACIONES,
CASE WHEN (PR_HAYSERIES IS NULL) OR (PR_HAYSERIES = '') THEN 'NULL' ELSE PR_HAYSERIES END AS PR_HAYSERIES,
CASE WHEN (PR_REPARABLE IS NULL) OR (PR_REPARABLE = '') THEN 'NULL' ELSE PR_REPARABLE END AS PR_REPARABLE,
CASE WHEN (ID_MARCAS IS NULL) OR (ID_MARCAS = 0) THEN 'NULL' ELSE ID_MARCAS END AS ID_MARCAS,
CASE WHEN (ID_DIVISIONES IS NULL) OR (ID_DIVISIONES = 0) THEN 'NULL' ELSE ID_DIVISIONES END AS ID_DIVISIONES,
CASE WHEN (ID_FAMILIAS IS NULL) OR (ID_FAMILIAS = 0) THEN 'NULL' ELSE ID_FAMILIAS END AS ID_FAMILIAS,
CASE WHEN (TITULODETALLE IS NULL) OR (TITULODETALLE = '') THEN 'NULL' ELSE TITULODETALLE END AS TITULODETALLE,
CASE WHEN (PR_VALSERIE IS NULL) OR (PR_VALSERIE = '') THEN 'NULL' ELSE PR_VALSERIE END AS PR_VALSERIE,
CASE WHEN (PR_FECVENTA IS NULL) OR (PR_FECVENTA = '') THEN '1900/01/01' ELSE PR_FECVENTA END AS PR_FECVENTA
FROM PRODUCTOS
