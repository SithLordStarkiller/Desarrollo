/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     20/12/2014 12:36:18 p. m.                    */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EMP_EMPLOYEE') and o.name = 'FK_EMP_EMPL_REFERENCE_CAT_DEPA')
alter table EMP_EMPLOYEE
   drop constraint FK_EMP_EMPL_REFERENCE_CAT_DEPA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CAT_DEPARMENT')
            and   type = 'U')
   drop table CAT_DEPARMENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EMP_EMPLOYEE')
            and   type = 'U')
   drop table EMP_EMPLOYEE
go

/*==============================================================*/
/* Table: CAT_DEPARMENT                                         */
/*==============================================================*/
create table CAT_DEPARMENT (
   IDDEPARMENT          int                  not null,
   DEPARMENTNAME        varchar(20)          not null,
   constraint PK_CAT_DEPARMENT primary key (IDDEPARMENT)
)
go

/*==============================================================*/
/* Table: EMP_EMPLOYEE                                          */
/*==============================================================*/
create table EMP_EMPLOYEE (
   IDEMPLOYEE           int                  identity,
   NAME                 varchar(100)         not null,
   GENDER               varchar(10)          not null,
   CITY                 varchar(15)          null,
   IDDEPARMENT          int                  null,
   constraint PK_EMP_EMPLOYEE primary key (IDEMPLOYEE)
)
go

alter table EMP_EMPLOYEE
   add constraint FK_EMP_EMPL_REFERENCE_CAT_DEPA foreign key (IDDEPARMENT)
      references CAT_DEPARMENT (IDDEPARMENT)
go

