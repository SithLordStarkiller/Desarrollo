/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     14/01/2016 01:17:10 p. m.                    */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ALU_ALUMNOS') and o.name = 'FK_ALU_ALUM_REFERENCE_PER_PERS')
alter table ALU_ALUMNOS
   drop constraint FK_ALU_ALUM_REFERENCE_PER_PERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ALU_HORARIO') and o.name = 'FK_ALU_HORA_REFERENCE_ALU_ALUM')
alter table ALU_HORARIO
   drop constraint FK_ALU_HORA_REFERENCE_ALU_ALUM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ALU_HORARIO') and o.name = 'FK_ALU_HORA_REFERENCE_CLA_CLAS')
alter table ALU_HORARIO
   drop constraint FK_ALU_HORA_REFERENCE_CLA_CLAS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('AUL_AULA_CLASES') and o.name = 'FK_AUL_AULA_REFERENCE_AUL_CAT_')
alter table AUL_AULA_CLASES
   drop constraint FK_AUL_AULA_REFERENCE_AUL_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_ALUMNO_KARDEX') and o.name = 'FK_CAL_ALUM_REFERENCE_ALU_ALUM')
alter table CAL_ALUMNO_KARDEX
   drop constraint FK_CAL_ALUM_REFERENCE_ALU_ALUM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_ALUMNO_KARDEX') and o.name = 'FK_CAL_ALUM_REFERENCE_CAL_CALI')
alter table CAL_ALUMNO_KARDEX
   drop constraint FK_CAL_ALUM_REFERENCE_CAL_CALI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_CALIFICACIONES') and o.name = 'FK_CAL_CALI_REFERENCE_ALU_ALUM')
alter table CAL_CALIFICACIONES
   drop constraint FK_CAL_CALI_REFERENCE_ALU_ALUM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_CALIFICACIONES') and o.name = 'FK_CAL_CALI_REFERENCE_MAT_CAT_')
alter table CAL_CALIFICACIONES
   drop constraint FK_CAL_CALI_REFERENCE_MAT_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_CALIFICACIONES') and o.name = 'FK_CAL_CALI_REF_CAL_C_CAL_CAT_')
alter table CAL_CALIFICACIONES
   drop constraint FK_CAL_CALI_REF_CAL_C_CAL_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_CALIFICACIONES') and o.name = 'FK_CAL_CALI_REF_CAL_C_CAL_CALI')
alter table CAL_CALIFICACIONES
   drop constraint FK_CAL_CALI_REF_CAL_C_CAL_CALI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_CALIFICACION_CLASE') and o.name = 'FK_CAL_CALI_CAL_CAL_C_CAL_CAT_')
alter table CAL_CALIFICACION_CLASE
   drop constraint FK_CAL_CALI_CAL_CAL_C_CAL_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_CALIFICACION_CLASE') and o.name = 'FK_CAL_CALI_CAL_CLA_C_CAL_CALI')
alter table CAL_CALIFICACION_CLASE
   drop constraint FK_CAL_CALI_CAL_CLA_C_CAL_CALI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAL_CALIFICACION_CLASE') and o.name = 'FK_CAL_CALI_REFERENCE_CLA_CLAS')
alter table CAL_CALIFICACION_CLASE
   drop constraint FK_CAL_CALI_REFERENCE_CLA_CLAS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CAR_CAT_CARRERAS') and o.name = 'FK_CAR_CAT__REFERENCE_CAR_CAT_')
alter table CAR_CAT_CARRERAS
   drop constraint FK_CAR_CAT__REFERENCE_CAR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLA_CLASE') and o.name = 'FK_CLA_CLAS_REFERENCE_MAT_CAT_')
alter table CLA_CLASE
   drop constraint FK_CLA_CLAS_REFERENCE_MAT_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLA_CLASE') and o.name = 'FK_CLA_CLAS_REFERENCE_PRO_PROF')
alter table CLA_CLASE
   drop constraint FK_CLA_CLAS_REFERENCE_PRO_PROF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLA_CLASE') and o.name = 'FK_CLA_CLAS_REFERENCE_CAR_CAT_')
alter table CLA_CLASE
   drop constraint FK_CLA_CLAS_REFERENCE_CAR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLA_CLASE') and o.name = 'FK_CLA_CLAS_REFERENCE_GEN_CAT_')
alter table CLA_CLASE
   drop constraint FK_CLA_CLAS_REFERENCE_GEN_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLA_HORARIO') and o.name = 'FK_CLA_HORA_REFERENCE_CLA_CLAS')
alter table CLA_HORARIO
   drop constraint FK_CLA_HORA_REFERENCE_CLA_CLAS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLA_HORARIO') and o.name = 'FK_CLA_HORA_REFERENCE_MAT_HORA')
alter table CLA_HORARIO
   drop constraint FK_CLA_HORA_REFERENCE_MAT_HORA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_COLONIAS') and o.name = 'FK_DIR_CAT__COLONIAS__DIR_CAT_')
alter table DIR_CAT_COLONIAS
   drop constraint FK_DIR_CAT__COLONIAS__DIR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_COLONIAS') and o.name = 'FK_DIR_CAT__REFERENCE_DIR_CAT_')
alter table DIR_CAT_COLONIAS
   drop constraint FK_DIR_CAT__REFERENCE_DIR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_COLONIAS') and o.name = 'Reference_26')
alter table DIR_CAT_COLONIAS
   drop constraint Reference_26
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_COLONIAS') and o.name = 'Reference_29')
alter table DIR_CAT_COLONIAS
   drop constraint Reference_29
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_DELG_MUNICIPIO') and o.name = 'Reference_27')
alter table DIR_CAT_DELG_MUNICIPIO
   drop constraint Reference_27
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_DIRECCIONES') and o.name = 'Reference_28')
alter table DIR_DIRECCIONES
   drop constraint Reference_28
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_DIRECCIONES') and o.name = 'Reference_32')
alter table DIR_DIRECCIONES
   drop constraint Reference_32
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOR_CAT_HORAS') and o.name = 'FK_HOR_CAT__REFERENCE_HOR_CAT_')
alter table HOR_CAT_HORAS
   drop constraint FK_HOR_CAT__REFERENCE_HOR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOR_HORAS_POR_DIA') and o.name = 'FK_HOR_HORA_HOR_HORAS_HOR_CAT_')
