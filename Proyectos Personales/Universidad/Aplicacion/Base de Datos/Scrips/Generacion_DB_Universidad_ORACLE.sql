/*==============================================================*/
/* DBMS name:      ORACLE Version 11g                           */
/* Created on:     05/02/2016 06:40:12 p. m.                    */
/*==============================================================*/



-- Type package declaration
create or replace package PDTypes  
as
    TYPE ref_cursor IS REF CURSOR;
end;

-- Integrity package declaration
create or replace package IntegrityPackage AS
 procedure InitNestLevel;
 function GetNestLevel return number;
 procedure NextNestLevel;
 procedure PreviousNestLevel;
 end IntegrityPackage;
/

-- Integrity package definition
create or replace package body IntegrityPackage AS
 NestLevel number;

-- Procedure to initialize the trigger nest level
 procedure InitNestLevel is
 begin
 NestLevel := 0;
 end;


-- Function to return the trigger nest level
 function GetNestLevel return number is
 begin
 if NestLevel is null then
     NestLevel := 0;
 end if;
 return(NestLevel);
 end;

-- Procedure to increase the trigger nest level
 procedure NextNestLevel is
 begin
 if NestLevel is null then
     NestLevel := 0;
 end if;
 NestLevel := NestLevel + 1;
 end;

-- Procedure to decrease the trigger nest level
 procedure PreviousNestLevel is
 begin
 NestLevel := NestLevel - 1;
 end;

 end IntegrityPackage;
/


drop trigger COMPOUNDDELETETRIGGER_CAL_CALI
/

drop trigger COMPOUNDINSERTTRIGGER_CAL_CALI
/

drop trigger COMPOUNDUPDATETRIGGER_CAL_CALI
/

drop trigger TIB_CAL_CALIFICACIONES
/

drop trigger COMPOUNDDELETETRIGGER_CAL_CAL2
/

drop trigger COMPOUNDDELETETRIGGER_CAL_CALI
/

drop trigger COMPOUNDINSERTTRIGGER_CAL_CAL2
/

drop trigger COMPOUNDINSERTTRIGGER_CAL_CALI
/

drop trigger COMPOUNDUPDATETRIGGER_CAL_CAL2
/

drop trigger COMPOUNDUPDATETRIGGER_CAL_CALI
/

drop trigger TIB_CAL_CALIFICACIONES_FECHAS
/

drop trigger COMPOUNDDELETETRIGGER_CLA_CLA2
/

drop trigger COMPOUNDDELETETRIGGER_CLA_CLAS
/

drop trigger COMPOUNDINSERTTRIGGER_CLA_CLA2
/

drop trigger COMPOUNDINSERTTRIGGER_CLA_CLAS
/

drop trigger COMPOUNDUPDATETRIGGER_CLA_CLA2
/

drop trigger COMPOUNDUPDATETRIGGER_CLA_CLAS
/

drop trigger TIB_CLA_CLASE
/

drop trigger COMPOUNDDELETETRIGGER_CLA_HOR2
/

drop trigger COMPOUNDDELETETRIGGER_CLA_HORA
/

drop trigger COMPOUNDINSERTTRIGGER_CLA_HOR2
/

drop trigger COMPOUNDINSERTTRIGGER_CLA_HORA
/

drop trigger COMPOUNDUPDATETRIGGER_CLA_HOR2
/

drop trigger COMPOUNDUPDATETRIGGER_CLA_HORA
/

drop trigger TIB_CLA_HORARIO
/

drop trigger COMPOUNDDELETETRIGGER_DIR_DIR2
/

drop trigger COMPOUNDDELETETRIGGER_DIR_DIRE
/

drop trigger COMPOUNDINSERTTRIGGER_DIR_DIR2
/

drop trigger COMPOUNDINSERTTRIGGER_DIR_DIRE
/

drop trigger COMPOUNDUPDATETRIGGER_DIR_DIR2
/

drop trigger COMPOUNDUPDATETRIGGER_DIR_DIRE
/

drop trigger TIB_DIR_DIRECCIONES
/

drop trigger COMPOUNDDELETETRIGGER_HOR_HOR2
/

drop trigger COMPOUNDDELETETRIGGER_HOR_HORA
/

drop trigger COMPOUNDINSERTTRIGGER_HOR_HOR2
/

drop trigger COMPOUNDINSERTTRIGGER_HOR_HORA
/

drop trigger COMPOUNDUPDATETRIGGER_HOR_HOR2
/

drop trigger COMPOUNDUPDATETRIGGER_HOR_HORA
/

drop trigger TIB_HOR_HORAS_POR_DIA
/

drop trigger COMPOUNDDELETETRIGGER_PER_CAT2
/

drop trigger COMPOUNDDELETETRIGGER_PER_CAT_
/

drop trigger COMPOUNDINSERTTRIGGER_PER_CAT2
/

drop trigger COMPOUNDINSERTTRIGGER_PER_CAT_
/

drop trigger COMPOUNDUPDATETRIGGER_PER_CAT2
/

drop trigger COMPOUNDUPDATETRIGGER_PER_CAT_
/

drop trigger TIB_PER_CAT_TELEFONOS
/

drop trigger COMPOUNDDELETETRIGGER_PER_FOT2
/

drop trigger COMPOUNDDELETETRIGGER_PER_FOTO
/

drop trigger COMPOUNDINSERTTRIGGER_PER_FOT2
/

drop trigger COMPOUNDINSERTTRIGGER_PER_FOTO
/

drop trigger COMPOUNDUPDATETRIGGER_PER_FOT2
/

drop trigger COMPOUNDUPDATETRIGGER_PER_FOTO
/

drop trigger TIB_PER_FOTOGRAFIA
/

drop trigger COMPOUNDDELETETRIGGER_PER_MED2
/

drop trigger COMPOUNDDELETETRIGGER_PER_MEDI
/

drop trigger COMPOUNDINSERTTRIGGER_PER_MED2
/

drop trigger COMPOUNDINSERTTRIGGER_PER_MEDI
/

drop trigger COMPOUNDUPDATETRIGGER_PER_MED2
/

drop trigger COMPOUNDUPDATETRIGGER_PER_MEDI
/

drop trigger TIB_PER_MEDIOS_ELECTRONICOS
/

drop trigger COMPOUNDDELETETRIGGER_PER_PER2
/

drop trigger COMPOUNDDELETETRIGGER_PER_PERS
/

drop trigger COMPOUNDINSERTTRIGGER_PER_PER2
/

drop trigger COMPOUNDINSERTTRIGGER_PER_PERS
/

drop trigger COMPOUNDUPDATETRIGGER_PER_PER2
/

drop trigger COMPOUNDUPDATETRIGGER_PER_PERS
/

drop trigger TIB_PER_PERSONAS
/

drop trigger COMPOUNDDELETETRIGGER_PRO_PRO2
/

drop trigger COMPOUNDDELETETRIGGER_PRO_PROF
/

drop trigger COMPOUNDINSERTTRIGGER_PRO_PRO2
/

drop trigger COMPOUNDINSERTTRIGGER_PRO_PROF
/

drop trigger COMPOUNDUPDATETRIGGER_PRO_PRO2
/

drop trigger COMPOUNDUPDATETRIGGER_PRO_PROF
/

drop trigger TIB_PRO_PROFESOR
/

drop trigger COMPOUNDDELETETRIGGER_SIS_AAD2
/

drop trigger COMPOUNDDELETETRIGGER_SIS_AADM
/

drop trigger COMPOUNDINSERTTRIGGER_SIS_AAD2
/

drop trigger COMPOUNDINSERTTRIGGER_SIS_AADM
/

drop trigger COMPOUNDUPDATETRIGGER_SIS_AAD2
/

drop trigger COMPOUNDUPDATETRIGGER_SIS_AADM
/

drop trigger TIB_SIS_AADM_APLICACIONES
/

drop trigger COMPOUNDDELETETRIGGER_US_USUA2
/

drop trigger COMPOUNDDELETETRIGGER_US_USUAR
/

