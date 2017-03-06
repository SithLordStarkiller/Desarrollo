/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     02/03/2017 01:22:44 p.m.                     */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('USERS')
            and   type = 'U')
   drop table USERS
go

/*==============================================================*/
/* Table: USERS                                                 */
/*==============================================================*/
create table USERS (
   PKID                 int                  not null,
   USERNAME             varchar(255)         null,
   APPLICATIONNAME      varchar(255)         null,
   EMAIL                varchar(128)         null,
   COMMENT              varchar(255)         null,
   PASSWORD             varchar(128)         null,
   PASSWORDQUESTION     varchar(255)         null,
   PASSWORDANSWER       varchar(255)         null,
   ISAPPROVED           bit                  null,
   LASTACTIVITYDATE     datetime             null,
   LASTLOGINDATE        datetime             null,
   LASTPASSWORDCHANGEDDATE datetime             null,
   CREATIONDATE         datetime             null,
   ISONLINE             bit                  null,
   ISLOCKEDOUT          bit                  null,
   LASTLOCKEDOUTDATE    datetime             null,
   FAILEDPASSWORDATTEMPTCOUNT int                  null,
   FAILEDPASSWORDATTEMPTWINDOWSTART datetime             null,
   FAILEDPASSWORDANSWERATTEMPTCOUNT int                  null,
   FAILEDPASSWORDANSWERATTEMPTWINDOWSTART datetime             null,
   ISSUBSCRIBER         bit                  null,
   CUSTOMERID           varchar(64)          null,
   constraint PK_USERS primary key (PKID)
)
go

