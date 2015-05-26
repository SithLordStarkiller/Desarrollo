/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     25/05/2015 11:54:08 a. m.                    */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_CAT_TIPO_OPERACION') and o.name = 'FK_COM_CAT__REFERENCE_LOG_COM_')
alter table COM_CAT_TIPO_OPERACION
   drop constraint FK_COM_CAT__REFERENCE_LOG_COM_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ORDENCOMPRA') and o.name = 'FK_COM_ORD_FAC_CAT_IMP')
alter table COM_ORDENCOMPRA
   drop constraint FK_COM_ORD_FAC_CAT_IMP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ORDENCOMPRA') and o.name = 'FK_COM_ORD_FAC_CAT_MON')
alter table COM_ORDENCOMPRA
   drop constraint FK_COM_ORD_FAC_CAT_MON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ORDENCOMPRA') and o.name = 'FK_COM_ORD_COM_PRO')
alter table COM_ORDENCOMPRA
   drop constraint FK_COM_ORD_COM_PRO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ORDENCOMPRA') and o.name = 'FK_COM_ORDE_REFERENCE_ALM_CAT_')
alter table COM_ORDENCOMPRA
   drop constraint FK_COM_ORDE_REFERENCE_ALM_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ORDENCOMPRA') and o.name = 'FK_COM_ORDE_REFERENCE_COM_ESTA')
alter table COM_ORDENCOMPRA
   drop constraint FK_COM_ORDE_REFERENCE_COM_ESTA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS') and o.name = 'FK_COM_PROD_REFERENCE_COM_ORDE')
alter table COM_PRODUCTOS
   drop constraint FK_COM_PROD_REFERENCE_COM_ORDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS') and o.name = 'FK_COM_PRO_COMCAT_UNI')
alter table COM_PRODUCTOS
   drop constraint FK_COM_PRO_COMCAT_UNI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS') and o.name = 'FK_COM_PRO_COM_CAT_PRE')
alter table COM_PRODUCTOS
   drop constraint FK_COM_PRO_COM_CAT_PRE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONA') and o.name = 'FK_PER_PERS_REFERENCE_US_USUAR')
alter table PER_PERSONA
   drop constraint FK_PER_PERS_REFERENCE_US_USUAR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_MENUARBOL') and o.name = 'FK_SIS_MENU_REFERENCE_SIS_MENU')
alter table SIS_MENUARBOL
   drop constraint FK_SIS_MENU_REFERENCE_SIS_MENU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_PERFILES_MENU') and o.name = 'FK_SIS_PERF_REFERENCE_SIS_MENU')
alter table SIS_PERFILES_MENU
   drop constraint FK_SIS_PERF_REFERENCE_SIS_MENU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SIS_PERFILES_MENU') and o.name = 'FK_SIS_PERF_REFERENCE_US_CAT_P')
alter table SIS_PERFILES_MENU
   drop constraint FK_SIS_PERF_REFERENCE_US_CAT_P
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_PERFILES') and o.name = 'FK_US_PERFI_REFERENCE_US_USUAR')
alter table US_PERFILES
   drop constraint FK_US_PERFI_REFERENCE_US_USUAR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_PERFILES') and o.name = 'FK_US_PERFI_REFERENCE_US_CAT_P')
alter table US_PERFILES
   drop constraint FK_US_PERFI_REFERENCE_US_CAT_P
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ALM_CAT_ALMECENES')
            and   type = 'U')
   drop table ALM_CAT_ALMECENES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_CAT_PRESENTACION')
            and   type = 'U')
   drop table COM_CAT_PRESENTACION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_CAT_TIPO_OPERACION')
            and   type = 'U')
   drop table COM_CAT_TIPO_OPERACION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_CAT_UNIDADES_MEDIDA')
            and   type = 'U')
   drop table COM_CAT_UNIDADES_MEDIDA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_ESTATUS_COMPRA')
            and   type = 'U')
   drop table COM_ESTATUS_COMPRA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_ORDENCOMPRA')
            and   type = 'U')
   drop table COM_ORDENCOMPRA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_PRODUCTOS')
            and   type = 'U')
   drop table COM_PRODUCTOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_PROVEEDORES')
            and   type = 'U')
   drop table COM_PROVEEDORES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FAC_CAT_IMPUESTO')
            and   type = 'U')
   drop table FAC_CAT_IMPUESTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FAC_CAT_MONEDA')
            and   type = 'U')
   drop table FAC_CAT_MONEDA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOG_COM_OPERACIONES')
            and   type = 'U')
   drop table LOG_COM_OPERACIONES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PER_PERSONA')
            and   type = 'U')
   drop table PER_PERSONA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SIS_MENUARBOL')
            and   type = 'U')
   drop table SIS_MENUARBOL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SIS_PERFILES_MENU')
            and   type = 'U')
   drop table SIS_PERFILES_MENU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_CAT_PERFILES')
            and   type = 'U')
   drop table US_CAT_PERFILES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_PERFILES')
            and   type = 'U')
   drop table US_PERFILES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_USUARIOS')
            and   type = 'U')
   drop table US_USUARIOS