drop trigger COMPOUNDINSERTTRIGGER_US_USUA2
/

drop trigger COMPOUNDINSERTTRIGGER_US_USUAR
/

drop trigger COMPOUNDUPDATETRIGGER_US_USUA2
/

drop trigger COMPOUNDUPDATETRIGGER_US_USUAR
/

drop trigger TIB_US_USUARIOS
/

alter table ALU_ALUMNOS
   drop constraint FK_ALU_ALUM_REFERENCE_PER_PERS
/

alter table ALU_HORARIO
   drop constraint FK_ALU_HORA_REFERENCE_ALU_ALUM
/

alter table ALU_HORARIO
   drop constraint FK_ALU_HORA_REFERENCE_CLA_CLAS
/

alter table AUL_AULA_CLASES
   drop constraint FK_AUL_AULA_REFERENCE_AUL_CAT_
/

alter table CAL_ALUMNO_KARDEX
   drop constraint FK_CAL_ALUM_REFERENCE_ALU_ALUM
/

alter table CAL_ALUMNO_KARDEX
   drop constraint FK_CAL_ALUM_REFERENCE_CAL_CALI
/

alter table CAL_CALIFICACIONES
   drop constraint FK_CAL_CALI_REFERENCE_ALU_ALUM
/

alter table CAL_CALIFICACIONES
   drop constraint FK_CAL_CALI_REFERENCE_MAT_CAT_
/

alter table CAL_CALIFICACIONES
   drop constraint FK_CAL_CALI_REF_CAL_C_CAL_CAT_
/

alter table CAL_CALIFICACIONES
   drop constraint FK_CAL_CALI_REF_CAL_C_CAL_CALI
/

alter table CAL_CALIFICACION_CLASE
   drop constraint FK_CAL_CALI_CAL_CAL_C_CAL_CAT_
/

alter table CAL_CALIFICACION_CLASE
   drop constraint FK_CAL_CALI_CAL_CLA_C_CAL_CALI
/

alter table CAL_CALIFICACION_CLASE
   drop constraint FK_CAL_CALI_REFERENCE_CLA_CLAS
/

alter table CAR_CAT_CARRERAS
   drop constraint FK_CAR_CAT__REFERENCE_CAR_CAT_
/

alter table CLA_CLASE
   drop constraint FK_CLA_CLAS_REFERENCE_MAT_CAT_
/

alter table CLA_CLASE
   drop constraint FK_CLA_CLAS_REFERENCE_PRO_PROF
/

alter table CLA_CLASE
   drop constraint FK_CLA_CLAS_REFERENCE_CAR_CAT_
/

alter table CLA_CLASE
   drop constraint FK_CLA_CLAS_REFERENCE_GEN_CAT_
/

alter table CLA_HORARIO
   drop constraint FK_CLA_HORA_REFERENCE_CLA_CLAS
/

alter table CLA_HORARIO
   drop constraint FK_CLA_HORA_REFERENCE_MAT_HORA
/

alter table DIR_CAT_COLONIAS
   drop constraint FK_DIR_CAT__COLONIAS__DIR_CAT_
/

alter table DIR_CAT_COLONIAS
   drop constraint FK_DIR_CAT__REFERENCE_DIR_CAT_
/

alter table DIR_CAT_COLONIAS
   drop constraint "Reference_26"
/

alter table DIR_CAT_COLONIAS
   drop constraint "Reference_29"
/

alter table DIR_CAT_DELG_MUNICIPIO
   drop constraint "Reference_27"
/

alter table DIR_DIRECCIONES
   drop constraint "Reference_28"
/

alter table DIR_DIRECCIONES
   drop constraint "Reference_32"
/

alter table HOR_CALENDARIO_ESCOLAR
   drop constraint FK_HOR_CALE_FK_HOR_CA_HOR_CAT_
/

alter table HOR_CALENDARIO_ESCOLAR
   drop constraint FK_HOR_CALE_HOR_CALEN_HOR_CAT_
/

alter table HOR_CAT_DIAS_FESTIVOS
   drop constraint FK_HOR_CAT__FK_HOR_CA_HOR_CAT_
/

alter table HOR_CAT_HORAS
   drop constraint FK_HOR_CAT__REFERENCE_HOR_CAT_
/

alter table HOR_HORAS_POR_DIA
   drop constraint FK_HOR_HORA_HOR_HORAS_HOR_CAT_
/

alter table HOR_HORAS_POR_DIA
   drop constraint FK_HOR_HORA_HOR_HOR_P_HOR_CAT_
/

alter table MAT_ARBOL_MATERIA
   drop constraint FK_MAT_ARBO_REFERENCE_MAT_ARBO
/

alter table MAT_ARBOL_MATERIA
   drop constraint FK_MAT_ARBO_REFERENCE_MAT_CAT_
/

alter table MAT_HORARIO_POR_MATERIA
   drop constraint FK_MAT_HORA_REFERENCE_HOR_HORA
/

alter table MAT_HORARIO_POR_MATERIA
   drop constraint FK_MAT_HORA_REFERENCE_AUL_AULA
/

alter table MAT_MATERIAS_POR_CARRERA
   drop constraint FK_MAT_MATE_REFERENCE_MAT_CAT_
/

alter table MAT_MATERIAS_POR_CARRERA
   drop constraint FK_MAT_MATE_REFERENCE_CAR_CAT_
/

alter table PER_PERSONAS
   drop constraint FK_PER_PERS_REFERENCE_PER_MEDI
/

alter table PER_PERSONAS
   drop constraint FK_PER_PERS_REFERENCE_PER_FOTO
/

alter table PER_PERSONAS
   drop constraint "Reference_33"
/

alter table PER_PERSONAS
   drop constraint "Reference_34"
/

alter table PER_PERSONAS
   drop constraint "Reference_6"
/

alter table PER_PERSONAS
   drop constraint "Reference_8"
/

alter table PER_PERSONAS
   drop constraint "Reference_9"
/

alter table PRO_PROFESOR
   drop constraint FK_PRO_PROF_REFERENCE_PER_PERS
/

alter table SIS_AADM_APLICACIONES
   drop constraint FK_SIS_AADM_REFERENCE_SIS_AADM
/

alter table SIS_AADM_APLICACIONES
   drop constraint FK_SIS_AADM_REFERENCE_SIS_CAT_
/

alter table SIS_AADM_APLICACIONES
   drop constraint FK_SIS_AADM_REFERENCE_US_CAT_N
/

alter table SIS_AADM_APLICACIONES
   drop constraint FK_SIS_AADM_REFERENCE_US_CAT_T
/

alter table SIS_WADM_ARBOLMENU
   drop constraint FK_SIS_WADM_REFERENCE_US_CAT_T
/

alter table SIS_WADM_ARBOLMENU
   drop constraint FK_SIS_WADM_REFERENCE_US_CAT_N
/

alter table SIS_WADM_ARBOLMENU
   drop constraint FK_SIS_WADM_REFERENCE_SIS_WADM
/

alter table SIS_WADM_ARBOLMENU_MVC
   drop constraint FK_MENU_US_TIPO_USUARIO
/

alter table SIS_WADM_ARBOLMENU_MVC
   drop constraint FK_WADM_NIVEL_USUARIO
/

alter table SIS_WADM_ARBOLMENU_MVC
   drop constraint FK_SIS_WADM_MVC_REC
/

alter table US_USUARIOS
   drop constraint "Reference_10"
/

alter table US_USUARIOS
   drop constraint "Reference_11"
/

alter table US_USUARIOS
   drop constraint "Reference_12"
/

drop table ALU_ALUMNOS cascade constraints
/

drop table ALU_HORARIO cascade constraints
/

drop table AUL_AULA_CLASES cascade constraints
/

drop table AUL_CAT_TIPO_AULA cascade constraints
/

drop table CAL_ALUMNO_KARDEX cascade constraints
/

drop table CAL_CALIFICACIONES cascade constraints
/

drop table CAL_CALIFICACIONES_FECHAS cascade constraints
/

drop table CAL_CALIFICACION_CLASE cascade constraints
/