alter table HOR_HORAS_POR_DIA
   drop constraint FK_HOR_HORA_HOR_HORAS_HOR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOR_HORAS_POR_DIA') and o.name = 'FK_HOR_HORA_HOR_HOR_P_HOR_CAT_')
alter table HOR_HORAS_POR_DIA
   drop constraint FK_HOR_HORA_HOR_HOR_P_HOR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MAT_ARBOL_MATERIA') and o.name = 'FK_MAT_ARBO_REFERENCE_MAT_ARBO')
alter table MAT_ARBOL_MATERIA
   drop constraint FK_MAT_ARBO_REFERENCE_MAT_ARBO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MAT_ARBOL_MATERIA') and o.name = 'FK_MAT_ARBO_REFERENCE_MAT_CAT_')
alter table MAT_ARBOL_MATERIA
   drop constraint FK_MAT_ARBO_REFERENCE_MAT_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MAT_CAT_MATERIAS') and o.name = 'FK_MAT_CAT__REFERENCE_CAR_CAT_')
alter table MAT_CAT_MATERIAS
   drop constraint FK_MAT_CAT__REFERENCE_CAR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MAT_HORARIO_POR_MATERIA') and o.name = 'FK_MAT_HORA_REFERENCE_HOR_HORA')
alter table MAT_HORARIO_POR_MATERIA
   drop constraint FK_MAT_HORA_REFERENCE_HOR_HORA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MAT_HORARIO_POR_MATERIA') and o.name = 'FK_MAT_HORA_REFERENCE_AUL_AULA')
alter table MAT_HORARIO_POR_MATERIA
   drop constraint FK_MAT_HORA_REFERENCE_AUL_AULA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONAS') and o.name = 'FK_PER_PERS_REFERENCE_PER_MEDI')
alter table PER_PERSONAS
   drop constraint FK_PER_PERS_REFERENCE_PER_MEDI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONAS') and o.name = 'FK_PER_PERS_REFERENCE_PER_FOTO')
alter table PER_PERSONAS
   drop constraint FK_PER_PERS_REFERENCE_PER_FOTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONAS') and o.name = 'Reference_33')
alter table PER_PERSONAS
   drop constraint Reference_33
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONAS') and o.name = 'Reference_34')
alter table PER_PERSONAS
   drop constraint Reference_34
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONAS') and o.name = 'Reference_6')
alter table PER_PERSONAS
   drop constraint Reference_6
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONAS') and o.name = 'Reference_8')
alter table PER_PERSONAS
   drop constraint Reference_8
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONAS') and o.name = 'Reference_9')
alter table PER_PERSONAS
   drop constraint Reference_9
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PRO_PROFESOR') and o.name = 'FK_PRO_PROF_REFERENCE_PER_PERS')
alter table PRO_PROFESOR
   drop constraint FK_PRO_PROF_REFERENCE_PER_PERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_AADM_APLICACIONES') and o.name = 'FK_SIS_AADM_REFERENCE_SIS_AADM')
alter table SIS_AADM_APLICACIONES
   drop constraint FK_SIS_AADM_REFERENCE_SIS_AADM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_AADM_APLICACIONES') and o.name = 'FK_SIS_AADM_REFERENCE_SIS_CAT_')
alter table SIS_AADM_APLICACIONES
   drop constraint FK_SIS_AADM_REFERENCE_SIS_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_AADM_APLICACIONES') and o.name = 'FK_SIS_AADM_REFERENCE_US_CAT_N')
alter table SIS_AADM_APLICACIONES
   drop constraint FK_SIS_AADM_REFERENCE_US_CAT_N
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_AADM_APLICACIONES') and o.name = 'FK_SIS_AADM_REFERENCE_US_CAT_T')
alter table SIS_AADM_APLICACIONES
   drop constraint FK_SIS_AADM_REFERENCE_US_CAT_T
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_WADM_ARBOLMENU') and o.name = 'FK_SIS_WADM_REFERENCE_US_CAT_T')
alter table SIS_WADM_ARBOLMENU
   drop constraint FK_SIS_WADM_REFERENCE_US_CAT_T
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_WADM_ARBOLMENU') and o.name = 'FK_SIS_WADM_REFERENCE_US_CAT_N')
alter table SIS_WADM_ARBOLMENU
   drop constraint FK_SIS_WADM_REFERENCE_US_CAT_N
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_WADM_ARBOLMENU') and o.name = 'FK_SIS_WADM_REFERENCE_SIS_WADM')
alter table SIS_WADM_ARBOLMENU
   drop constraint FK_SIS_WADM_REFERENCE_SIS_WADM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_WADM_ARBOLMENU_MVC') and o.name = 'FK_MENU_US_TIPO_USUARIO')
alter table SIS_WADM_ARBOLMENU_MVC
   drop constraint FK_MENU_US_TIPO_USUARIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_WADM_ARBOLMENU_MVC') and o.name = 'FK_WADM_NIVEL_USUARIO')
alter table SIS_WADM_ARBOLMENU_MVC
   drop constraint FK_WADM_NIVEL_USUARIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_WADM_ARBOLMENU_MVC') and o.name = 'FK_SIS_WADM_MVC_REC')
alter table SIS_WADM_ARBOLMENU_MVC
   drop constraint FK_SIS_WADM_MVC_REC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'Reference_10')
