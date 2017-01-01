/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     25/12/2016 03:22:27 a. m.                    */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUSUARIOS') and o.name = 'FK_USUSUARI_REFERENCE_USESTATU')
alter table USUSUARIOS
   drop constraint FK_USUSUARI_REFERENCE_USESTATU
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
/* Table: USESTATUS                                             */
/*==============================================================*/
create table USESTATUS (
   IDESTATUS            int                  not null,
   ESTATUS              Varchar(50)          not null,
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
   USUARIO              varchar(50)          null,
   CONTRASENA           varchar(20)          null,
   constraint PK_USUSUARIOS primary key (IDUSUARIO)
)
go

alter table USUSUARIOS
   add constraint FK_USUSUARI_REFERENCE_USESTATU foreign key (IDESTATUS)
      references USESTATUS (IDESTATUS)
go

