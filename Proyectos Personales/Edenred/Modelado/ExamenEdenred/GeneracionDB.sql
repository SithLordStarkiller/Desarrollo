/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     27/10/2015 01:01:44 p. m.                    */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('PER_PERSONAS')
            and   type = 'U')
   drop table PER_PERSONAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_USUARIOS')
            and   type = 'U')
   drop table US_USUARIOS
go

/*==============================================================*/
/* Table: PER_PERSONAS                                          */
/*==============================================================*/
create table PER_PERSONAS (
   IDPERSONA            int                  identity,
   NOMBRE               varchar(100)         null,
   SEXO                 varchar(10)          null,
   EDAD                 int                  null,
   constraint PK_PER_PERSONAS primary key (IDPERSONA)
)
go

/*==============================================================*/
/* Table: US_USUARIOS                                           */
/*==============================================================*/
create table US_USUARIOS (
   IDUSUARIO            int                  identity,
   USUARIO              varchar(50)          null,
   CONTRASENA           varchar(30)          null,
   constraint PK_US_USUARIOS primary key (IDUSUARIO)
)
go