drop table CAL_CAT_TIPO_EVALUACION cascade constraints
/

drop table CAR_CAT_CARRERAS cascade constraints
/

drop table CAR_CAT_ESPECIALIDAD cascade constraints
/

drop table CLA_CLASE cascade constraints
/

drop table CLA_HORARIO cascade constraints
/

drop table DIR_CAT_COLONIAS cascade constraints
/

drop table DIR_CAT_DELG_MUNICIPIO cascade constraints
/

drop table DIR_CAT_ESTADO cascade constraints
/

drop table DIR_CAT_TIPO_ASENTAMIENTO cascade constraints
/

drop table DIR_CAT_TIPO_ZONA cascade constraints
/

drop table DIR_DIRECCIONES cascade constraints
/

drop table GEN_CAT_SEMESTRE_PERIODOS cascade constraints
/

drop table HOR_CALENDARIO_ESCOLAR cascade constraints
/

drop table HOR_CAT_DIAS_FESTIVOS cascade constraints
/

drop table HOR_CAT_DIAS_SEMANA cascade constraints
/

drop table HOR_CAT_HORAS cascade constraints
/

drop table HOR_CAT_TIPO_DIA_FERIADO cascade constraints
/

drop table HOR_CAT_TURNO cascade constraints
/

drop table HOR_HORAS_POR_DIA cascade constraints
/

drop table MAT_ARBOL_MATERIA cascade constraints
/

drop table MAT_CAT_CREDITOS_POR_HORAS cascade constraints
/

drop table MAT_CAT_MATERIAS cascade constraints
/

drop table MAT_HORARIO_POR_MATERIA cascade constraints
/

drop table MAT_MATERIAS_POR_CARRERA cascade constraints
/

drop table PER_CAT_NACIONALIDAD cascade constraints
/

drop table PER_CAT_TELEFONOS cascade constraints
/

drop table PER_CAT_TIPO_PERSONA cascade constraints
/

drop table PER_FOTOGRAFIA cascade constraints
/

drop table PER_MEDIOS_ELECTRONICOS cascade constraints
/

drop table PER_PERSONAS cascade constraints
/

drop table PRO_PROFESOR cascade constraints
/

drop table SIS_AADM_APLICACIONES cascade constraints
/

drop table SIS_AADM_ARBOLMENUS cascade constraints
/

drop table SIS_CAT_TABPAGES cascade constraints
/

drop table SIS_WADM_ARBOLMENU cascade constraints
/

drop table SIS_WADM_ARBOLMENU_MVC cascade constraints
/

drop table US_CAT_ESTATUS_USUARIO cascade constraints
/

drop table US_CAT_NIVEL_USUARIO cascade constraints
/

drop table US_CAT_TIPO_USUARIO cascade constraints
/

drop table US_USUARIOS cascade constraints
/

drop sequence S_CAL_CALIFICACIONES
/

drop sequence S_CAL_CALIFICACIONES_FECHAS
/

drop sequence S_CLA_CLASE
/

drop sequence S_CLA_HORARIO
/

drop sequence S_DIR_DIRECCIONES
/

drop sequence S_HOR_HORAS_POR_DIA
/

drop sequence S_PER_CAT_TELEFONOS
/

drop sequence S_PER_FOTOGRAFIA
/

drop sequence S_PER_MEDIOS_ELECTRONICOS
/

drop sequence S_PER_PERSONAS
/

drop sequence S_PRO_PROFESOR
/

drop sequence S_SIS_AADM_APLICACIONES
/

drop sequence S_US_USUARIOS
/

create sequence S_CAL_CALIFICACIONES
/

create sequence S_CAL_CALIFICACIONES_FECHAS
/

create sequence S_CLA_CLASE
/

create sequence S_CLA_HORARIO
/

create sequence S_DIR_DIRECCIONES
/

create sequence S_HOR_HORAS_POR_DIA
/

create sequence S_PER_CAT_TELEFONOS
/

create sequence S_PER_FOTOGRAFIA
/

create sequence S_PER_MEDIOS_ELECTRONICOS
/

create sequence S_PER_PERSONAS
/

create sequence S_PRO_PROFESOR
/

create sequence S_SIS_AADM_APLICACIONES
/

create sequence S_US_USUARIOS
/

/*==============================================================*/
/* Table: ALU_ALUMNOS                                           */
/*==============================================================*/
create table ALU_ALUMNOS 
(
   IDALUMNOS            INTEGER              not null,
   ID_PERSONA           INTEGER,
   ID_PER_LINKID        VARCHAR2(50),
   constraint PK_ALU_ALUMNOS primary key (IDALUMNOS)
)
/

/*==============================================================*/
/* Table: ALU_HORARIO                                           */
/*==============================================================*/
create table ALU_HORARIO 
(
   IDHORARIO            INTEGER              not null,
   IDALUMNOS            INTEGER,
   IDCLASE              INTEGER,
   constraint PK_ALU_HORARIO primary key (IDHORARIO)
)
/

/*==============================================================*/
/* Table: AUL_AULA_CLASES                                       */
/*==============================================================*/
create table AUL_AULA_CLASES 
(
   IDAULACLASES         SMALLINT             not null,
   IDTIPOAULA           SMALLINT,
   AULA                 VARCHAR2(10)         not null,
   MAXLUGARES           SMALLINT,
   constraint PK_AUL_AULA_CLASES primary key (IDAULACLASES)
)
/

/*==============================================================*/
/* Table: AUL_CAT_TIPO_AULA                                     */
/*==============================================================*/
create table AUL_CAT_TIPO_AULA 
(
   IDTIPOAULA           SMALLINT             not null,
   TIPOAULA             VARCHAR2(100)        not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   constraint PK_AUL_CAT_TIPO_AULA primary key (IDTIPOAULA)
)
/

/*==============================================================*/
/* Table: CAL_ALUMNO_KARDEX                                     */
/*==============================================================*/
create table CAL_ALUMNO_KARDEX 
(
   IDKARDEX             INTEGER              not null,
   IDALUMNOS            INTEGER,
   IDCALIFICACION       INTEGER,
   constraint PK_CAL_ALUMNO_KARDEX primary key (IDKARDEX)
)
/

/*==============================================================*/
/* Table: CAL_CALIFICACIONES                                    */
/*==============================================================*/
create table CAL_CALIFICACIONES 
(
   IDCALIFICACION       NUMBER(6)            not null,
   IDALUMNOS            INTEGER,
   IDCALIFICACIONESFECHAS INTEGER,
   IDMATERIA            SMALLINT,
   IDTIPOEVALUACION     SMALLINT,
   CALPRIMPERORD        NUMBER(5,2),
   CALSEGPERORD         NUMBER(2,2),
   CALTERPERORD         NUMBER(2,2),
   CALFINALORD          NUMBER(2,2),
   CALPRIPERREC         NUMBER(2,2),
   CALSEGPERREC         NUMBER(2,2),
   CALTERPERREC         NUMBER(2,2),
   CALFINALREC          NUMBER(2,2),
   CALIFICACIONETS1     NUMBER(2,2),
   CALIFICACIONETS2     NUMBER(2,2),
   CALIFICACIONETS3     NUMBER(2,2),
   CALIFICACIONETS4     NUMBER(2,2),
   CREDITOSOBTENIDOS    INTEGER,
   ACREDITADA           SMALLINT             not null,
   constraint PK_CAL_CALIFICACIONES primary key (IDCALIFICACION)
)
/

/*==============================================================*/
/* Table: CAL_CALIFICACIONES_FECHAS                             */
/*==============================================================*/
create table CAL_CALIFICACIONES_FECHAS 
(
   IDCALIFICACIONESFECHAS NUMBER(6)            not null,
   FECHACALIFICACIONETS1 DATE,
   FECHACALIFICACIONETS2 DATE,
   FECHACALIFICACIONETS3 DATE,
   FECHACALIFICACIONETS4 DATE,
   constraint PK_CAL_CALIFICACIONES_FECHAS primary key (IDCALIFICACIONESFECHAS)
)
/

