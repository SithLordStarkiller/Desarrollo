﻿ALTER TABLE EC_PERSONAS_TERMINALES ADD
	PERSONA_TERMINAL_UPDATE datetime NULL;
ALTER TABLE EC_PERSONAS ADD CONSTRAINT
	DF_EC_PERSONAS_AGRUPACION_NOMBRE DEFAULT '' FOR AGRUPACION_NOMBRE;
ALTER TABLE EC_PERSONAS ADD CONSTRAINT
	DF_EC_PERSONAS_PERSONA_EMAIL DEFAULT '' FOR PERSONA_EMAIL;

UPDATE EC_PERSONAS SET AGRUPACION_NOMBRE = '' WHERE AGRUPACION_NOMBRE IS NULL;

ALTER TABLE EC_TIPO_INCIDENCIAS_EX_INC_SIS ADD
	SUSCRIPCION_ID decimal(38, 0) NULL;

ALTER TABLE EC_DIAS_FESTIVOS ADD
	DIA_FESTIVO_COLOR INTEGER NULL;
	
ALTER TABLE EC_TIPO_INCIDENCIAS ADD
	TIPO_INCIDENCIA_COLOR INTEGER NULL,
	TIPO_INCIDENCIA_AGRUPADOR VARCHAR(45);
	
ALTER TABLE EC_TIPO_ACCESOS ADD
	TIPO_ACCESO_COLOR INTEGER NULL;

ALTER TABLE EC_PERIODOS_N ADD
	PERIODO_N_COLOR INTEGER NULL;
	
ALTER TABLE EC_TIPO_NOMINA ADD
	TIPO_NOMINA_COLOR INTEGER NULL;

ALTER TABLE EC_SESIONES ADD
	SESION_ACT_FECHAHORA DATETIME NULL,
 	SESION_SEGURIDAD VARCHAR(255) NULL;

ALTER TABLE EC_SUSCRIPCION ADD
  SUSCRIP_PRECIO_ID INTEGER  ,
  SUSCRIPCION_RAZON VARCHAR(255)    ,
  SUSCRIPCION_RFC VARCHAR(255)    ,
  SUSCRIPCION_DIRECCION1 VARCHAR(255)    ,
  SUSCRIPCION_DIRECCION2 VARCHAR(255)    ,
  SUSCRIPCION_CIUDAD VARCHAR(255)    ,
  SUSCRIPCION_ESTADO VARCHAR(255)    ,
  SUSCRIPCION_PAIS VARCHAR(255)    ,
  SUSCRIPCION_FACTURAR BOOL    ,
  SUSCRIPCION_CONTRATACION DATETIME    ,
  SUSCRIPCION_EMPLEADOS INTEGER    ,
  SUSCRIPCION_TERMINALES INTEGER    ,
  SUSCRIPCION_USUARIOS INTEGER    ,
  SUSCRIPCION_ALUMNOS INTEGER    ,
  SUSCRIPCION_VISITANTES INTEGER    ,
  SUSCRIPCION_ADICIONALES BOOL    ,
  SUSCRIPCION_OTROS VARCHAR(255)    ,
  SUSCRIPCION_MENSUAL DECIMAL    ,
  SUSCRIPCION_FINAL DATETIME    ,
  EDO_SUSCRIPCION_ID INTEGER ;

