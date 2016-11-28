/*==============================================================*/
/* DBMS name:      ORACLE Version 11g                           */
/* Created on:     21/11/2016 09:32:06 a. m.                    */
/*==============================================================*/


alter table USUSUARIO
   drop constraint FK_USUSUARI_REFERENCE_USSTATUS;

drop table USSTATUS cascade constraints;

drop table USUSUARIO cascade constraints;

/*==============================================================*/
/* Table: USSTATUS                                              */
/*==============================================================*/
create table USSTATUS 
(
   IDSTATUS             NUMBER               not null,
   STATUS               VARCHAR(100),
   DESCRIPTION          VARCHAR(200),
   constraint PK_USSTATUS primary key (IDSTATUS)
);

/*==============================================================*/
/* Table: USUSUARIO                                             */
/*==============================================================*/
create table USUSUARIO 
(
   IDUSER               NUMBER               not null,
   IDSTATUS             NUMBER,
   USERNAME             VARCHAR(50),
   PASSWORD             VARCHAR(20),
   constraint PK_USUSUARIO primary key (IDUSER)
);

alter table USUSUARIO
   add constraint FK_USUSUARI_REFERENCE_USSTATUS foreign key (IDSTATUS)
      references USSTATUS (IDSTATUS);

