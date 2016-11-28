
CREATE PROCEDURE Usp_ObtenCatalogosSistema
AS
BEGIN
SELECT TABLE_NAME AS TableName FROM information_schema.tables WHERE TABLE_TYPE='BASE TABLE' AND
TABLE_NAME IN (
'AUL_CAT_TIPO_AULA'
,'HOR_CAT_TURNO'
,'HOR_CAT_DIAS_SEMANA'
,'CAR_CAT_ESPECIALIDAD'
,'HOR_CAT_HORAS'
,'CAR_CAT_CARRERAS'
,'MAT_CAT_MATERIAS'
,'MAT_CAT_CREDITOS_POR_HORAS'
,'HOR_HORAS_POR_DIA'
)
END