alter table US_USUARIOS
   drop constraint Reference_10
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'Reference_11')
alter table US_USUARIOS
   drop constraint Reference_11
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'Reference_12')
alter table US_USUARIOS
   drop constraint Reference_12
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ALU_ALUMNOS')
            and   type = 'U')
   drop table ALU_ALUMNOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ALU_HORARIO')
            and   type = 'U')
   drop table ALU_HORARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AUL_AULA_CLASES')
            and   type = 'U')
   drop table AUL_AULA_CLASES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AUL_CAT_TIPO_AULA')
            and   type = 'U')
   drop table AUL_CAT_TIPO_AULA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAL_ALUMNO_KARDEX')
            and   type = 'U')
   drop table CAL_ALUMNO_KARDEX
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAL_CALIFICACIONES')
            and   type = 'U')
   drop table CAL_CALIFICACIONES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAL_CALIFICACIONES_FECHAS')
            and   type = 'U')
   drop table CAL_CALIFICACIONES_FECHAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAL_CALIFICACION_CLASE')
            and   type = 'U')
   drop table CAL_CALIFICACION_CLASE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAL_CAT_TIPO_EVALUACION')
            and   type = 'U')
   drop table CAL_CAT_TIPO_EVALUACION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAR_CAT_CARRERAS')
            and   type = 'U')
   drop table CAR_CAT_CARRERAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAR_CAT_ESPECIALIDAD')
            and   type = 'U')
   drop table CAR_CAT_ESPECIALIDAD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLA_CLASE')
            and   type = 'U')
   drop table CLA_CLASE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLA_HORARIO')
            and   type = 'U')
   drop table CLA_HORARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_COLONIAS')
            and   type = 'U')
   drop table DIR_CAT_COLONIAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_DELG_MUNICIPIO')
            and   type = 'U')
   drop table DIR_CAT_DELG_MUNICIPIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_ESTADO')
            and   type = 'U')
   drop table DIR_CAT_ESTADO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_TIPO_ASENTAMIENTO')
            and   type = 'U')
   drop table DIR_CAT_TIPO_ASENTAMIENTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_TIPO_ZONA')
            and   type = 'U')
   drop table DIR_CAT_TIPO_ZONA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_DIRECCIONES')
            and   type = 'U')
   drop table DIR_DIRECCIONES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GEN_CAT_SEMESTRE_PERIODOS')
            and   type = 'U')
   drop table GEN_CAT_SEMESTRE_PERIODOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOR_CAT_DIAS_SEMANA')
            and   type = 'U')
   drop table HOR_CAT_DIAS_SEMANA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOR_CAT_HORAS')
            and   type = 'U')
   drop table HOR_CAT_HORAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOR_CAT_TURNO')
            and   type = 'U')
   drop table HOR_CAT_TURNO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOR_HORAS_POR_DIA')
            and   type = 'U')
   drop table HOR_HORAS_POR_DIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MAT_ARBOL_MATERIA')
            and   type = 'U')
   drop table MAT_ARBOL_MATERIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MAT_CAT_MATERIAS')
            and   type = 'U')
   drop table MAT_CAT_MATERIAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MAT_HORARIO_POR_MATERIA')
            and   type = 'U')
   drop table MAT_HORARIO_POR_MATERIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PER_CAT_NACIONALIDAD')
            and   type = 'U')
   drop table PER_CAT_NACIONALIDAD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PER_CAT_TELEFONOS')
            and   type = 'U')
   drop table PER_CAT_TELEFONOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PER_CAT_TIPO_PERSONA')
            and   type = 'U')
   drop table PER_CAT_TIPO_PERSONA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PER_FOTOGRAFIA')
            and   type = 'U')
   drop table PER_FOTOGRAFIA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PER_MEDIOS_ELECTRONICOS')
            and   type = 'U')
   drop table PER_MEDIOS_ELECTRONICOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PER_PERSONAS')
            and   type = 'U')
   drop table PER_PERSONAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PRO_PROFESOR')
            and   type = 'U')
   drop table PRO_PROFESOR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SIS_AADM_APLICACIONES')
            and   type = 'U')
   drop table SIS_AADM_APLICACIONES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SIS_AADM_ARBOLMENUS')
            and   type = 'U')
   drop table SIS_AADM_ARBOLMENUS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SIS_CAT_TABPAGES')
            and   type = 'U')
   drop table SIS_CAT_TABPAGES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SIS_WADM_ARBOLMENU')
            and   type = 'U')
   drop table SIS_WADM_ARBOLMENU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SIS_WADM_ARBOLMENU_MVC')
            and   type = 'U')
   drop table SIS_WADM_ARBOLMENU_MVC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_CAT_ESTATUS_USUARIO')
            and   type = 'U')
   drop table US_CAT_ESTATUS_USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_CAT_NIVEL_USUARIO')
            and   type = 'U')
   drop table US_CAT_NIVEL_USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_CAT_TIPO_USUARIO')
            and   type = 'U')
   drop table US_CAT_TIPO_USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_USUARIOS')
            and   type = 'U')
   drop table US_USUARIOS
go

/*==============================================================*/
/* Table: ALU_ALUMNOS                                           */
/*==============================================================*/
create table ALU_ALUMNOS (
   IDALUMNOS            int                  not null,
   ID_PERSONA           int                  null,
   ID_PER_LINKID        varchar(50)          null,
   constraint PK_ALU_ALUMNOS primary key (IDALUMNOS)
)
go

/*==============================================================*/
/* Table: ALU_HORARIO                                           */
/*==============================================================*/
create table ALU_HORARIO (
   IDHORARIO            int                  not null,
   IDALUMNOS            int                  null,
   IDCLASE              int                  null,
   constraint PK_ALU_HORARIO primary key (IDHORARIO)
)
go

/*==============================================================*/
/* Table: AUL_AULA_CLASES                                       */
/*==============================================================*/
create table AUL_AULA_CLASES (
   IDAULACLASES         smallint             not null,
   IDTIPOAULA           smallint             null,
   AULA                 varchar(10)          not null,
   MAXLUGARES           smallint             null,
   constraint PK_AUL_AULA_CLASES primary key (IDAULACLASES)
)
go

/*==============================================================*/
/* Table: AUL_CAT_TIPO_AULA                                     */
/*==============================================================*/
create table AUL_CAT_TIPO_AULA (
   IDTIPOAULA           smallint             not null,
   TIPOAULA             varchar(100)         not null,
   DESCRIPCION          varchar(100)         not null,
   constraint PK_AUL_CAT_TIPO_AULA primary key (IDTIPOAULA)
)
go

/*==============================================================*/
/* Table: CAL_ALUMNO_KARDEX                                     */
/*==============================================================*/
create table CAL_ALUMNO_KARDEX (
   IDKARDEX             int                  not null,
   IDALUMNOS            int                  null,
   IDCALIFICACION       int                  null,
   constraint PK_CAL_ALUMNO_KARDEX primary key (IDKARDEX)
)
go