/*==============================================================*/
/* Table: CAL_CALIFICACION_CLASE                                */
/*==============================================================*/
create table CAL_CALIFICACION_CLASE 
(
   IDCALIFICACIONCLASE  INTEGER              not null,
   IDCLASE              INTEGER,
   IDCALIFICACION       INTEGER,
   IDTIPOEVALUACION     SMALLINT,
   constraint PK_CAL_CALIFICACION_CLASE primary key (IDCALIFICACIONCLASE)
)
/

/*==============================================================*/
/* Table: CAL_CAT_TIPO_EVALUACION                               */
/*==============================================================*/
create table CAL_CAT_TIPO_EVALUACION 
(
   IDTIPOEVALUACION     SMALLINT             not null,
   TIPOEVALUACION       VARCHAR2(100)        not null,
   DESCRIPCION          VARCHAR2(300),
   constraint PK_CAL_CAT_TIPO_EVALUACION primary key (IDTIPOEVALUACION)
)
/

/*==============================================================*/
/* Table: CAR_CAT_CARRERAS                                      */
/*==============================================================*/
create table CAR_CAT_CARRERAS 
(
   IDCARRERA            SMALLINT             not null,
   IDESPECIALIDAD       SMALLINT,
   NOMBRECARRERA        VARCHAR2(100)        not null,
   constraint PK_CAR_CAT_CARRERAS primary key (IDCARRERA)
)
/

/*==============================================================*/
/* Table: CAR_CAT_ESPECIALIDAD                                  */
/*==============================================================*/
create table CAR_CAT_ESPECIALIDAD 
(
   IDESPECIALIDAD       SMALLINT             not null,
   ESPECIALIDAD         VARCHAR2(100)        not null,
   constraint PK_CAR_CAT_ESPECIALIDAD primary key (IDESPECIALIDAD)
)
/

/*==============================================================*/
/* Table: CLA_CLASE                                             */
/*==============================================================*/
create table CLA_CLASE 
(
   IDCLASE              NUMBER(6)            not null,
   IDMATERIA            SMALLINT,
   IDPROFESOR           INTEGER,
   IDCARRERA            SMALLINT,
   IDSEMESTRE           INTEGER,
   constraint PK_CLA_CLASE primary key (IDCLASE)
)
/

/*==============================================================*/
/* Table: CLA_HORARIO                                           */
/*==============================================================*/
create table CLA_HORARIO 
(
   IDHORARIO            NUMBER(6)            not null,
   IDHORARIOMATERIA     SMALLINT,
   IDCLASE              INTEGER,
   constraint PK_CLA_HORARIO primary key (IDHORARIO)
)
/

/*==============================================================*/
/* Table: DIR_CAT_COLONIAS                                      */
/*==============================================================*/
create table DIR_CAT_COLONIAS 
(
   IDCOLONIA            INTEGER              not null,
   IDESTADO             INTEGER,
   IDDELGMUNICIPIO      INTEGER,
   IDTIPOASENTAMIENTO   INTEGER,
   IDTIPOZONA           INTEGER,
   IDMUNICIPIO          INTEGER,
   CODIGOPOSTAL         INTEGER,
   NOMBRECOLONIA        VARCHAR2(100),
   constraint PK_DIR_CAT_COLONIAS primary key (IDCOLONIA)
)
/

/*==============================================================*/
/* Table: DIR_CAT_DELG_MUNICIPIO                                */
/*==============================================================*/
create table DIR_CAT_DELG_MUNICIPIO 
(
   IDDELGMUNICIPIO      INTEGER              not null,
   IDESTADO             INTEGER,
   IDMUNICIPIO          INTEGER,
   NOMBREDELGMUNICIPIO  VARCHAR2(50),
   constraint PK_DIR_CAT_DELG_MUNICIPIO primary key (IDDELGMUNICIPIO)
)
/

/*==============================================================*/
/* Table: DIR_CAT_ESTADO                                        */
/*==============================================================*/
create table DIR_CAT_ESTADO 
(
   IDESTADO             INTEGER              not null,
   NOMBREESTADO         VARCHAR2(50),
   NOMBREOFICIAL        VARCHAR2(50),
   constraint PK_DIR_CAT_ESTADO primary key (IDESTADO)
)
/

/*==============================================================*/
/* Table: DIR_CAT_TIPO_ASENTAMIENTO                             */
/*==============================================================*/
create table DIR_CAT_TIPO_ASENTAMIENTO 
(
   IDTIPOASENTAMIENTO   INTEGER              not null,
   TIPOASENTAMIENTO     VARCHAR2(100)        not null,
   constraint PK_DIR_CAT_TIPO_ASENTAMIENTO primary key (IDTIPOASENTAMIENTO)
)
/

/*==============================================================*/
/* Table: DIR_CAT_TIPO_ZONA                                     */
/*==============================================================*/
create table DIR_CAT_TIPO_ZONA 
(
   IDTIPOZONA           INTEGER              not null,
   TIPOZONA             VARCHAR2(100),
   constraint PK_DIR_CAT_TIPO_ZONA primary key (IDTIPOZONA)
)
/

/*==============================================================*/
/* Table: DIR_DIRECCIONES                                       */
/*==============================================================*/
create table DIR_DIRECCIONES 
(
   IDDIRECCION          NUMBER(6)            not null,
   IDESTADO             INTEGER,
   IDMUNICIPIO          INTEGER,
   IDCOLONIA            INTEGER,
   CALLE                VARCHAR2(100),
   NOEXT                VARCHAR2(30),
   NOINT                VARCHAR2(30),
   REFERENCIAS          VARCHAR2(150),
   constraint PK_DIR_DIRECCIONES primary key (IDDIRECCION)
)
/

/*==============================================================*/
/* Table: GEN_CAT_SEMESTRE_PERIODOS                             */
/*==============================================================*/
create table GEN_CAT_SEMESTRE_PERIODOS 
(
   IDSEMESTRE           INTEGER              not null,
   NOMBRESEMESTRE       VARCHAR2(100),
   ABREVIATURA          VARCHAR2(50),
   FECHAINICIO          DATE                 not null,
   FECHAFIN             DATE                 not null,
   constraint PK_GEN_CAT_SEMESTRE_PERIODOS primary key (IDSEMESTRE)
)
/

/*==============================================================*/
/* Table: HOR_CALENDARIO_ESCOLAR                                */
/*==============================================================*/
create table HOR_CALENDARIO_ESCOLAR 
(
   IDCALENDARIOESCOLAR  INTEGER              not null,
   IDDIASFESTIVOS       INTEGER,
   IDDIA                SMALLINT,
   FECHA                DATE,
   ESFINDESEMANA        SMALLINT,
   SEMANA               INTEGER,
   constraint PK_HOR_CALENDARIO_ESCOLAR primary key (IDCALENDARIOESCOLAR)
)
/

/*==============================================================*/
/* Table: HOR_CAT_DIAS_FESTIVOS                                 */
/*==============================================================*/
create table HOR_CAT_DIAS_FESTIVOS 
(
   IDDIASFESTIVOS       INTEGER              not null,
   IDTIPOFERIADO        INTEGER,
   NOMBREDIAFESTIVO     VARCHAR2(100)        not null,
   FECHADIAFESTIVO      DATE                 not null,
   constraint PK_HOR_CAT_DIAS_FESTIVOS primary key (IDDIASFESTIVOS)
)
/

/*==============================================================*/
/* Table: HOR_CAT_DIAS_SEMANA                                   */
/*==============================================================*/
create table HOR_CAT_DIAS_SEMANA 
(
   IDDIA                SMALLINT             not null,
   DIASEMANA            VARCHAR2(20)         not null,
   constraint PK_HOR_CAT_DIAS_SEMANA primary key (IDDIA)
)
/

/*==============================================================*/
/* Table: HOR_CAT_HORAS                                         */
/*==============================================================*/
create table HOR_CAT_HORAS 
(
   IDHORA               SMALLINT             not null,
   IDTURNO              SMALLINT,
   NOMBREHORA           VARCHAR2(100)        not null,
   HORAINICIO           DATE                 not null,
   HORATERMINO          DATE                 not null,
   DESCRIPCION          VARCHAR2(100),
   constraint PK_HOR_CAT_HORAS primary key (IDHORA)
)
/