MERGE INTO EC_SUSCRIPCION USING EC_SUSCRIP_DATOS
ON (EC_SUSCRIPCION.SUSCRIPCION_ID = EC_SUSCRIP_DATOS.SUSCRIPCION_ID) 
WHEN MATCHED THEN 
UPDATE SET EC_SUSCRIPCION.SUSCRIP_PRECIO_ID=EC_SUSCRIP_DATOS.SUSCRIP_PRECIO_ID,
EC_SUSCRIPCION.SUSCRIPCION_RAZON=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_RAZON,
EC_SUSCRIPCION.SUSCRIPCION_RFC=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_RFC,
EC_SUSCRIPCION.SUSCRIPCION_DIRECCION1=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_DIRECCION1,
EC_SUSCRIPCION.SUSCRIPCION_DIRECCION2=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_DIRECCION2,
EC_SUSCRIPCION.SUSCRIPCION_CIUDAD=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_CIUDAD,
EC_SUSCRIPCION.SUSCRIPCION_ESTADO=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_ESTADO,
EC_SUSCRIPCION.SUSCRIPCION_PAIS=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_PAIS,
EC_SUSCRIPCION.EDO_SUSCRIPCION_ID=EC_SUSCRIP_DATOS.EDO_SUSCRIPCION_ID,
EC_SUSCRIPCION.SUSCRIPCION_FACTURAR=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_FACTURAR,
EC_SUSCRIPCION.SUSCRIPCION_CONTRATACION=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_CONTRATACION,
EC_SUSCRIPCION.SUSCRIPCION_EMPLEADOS=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_EMPLEADOS,
EC_SUSCRIPCION.SUSCRIPCION_TERMINALES=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_TERMINALES,
EC_SUSCRIPCION.SUSCRIPCION_USUARIOS=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_USUARIOS,
EC_SUSCRIPCION.SUSCRIPCION_ALUMNOS=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_ALUMNOS,
EC_SUSCRIPCION.SUSCRIPCION_VISITANTES=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_VISITANTES,
EC_SUSCRIPCION.SUSCRIPCION_ADICIONALES=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_ADICIONALES,
EC_SUSCRIPCION.SUSCRIPCION_OTROS=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_OTROS,
EC_SUSCRIPCION.SUSCRIPCION_MENSUAL=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_MENSUAL,
EC_SUSCRIPCION.SUSCRIPCION_FINAL=EC_SUSCRIP_DATOS.SUSCRIP_DATOS_FINAL
WHEN NOT MATCHED THEN 
INSERT (SUSCRIP_PRECIO_ID,
SUSCRIPCION_RAZON,
SUSCRIPCION_RFC,
SUSCRIPCION_DIRECCION1,
SUSCRIPCION_DIRECCION2,
SUSCRIPCION_CIUDAD,
SUSCRIPCION_ESTADO,
SUSCRIPCION_PAIS,
EDO_SUSCRIPCION_ID,
SUSCRIPCION_FACTURAR,
SUSCRIPCION_CONTRATACION,
SUSCRIPCION_EMPLEADOS,
SUSCRIPCION_TERMINALES,
SUSCRIPCION_USUARIOS,
SUSCRIPCION_ALUMNOS,
SUSCRIPCION_VISITANTES,
SUSCRIPCION_ADICIONALES,
SUSCRIPCION_OTROS,
SUSCRIPCION_MENSUAL,
SUSCRIPCION_FINAL
)
VALUES (EC_SUSCRIP_DATOS.SUSCRIP_PRECIO_ID,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_RAZON,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_RFC,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_DIRECCION1,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_DIRECCION2,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_CIUDAD,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_ESTADO,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_PAIS,
EC_SUSCRIP_DATOS.EDO_SUSCRIPCION_ID,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_FACTURAR,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_CONTRATACION,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_EMPLEADOS,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_TERMINALES,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_USUARIOS,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_ALUMNOS,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_VISITANTES,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_ADICIONALES,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_OTROS,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_MENSUAL,
EC_SUSCRIP_DATOS.SUSCRIP_DATOS_FINAL
);

UPDATE EC_SUSCRIPCION SET SUSCRIP_PRECIO_ID=0 WHERE SUSCRIP_PRECIO_ID IS NULL;
UPDATE EC_SUSCRIPCION SET EDO_SUSCRIPCION_ID=0 WHERE EDO_SUSCRIPCION_ID IS NULL;

ALTER TABLE EC_SUSCRIPCION ALTER COLUMN SUSCRIP_PRECIO_ID decimal(38) NOT NULL;

ALTER TABLE EC_SUSCRIPCION ALTER COLUMN EDO_SUSCRIPCION_ID decimal(38) NOT NULL;

ALTER TABLE EC_SUSCRIPCION ALTER COLUMN SUSCRIPCION_NOMBRE VARCHAR(45) NOT NULL;

ALTER TABLE EC_SUSCRIPCION 
ADD FOREIGN KEY(SUSCRIP_PRECIO_ID) REFERENCES EC_SUSCRIP_PRECIOS(SUSCRIP_PRECIO_ID);

ALTER TABLE EC_SUSCRIPCION 
ADD FOREIGN KEY(EDO_SUSCRIPCION_ID) REFERENCES EC_EDO_SUSCRIPCION(EDO_SUSCRIPCION_ID);

ALTER TABLE EC_TURNOS_DIA ADD
	TURNO_DIA_HERETARDO_B  DATETIME ,
	TURNO_DIA_HERETARDO_C  DATETIME ,
	TURNO_DIA_HERETARDO_D  DATETIME ;

ALTER TABLE EC_TERMINALES ADD
	TERMINAL_DESCRIPCION VARCHAR(255),
	TERMINAL_BIN BLOB;

ALTER TABLE EC_SITIOS ADD
	SITIO_RESPONSABLES VARCHAR(255),
	SITIO_TELEFONOS VARCHAR(255),
	SITIO_DIRECCION_1 VARCHAR(255),
	SITIO_DIRECCION_2 VARCHAR(255),
	SITIO_REFERENCIAS VARCHAR(255),
	SITIO_COMENTARIOS VARCHAR(255);

ALTER TABLE EC_TERMINALES ADD
	TERMINAL_AGRUPACION VARCHAR(255);