/*==============================================================*/
/* Table: CAL_CALIFICACIONES                                    */
/*==============================================================*/
create table CAL_CALIFICACIONES (
   IDCALIFICACION       int                  identity,
   IDALUMNOS            int                  null,
   IDCALIFICACIONESFECHAS int                  null,
   IDMATERIA            smallint             null,
   IDTIPOEVALUACION     smallint             null,
   CALIFICACIONPRIMERPERIODOORDINARIO decimal(2,2)         null,
   CALFIICACIONSEGUNDOPERIODOORDINARIO decimal(2,2)         null,
   CALIFICACIONTERCERPERIODOORDINARIO decimal(2,2)         null,
   CALIFICACIONFINALORDINARIA decimal(2,2)         null,
   CALIFICACIONPRIMERPERIODORECURSAMIENTO decimal(2,2)         null,
   CALIFICACIONSEGUNDOPERIODORECURSAMIENTO decimal(2,2)         null,
   CALIFICACIONTERCERPERIODORECUSAMIENTO decimal(2,2)         null,
   CALIFICACIONFINALRECURSAMIENTO decimal(2,2)         null,
   CALIFICACIONETS1     decimal(2,2)         null,
   CALIFICACIONETS2     decimal(2,2)         null,
   CALIFICACIONETS3     decimal(2,2)         null,
   CALIFICACIONETS4     decimal(2,2)         null,
   CREDITOSOBTENIDOS    int                  null,
   ACREDITADA           bit                  not null,
   constraint PK_CAL_CALIFICACIONES primary key (IDCALIFICACION)
)
go

/*==============================================================*/
/* Table: CAL_CALIFICACIONES_FECHAS                             */
/*==============================================================*/
create table CAL_CALIFICACIONES_FECHAS (
   IDCALIFICACIONESFECHAS int                  identity,
   FECHACALIFICACIONPRIMERPERIODOORDINARIO datetime             null,
   FECHACALIFICACIONSEGUNDOPERIODOORDINARIO datetime             null,
   FECHACALIFICACIONTERCERPERIODOORDINARIO datetime             null,
   FECHACALIFICACIONFINALORDINARIA datetime             null,
   FECHACALIFICACIONPRIMERPERIODORECURSAMIENTO datetime             null,
   FECHACALIFICACIONSEGUNDOPERIODORECURSAMIENTO datetime             null,
   FECHACALIFICACIONTERCERPERIODORECURSAMIENTO datetime             null,
   FECHACALIFICACIONFINALRECURSAMIENTO datetime             null,
   FECHACALIFICACIONETS1 datetime             null,
   FECHACALIFICACIONETS2 datetime             null,
   FECHACALIFICACIONETS3 datetime             null,
   FECHACALIFICACIONETS4 datetime             null,
   constraint PK_CAL_CALIFICACIONES_FECHAS primary key (IDCALIFICACIONESFECHAS)
)
go

/*==============================================================*/
/* Table: CAL_CALIFICACION_CLASE                                */
/*==============================================================*/
create table CAL_CALIFICACION_CLASE (
   IDCALIFICACIONCLASE  int                  not null,
   IDCLASE              int                  null,
   IDCALIFICACION       int                  null,
   IDTIPOEVALUACION     smallint             null,
   constraint PK_CAL_CALIFICACION_CLASE primary key (IDCALIFICACIONCLASE)
)
go

/*==============================================================*/
/* Table: CAL_CAT_TIPO_EVALUACION                               */
/*==============================================================*/
create table CAL_CAT_TIPO_EVALUACION (
   IDTIPOEVALUACION     smallint             identity,
   TIPOEVALUACION       varchar(100)         not null,
   DESCRIPCION          varchar(300)         null,
   constraint PK_CAL_CAT_TIPO_EVALUACION primary key (IDTIPOEVALUACION)
)
go

/*==============================================================*/
/* Table: CAR_CAT_CARRERAS                                      */
/*==============================================================*/
create table CAR_CAT_CARRERAS (
   IDCARRERA            smallint             not null,
   IDESPECIALIDAD       smallint             null,
   NOMBRECARRERA        varchar(100)         not null,
   constraint PK_CAR_CAT_CARRERAS primary key (IDCARRERA)
)
go

/*==============================================================*/
/* Table: CAR_CAT_ESPECIALIDAD                                  */
/*==============================================================*/
create table CAR_CAT_ESPECIALIDAD (
   IDESPECIALIDAD       smallint             not null,
   ESPECIALIDAD         varchar(100)         not null,
   constraint PK_CAR_CAT_ESPECIALIDAD primary key (IDESPECIALIDAD)
)
go

/*==============================================================*/
/* Table: CLA_CLASE                                             */
/*==============================================================*/
create table CLA_CLASE (
   IDCLASE              int                  identity,
   IDMATERIA            smallint             null,
   IDPROFESOR           int                  null,
   IDCARRERA            smallint             null,
   IDSEMESTRE           int                  null,
   constraint PK_CLA_CLASE primary key (IDCLASE)
)
go

/*==============================================================*/
/* Table: CLA_HORARIO                                           */
/*==============================================================*/
create table CLA_HORARIO (
   IDHORARIO            smallint             identity,
   IDHORARIOMATERIA     smallint             null,
   IDCLASE              int                  null,
   constraint PK_CLA_HORARIO primary key (IDHORARIO)
)
go

/*==============================================================*/
/* Table: DIR_CAT_COLONIAS                                      */
/*==============================================================*/
create table DIR_CAT_COLONIAS (
   IDCOLONIA            int                  not null,
   IDESTADO             int                  null,
   IDDELGMUNICIPIO      int                  null,
   IDTIPOASENTAMIENTO   int                  null,
   IDTIPOZONA           int                  null,
   IDMUNICIPIO          int                  null,
   CODIGOPOSTAL         int                  null,
   NOMBRECOLONIA        varchar(100)         null,
   constraint PK_DIR_CAT_COLONIAS primary key (IDCOLONIA)
)
go

/*==============================================================*/
/* Table: DIR_CAT_DELG_MUNICIPIO                                */
/*==============================================================*/
create table DIR_CAT_DELG_MUNICIPIO (
   IDDELGMUNICIPIO      int                  not null,
   IDESTADO             int                  null,
   IDMUNICIPIO          int                  null,
   NOMBREDELGMUNICIPIO  varchar(50)          null,
   constraint PK_DIR_CAT_DELG_MUNICIPIO primary key (IDDELGMUNICIPIO)
)
go