go

/*==============================================================*/
/* Table: ALM_CAT_ALMECENES                                     */
/*==============================================================*/
create table ALM_CAT_ALMECENES (
   IDALAMACENES         smallint             not null,
   NOMBREALMACEN        varchar(50)          not null,
   DESCRIPCION          varchar(100)         null,
   BORRADO              bit                  not null,
   constraint PK_ALM_CAT_ALMECENES primary key (IDALAMACENES)
)
go

/*==============================================================*/
/* Table: COM_CAT_PRESENTACION                                  */
/*==============================================================*/
create table COM_CAT_PRESENTACION (
   IDPRESENTACION       smallint             not null,
   PRESENTACION         varchar(100)         null,
   DESCRIPCION          varchar(200)         null,
   BORRADO              bit                  null,
   constraint PK_COM_CAT_PRESENTACION primary key (IDPRESENTACION)
)
go

/*==============================================================*/
/* Table: COM_CAT_TIPO_OPERACION                                */
/*==============================================================*/
create table COM_CAT_TIPO_OPERACION (
   IDTIPOOPERACION      int                  not null,
   IDLOGOPERACIONES     int                  null,
   NOMBREOPERACION      varchar(50)          not null,
   DESCRIPCION          varchar(50)          null,
   BORRADO              bit                  not null,
   constraint PK_COM_CAT_TIPO_OPERACION primary key (IDTIPOOPERACION)
)
go

/*==============================================================*/
/* Table: COM_CAT_UNIDADES_MEDIDA                               */
/*==============================================================*/
create table COM_CAT_UNIDADES_MEDIDA (
   IDUNIDADESMEDIDA     smallint             not null,
   TIPOUNIDAD           varchar(100)         null,
   DESCRIPCION          varchar(100)         null,
   BORRADO              bit                  null,
   constraint PK_COM_CAT_UNIDADES_MEDIDA primary key (IDUNIDADESMEDIDA)
)
go

/*==============================================================*/
/* Table: COM_ESTATUS_COMPRA                                    */
/*==============================================================*/
create table COM_ESTATUS_COMPRA (
   IDESTATUSCOMPRA      int                  not null,
   NOMBREESTATUS        varchar(100)         not null,
   DESCRIPCION          varchar(100)         null,
   BORRADO              bit                  not null,
   constraint PK_COM_ESTATUS_COMPRA primary key (IDESTATUSCOMPRA)
)
go

/*==============================================================*/
/* Table: COM_ORDENCOMPRA                                       */
/*==============================================================*/
create table COM_ORDENCOMPRA (
   IDORDENCOMPRA        int                  identity,
   NOORDENCOMPRA        varchar(50)          not null,
   IDALAMACENES         smallint             null,
   IDESTATUSCOMPRA      int                  null,
   IDIMPUESTO           int                  null,
   IDMONEDA             int                  null,
   IDPROVEEDOR          int                  null,
   FECHAORDENCOMPRA     datetime             not null,
   FECHAPEDIDO          datetime             not null,
   FECHAENTREGA         datetime             not null,
   CANTIDADENTREGADA    decimal              not null,
   CANTIDADTOTAL        decimal              not null,
   ENTREGAFRACCIONARIA  bit                  not null,
   SUBTOTAL             decimal              not null,
   TOTAL                decimal              not null,
   BORRADO              bit                  not null,
   constraint PK_COM_ORDENCOMPRA primary key nonclustered (NOORDENCOMPRA, IDORDENCOMPRA)
)
go