/*==============================================================*/
/* Table: HOR_CAT_TIPO_DIA_FERIADO                              */
/*==============================================================*/
create table HOR_CAT_TIPO_DIA_FERIADO 
(
   IDTIPOFERIADO        INTEGER              not null,
   NOMBRETIPOFERIADO    VARCHAR2(100)        not null,
   DESCRIPCION          VARCHAR2(50),
   constraint PK_HOR_CAT_TIPO_DIA_FERIADO primary key (IDTIPOFERIADO)
)
/

/*==============================================================*/
/* Table: HOR_CAT_TURNO                                         */
/*==============================================================*/
create table HOR_CAT_TURNO 
(
   IDTURNO              SMALLINT             not null,
   NOMBRETURNO          VARCHAR2(100)        not null,
   constraint PK_HOR_CAT_TURNO primary key (IDTURNO)
)
/

/*==============================================================*/
/* Table: HOR_HORAS_POR_DIA                                     */
/*==============================================================*/
create table HOR_HORAS_POR_DIA 
(
   IDHORASPORDIA        NUMBER(6)            not null,
   IDHORA               SMALLINT,
   IDDIA                SMALLINT,
   constraint PK_HOR_HORAS_POR_DIA primary key (IDHORASPORDIA)
)
/

/*==============================================================*/
/* Table: MAT_ARBOL_MATERIA                                     */
/*==============================================================*/
create table MAT_ARBOL_MATERIA 
(
   IDMATERIADEPENDENCIA INTEGER              not null,
   IDMATERIADEPENDENCIAHIJO INTEGER,
   IDMATERIA            SMALLINT,
   constraint PK_MAT_ARBOL_MATERIA primary key (IDMATERIADEPENDENCIA)
)
/

/*==============================================================*/
/* Table: MAT_CAT_CREDITOS_POR_HORAS                            */
/*==============================================================*/
create table MAT_CAT_CREDITOS_POR_HORAS 
(
   IDCREDITOS           INTEGER              not null,
   CREDITOSMINIMOS      NUMBER(5,2),
   CREDITOSMAXIMOS      NUMBER(5,2),
   HORASMINIMASSEMANA   NUMBER(5,2),
   HORASMAXIMASSEMANA   NUMBER(5,2),
   HORASTOTALESPORSEMESTRE NUMBER(5,2),
   constraint PK_MAT_CAT_CREDITOS_POR_HORAS primary key (IDCREDITOS)
)
/

/*==============================================================*/
/* Table: MAT_CAT_MATERIAS                                      */
/*==============================================================*/
create table MAT_CAT_MATERIAS 
(
   IDMATERIA            SMALLINT             not null,
   NOMBREMATERIA        VARCHAR2(100)        not null,
   CREDITOS             NUMBER(5,2),
   OPTATIVA             SMALLINT,
   constraint PK_MAT_CAT_MATERIAS primary key (IDMATERIA)
)
/

/*==============================================================*/
/* Table: MAT_HORARIO_POR_MATERIA                               */
/*==============================================================*/
create table MAT_HORARIO_POR_MATERIA 
(
   IDHORARIOMATERIA     SMALLINT             not null,
   IDHORASPORDIA        SMALLINT,
   IDAULACLASES         SMALLINT,
   constraint PK_MAT_HORARIO_POR_MATERIA primary key (IDHORARIOMATERIA)
)
/

/*==============================================================*/
/* Table: MAT_MATERIAS_POR_CARRERA                              */
/*==============================================================*/
create table MAT_MATERIAS_POR_CARRERA 
(
   IDMATERIACARRERA     INTEGER              not null,
   IDMATERIA            SMALLINT,
   IDCARRERA            SMALLINT,
   constraint PK_MAT_MATERIAS_POR_CARRERA primary key (IDMATERIACARRERA)
)
/

/*==============================================================*/
/* Table: PER_CAT_NACIONALIDAD                                  */
/*==============================================================*/
create table PER_CAT_NACIONALIDAD 
(
   CVE_NACIONALIDAD     INTEGER              not null,
   NOMBRE_PAIS          VARCHAR2(50),
   constraint PK_PER_CAT_NACIONALIDAD primary key (CVE_NACIONALIDAD)
)
/

/*==============================================================*/
/* Table: PER_CAT_TELEFONOS                                     */
/*==============================================================*/
create table PER_CAT_TELEFONOS 
(
   ID_TELEFONOS         NUMBER(6)            not null,
   TELEFONO_FIJO_DOMICILIO VARCHAR2(20),
   TELEFONO_FIJO_TRABAJO VARCHAR2(20),
   TELEFONO_CELULAR_PERSONAL VARCHAR2(20),
   TELEFONO_CELULAR_TRABAJO VARCHAR2(20),
   FAX                  VARCHAR2(20),
   constraint PK_PER_CAT_TELEFONOS primary key (ID_TELEFONOS)
)
/

/*==============================================================*/
/* Table: PER_CAT_TIPO_PERSONA                                  */
/*==============================================================*/
create table PER_CAT_TIPO_PERSONA 
(
   ID_TIPO_PERSONA      INTEGER              not null,
   TIPO_PERSONA         VARCHAR2(50),
   DESCRIPCION          VARCHAR2(50),
   constraint PK_PER_CAT_TIPO_PERSONA primary key (ID_TIPO_PERSONA)
)
/

/*==============================================================*/
/* Table: PER_FOTOGRAFIA                                        */
/*==============================================================*/
create table PER_FOTOGRAFIA 
(
   IDFOTO               NUMBER(6)            not null,
   NOMBRE               VARCHAR2(250),
   EXTENCION            VARCHAR2(10),
   FOTOGRAFIA           VBINMAX,
   LONGITUD             INTEGER,
   constraint PK_PER_FOTOGRAFIA primary key (IDFOTO)
)
/

/*==============================================================*/
/* Table: PER_MEDIOS_ELECTRONICOS                               */
/*==============================================================*/
create table PER_MEDIOS_ELECTRONICOS 
(
   ID_MEDIOS_ELECTRONICOS NUMBER(6)            not null,
   CORREO_ELECTRONICO_UNIVERSIDAD VARCHAR2(100),
   CORREO_ELECTRONICO_PERSONAL VARCHAR2(100),
   FACEBOOK             VARCHAR2(50),
   TWITTER              VARCHAR2(50),
   constraint PK_PER_MEDIOS_ELECTRONICOS primary key (ID_MEDIOS_ELECTRONICOS)
)
/

/*==============================================================*/
/* Table: PER_PERSONAS                                          */
/*==============================================================*/
create table PER_PERSONAS 
(
   ID_PERSONA           NUMBER(6)            not null,
   ID_PER_LINKID        VARCHAR2(50)         not null,
   IDDIRECCION          INTEGER,
   CVE_NACIONALIDAD     INTEGER,
   ID_TELEFONOS         INTEGER,
   ID_TIPO_PERSONA      INTEGER,
   ID_USUARIO           INTEGER,
   ID_MEDIOS_ELECTRONICOS INTEGER,
   IDFOTO               INTEGER,
   NOMBRE               VARCHAR2(50)         not null,
   A_PATERNO            VARCHAR2(50),
   A_MATERNO            VARCHAR2(50),
   NOMBRE_COMPLETO      VARCHAR2(100)        not null,
   FECHA_NAC            DATE                 not null,
   FECHAINGRESO         DATE                 not null,
   SEXO                 VARCHAR2(20)         not null,
   CURP                 VARCHAR2(30),
   RFC                  VARCHAR2(30),
   IMSS                 VARCHAR2(20),
   constraint PK_PER_PERSONAS primary key (ID_PERSONA, ID_PER_LINKID)
)
/

