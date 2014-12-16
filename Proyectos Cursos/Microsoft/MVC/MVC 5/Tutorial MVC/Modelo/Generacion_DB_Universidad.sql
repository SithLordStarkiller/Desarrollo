/*==============================================================*/
/* DBMS name:      Sybase SQL Anywhere 12                       */
/* Created on:     15/12/2014 09:32:04 p. m.                    */
/*==============================================================*/


if exists(select 1 from sys.sysforeignkey where role='FK_EMP_EMPL_REFERENCE_CAT_DEPA') then
    alter table EMP_EMPLOYEE
       delete foreign key FK_EMP_EMPL_REFERENCE_CAT_DEPA
end if;

drop table if exists CAT_DEPARMENT;

drop table if exists EMP_EMPLOYEE;

/*==============================================================*/
/* Table: CAT_DEPARMENT                                         */
/*==============================================================*/
create table CAT_DEPARMENT 
(
   DEPARMENTID          int                            not null,
   DEPARMENTNAME        varchar(20)                    not null,
   constraint PK_CAT_DEPARMENT primary key clustered (DEPARMENTID)
);

/*==============================================================*/
/* Table: EMP_EMPLOYEE                                          */
/*==============================================================*/
create table EMP_EMPLOYEE 
(
   EMPLOYEEID           int                            not null,
   EMPLOYEENAME         varchar(100)                   not null,
   GENDER               varchar(10)                    null,
   CITY                 varchar(10)                    null,
   DEPARMENTID          int                            null,
   constraint PK_EMP_EMPLOYEE primary key clustered (EMPLOYEEID)
);

alter table EMP_EMPLOYEE
   add constraint FK_EMP_EMPL_REFERENCE_CAT_DEPA foreign key (DEPARMENTID)
      references CAT_DEPARMENT (DEPARMENTID)
      on update restrict
      on delete restrict;