/*==============================================================*/
/* Table: COM_PRODUCTOS                                         */
/*==============================================================*/
create table COM_PRODUCTOS (
   IDPRODUCTOS          int                  identity,
   NOORDENCOMPRA        varchar(50)          null,
   IDORDENCOMPRA        int                  null,
   IDUNIDADESMEDIDA     smallint             null,
   IDPRESENTACION       smallint             null,
   NOMBREPRODUCTO       varchar(100)         not null,
   LOTE                 varchar(50)          not null,
   CANTIDADPRODUCTO     decimal              not null,
   FECHAENTREGA         datetime             not null,
   PRECIOUNITARIO       money                not null,
   ENTREGADO            bit                  not null,
   BORRADO              bit                  not null,
   constraint PK_COM_PRODUCTOS primary key (IDPRODUCTOS)
)
go

/*==============================================================*/
/* Table: COM_PROVEEDORES                                       */
/*==============================================================*/
create table COM_PROVEEDORES (
   IDPROVEEDOR          int                  not null,
   NOMBREPROVEEDOR      varchar(100)         not null,
   NOMBRECONTACTO       varchar(100)         not null,
   TELEFONOCONTACTO     varchar(100)         null,
   CORREOELECTRONICOCONTACTO varchar(100)         null,
   constraint PK_COM_PROVEEDORES primary key (IDPROVEEDOR)
)
go

/*==============================================================*/
/* Table: FAC_CAT_IMPUESTO                                      */
/*==============================================================*/
create table FAC_CAT_IMPUESTO (
   IDIMPUESTO           int                  not null,
   NOMBREIMPUESTO       varchar(100)         not null,
   PORSENTAGEIMPUESTO   decimal              not null,
   constraint PK_FAC_CAT_IMPUESTO primary key (IDIMPUESTO)
)
go

/*==============================================================*/
/* Table: FAC_CAT_MONEDA                                        */
/*==============================================================*/
create table FAC_CAT_MONEDA (
   IDMONEDA             int                  not null,
   NOMBREMONEDA         varchar(100)         not null,
   VALORPORDOLAR        decimal              not null,
   constraint PK_FAC_CAT_MONEDA primary key (IDMONEDA)
)
go

/*==============================================================*/
/* Table: LOG_COM_OPERACIONES                                   */
/*==============================================================*/
create table LOG_COM_OPERACIONES (
   IDLOGOPERACIONES     int                  identity,
   TIPOOPERACION        varchar(50)          null,
   DESCRIPCION          varchar(MAX)         null,
   constraint PK_LOG_COM_OPERACIONES primary key (IDLOGOPERACIONES)
)
go

/*==============================================================*/
/* Table: PER_PERSONA                                           */
/*==============================================================*/
create table PER_PERSONA (
   IDPERSONA            int                  identity,
   IDUSUARIOS           int                  null,
   NOMBRE               varchar(100)         not null,
   APELLIDOP            varchar(100)         null,
   APELLIDOM            varchar(100)         null,
   FECHANACIMINETO      datetime             null,
   GENERO               varchar(20)          null,
   FECHAREGISTRO        datetime             null,
   BORRADO              bit                  not null,
   constraint PK_PER_PERSONA primary key (IDPERSONA)
)
go

/*==============================================================*/
/* Table: SIS_MENUARBOL                                         */
/*==============================================================*/
create table SIS_MENUARBOL (
   IDMENU               int                  not null,
   IDMENUPADRE          int                  null,
   NOMBRE               varchar(50)          not null,
   DIRECCION            varchar(MAX)         not null,
   constraint PK_SIS_MENUARBOL primary key (IDMENU)
)
go

/*==============================================================*/
/* Table: SIS_PERFILES_MENU                                     */
/*==============================================================*/
create table SIS_PERFILES_MENU (
   IDPERFILESMENU       smallint             not null,
   IDMENU               int                  null,
   IDPERFIL             smallint             null,
   ENLICENCIA           bit                  not null,
   ACTIVO               bit                  not null,
   BORRADO              bit                  not null,
   constraint PK_SIS_PERFILES_MENU primary key (IDPERFILESMENU)
)
go

/*==============================================================*/
/* Table: US_CAT_PERFILES                                       */
/*==============================================================*/
create table US_CAT_PERFILES (
   IDPERFIL             smallint             not null,
   NOMBREPERFIL         varchar(50)          not null,
   DESCRIPCION          varchar(100)         null,
   BORRADO              bit                  not null,
   constraint PK_US_CAT_PERFILES primary key (IDPERFIL)
)
go

