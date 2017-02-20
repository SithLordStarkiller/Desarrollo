/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     18/02/2017 11:05:58 p. m.                    */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUSUARIOS') and o.name = 'FK_USUSUARI_USUARIOS__USESTATU')
alter table USUSUARIOS
   drop constraint FK_USUSUARI_USUARIOS__USESTATU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOGLOGGER')
            and   type = 'U')
   drop table LOGLOGGER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOGMOVIMIENTOSBD')
            and   type = 'U')
   drop table LOGMOVIMIENTOSBD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USESTATUS')
            and   type = 'U')
   drop table USESTATUS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUSUARIOS')
            and   type = 'U')
   drop table USUSUARIOS
go

/*==============================================================*/
/* Table: LOGLOGGER                                             */
/*==============================================================*/
create table LOGLOGGER (
   IDLOG                bigint               identity,
   TIPOLOG              varchar(10)          not null,
   PROYECTO             varchar(10)          null,
   CLASE                varchar(10)          null,
   METODO               varchar(10)          null,
   MENSAGE              varchar(100)         null,
   LOG                  varchar(Max)         null,
   EXCEPCION            varchar(Max)         null,
   AUXILIAR             varchar(Max)         null,
   CREADO               datetime             null,
   constraint PK_LOGLOGGER primary key (IDLOG)
)
go

/*==============================================================*/
/* Table: LOGMOVIMIENTOSBD                                      */
/*==============================================================*/
create table LOGMOVIMIENTOSBD (
   IDLOGMOVIMIENTO      bigint               identity,
   TIPOMOVIMIENTO       varchar(10)          not null,
   TABLA                varchar(10)          null,
   IDREGISTRO           bigint               null,
   REGISTROORIGINAL     xml                  null,
   REGISTRO             xml                  null,
   constraint PK_LOGMOVIMIENTOSBD primary key (IDLOGMOVIMIENTO)
)
go

/*==============================================================*/
/* Table: USESTATUS                                             */
/*==============================================================*/
create table USESTATUS (
   IDESTATUS            int                  not null,
   ESTATUS              varchar(50)          not null,
   DESCRIPCION          varchar(200)         null,
   constraint PK_USESTATUS primary key (IDESTATUS)
)
go

/*==============================================================*/
/* Table: USUSUARIOS                                            */
/*==============================================================*/
create table USUSUARIOS (
   IDUSUARIO            int                  not null,
   IDESTATUS            int                  null,
   USUARIO              varchar(30)          not null,
   CONTRASENA           varchar(20)          not null,
   constraint PK_USUSUARIOS primary key (IDUSUARIO)
)
go

alter table USUSUARIOS
   add constraint FK_USUSUARI_USUARIOS__USESTATU foreign key (IDESTATUS)
      references USESTATUS (IDESTATUS)
go

