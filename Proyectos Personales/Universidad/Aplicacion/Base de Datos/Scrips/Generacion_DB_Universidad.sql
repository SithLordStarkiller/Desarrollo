/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     13/02/2015 01:18:00 p. m.                    */
/*==============================================================*/


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
   where r.fkeyid = object_id('DIR_DIRECCIONES') and o.name = 'Reference_31')
alter table DIR_DIRECCIONES
   drop constraint Reference_31
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_DIRECCIONES') and o.name = 'Reference_32')
alter table DIR_DIRECCIONES
   drop constraint Reference_32
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
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'Reference_13')
alter table US_USUARIOS
   drop constraint Reference_13
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
           where  id = object_id('US_HISTORIAL')
            and   type = 'U')
   drop table US_HISTORIAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_USUARIOS')
            and   type = 'U')
   drop table US_USUARIOS
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
   IDDELGMUNICIPIO      int                  null,
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
/* Table: US_HISTORIAL                                          */
/*==============================================================*/
create table US_HISTORIAL (
   ID_HISTORIAL         int                  identity,
   FECHA_ULTIMA_CESION  datetime             null,
   FECHA_CAMBIOCONTRASENA_ULTIMO datetime             null,
   ULTIMA_CONTRASENA    varchar(30)          null,
   constraint PK_US_HISTORIAL primary key (ID_HISTORIAL)
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
   ID_HISTORIAL         int                  null,
   USUARIO              varchar(50)          null,
   CONTRASENA           varchar(30)          null,
   constraint PK_US_USUARIOS primary key (ID_USUARIO)
)
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
   add constraint Reference_31 foreign key (IDDELGMUNICIPIO)
      references DIR_CAT_DELG_MUNICIPIO (IDDELGMUNICIPIO)
go

alter table DIR_DIRECCIONES
   add constraint Reference_32 foreign key (IDCOLONIA)
      references DIR_CAT_COLONIAS (IDCOLONIA)
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

alter table US_USUARIOS
   add constraint Reference_13 foreign key (ID_HISTORIAL)
      references US_HISTORIAL (ID_HISTORIAL)
go

