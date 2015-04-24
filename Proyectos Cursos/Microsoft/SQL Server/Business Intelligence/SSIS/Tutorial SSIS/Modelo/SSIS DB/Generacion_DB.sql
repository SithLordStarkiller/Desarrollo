/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     23/04/2015 12:12:40 p. m.                    */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('SERIES')
            and   type = 'U')
   drop table SERIES
go

/*==============================================================*/
/* Table: SERIES                                                */
/*==============================================================*/
create table SERIES (
   IDSERIES             smallint             not null,
   WINNER               varchar(100)         null,
   STARDATE             datetime             null,
   ENDDATE              datetime             null,
   GENDER               char(1)              null,
   constraint PK_SERIES primary key (IDSERIES)
)
go