/*==============================================================*/
/* Table: US_PERFILES                                           */
/*==============================================================*/
create table US_PERFILES (
   IDUSURIOPERFILES     int                  identity,
   IDUSUARIOS           int                  null,
   IDPERFIL             smallint             null,
   BORRADO              bit                  null,
   constraint PK_US_PERFILES primary key (IDUSURIOPERFILES)
)
go

/*==============================================================*/
/* Table: US_USUARIOS                                           */
/*==============================================================*/
create table US_USUARIOS (
   IDUSUARIOS           int                  identity,
   USUARIO              varchar(50)          not null,
   CONTRASENA           varchar(50)          not null,
   ULTIMASESION         datetime             null,
   BORRADO              bit                  not null,
   constraint PK_US_USUARIOS primary key (IDUSUARIOS)
)
go

alter table COM_CAT_TIPO_OPERACION
   add constraint FK_COM_CAT__REFERENCE_LOG_COM_ foreign key (IDLOGOPERACIONES)
      references LOG_COM_OPERACIONES (IDLOGOPERACIONES)
go

alter table COM_ORDENCOMPRA
   add constraint FK_COM_ORD_FAC_CAT_IMP foreign key (IDIMPUESTO)
      references FAC_CAT_IMPUESTO (IDIMPUESTO)
go

alter table COM_ORDENCOMPRA
   add constraint FK_COM_ORD_FAC_CAT_MON foreign key (IDMONEDA)
      references FAC_CAT_MONEDA (IDMONEDA)
go

alter table COM_ORDENCOMPRA
   add constraint FK_COM_ORD_COM_PRO foreign key (IDPROVEEDOR)
      references COM_PROVEEDORES (IDPROVEEDOR)
go

alter table COM_ORDENCOMPRA
   add constraint FK_COM_ORDE_REFERENCE_ALM_CAT_ foreign key (IDALAMACENES)
      references ALM_CAT_ALMECENES (IDALAMACENES)
go

alter table COM_ORDENCOMPRA
   add constraint FK_COM_ORDE_REFERENCE_COM_ESTA foreign key (IDESTATUSCOMPRA)
      references COM_ESTATUS_COMPRA (IDESTATUSCOMPRA)
go

alter table COM_PRODUCTOS
   add constraint FK_COM_PROD_REFERENCE_COM_ORDE foreign key (NOORDENCOMPRA, IDORDENCOMPRA)
      references COM_ORDENCOMPRA (NOORDENCOMPRA, IDORDENCOMPRA)
go

alter table COM_PRODUCTOS
   add constraint FK_COM_PRO_COMCAT_UNI foreign key (IDUNIDADESMEDIDA)
      references COM_CAT_UNIDADES_MEDIDA (IDUNIDADESMEDIDA)
go

alter table COM_PRODUCTOS
   add constraint FK_COM_PRO_COM_CAT_PRE foreign key (IDPRESENTACION)
      references COM_CAT_PRESENTACION (IDPRESENTACION)
go

alter table PER_PERSONA
   add constraint FK_PER_PERS_REFERENCE_US_USUAR foreign key (IDUSUARIOS)
      references US_USUARIOS (IDUSUARIOS)
go

alter table SIS_MENUARBOL
   add constraint FK_SIS_MENU_REFERENCE_SIS_MENU foreign key (IDMENUPADRE)
      references SIS_MENUARBOL (IDMENU)
go

alter table SIS_PERFILES_MENU
   add constraint FK_SIS_PERF_REFERENCE_SIS_MENU foreign key (IDMENU)
      references SIS_MENUARBOL (IDMENU)
go

alter table SIS_PERFILES_MENU
   add constraint FK_SIS_PERF_REFERENCE_US_CAT_P foreign key (IDPERFIL)
      references US_CAT_PERFILES (IDPERFIL)
go

alter table US_PERFILES
   add constraint FK_US_PERFI_REFERENCE_US_USUAR foreign key (IDUSUARIOS)
      references US_USUARIOS (IDUSUARIOS)
go

alter table US_PERFILES
   add constraint FK_US_PERFI_REFERENCE_US_CAT_P foreign key (IDPERFIL)
      references US_CAT_PERFILES (IDPERFIL)
go