/*==============================================================*/
/* Table: DIR_CAT_ESTADO                                        */
/*==============================================================*/
create table DIR_CAT_ESTADO (
   IDESTADO             int                  not null,
   NOMBREESTADO         varchar(50)          null,
   NOMBREOFICIAL        varchar(50)          null,
   constraint PK_DIR_CAT_ESTADO primary key (IDESTADO)
)
go

/*==============================================================*/
/* Table: DIR_CAT_TIPO_ASENTAMIENTO                             */
/*==============================================================*/
create table DIR_CAT_TIPO_ASENTAMIENTO (
   IDTIPOASENTAMIENTO   int                  not null,
   TIPOASENTAMIENTO     varchar(100)         not null,
   constraint PK_DIR_CAT_TIPO_ASENTAMIENTO primary key (IDTIPOASENTAMIENTO)
)
go

/*==============================================================*/
/* Table: DIR_CAT_TIPO_ZONA                                     */
/*==============================================================*/
create table DIR_CAT_TIPO_ZONA (
   IDTIPOZONA           int                  not null,
   TIPOZONA             varchar(100)         null,
   constraint PK_DIR_CAT_TIPO_ZONA primary key (IDTIPOZONA)
)
go

/*==============================================================*/
/* Table: DIR_DIRECCIONES                                       */
/*==============================================================*/
create table DIR_DIRECCIONES (
   IDDIRECCION          int                  identity,
   IDESTADO             int                  null,
   IDMUNICIPIO          int                  null,
   IDCOLONIA            int                  null,
   CALLE                varchar(100)         null,
   NOEXT                varchar(30)          null,
   NOINT                varchar(30)          null,
   REFERENCIAS          varchar(150)         null,
   constraint PK_DIR_DIRECCIONES primary key (IDDIRECCION)
)
go

/*==============================================================*/
/* Table: GEN_CAT_SEMESTRE_PERIODOS                             */
/*==============================================================*/
create table GEN_CAT_SEMESTRE_PERIODOS (
   IDSEMESTRE           int                  not null,
   PERIODOSEMESTRE      varchar(100)         null,
   constraint PK_GEN_CAT_SEMESTRE_PERIODOS primary key nonclustered (IDSEMESTRE)
)
go

/*==============================================================*/
/* Table: HOR_CAT_DIAS_SEMANA                                   */
/*==============================================================*/
create table HOR_CAT_DIAS_SEMANA (
   IDDIA                smallint             not null,
   DIASEMANA            varchar(20)          not null,
   constraint PK_HOR_CAT_DIAS_SEMANA primary key (IDDIA)
)
go

/*==============================================================*/
/* Table: HOR_CAT_HORAS                                         */
/*==============================================================*/
create table HOR_CAT_HORAS (
   IDHORA               smallint             not null,
   IDTURNO              smallint             null,
   NOMBREHORA           varchar(100)         not null,
   HORAINICIO           datetime             not null,
   HORATERMINO          datetime             not null,
   DESCRIPCION          varchar(100)         null,
   constraint PK_HOR_CAT_HORAS primary key (IDHORA)
)
go

/*==============================================================*/
/* Table: HOR_CAT_TURNO                                         */
/*==============================================================*/
create table HOR_CAT_TURNO (
   IDTURNO              smallint             not null,
   NOMBRETURNO          varchar(100)         not null,
   constraint PK_HOR_CAT_TURNO primary key (IDTURNO)
)
go

/*==============================================================*/
/* Table: HOR_HORAS_POR_DIA                                     */
/*==============================================================*/
create table HOR_HORAS_POR_DIA (
   IDHORASPORDIA        smallint             identity,
   IDHORA               smallint             null,
   IDDIA                smallint             null,
   constraint PK_HOR_HORAS_POR_DIA primary key (IDHORASPORDIA)
)
go

/*==============================================================*/
/* Table: MAT_ARBOL_MATERIA                                     */
/*==============================================================*/
create table MAT_ARBOL_MATERIA (
   IDMATERIADEPENDENCIA int                  not null,
   IDMATERIADEPENDENCIAHIJO int                  null,
   IDMATERIA            smallint             null,
   constraint PK_MAT_ARBOL_MATERIA primary key (IDMATERIADEPENDENCIA)
)
go

/*==============================================================*/
/* Table: MAT_CAT_MATERIAS                                      */
/*==============================================================*/
create table MAT_CAT_MATERIAS (
   IDMATERIA            smallint             not null,
   IDCARRERA            smallint             null,
   NOMBREMATERIA        varchar(100)         not null,
   CREDITOS             decimal(3,2)         null,
   constraint PK_MAT_CAT_MATERIAS primary key (IDMATERIA)
)
go

/*==============================================================*/
/* Table: MAT_HORARIO_POR_MATERIA                               */
/*==============================================================*/
create table MAT_HORARIO_POR_MATERIA (
   IDHORARIOMATERIA     smallint             not null,
   IDHORASPORDIA        smallint             null,
   IDAULACLASES         smallint             null,
   constraint PK_MAT_HORARIO_POR_MATERIA primary key (IDHORARIOMATERIA)
)
go

/*==============================================================*/
/* Table: PER_CAT_NACIONALIDAD                                  */
/*==============================================================*/
create table PER_CAT_NACIONALIDAD (
   CVE_NACIONALIDAD     int                  not null,
   NOMBRE_PAIS          varchar(50)          null,
   constraint PK_PER_CAT_NACIONALIDAD primary key (CVE_NACIONALIDAD)
)
go

/*==============================================================*/
/* Table: PER_CAT_TELEFONOS                                     */
/*==============================================================*/
create table PER_CAT_TELEFONOS (
   ID_TELEFONOS         int                  identity,
   TELEFONO_FIJO_DOMICILIO varchar(20)          null,
   TELEFONO_FIJO_TRABAJO varchar(20)          null,
   TELEFONO_CELULAR_PERSONAL varchar(20)          null,
   TELEFONO_CELULAR_TRABAJO varchar(20)          null,
   FAX                  varchar(20)          null,
   constraint PK_PER_CAT_TELEFONOS primary key (ID_TELEFONOS)
)
go

