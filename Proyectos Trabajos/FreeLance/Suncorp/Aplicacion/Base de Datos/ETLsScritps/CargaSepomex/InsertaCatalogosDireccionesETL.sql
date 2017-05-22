-- ===============================================================================================================================================
-- Autor:					Esteban Cruz Lagunes
-- Solucion:				Suncorp
-- Modulo:					ETLCargarCEPOMEX
-- Nombre:					InsertaCatalogosDireccionesETL
-- Fecha de creacion:		18/05/2017
-- Fecha de Modificacion:	Ninguna
--
-- Descripcion:	Script encargado de la insercion a los catalogos propios de modulo de direcciones, primero se realizara un llenado de los Estados,
--			   Municipios y colonias de la tabla CEPOMEX en la cual se insertaron los registro del excel
-- ================================================================================================================================================

INSERT INTO 
	DirCatEstados(IdEstado,NombreOficial,NombreNoOficial,ClaveEstadoSepomex) 
SELECT 
	c_estado,
	d_estado,
	CASE c_estado 
		WHEN 2  THEN 'Baja California Norte' 
		WHEN 5  THEN 'Coahuila' 
		WHEN 15 THEN 'Estado de Mexico' 
		WHEN 16 THEN 'Michoacán' 
		WHEN 30 THEN 'Veracruz'
	ELSE 
		d_estado
	END	AS NombreOficial,
	d_estado,
	CAST(c_estado as varchar(10)) AS ClaveEstadoSepomex
FROM 
	CEPOMEX 
GROUP BY
	c_estado,
	d_estado 
ORDER BY
	c_estado

INSERT INTO DirCatMunicipios(IdMunicipio,IdEstado,NombreOficial,NombreCiudad,ClaveMunicipio)
SELECT ROW_NUMBER() OVER(ORDER BY c_estado,c_mnpio), c_mnpio, D_mnpio,c_estado FROM CEPOMEX GROUP BY c_mnpio , D_mnpio , c_estado ORDER BY c_estado,c_mnpio

insert into DIR_CAT_TIPO_ASENTAMIENTO (IDTIPOASENTAMIENTO,TIPOASENTAMIENTO)
select c_tipo_asenta,d_tipo_asenta from CEPOMEX group by c_tipo_asenta,d_tipo_asenta order by c_tipo_asenta

insert into DIR_CAT_TIPO_ZONA (IDTIPOZONA,TIPOZONA)
select ROW_NUMBER() OVER(Order by d_zona),d_zona from CEPOMEX group by d_zona Order by d_zona

insert into DIR_CAT_COLONIAS (IDCOLONIA,IDESTADO,IDTIPOASENTAMIENTO,IDTIPOZONA,IDMUNICIPIO,CODIGOPOSTAL,NOMBRECOLONIA)

SELECT 
ROW_NUMBER() OVER(order by c_estado,c_mnpio)
,c.c_estado
--,DCDM.IDDELGMUNICIPIO
,c.c_tipo_asenta
,DCTZ.IDTIPOZONA
,c.c_mnpio
,c.d_codigo
,c.d_asenta
 FROM CEPOMEX AS C
 INNER JOIN DIR_CAT_DELG_MUNICIPIO AS DCDM ON c.c_estado = DCDM.IDESTADO and c.c_mnpio = DCDM.IDMUNICIPIO
 INNER JOIN DIR_CAT_TIPO_ZONA AS DCTZ ON c.d_zona = DCTZ.TIPOZONA
 order by c_estado,c_mnpio
