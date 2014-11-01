﻿	--Borrar Vistas en orden inverso a como se crearon
DROP VIEW EC_V_P_DIARIO_INC_SOL;
DROP VIEW EC_V_CHATS;
DROP VIEW EC_V_CHATS_ULTIMOS;
DROP VIEW EC_V_CHATS_MAILS_IDS_MAX;
DROP VIEW EC_V_CHATS_MAILS_IDS;
DROP VIEW EC_V_PERSONAS_TERM_CAMBIOS;
DROP VIEW EC_V_PERSONAS_TERMINALES;
DROP VIEW EC_V_PERSONAS_COMIDA;
DROP VIEW EC_V_MONEDERO_SALDO;
DROP VIEW EC_V_TERMINALES;
DROP VIEW EC_V_ACCESOS_X_DIA;
DROP VIEW EC_V_TERMINALES_EDO;
DROP VIEW EC_V_TERMINAL_CID_PERSONA;
DROP VIEW EC_V_PERSONAS_DIARIO_EX;
DROP VIEW EC_V_ASISTENCIAS_ES;
DROP VIEW EC_V_ASISTENCIAS_SEMANA;
DROP VIEW EC_V_ACCESOS;
DROP VIEW EC_V_ASISTENCIAS_V5;
DROP VIEW EC_V_ASISTENCIAS;
DROP VIEW EC_V_PERSONAS_DIARIO;
DROP VIEW EC_V_TURNOS_DIAS;

CREATE TABLE PERSONAS_DIARIO (
  PERSONA_DIARIO_ID decimal(38)  NOT NULL  ,
  PERSONA_D_HE_ID decimal(38)  NOT NULL DEFAULT 0 ,
  ACCESO_E_ID decimal(38)  NOT NULL  ,
  ACCESO_S_ID decimal(38)  NOT NULL  ,
  ACCESO_CS_ID decimal(38)  NOT NULL  ,
  ACCESO_CR_ID decimal(38)  NOT NULL  ,
  PERSONA_ID decimal(38)  NOT NULL  ,
  PERSONA_DIARIO_FECHA DATETIME  NOT NULL  ,
  TIPO_INC_SIS_ID decimal(38)  NOT NULL  ,
  TIPO_INC_C_SIS_ID decimal(38)  NOT NULL  ,
  INCIDENCIA_ID decimal(38)  NOT NULL  ,
  TURNO_DIA_ID decimal(38)  NOT NULL  ,
  PERSONA_DIARIO_TT DATETIME    ,
  PERSONA_DIARIO_TE DATETIME    ,
  PERSONA_DIARIO_TC DATETIME    ,
  PERSONA_DIARIO_TDE DATETIME    ,
  PERSONA_DIARIO_TES DATETIME      );

  DELETE PERSONAS_DIARIO;

  INSERT INTO PERSONAS_DIARIO (PERSONA_DIARIO_ID, PERSONA_D_HE_ID, ACCESO_E_ID, ACCESO_S_ID, ACCESO_CS_ID, ACCESO_CR_ID, PERSONA_ID, PERSONA_DIARIO_FECHA, TIPO_INC_SIS_ID, TIPO_INC_C_SIS_ID, INCIDENCIA_ID, TURNO_DIA_ID, PERSONA_DIARIO_TT, PERSONA_DIARIO_TE, PERSONA_DIARIO_TC, PERSONA_DIARIO_TDE, PERSONA_DIARIO_TES) 
	SELECT (PERSONA_ID * 10000 + DATEPART(year,PERSONA_DIARIO_FECHA)%100 * 366 +DATEPART(dayofyear,PERSONA_DIARIO_FECHA)) AS PERSONA_DIARIO_ID, PERSONA_D_HE_ID, ACCESO_E_ID, ACCESO_S_ID, ACCESO_CS_ID, ACCESO_CR_ID, PERSONA_ID, PERSONA_DIARIO_FECHA, TIPO_INC_SIS_ID, TIPO_INC_C_SIS_ID, INCIDENCIA_ID, TURNO_DIA_ID, PERSONA_DIARIO_TT, PERSONA_DIARIO_TE, PERSONA_DIARIO_TC, PERSONA_DIARIO_TDE, PERSONA_DIARIO_TES FROM EC_PERSONAS_DIARIO;