/*==============================================================*/
/* Table: PRO_PROFESOR                                          */
/*==============================================================*/
create table PRO_PROFESOR 
(
   IDPROFESOR           NUMBER(6)            not null,
   ID_PERSONA           INTEGER,
   ID_PER_LINKID        VARCHAR2(50),
   constraint PK_PRO_PROFESOR primary key (IDPROFESOR)
)
/

/*==============================================================*/
/* Table: SIS_AADM_APLICACIONES                                 */
/*==============================================================*/
create table SIS_AADM_APLICACIONES 
(
   IDAPLICACIONES       NUMBER(6)            not null,
   IDMENU               INTEGER,
   IDTABPAGES           INTEGER,
   ID_NIVEL_USUARIO     INTEGER,
   ID_TIPO_USUARIO      INTEGER,
   constraint PK_SIS_AADM_APLICACIONES primary key (IDAPLICACIONES)
)
/

/*==============================================================*/
/* Table: SIS_AADM_ARBOLMENUS                                   */
/*==============================================================*/
create table SIS_AADM_ARBOLMENUS 
(
   IDMENU               INTEGER              not null,
   NOMBRENODO           VARCHAR2(100),
   RUTA                 CLOB,
   IDMENUPADRE          INTEGER              not null,
   constraint PK_SIS_AADM_ARBOLMENUS primary key (IDMENU)
)
/

/*==============================================================*/
/* Table: SIS_CAT_TABPAGES                                      */
/*==============================================================*/
create table SIS_CAT_TABPAGES 
(
   IDTABPAGES           INTEGER              not null,
   RUTATAB              VARCHAR2(100),
   NOMBRETABPAGE        VARCHAR2(100),
   constraint PK_SIS_CAT_TABPAGES primary key (IDTABPAGES)
)
/

/*==============================================================*/
/* Table: SIS_WADM_ARBOLMENU                                    */
/*==============================================================*/
create table SIS_WADM_ARBOLMENU 
(
   IDMENU               INTEGER              not null,
   ID_TIPO_USUARIO      INTEGER,
   ID_NIVEL_USUARIO     INTEGER,
   IDMENUPADRE          INTEGER,
   NOMBRE               VARCHAR2(100),
   LINK                 VARCHAR2(100),
   constraint PK_SIS_WADM_ARBOLMENU primary key (IDMENU)
)
/

/*==============================================================*/
/* Table: SIS_WADM_ARBOLMENU_MVC                                */
/*==============================================================*/
create table SIS_WADM_ARBOLMENU_MVC 
(
   IDMENU               INTEGER              not null,
   IDMENUPADRE          INTEGER,
   ID_NIVEL_USUARIO     INTEGER,
   ID_TIPO_USUARIO      INTEGER,
   NOMBRE               VARCHAR2(100),
   CONTROLLER           VARCHAR2(100),
   ACTION               VARCHAR2(100),
   URL                  VARCHAR2(100),
   constraint PK_SIS_WADM_ARBOLMENU_MVC primary key (IDMENU)
)
/

/*==============================================================*/
/* Table: US_CAT_ESTATUS_USUARIO                                */
/*==============================================================*/
create table US_CAT_ESTATUS_USUARIO 
(
   ID_ESTATUS_USUARIOS  INTEGER              not null,
   ESTATUS_USUARIO      VARCHAR2(30)         not null,
   DESCRIPCION          VARCHAR2(150)        not null,
   constraint PK_US_CAT_ESTATUS_USUARIO primary key (ID_ESTATUS_USUARIOS)
)
/

/*==============================================================*/
/* Table: US_CAT_NIVEL_USUARIO                                  */
/*==============================================================*/
create table US_CAT_NIVEL_USUARIO 
(
   ID_NIVEL_USUARIO     INTEGER              not null,
   NIVEL_USUARIO        VARCHAR2(30)         not null,
   DESCRIPCION          VARCHAR2(150)        not null,
   constraint PK_US_CAT_NIVEL_USUARIO primary key (ID_NIVEL_USUARIO)
)
/

/*==============================================================*/
/* Table: US_CAT_TIPO_USUARIO                                   */
/*==============================================================*/
create table US_CAT_TIPO_USUARIO 
(
   ID_TIPO_USUARIO      INTEGER              not null,
   TIPO_USUARIO         VARCHAR2(30)         not null,
   DESCRIPCION          VARCHAR2(150),
   constraint PK_US_CAT_TIPO_USUARIO primary key (ID_TIPO_USUARIO)
)
/

/*==============================================================*/
/* Table: US_USUARIOS                                           */
/*==============================================================*/
create table US_USUARIOS 
(
   ID_USUARIO           NUMBER(6)            not null,
   ID_TIPO_USUARIO      INTEGER,
   ID_ESTATUS_USUARIOS  INTEGER,
   ID_NIVEL_USUARIO     INTEGER,
   USUARIO              VARCHAR2(50),
   CONTRASENA           VARCHAR2(30),
   constraint PK_US_USUARIOS primary key (ID_USUARIO)
)
/

alter table ALU_ALUMNOS
   add constraint FK_ALU_ALUM_REFERENCE_PER_PERS foreign key (ID_PERSONA, ID_PER_LINKID)
      references PER_PERSONAS (ID_PERSONA, ID_PER_LINKID)
/

alter table ALU_HORARIO
   add constraint FK_ALU_HORA_REFERENCE_ALU_ALUM foreign key (IDALUMNOS)
      references ALU_ALUMNOS (IDALUMNOS)
/

alter table ALU_HORARIO
   add constraint FK_ALU_HORA_REFERENCE_CLA_CLAS foreign key (IDCLASE)
      references CLA_CLASE (IDCLASE)
/

alter table AUL_AULA_CLASES
   add constraint FK_AUL_AULA_REFERENCE_AUL_CAT_ foreign key (IDTIPOAULA)
      references AUL_CAT_TIPO_AULA (IDTIPOAULA)
/

alter table CAL_ALUMNO_KARDEX
   add constraint FK_CAL_ALUM_REFERENCE_ALU_ALUM foreign key (IDALUMNOS)
      references ALU_ALUMNOS (IDALUMNOS)
/

alter table CAL_ALUMNO_KARDEX
   add constraint FK_CAL_ALUM_REFERENCE_CAL_CALI foreign key (IDCALIFICACION)
      references CAL_CALIFICACIONES (IDCALIFICACION)
/

alter table CAL_CALIFICACIONES
   add constraint FK_CAL_CALI_REFERENCE_ALU_ALUM foreign key (IDALUMNOS)
      references ALU_ALUMNOS (IDALUMNOS)
/

alter table CAL_CALIFICACIONES
   add constraint FK_CAL_CALI_REFERENCE_MAT_CAT_ foreign key (IDMATERIA)
      references MAT_CAT_MATERIAS (IDMATERIA)
/

alter table CAL_CALIFICACIONES
   add constraint FK_CAL_CALI_REF_CAL_C_CAL_CAT_ foreign key (IDTIPOEVALUACION)
      references CAL_CAT_TIPO_EVALUACION (IDTIPOEVALUACION)
/

alter table CAL_CALIFICACIONES
   add constraint FK_CAL_CALI_REF_CAL_C_CAL_CALI foreign key (IDCALIFICACIONESFECHAS)
      references CAL_CALIFICACIONES_FECHAS (IDCALIFICACIONESFECHAS)
/

alter table CAL_CALIFICACION_CLASE
   add constraint FK_CAL_CALI_CAL_CAL_C_CAL_CAT_ foreign key (IDTIPOEVALUACION)
      references CAL_CAT_TIPO_EVALUACION (IDTIPOEVALUACION)
/

alter table CAL_CALIFICACION_CLASE
   add constraint FK_CAL_CALI_CAL_CLA_C_CAL_CALI foreign key (IDCALIFICACION)
      references CAL_CALIFICACIONES (IDCALIFICACION)
/

alter table CAL_CALIFICACION_CLASE
   add constraint FK_CAL_CALI_REFERENCE_CLA_CLAS foreign key (IDCLASE)
      references CLA_CLASE (IDCLASE)
/