/*==============================================================*/
/* Table: PER_CAT_TIPO_PERSONA                                  */
/*==============================================================*/
create table PER_CAT_TIPO_PERSONA (
   ID_TIPO_PERSONA      int                  not null,
   TIPO_PERSONA         varchar(50)          null,
   DESCRIPCION          varchar(50)          null,
   constraint PK_PER_CAT_TIPO_PERSONA primary key (ID_TIPO_PERSONA)
)
go

/*==============================================================*/
/* Table: PER_FOTOGRAFIA                                        */
/*==============================================================*/
create table PER_FOTOGRAFIA (
   IDFOTO               int                  identity,
   NOMBRE               varchar(250)         null,
   EXTENCION            varchar(10)          null,
   FOTOGRAFIA           varbinary(MAX)       null,
   LONGITUD             bigint               null,
   constraint PK_PER_FOTOGRAFIA primary key (IDFOTO)
)
go

/*==============================================================*/
/* Table: PER_MEDIOS_ELECTRONICOS                               */
/*==============================================================*/
create table PER_MEDIOS_ELECTRONICOS (
   ID_MEDIOS_ELECTRONICOS int                  identity,
   CORREO_ELECTRONICO_UNIVERSIDAD varchar(100)         null,
   CORREO_ELECTRONICO_PERSONAL varchar(100)         null,
   FACEBOOK             varchar(50)          null,
   TWITTER              varchar(50)          null,
   constraint PK_PER_MEDIOS_ELECTRONICOS primary key (ID_MEDIOS_ELECTRONICOS)
)
go

/*==============================================================*/
/* Table: PER_PERSONAS                                          */
/*==============================================================*/
create table PER_PERSONAS (
   ID_PERSONA           int                  identity,
   ID_PER_LINKID        varchar(50)          not null,
   IDDIRECCION          int                  null,
   CVE_NACIONALIDAD     int                  null,
   ID_TELEFONOS         int                  null,
   ID_TIPO_PERSONA      int                  null,
   ID_USUARIO           int                  null,
   ID_MEDIOS_ELECTRONICOS int                  null,
   IDFOTO               int                  null,
   NOMBRE               varchar(50)          not null,
   A_PATERNO            varchar(50)          null,
   A_MATERNO            varchar(50)          null,
   NOMBRE_COMPLETO      varchar(100)         not null,
   FECHA_NAC            datetime             not null,
   FECHAINGRESO         datetime             not null,
   SEXO                 varchar(20)          not null,
   CURP                 varchar(30)          null,
   RFC                  varchar(30)          null,
   IMSS                 varchar(20)          null,
   constraint PK_PER_PERSONAS primary key (ID_PERSONA, ID_PER_LINKID)
)
go

/*==============================================================*/
/* Table: PRO_PROFESOR                                          */
/*==============================================================*/
create table PRO_PROFESOR (
   IDPROFESOR           int                  identity,
   ID_PERSONA           int                  null,
   ID_PER_LINKID        varchar(50)          null,
   constraint PK_PRO_PROFESOR primary key (IDPROFESOR)
)
go

/*==============================================================*/
/* Table: SIS_AADM_APLICACIONES                                 */
/*==============================================================*/
create table SIS_AADM_APLICACIONES (
   IDAPLICACIONES       int                  identity,
   IDMENU               int                  null,
   IDTABPAGES           int                  null,
   ID_NIVEL_USUARIO     int                  null,
   ID_TIPO_USUARIO      int                  null,
   constraint PK_SIS_AADM_APLICACIONES primary key (IDAPLICACIONES)
)
go

/*==============================================================*/
/* Table: SIS_AADM_ARBOLMENUS                                   */
/*==============================================================*/
create table SIS_AADM_ARBOLMENUS (
   IDMENU               int                  not null,
   NOMBRENODO           varchar(100)         null,
   RUTA                 varchar(MAX)         null,
   IDMENUPADRE          int                  not null,
   constraint PK_SIS_AADM_ARBOLMENUS primary key (IDMENU)
)
go

/*==============================================================*/
/* Table: SIS_CAT_TABPAGES                                      */
/*==============================================================*/
create table SIS_CAT_TABPAGES (
   IDTABPAGES           int                  not null,
   RUTATAB              varchar(100)         null,
   NOMBRETABPAGE        varchar(100)         null,
   constraint PK_SIS_CAT_TABPAGES primary key (IDTABPAGES)
)
go

/*==============================================================*/
/* Table: SIS_WADM_ARBOLMENU                                    */
/*==============================================================*/
create table SIS_WADM_ARBOLMENU (
   IDMENU               int                  not null,
   ID_TIPO_USUARIO      int                  null,
   ID_NIVEL_USUARIO     int                  null,
   IDMENUPADRE          int                  null,
   NOMBRE               varchar(100)         null,
   LINK                 varchar(100)         null,
   constraint PK_SIS_WADM_ARBOLMENU primary key (IDMENU)
)
go

/*==============================================================*/
/* Table: SIS_WADM_ARBOLMENU_MVC                                */
/*==============================================================*/
create table SIS_WADM_ARBOLMENU_MVC (
   IDMENU               int                  not null,
   IDMENUPADRE          int                  null,
   ID_NIVEL_USUARIO     int                  null,
   ID_TIPO_USUARIO      int                  null,
   NOMBRE               varchar(100)         null,
   CONTROLLER           varchar(100)         null,
   ACTION               varchar(100)         null,
   URL                  varchar(100)         null,
   constraint PK_SIS_WADM_ARBOLMENU_MVC primary key (IDMENU)
)
go

/*==============================================================*/
/* Table: US_CAT_ESTATUS_USUARIO                                */
/*==============================================================*/
create table US_CAT_ESTATUS_USUARIO (
   ID_ESTATUS_USUARIOS  int                  not null,
   ESTATUS_USUARIO      varchar(30)          not null,
   DESCRIPCION          varchar(150)         not null,
   constraint PK_US_CAT_ESTATUS_USUARIO primary key (ID_ESTATUS_USUARIOS)
)
go

/*==============================================================*/
/* Table: US_CAT_NIVEL_USUARIO                                  */
/*==============================================================*/
create table US_CAT_NIVEL_USUARIO (
   ID_NIVEL_USUARIO     int                  not null,
   NIVEL_USUARIO        varchar(30)          not null,
   DESCRIPCION          varchar(150)         not null,
   constraint PK_US_CAT_NIVEL_USUARIO primary key (ID_NIVEL_USUARIO)
)
go