DROP TABLE EC_PERSONAS_ES;

DROP TABLE EC_PERSONAS_DIARIO;

CREATE TABLE EC_PERSONAS_DIARIO (
  PERSONA_DIARIO_ID decimal(38)  NOT NULL  ,
  PERSONA_D_HE_ID decimal(38)  NOT NULL DEFAULT 0 ,
  ACCESO_E_ID decimal(38)  NOT NULL  ,
  ACCESO_S_ID decimal(38)  NOT NULL  ,
  ACCESO_CS_ID decimal(38)  NOT NULL  ,
  ACCESO_CR_ID decimal(38)  NOT NULL  ,
  PERSONA_ID decimal(38)  NOT NULL  ,
  PERSONA_DIARIO_FECHA DATETIME  NOT NULL  ,
  TIPO_INC_SIS_ID decimal(38)  NOT NULL  ,
  TIPO_INC_C_SIS_ID decimal(38)  NOT NULL  ,
  INCIDENCIA_ID decimal(38)  NOT NULL  ,
  TURNO_DIA_ID decimal(38)  NOT NULL  ,
  PERSONA_DIARIO_TT DATETIME    ,
  PERSONA_DIARIO_TE DATETIME    ,
  PERSONA_DIARIO_TC DATETIME    ,
  PERSONA_DIARIO_TDE DATETIME    ,
  PERSONA_DIARIO_TES DATETIME      ,
PRIMARY KEY(PERSONA_DIARIO_ID)                              ,
  FOREIGN KEY(TURNO_DIA_ID)
    REFERENCES EC_TURNOS_DIA(TURNO_DIA_ID),
  FOREIGN KEY(ACCESO_E_ID)
    REFERENCES EC_ACCESOS(ACCESO_ID),
  FOREIGN KEY(ACCESO_S_ID)
    REFERENCES EC_ACCESOS(ACCESO_ID),
  FOREIGN KEY(ACCESO_CS_ID)
    REFERENCES EC_ACCESOS(ACCESO_ID),
  FOREIGN KEY(ACCESO_CR_ID)
    REFERENCES EC_ACCESOS(ACCESO_ID),
  FOREIGN KEY(PERSONA_ID)
    REFERENCES EC_PERSONAS(PERSONA_ID),
  FOREIGN KEY(TIPO_INC_SIS_ID)
    REFERENCES EC_TIPO_INC_SIS(TIPO_INC_SIS_ID),
  FOREIGN KEY(TIPO_INC_C_SIS_ID)
    REFERENCES EC_TIPO_INC_COMIDA_SIS(TIPO_INC_C_SIS_ID),
  FOREIGN KEY(INCIDENCIA_ID)
    REFERENCES EC_INCIDENCIAS(INCIDENCIA_ID),
  FOREIGN KEY(PERSONA_D_HE_ID)
    REFERENCES EC_PERSONAS_D_HE(PERSONA_D_HE_ID));

	INSERT INTO EC_PERSONAS_DIARIO (PERSONA_DIARIO_ID, PERSONA_D_HE_ID, ACCESO_E_ID, ACCESO_S_ID, ACCESO_CS_ID, ACCESO_CR_ID, PERSONA_ID, PERSONA_DIARIO_FECHA, TIPO_INC_SIS_ID, TIPO_INC_C_SIS_ID, INCIDENCIA_ID, TURNO_DIA_ID, PERSONA_DIARIO_TT, PERSONA_DIARIO_TE, PERSONA_DIARIO_TC, PERSONA_DIARIO_TDE, PERSONA_DIARIO_TES) 
	SELECT PERSONA_DIARIO_ID, PERSONA_D_HE_ID, ACCESO_E_ID, ACCESO_S_ID, ACCESO_CS_ID, ACCESO_CR_ID, PERSONA_ID, PERSONA_DIARIO_FECHA, TIPO_INC_SIS_ID, TIPO_INC_C_SIS_ID, INCIDENCIA_ID, TURNO_DIA_ID, PERSONA_DIARIO_TT, PERSONA_DIARIO_TE, PERSONA_DIARIO_TC, PERSONA_DIARIO_TDE, PERSONA_DIARIO_TES FROM PERSONAS_DIARIO;