alter table CAR_CAT_CARRERAS
   add constraint FK_CAR_CAT__REFERENCE_CAR_CAT_ foreign key (IDESPECIALIDAD)
      references CAR_CAT_ESPECIALIDAD (IDESPECIALIDAD)
/

alter table CLA_CLASE
   add constraint FK_CLA_CLAS_REFERENCE_MAT_CAT_ foreign key (IDMATERIA)
      references MAT_CAT_MATERIAS (IDMATERIA)
/

alter table CLA_CLASE
   add constraint FK_CLA_CLAS_REFERENCE_PRO_PROF foreign key (IDPROFESOR)
      references PRO_PROFESOR (IDPROFESOR)
/

alter table CLA_CLASE
   add constraint FK_CLA_CLAS_REFERENCE_CAR_CAT_ foreign key (IDCARRERA)
      references CAR_CAT_CARRERAS (IDCARRERA)
/

alter table CLA_CLASE
   add constraint FK_CLA_CLAS_REFERENCE_GEN_CAT_ foreign key (IDSEMESTRE)
      references GEN_CAT_SEMESTRE_PERIODOS (IDSEMESTRE)
/

alter table CLA_HORARIO
   add constraint FK_CLA_HORA_REFERENCE_CLA_CLAS foreign key (IDCLASE)
      references CLA_CLASE (IDCLASE)
/

alter table CLA_HORARIO
   add constraint FK_CLA_HORA_REFERENCE_MAT_HORA foreign key (IDHORARIOMATERIA)
      references MAT_HORARIO_POR_MATERIA (IDHORARIOMATERIA)
/

alter table DIR_CAT_COLONIAS
   add constraint FK_DIR_CAT__COLONIAS__DIR_CAT_ foreign key (IDTIPOASENTAMIENTO)
      references DIR_CAT_TIPO_ASENTAMIENTO (IDTIPOASENTAMIENTO)
/

alter table DIR_CAT_COLONIAS
   add constraint FK_DIR_CAT__REFERENCE_DIR_CAT_ foreign key (IDTIPOZONA)
      references DIR_CAT_TIPO_ZONA (IDTIPOZONA)
/

alter table DIR_CAT_COLONIAS
   add constraint "Reference_26" foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
/

alter table DIR_CAT_COLONIAS
   add constraint "Reference_29" foreign key (IDDELGMUNICIPIO)
      references DIR_CAT_DELG_MUNICIPIO (IDDELGMUNICIPIO)
/

alter table DIR_CAT_DELG_MUNICIPIO
   add constraint "Reference_27" foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
/

alter table DIR_DIRECCIONES
   add constraint "Reference_28" foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
/

alter table DIR_DIRECCIONES
   add constraint "Reference_32" foreign key (IDCOLONIA)
      references DIR_CAT_COLONIAS (IDCOLONIA)
/

alter table HOR_CALENDARIO_ESCOLAR
   add constraint FK_HOR_CALE_FK_HOR_CA_HOR_CAT_ foreign key (IDDIA)
      references HOR_CAT_DIAS_SEMANA (IDDIA)
/

alter table HOR_CALENDARIO_ESCOLAR
   add constraint FK_HOR_CALE_HOR_CALEN_HOR_CAT_ foreign key (IDDIASFESTIVOS)
      references HOR_CAT_DIAS_FESTIVOS (IDDIASFESTIVOS)
/

alter table HOR_CAT_DIAS_FESTIVOS
   add constraint FK_HOR_CAT__FK_HOR_CA_HOR_CAT_ foreign key (IDTIPOFERIADO)
      references HOR_CAT_TIPO_DIA_FERIADO (IDTIPOFERIADO)
/

alter table HOR_CAT_HORAS
   add constraint FK_HOR_CAT__REFERENCE_HOR_CAT_ foreign key (IDTURNO)
      references HOR_CAT_TURNO (IDTURNO)
/

alter table HOR_HORAS_POR_DIA
   add constraint FK_HOR_HORA_HOR_HORAS_HOR_CAT_ foreign key (IDDIA)
      references HOR_CAT_DIAS_SEMANA (IDDIA)
/

alter table HOR_HORAS_POR_DIA
   add constraint FK_HOR_HORA_HOR_HOR_P_HOR_CAT_ foreign key (IDHORA)
      references HOR_CAT_HORAS (IDHORA)
/

alter table MAT_ARBOL_MATERIA
   add constraint FK_MAT_ARBO_REFERENCE_MAT_ARBO foreign key (IDMATERIADEPENDENCIAHIJO)
      references MAT_ARBOL_MATERIA (IDMATERIADEPENDENCIA)
/

alter table MAT_ARBOL_MATERIA
   add constraint FK_MAT_ARBO_REFERENCE_MAT_CAT_ foreign key (IDMATERIA)
      references MAT_CAT_MATERIAS (IDMATERIA)
/

alter table MAT_HORARIO_POR_MATERIA
   add constraint FK_MAT_HORA_REFERENCE_HOR_HORA foreign key (IDHORASPORDIA)
      references HOR_HORAS_POR_DIA (IDHORASPORDIA)
/

alter table MAT_HORARIO_POR_MATERIA
   add constraint FK_MAT_HORA_REFERENCE_AUL_AULA foreign key (IDAULACLASES)
      references AUL_AULA_CLASES (IDAULACLASES)
/

alter table MAT_MATERIAS_POR_CARRERA
   add constraint FK_MAT_MATE_REFERENCE_MAT_CAT_ foreign key (IDMATERIA)
      references MAT_CAT_MATERIAS (IDMATERIA)
/

alter table MAT_MATERIAS_POR_CARRERA
   add constraint FK_MAT_MATE_REFERENCE_CAR_CAT_ foreign key (IDCARRERA)
      references CAR_CAT_CARRERAS (IDCARRERA)
/

alter table PER_PERSONAS
   add constraint FK_PER_PERS_REFERENCE_PER_MEDI foreign key (ID_MEDIOS_ELECTRONICOS)
      references PER_MEDIOS_ELECTRONICOS (ID_MEDIOS_ELECTRONICOS)
/

alter table PER_PERSONAS
   add constraint FK_PER_PERS_REFERENCE_PER_FOTO foreign key (IDFOTO)
      references PER_FOTOGRAFIA (IDFOTO)
/

alter table PER_PERSONAS
   add constraint "Reference_33" foreign key (ID_TIPO_PERSONA)
      references PER_CAT_TIPO_PERSONA (ID_TIPO_PERSONA)
/

alter table PER_PERSONAS
   add constraint "Reference_34" foreign key (ID_USUARIO)
      references US_USUARIOS (ID_USUARIO)
/

alter table PER_PERSONAS
   add constraint "Reference_6" foreign key (IDDIRECCION)
      references DIR_DIRECCIONES (IDDIRECCION)
/

alter table PER_PERSONAS
   add constraint "Reference_8" foreign key (CVE_NACIONALIDAD)
      references PER_CAT_NACIONALIDAD (CVE_NACIONALIDAD)
/

alter table PER_PERSONAS
   add constraint "Reference_9" foreign key (ID_TELEFONOS)
      references PER_CAT_TELEFONOS (ID_TELEFONOS)
/

alter table PRO_PROFESOR
   add constraint FK_PRO_PROF_REFERENCE_PER_PERS foreign key (ID_PERSONA, ID_PER_LINKID)
      references PER_PERSONAS (ID_PERSONA, ID_PER_LINKID)
/

alter table SIS_AADM_APLICACIONES
   add constraint FK_SIS_AADM_REFERENCE_SIS_AADM foreign key (IDMENU)
      references SIS_AADM_ARBOLMENUS (IDMENU)
/

alter table SIS_AADM_APLICACIONES
   add constraint FK_SIS_AADM_REFERENCE_SIS_CAT_ foreign key (IDTABPAGES)
      references SIS_CAT_TABPAGES (IDTABPAGES)
/

alter table SIS_AADM_APLICACIONES
   add constraint FK_SIS_AADM_REFERENCE_US_CAT_N foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
/

