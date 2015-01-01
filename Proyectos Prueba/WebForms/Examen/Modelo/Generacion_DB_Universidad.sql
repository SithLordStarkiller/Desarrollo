/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     30/12/2014 12:00:57 p. m.                    */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONAS') and o.name = 'FK_PER_PERS_REFERENCE_CAT_AREA')
alter table PER_PERSONAS
   drop constraint FK_PER_PERS_REFERENCE_CAT_AREA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_APLICACIONNES') and o.name = 'FK_SIS_APLI_REFERENCE_SIS_APLI')
alter table SIS_APLICACIONNES
   drop constraint FK_SIS_APLI_REFERENCE_SIS_APLI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_APLICACIONNES') and o.name = 'FK_SIS_APLI_REFERENCE_CAT_AREA')
alter table SIS_APLICACIONNES
   drop constraint FK_SIS_APLI_REFERENCE_CAT_AREA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAT_AREANEGOCIO')
            and   type = 'U')
   drop table CAT_AREANEGOCIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PER_PERSONAS')
            and   type = 'U')
   drop table PER_PERSONAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SIS_APLICACIONNES')
            and   type = 'U')
   drop table SIS_APLICACIONNES
go

/*==============================================================*/
/* Table: CAT_AREANEGOCIO                                       */
/*==============================================================*/
create table CAT_AREANEGOCIO (
   IDAREANEGOCIO        int                  not null,
   NOMBREAREANEGOCIO    varchar(50)          not null,
   constraint PK_CAT_AREANEGOCIO primary key (IDAREANEGOCIO)
)
go

/*==============================================================*/
/* Table: PER_PERSONAS                                          */
/*==============================================================*/
create table PER_PERSONAS (
   IDPERSONA            int                  identity,
   IDAREANEGOCIO        int                  null,
   NOMBRES              varchar(100)         not null,
   APELLIDOPATERNO      varchar(100)         null,
   APELLIDOMATERNO      varchar(100)         null,
   CORREOELECTRONICO    varchar(50)          not null,
   CONTRASENA           varchar(30)          not null,
   constraint PK_PER_PERSONAS primary key (IDPERSONA)
)
go

/*==============================================================*/
/* Table: SIS_APLICACIONNES                                     */
/*==============================================================*/
create table SIS_APLICACIONNES (
   IDPAGINAPADRE        int                  not null,
   IDPAGINAHIJO         int                  null,
   IDAREANEGOCIO        int                  null,
   NOMBREPAGINA         varchar(30)          not null,
   URL                  varchar(100)         not null,
   constraint PK_SIS_APLICACIONNES primary key (IDPAGINAPADRE)
)
go

alter table PER_PERSONAS
   add constraint FK_PER_PERS_REFERENCE_CAT_AREA foreign key (IDAREANEGOCIO)
      references CAT_AREANEGOCIO (IDAREANEGOCIO)
go

alter table SIS_APLICACIONNES
   add constraint FK_SIS_APLI_REFERENCE_SIS_APLI foreign key (IDPAGINAHIJO)
      references SIS_APLICACIONNES (IDPAGINAPADRE)
go

alter table SIS_APLICACIONNES
   add constraint FK_SIS_APLI_REFERENCE_CAT_AREA foreign key (IDAREANEGOCIO)
      references CAT_AREANEGOCIO (IDAREANEGOCIO)
go