ALTER TABLE EC_PERSONAS_A_VEC 
ADD	PERSONAS_A_VEC_1_UMOD  DATETIME ,
	PERSONAS_A_VEC_2_UMOD  DATETIME ,
	PERSONAS_A_VEC_3_UMOD  DATETIME ;

ALTER TABLE EC_PERSONAS_TERMINALES 
ADD	PERSONA_TERMINAL_L_FH_UC  DATETIME ,
	PERSONA_TERMINAL_A_FH_UC  DATETIME ,
	PERSONA_TERMINAL_V1_FH_UC  DATETIME ,
	PERSONA_TERMINAL_V2_FH_UC  DATETIME ,
	PERSONA_TERMINAL_V3_FH_UC  DATETIME ,
	PERSONA_TERMINAL_L_FH_UT  DATETIME ,
	PERSONA_TERMINAL_A_FH_UT  DATETIME ,
	PERSONA_TERMINAL_V1_FH_UT  DATETIME ,
	PERSONA_TERMINAL_V2_FH_UT  DATETIME ,
	PERSONA_TERMINAL_V3_FH_UT  DATETIME ,
	PERSONA_TERMINAL_BORRADO  BOOL ,
	PERSONA_TERMINAL_B_FH   DATETIME ,
	PERSONA_TERMINAL_B_APLICADO  DATETIME ;

ALTER TABLE EC_PERSONAS_TERMINALES 
ADD	PERSONA_TERMINAL_SH_UC DATETIME ,
	PERSONA_TERMINAL_SH_UT DATETIME ;

ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_L_FH_UC datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_A_FH_UC datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_V1_FH_UC datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_V2_FH_UC datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_V3_FH_UC datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_L_FH_UT datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_A_FH_UT datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_V1_FH_UT datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_V2_FH_UT datetime NULL;
ALTER TABLE EC_PERSONAS_TERMINALES ALTER COLUMN PERSONA_TERMINAL_V3_FH_UT datetime NULL;

ALTER TABLE EC_PERSONAS_TERMINALES 
ADD	PERSONA_TERMINAL_ERRORFH DATETIME ,
	PERSONA_TERMINAL_ERROR VARCHAR(255) ;

ALTER TABLE EC_PERSONAS_A_VEC ALTER COLUMN PERSONAS_A_VEC_1_UMOD datetime NULL;
ALTER TABLE EC_PERSONAS_A_VEC ALTER COLUMN PERSONAS_A_VEC_2_UMOD datetime NULL;
ALTER TABLE EC_PERSONAS_A_VEC ALTER COLUMN PERSONAS_A_VEC_3_UMOD datetime NULL;

ALTER TABLE EC_TERMINALES 
ADD	TERMINAL_MODELO VARCHAR(255) ,
	TERMINAL_NO_SERIE VARCHAR(255) ,
	TERMINAL_FIRMWARE_VER VARCHAR(255) ,
	TERMINAL_NO_HUELLAS INTEGER ,
	TERMINAL_NO_EMPLEADOS INTEGER , 
	TERMINAL_NO_TARJETAS INTEGER ,
	TERMINAL_NO_ROSTROS INTEGER ,
	TERMINAL_NO_CHECADAS INTEGER ,
	TERMINAL_NO_PALMAS INTEGER ,
	TERMINAL_NO_IRIS INTEGER ,
	TERMINAL_GARANTIA_INICIO DATETIME ,
	TERMINAL_GARANTIA_FIN DATETIME ;
	
ALTER TABLE EC_TIPO_INC_SIS 
ADD	TIPO_INC_SIS_COLOR INTEGER NULL;


ALTER TABLE EC_TIPO_INC_COMIDA_SIS
ADD	TIPO_INC_C_SIS_COLOR INTEGER NULL;

ALTER TABLE EC_LOCALIZACIONES ALTER COLUMN LOCALIZACION_DESCRIPCION VARCHAR(8000);

ALTER TABLE EC_MAILS ADD
	MAIL_TIPO INTEGER NULL;

UPDATE EC_MAILS SET MAIL_TIPO = 0 WHERE MAIL_TIPO IS NULL;

alter table EC_PERSONAS_ES drop EC_PERSONAS_ES_FKI3;
ALTER TABLE EC_PERSONAS_ES
	DROP CONSTRAINT FK__EC_PERSON__PERSO__34FEAF52;
UPDATE EC_PERSONAS_DIARIO SET PERSONA_DIARIO_ID = (PERSONA_ID * 10000 + DATEPART(year,PERSONA_DIARIO_FECHA)%100 * 366 +DATEPART(dayofyear,PERSONA_DIARIO_FECHA))
WHERE PERSONA_DIARIO_ID <> (PERSONA_ID * 10000 + DATEPART(year,PERSONA_DIARIO_FECHA)%100 * 366 +DATEPART(dayofyear,PERSONA_DIARIO_FECHA));