alter table SIS_AADM_APLICACIONES
   add constraint FK_SIS_AADM_REFERENCE_US_CAT_T foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
/

alter table SIS_WADM_ARBOLMENU
   add constraint FK_SIS_WADM_REFERENCE_US_CAT_T foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
/

alter table SIS_WADM_ARBOLMENU
   add constraint FK_SIS_WADM_REFERENCE_US_CAT_N foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
/

alter table SIS_WADM_ARBOLMENU
   add constraint FK_SIS_WADM_REFERENCE_SIS_WADM foreign key (IDMENUPADRE)
      references SIS_WADM_ARBOLMENU (IDMENU)
/

alter table SIS_WADM_ARBOLMENU_MVC
   add constraint FK_MENU_US_TIPO_USUARIO foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
/

alter table SIS_WADM_ARBOLMENU_MVC
   add constraint FK_WADM_NIVEL_USUARIO foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
/

alter table SIS_WADM_ARBOLMENU_MVC
   add constraint FK_SIS_WADM_MVC_REC foreign key (IDMENUPADRE)
      references SIS_WADM_ARBOLMENU_MVC (IDMENU)
/

alter table US_USUARIOS
   add constraint "Reference_10" foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
/

alter table US_USUARIOS
   add constraint "Reference_11" foreign key (ID_ESTATUS_USUARIOS)
      references US_CAT_ESTATUS_USUARIO (ID_ESTATUS_USUARIOS)
/

alter table US_USUARIOS
   add constraint "Reference_12" foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
/


create or replace trigger COMPOUNDDELETETRIGGER_CAL_CALI
for delete on CAL_CALIFICACIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_CAL_CALI
for insert on CAL_CALIFICACIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_CAL_CALI
for update on CAL_CALIFICACIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_CAL_CALIFICACIONES before insert
on CAL_CALIFICACIONES for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDCALIFICACION" uses sequence S_CAL_CALIFICACIONES
    select S_CAL_CALIFICACIONES.NEXTVAL INTO :new.IDCALIFICACION from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_CAL_CAL2
for delete on CAL_CALIFICACIONES_FECHAS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_CAL_CAL2
for insert on CAL_CALIFICACIONES_FECHAS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_CAL_CAL2
for update on CAL_CALIFICACIONES_FECHAS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_CAL_CALIFICACIONES_FECHAS before insert
on CAL_CALIFICACIONES_FECHAS for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDCALIFICACIONESFECHAS" uses sequence S_CAL_CALIFICACIONES_FECHAS
    select S_CAL_CALIFICACIONES_FECHAS.NEXTVAL INTO :new.IDCALIFICACIONESFECHAS from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_CLA_CLA2
for delete on CLA_CLASE compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_CLA_CLA2
for insert on CLA_CLASE compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_CLA_CLA2
for update on CLA_CLASE compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_CLA_CLASE before insert
on CLA_CLASE for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDCLASE" uses sequence S_CLA_CLASE
    select S_CLA_CLASE.NEXTVAL INTO :new.IDCLASE from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_CLA_HOR2
for delete on CLA_HORARIO compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_CLA_HOR2
for insert on CLA_HORARIO compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_CLA_HOR2
for update on CLA_HORARIO compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_CLA_HORARIO before insert
on CLA_HORARIO for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDHORARIO" uses sequence S_CLA_HORARIO
    select S_CLA_HORARIO.NEXTVAL INTO :new.IDHORARIO from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_DIR_DIR2
for delete on DIR_DIRECCIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_DIR_DIR2
for insert on DIR_DIRECCIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_DIR_DIR2
for update on DIR_DIRECCIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_DIR_DIRECCIONES before insert
on DIR_DIRECCIONES for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDDIRECCION" uses sequence S_DIR_DIRECCIONES
    select S_DIR_DIRECCIONES.NEXTVAL INTO :new.IDDIRECCION from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_HOR_HOR2
for delete on HOR_HORAS_POR_DIA compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_HOR_HOR2
for insert on HOR_HORAS_POR_DIA compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_HOR_HOR2
for update on HOR_HORAS_POR_DIA compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_HOR_HORAS_POR_DIA before insert
on HOR_HORAS_POR_DIA for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDHORASPORDIA" uses sequence S_HOR_HORAS_POR_DIA
    select S_HOR_HORAS_POR_DIA.NEXTVAL INTO :new.IDHORASPORDIA from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_PER_CAT2
for delete on PER_CAT_TELEFONOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_PER_CAT2
for insert on PER_CAT_TELEFONOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_PER_CAT2
for update on PER_CAT_TELEFONOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_PER_CAT_TELEFONOS before insert
on PER_CAT_TELEFONOS for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "ID_TELEFONOS" uses sequence S_PER_CAT_TELEFONOS
    select S_PER_CAT_TELEFONOS.NEXTVAL INTO :new.ID_TELEFONOS from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_PER_FOT2
for delete on PER_FOTOGRAFIA compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_PER_FOT2
for insert on PER_FOTOGRAFIA compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_PER_FOT2
for update on PER_FOTOGRAFIA compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_PER_FOTOGRAFIA before insert
on PER_FOTOGRAFIA for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDFOTO" uses sequence S_PER_FOTOGRAFIA
    select S_PER_FOTOGRAFIA.NEXTVAL INTO :new.IDFOTO from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_PER_MED2
for delete on PER_MEDIOS_ELECTRONICOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_PER_MED2
for insert on PER_MEDIOS_ELECTRONICOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_PER_MED2
for update on PER_MEDIOS_ELECTRONICOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_PER_MEDIOS_ELECTRONICOS before insert
on PER_MEDIOS_ELECTRONICOS for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "ID_MEDIOS_ELECTRONICOS" uses sequence S_PER_MEDIOS_ELECTRONICOS
    select S_PER_MEDIOS_ELECTRONICOS.NEXTVAL INTO :new.ID_MEDIOS_ELECTRONICOS from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_PER_PER2
for delete on PER_PERSONAS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_PER_PER2
for insert on PER_PERSONAS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_PER_PER2
for update on PER_PERSONAS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_PER_PERSONAS before insert
on PER_PERSONAS for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "ID_PERSONA" uses sequence S_PER_PERSONAS
    select S_PER_PERSONAS.NEXTVAL INTO :new.ID_PERSONA from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_PRO_PRO2
for delete on PRO_PROFESOR compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_PRO_PRO2
for insert on PRO_PROFESOR compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_PRO_PRO2
for update on PRO_PROFESOR compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_PRO_PROFESOR before insert
on PRO_PROFESOR for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDPROFESOR" uses sequence S_PRO_PROFESOR
    select S_PRO_PROFESOR.NEXTVAL INTO :new.IDPROFESOR from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_SIS_AAD2
for delete on SIS_AADM_APLICACIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_SIS_AAD2
for insert on SIS_AADM_APLICACIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_SIS_AAD2
for update on SIS_AADM_APLICACIONES compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_SIS_AADM_APLICACIONES before insert
on SIS_AADM_APLICACIONES for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "IDAPLICACIONES" uses sequence S_SIS_AADM_APLICACIONES
    select S_SIS_AADM_APLICACIONES.NEXTVAL INTO :new.IDAPLICACIONES from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger COMPOUNDDELETETRIGGER_US_USUA2
for delete on US_USUARIOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDINSERTTRIGGER_US_USUA2
for insert on US_USUARIOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create or replace trigger COMPOUNDUPDATETRIGGER_US_USUA2
for update on US_USUARIOS compound trigger
// Declaration
// Body
  before statement is
  begin
     NULL;
  end before statement;

  before each row is
  begin
     NULL;
  end before each row;

  after each row is
  begin
     NULL;
  end after each row;

  after statement is
  begin
     NULL;
  end after statement;

END
/


create trigger TIB_US_USUARIOS before insert
on US_USUARIOS for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    --  Column "ID_USUARIO" uses sequence S_US_USUARIOS
    select S_US_USUARIOS.NEXTVAL INTO :new.ID_USUARIO from dual;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/