/*==============================================================*/
/* Table: US_CAT_TIPO_USUARIO                                   */
/*==============================================================*/
create table US_CAT_TIPO_USUARIO (
   ID_TIPO_USUARIO      int                  not null,
   TIPO_USUARIO         varchar(30)          not null,
   DESCRIPCION          varchar(150)         null,
   constraint PK_US_CAT_TIPO_USUARIO primary key (ID_TIPO_USUARIO)
)
go

/*==============================================================*/
/* Table: US_USUARIOS                                           */
/*==============================================================*/
create table US_USUARIOS (
   ID_USUARIO           int                  identity,
   ID_TIPO_USUARIO      int                  null,
   ID_ESTATUS_USUARIOS  int                  null,
   ID_NIVEL_USUARIO     int                  null,
   USUARIO              varchar(50)          null,
   CONTRASENA           varchar(30)          null,
   constraint PK_US_USUARIOS primary key (ID_USUARIO)
)
go

alter table ALU_ALUMNOS
   add constraint FK_ALU_ALUM_REFERENCE_PER_PERS foreign key (ID_PERSONA, ID_PER_LINKID)
      references PER_PERSONAS (ID_PERSONA, ID_PER_LINKID)
go

alter table ALU_HORARIO
   add constraint FK_ALU_HORA_REFERENCE_ALU_ALUM foreign key (IDALUMNOS)
      references ALU_ALUMNOS (IDALUMNOS)
go

alter table ALU_HORARIO
   add constraint FK_ALU_HORA_REFERENCE_CLA_CLAS foreign key (IDCLASE)
      references CLA_CLASE (IDCLASE)
go

alter table AUL_AULA_CLASES
   add constraint FK_AUL_AULA_REFERENCE_AUL_CAT_ foreign key (IDTIPOAULA)
      references AUL_CAT_TIPO_AULA (IDTIPOAULA)
go

alter table CAL_ALUMNO_KARDEX
   add constraint FK_CAL_ALUM_REFERENCE_ALU_ALUM foreign key (IDALUMNOS)
      references ALU_ALUMNOS (IDALUMNOS)
go

alter table CAL_ALUMNO_KARDEX
   add constraint FK_CAL_ALUM_REFERENCE_CAL_CALI foreign key (IDCALIFICACION)
      references CAL_CALIFICACIONES (IDCALIFICACION)
go

alter table CAL_CALIFICACIONES
   add constraint FK_CAL_CALI_REFERENCE_ALU_ALUM foreign key (IDALUMNOS)
      references ALU_ALUMNOS (IDALUMNOS)
go

alter table CAL_CALIFICACIONES
   add constraint FK_CAL_CALI_REFERENCE_MAT_CAT_ foreign key (IDMATERIA)
      references MAT_CAT_MATERIAS (IDMATERIA)
go

alter table CAL_CALIFICACIONES
   add constraint FK_CAL_CALI_REF_CAL_C_CAL_CAT_ foreign key (IDTIPOEVALUACION)
      references CAL_CAT_TIPO_EVALUACION (IDTIPOEVALUACION)
go

alter table CAL_CALIFICACIONES
   add constraint FK_CAL_CALI_REF_CAL_C_CAL_CALI foreign key (IDCALIFICACIONESFECHAS)
      references CAL_CALIFICACIONES_FECHAS (IDCALIFICACIONESFECHAS)
go

alter table CAL_CALIFICACION_CLASE
   add constraint FK_CAL_CALI_CAL_CAL_C_CAL_CAT_ foreign key (IDTIPOEVALUACION)
      references CAL_CAT_TIPO_EVALUACION (IDTIPOEVALUACION)
go

alter table CAL_CALIFICACION_CLASE
   add constraint FK_CAL_CALI_CAL_CLA_C_CAL_CALI foreign key (IDCALIFICACION)
      references CAL_CALIFICACIONES (IDCALIFICACION)
go

alter table CAL_CALIFICACION_CLASE
   add constraint FK_CAL_CALI_REFERENCE_CLA_CLAS foreign key (IDCLASE)
      references CLA_CLASE (IDCLASE)
go

alter table CAR_CAT_CARRERAS
   add constraint FK_CAR_CAT__REFERENCE_CAR_CAT_ foreign key (IDESPECIALIDAD)
      references CAR_CAT_ESPECIALIDAD (IDESPECIALIDAD)
go

alter table CLA_CLASE
   add constraint FK_CLA_CLAS_REFERENCE_MAT_CAT_ foreign key (IDMATERIA)
      references MAT_CAT_MATERIAS (IDMATERIA)
go

alter table CLA_CLASE
   add constraint FK_CLA_CLAS_REFERENCE_PRO_PROF foreign key (IDPROFESOR)
      references PRO_PROFESOR (IDPROFESOR)
go

alter table CLA_CLASE
   add constraint FK_CLA_CLAS_REFERENCE_CAR_CAT_ foreign key (IDCARRERA)
      references CAR_CAT_CARRERAS (IDCARRERA)
go

alter table CLA_CLASE
   add constraint FK_CLA_CLAS_REFERENCE_GEN_CAT_ foreign key (IDSEMESTRE)
      references GEN_CAT_SEMESTRE_PERIODOS (IDSEMESTRE)
go

alter table CLA_HORARIO
   add constraint FK_CLA_HORA_REFERENCE_CLA_CLAS foreign key (IDCLASE)
      references CLA_CLASE (IDCLASE)
go

alter table CLA_HORARIO
   add constraint FK_CLA_HORA_REFERENCE_MAT_HORA foreign key (IDHORARIOMATERIA)
      references MAT_HORARIO_POR_MATERIA (IDHORARIOMATERIA)
go

alter table DIR_CAT_COLONIAS
   add constraint FK_DIR_CAT__COLONIAS__DIR_CAT_ foreign key (IDTIPOASENTAMIENTO)
      references DIR_CAT_TIPO_ASENTAMIENTO (IDTIPOASENTAMIENTO)
go

alter table DIR_CAT_COLONIAS
   add constraint FK_DIR_CAT__REFERENCE_DIR_CAT_ foreign key (IDTIPOZONA)
      references DIR_CAT_TIPO_ZONA (IDTIPOZONA)
go

alter table DIR_CAT_COLONIAS
   add constraint Reference_26 foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
