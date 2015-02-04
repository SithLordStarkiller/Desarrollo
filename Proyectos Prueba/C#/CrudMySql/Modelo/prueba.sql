/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     03/02/2015 10:14:33 a. m.                    */
/*==============================================================*/


drop table if exists US_CAT_ROL;

drop table if exists US_USUARIOS;

/*==============================================================*/
/* Table: US_CAT_ROL                                            */
/*==============================================================*/
create table US_CAT_ROL
(
   IDROL                int not null,
   NOMBREROL            varchar(50) not null,
   primary key (IDROL)
);

/*==============================================================*/
/* Table: US_USUARIOS                                           */
/*==============================================================*/
create table US_USUARIOS
(
   IDUSUARIO            int not null auto_increment,
   IDROL                int,
   NOMBRECOMPLETO       varchar(100) not null,
   FECHANACIMIENTO      date not null,
   SEXO                 varchar(10),
   USUARIO              varchar(20) not null,
   CONTRASENA           varchar(20) not null,
   primary key (IDUSUARIO)
);

alter table US_USUARIOS add constraint FK_REFERENCE_1 foreign key (IDROL)
      references US_CAT_ROL (IDROL) on delete restrict on update restrict;