go

alter table DIR_CAT_COLONIAS
   add constraint Reference_29 foreign key (IDDELGMUNICIPIO)
      references DIR_CAT_DELG_MUNICIPIO (IDDELGMUNICIPIO)
go

alter table DIR_CAT_DELG_MUNICIPIO
   add constraint Reference_27 foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
go

alter table DIR_DIRECCIONES
   add constraint Reference_28 foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
go

alter table DIR_DIRECCIONES
   add constraint Reference_32 foreign key (IDCOLONIA)
      references DIR_CAT_COLONIAS (IDCOLONIA)
go

alter table HOR_CAT_HORAS
   add constraint FK_HOR_CAT__REFERENCE_HOR_CAT_ foreign key (IDTURNO)
      references HOR_CAT_TURNO (IDTURNO)
go

alter table HOR_HORAS_POR_DIA
   add constraint FK_HOR_HORA_HOR_HORAS_HOR_CAT_ foreign key (IDDIA)
      references HOR_CAT_DIAS_SEMANA (IDDIA)
go

alter table HOR_HORAS_POR_DIA
   add constraint FK_HOR_HORA_HOR_HOR_P_HOR_CAT_ foreign key (IDHORA)
      references HOR_CAT_HORAS (IDHORA)
go

alter table MAT_ARBOL_MATERIA
   add constraint FK_MAT_ARBO_REFERENCE_MAT_ARBO foreign key (IDMATERIADEPENDENCIAHIJO)
      references MAT_ARBOL_MATERIA (IDMATERIADEPENDENCIA)
go

alter table MAT_ARBOL_MATERIA
   add constraint FK_MAT_ARBO_REFERENCE_MAT_CAT_ foreign key (IDMATERIA)
      references MAT_CAT_MATERIAS (IDMATERIA)
go

alter table MAT_CAT_MATERIAS
   add constraint FK_MAT_CAT__REFERENCE_CAR_CAT_ foreign key (IDCARRERA)
      references CAR_CAT_CARRERAS (IDCARRERA)
go

alter table MAT_HORARIO_POR_MATERIA
   add constraint FK_MAT_HORA_REFERENCE_HOR_HORA foreign key (IDHORASPORDIA)
      references HOR_HORAS_POR_DIA (IDHORASPORDIA)
go

alter table MAT_HORARIO_POR_MATERIA
   add constraint FK_MAT_HORA_REFERENCE_AUL_AULA foreign key (IDAULACLASES)
      references AUL_AULA_CLASES (IDAULACLASES)
go

alter table PER_PERSONAS
   add constraint FK_PER_PERS_REFERENCE_PER_MEDI foreign key (ID_MEDIOS_ELECTRONICOS)
      references PER_MEDIOS_ELECTRONICOS (ID_MEDIOS_ELECTRONICOS)
go

alter table PER_PERSONAS
   add constraint FK_PER_PERS_REFERENCE_PER_FOTO foreign key (IDFOTO)
      references PER_FOTOGRAFIA (IDFOTO)
go

alter table PER_PERSONAS
   add constraint Reference_33 foreign key (ID_TIPO_PERSONA)
      references PER_CAT_TIPO_PERSONA (ID_TIPO_PERSONA)
go

alter table PER_PERSONAS
   add constraint Reference_34 foreign key (ID_USUARIO)
      references US_USUARIOS (ID_USUARIO)
go

alter table PER_PERSONAS
   add constraint Reference_6 foreign key (IDDIRECCION)
      references DIR_DIRECCIONES (IDDIRECCION)
go

alter table PER_PERSONAS
   add constraint Reference_8 foreign key (CVE_NACIONALIDAD)
      references PER_CAT_NACIONALIDAD (CVE_NACIONALIDAD)
go

alter table PER_PERSONAS
   add constraint Reference_9 foreign key (ID_TELEFONOS)
      references PER_CAT_TELEFONOS (ID_TELEFONOS)
go

alter table PRO_PROFESOR
   add constraint FK_PRO_PROF_REFERENCE_PER_PERS foreign key (ID_PERSONA, ID_PER_LINKID)
      references PER_PERSONAS (ID_PERSONA, ID_PER_LINKID)
go

alter table SIS_AADM_APLICACIONES
   add constraint FK_SIS_AADM_REFERENCE_SIS_AADM foreign key (IDMENU)
      references SIS_AADM_ARBOLMENUS (IDMENU)
go

alter table SIS_AADM_APLICACIONES
   add constraint FK_SIS_AADM_REFERENCE_SIS_CAT_ foreign key (IDTABPAGES)
      references SIS_CAT_TABPAGES (IDTABPAGES)
go

alter table SIS_AADM_APLICACIONES
   add constraint FK_SIS_AADM_REFERENCE_US_CAT_N foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
go

alter table SIS_AADM_APLICACIONES
   add constraint FK_SIS_AADM_REFERENCE_US_CAT_T foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
go

alter table SIS_WADM_ARBOLMENU
   add constraint FK_SIS_WADM_REFERENCE_US_CAT_T foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
go

alter table SIS_WADM_ARBOLMENU
   add constraint FK_SIS_WADM_REFERENCE_US_CAT_N foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
go

alter table SIS_WADM_ARBOLMENU
   add constraint FK_SIS_WADM_REFERENCE_SIS_WADM foreign key (IDMENUPADRE)
      references SIS_WADM_ARBOLMENU (IDMENU)
go

alter table SIS_WADM_ARBOLMENU_MVC
   add constraint FK_MENU_US_TIPO_USUARIO foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
go

alter table SIS_WADM_ARBOLMENU_MVC
   add constraint FK_WADM_NIVEL_USUARIO foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
go

alter table SIS_WADM_ARBOLMENU_MVC
   add constraint FK_SIS_WADM_MVC_REC foreign key (IDMENUPADRE)
      references SIS_WADM_ARBOLMENU_MVC (IDMENU)
go

alter table US_USUARIOS
   add constraint Reference_10 foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
go

alter table US_USUARIOS
   add constraint Reference_11 foreign key (ID_ESTATUS_USUARIOS)
      references US_CAT_ESTATUS_USUARIO (ID_ESTATUS_USUARIOS)
go

alter table US_USUARIOS
   add constraint Reference_12 foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
go

