/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     01/06/2015 10:07:39 a. m.                    */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_CAT_TIPO_OPERACION') and o.name = 'FK_COM_CAT__REFERENCE_LOG_COM_')
alter table COM_CAT_TIPO_OPERACION
   drop constraint FK_COM_CAT__REFERENCE_LOG_COM_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ENTREGAS_PRODUCTO') and o.name = 'FK_COM_ENTR_REFERENCE_ALM_ALME')
alter table COM_ENTREGAS_PRODUCTO
   drop constraint FK_COM_ENTR_REFERENCE_ALM_ALME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ENTREGAS_PRODUCTO') and o.name = 'FK_COM_ENTR_REFERENCE_COM_PROD')
alter table COM_ENTREGAS_PRODUCTO
   drop constraint FK_COM_ENTR_REFERENCE_COM_PROD
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
   where r.fkeyid = object_id('COM_ORDENCOMPRA') and o.name = 'FK_COM_ORDE_REFERENCE_FAC_CAT_')
alter table COM_ORDENCOMPRA
   drop constraint FK_COM_ORDE_REFERENCE_FAC_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ORDENCOMPRA') and o.name = 'FK_COM_ORDE_REFERENCE_COM_CAT_')
alter table COM_ORDENCOMPRA
   drop constraint FK_COM_ORDE_REFERENCE_COM_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS_PEDIDOS') and o.name = 'FK_COM_PROD_REFERENCE_COM_ORDE')
alter table COM_PRODUCTOS_PEDIDOS
   drop constraint FK_COM_PROD_REFERENCE_COM_ORDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS_PEDIDOS') and o.name = 'FK_COM_PRO_COMCAT_UNI')
alter table COM_PRODUCTOS_PEDIDOS
   drop constraint FK_COM_PRO_COMCAT_UNI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS_PEDIDOS') and o.name = 'FK_COM_PRO_COM_CAT_PRE')
alter table COM_PRODUCTOS_PEDIDOS
   drop constraint FK_COM_PRO_COM_CAT_PRE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS_PEDIDOS') and o.name = 'FK_COM_PROD_REFERENCE_COM_CAT_')
alter table COM_PRODUCTOS_PEDIDOS
   drop constraint FK_COM_PROD_REFERENCE_COM_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS_PEDIDOS') and o.name = 'FK_COM_PROD_REFERENCE_ALM_ALME')
alter table COM_PRODUCTOS_PEDIDOS
   drop constraint FK_COM_PROD_REFERENCE_ALM_ALME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS_PEDIDOS') and o.name = 'FK_COM_PROD_REFERENCE_COM_PROD')
alter table COM_PRODUCTOS_PEDIDOS
   drop constraint FK_COM_PROD_REFERENCE_COM_PROD
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PROVEEDORES') and o.name = 'FK_COM_PROV_REFERENCE_COM_PROV')
alter table COM_PROVEEDORES
   drop constraint FK_COM_PROV_REFERENCE_COM_PROV
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FAC_TIPO_CAMBIO_HISTORICO') and o.name = 'FK_FAC_TIPO_REFERENCE_FAC_CAT_')
alter table FAC_TIPO_CAMBIO_HISTORICO
   drop constraint FK_FAC_TIPO_REFERENCE_FAC_CAT_
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
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'FK_US_USUAR_REFERENCE_PER_PERS')
alter table US_USUARIOS
   drop constraint FK_US_USUAR_REFERENCE_PER_PERS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ALM_ALMECENES')
            and   type = 'U')
   drop table ALM_ALMECENES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_CAT_ESTATUS_COMPRA')
            and   type = 'U')
   drop table COM_CAT_ESTATUS_COMPRA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_CAT_ESTATUS_PRODUCTO')
            and   type = 'U')
   drop table COM_CAT_ESTATUS_PRODUCTO
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
           where  id = object_id('COM_ENTREGAS_PRODUCTO')
            and   type = 'U')
   drop table COM_ENTREGAS_PRODUCTO
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
           where  id = object_id('COM_PRODUCTOS_PEDIDOS')
            and   type = 'U')
   drop table COM_PRODUCTOS_PEDIDOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_PROVEEDORES')
            and   type = 'U')
   drop table COM_PROVEEDORES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COM_PROVEEDORES_CONTACTOS')
            and   type = 'U')
   drop table COM_PROVEEDORES_CONTACTOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FAC_CAT_CONDICIONES_PAGO')
            and   type = 'U')
   drop table FAC_CAT_CONDICIONES_PAGO
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
           where  id = object_id('FAC_TIPO_CAMBIO_HISTORICO')
            and   type = 'U')
   drop table FAC_TIPO_CAMBIO_HISTORICO
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
/* Table: ALM_ALMECENES                                         */
/*==============================================================*/
create table ALM_ALMECENES (
   IDALAMACENES         smallint             not null,
   NOMBREALMACEN        varchar(50)          not null,
   DESCRIPCION          varchar(100)         null,
   BORRADO              bit                  not null,
   constraint PK_ALM_ALMECENES primary key (IDALAMACENES)
)
go

/*==============================================================*/
/* Table: COM_CAT_ESTATUS_COMPRA                                */
/*==============================================================*/
create table COM_CAT_ESTATUS_COMPRA (
   IDESTATUSCOMPRA      int                  not null,
   NOMBREESTATUS        varchar(100)         not null,
   DESCRIPCION          varchar(100)         null,
   BORRADO              bit                  not null,
   constraint PK_COM_CAT_ESTATUS_COMPRA primary key (IDESTATUSCOMPRA)
)
go

/*==============================================================*/
/* Table: COM_CAT_ESTATUS_PRODUCTO                              */
/*==============================================================*/
create table COM_CAT_ESTATUS_PRODUCTO (
   IDESTAUSPRODUCTO     smallint             not null,
   ESTATUSPRODUCTO      varchar(100)         not null,
   DESCRIPCION          varchar(200)         null,
   BORRADO              bit                  not null,
   constraint PK_COM_CAT_ESTATUS_PRODUCTO primary key (IDESTAUSPRODUCTO)
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
/* Table: COM_ENTREGAS_PRODUCTO                                 */
/*==============================================================*/
create table COM_ENTREGAS_PRODUCTO (
   IDENTREGASPRODUCTO   int                  identity,
   IDALAMACENES         smallint             null,
   IDPRODUCTOSPEDIDOS   int                  null,
   FECHAENTREGA         datetime             not null,
   PARTIDASENTREGADAS   decimal(10,4)        not null,
   PAGOREALIZADO        decimal(10,2)        not null,
   constraint PK_COM_ENTREGAS_PRODUCTO primary key (IDENTREGASPRODUCTO)
)
go

/*==============================================================*/
/* Table: COM_ORDENCOMPRA                                       */
/*==============================================================*/
create table COM_ORDENCOMPRA (
   NOORDENCOMPRA        varchar(50)          not null,
   IDESTATUSCOMPRA      int                  null,
   IDIMPUESTO           int                  null,
   IDMONEDA             int                  null,
   CVEPROVEEDOR         varchar(50)          null,
   IDCONDICIONESPAGO    smallint             null,
   FECHAORDENCOMPRA     datetime             not null,
   FECHAPEDIDO          datetime             not null,
   FECHAENTREGA         datetime             not null,
   ENTREGAFRACCIONARIA  bit                  not null,
   SUBTOTAL             decimal              not null,
   TOTAL                decimal              not null,
   BORRADO              bit                  not null,
   constraint PK_COM_ORDENCOMPRA primary key nonclustered (NOORDENCOMPRA)
)
go

/*==============================================================*/
/* Table: COM_PRODUCTOS                                         */
/*==============================================================*/
create table COM_PRODUCTOS (
   IDPRODUCTO           int                  not null,
   NOMBREPRODUCTO       varchar(100)         not null,
   DESCRIPCION          varchar(200)         not null,
   constraint PK_COM_PRODUCTOS primary key (IDPRODUCTO)
)
go

/*==============================================================*/
/* Table: COM_PRODUCTOS_PEDIDOS                                 */
/*==============================================================*/
create table COM_PRODUCTOS_PEDIDOS (
   IDPRODUCTOSPEDIDOS   int                  identity,
   NOORDENCOMPRA        varchar(50)          null,
   IDUNIDADESMEDIDA     smallint             null,
   IDPRESENTACION       smallint             null,
   IDESTAUSPRODUCTO     smallint             null,
   IDALAMACENES         smallint             null,
   IDPRODUCTO           int                  null,
   CANTIDAD             decimal(10,2)        not null,
   PARTIDAS             double precision     not null,
   TOTALPARTIDAS        double precision     not null,
   FECHAENTREGA         datetime             not null,
   PRECIOUNITARIO       decimal(10,2)        not null,
   ENTREGADO            bit                  not null,
   BORRADO              bit                  not null,
   constraint PK_COM_PRODUCTOS_PEDIDOS primary key (IDPRODUCTOSPEDIDOS)
)
go

/*==============================================================*/
/* Table: COM_PROVEEDORES                                       */
/*==============================================================*/
create table COM_PROVEEDORES (
   CVEPROVEEDOR         varchar(50)          not null,
   IDPROVEEDORESCONTATOS int                  null,
   RAZONSOCIAL          varchar(100)         not null,
   RFC                  varchar(15)          null,
   DIRECCION            varchar(250)         null,
   TELEFONO             varchar(10)          null,
   constraint PK_COM_PROVEEDORES primary key (CVEPROVEEDOR)
)
go

/*==============================================================*/
/* Table: COM_PROVEEDORES_CONTACTOS                             */
/*==============================================================*/
create table COM_PROVEEDORES_CONTACTOS (
   IDPROVEEDORESCONTATOS int                  not null,
   NOMBRE               varchar(100)         not null,
   APELLIDOP            varchar(100)         null,
   APELLIDOM            varchar(100)         null,
   TELEFONOFIJO         varchar(10)          null,
   TELEFONOMOVIL        varchar(10)          null,
   CORREOELECTRONICO    varchar(50)          null,
   constraint PK_COM_PROVEEDORES_CONTACTOS primary key (IDPROVEEDORESCONTATOS)
)
go

/*==============================================================*/
/* Table: FAC_CAT_CONDICIONES_PAGO                              */
/*==============================================================*/
create table FAC_CAT_CONDICIONES_PAGO (
   IDCONDICIONESPAGO    smallint             not null,
   CONDICIONPAGO        varchar(50)          not null,
   DESCRIPCIONPAGO      varchar(150)         null,
   BORRADO              bit                  not null,
   constraint PK_FAC_CAT_CONDICIONES_PAGO primary key (IDCONDICIONESPAGO)
)
go

/*==============================================================*/
/* Table: FAC_CAT_IMPUESTO                                      */
/*==============================================================*/
create table FAC_CAT_IMPUESTO (
   IDIMPUESTO           int                  not null,
   NOMBREIMPUESTO       varchar(100)         not null,
   NOMBRECORTO          varchar(10)          null,
   PORSENTAGEIMPUESTO   decimal(10,2)        not null,
   BORRADO              bit                  not null,
   constraint PK_FAC_CAT_IMPUESTO primary key (IDIMPUESTO)
)
go

/*==============================================================*/
/* Table: FAC_CAT_MONEDA                                        */
/*==============================================================*/
create table FAC_CAT_MONEDA (
   IDMONEDA             int                  not null,
   NOMBREMONEDA         varchar(100)         not null,
   NOMBRECORTO          varchar(10)          null,
   BORRADO              bit                  not null,
   constraint PK_FAC_CAT_MONEDA primary key (IDMONEDA)
)
go

/*==============================================================*/
/* Table: FAC_TIPO_CAMBIO_HISTORICO                             */
/*==============================================================*/
create table FAC_TIPO_CAMBIO_HISTORICO (
   IDTIPOCAMBIO         int                  identity,
   IDMONEDA             int                  null,
   VALORCOMPRA          decimal(10,2)        not null,
   VALORVENTA           decimal(10,2)        not null,
   FECHA                datetime             not null,
   BORRADO              bit                  not null,
   constraint PK_FAC_TIPO_CAMBIO_HISTORICO primary key (IDTIPOCAMBIO)
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
   NOMBRE               varchar(100)         not null,
   APELLIDOP            varchar(100)         not null,
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
   IDPERSONA            int                  null,
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

alter table COM_ENTREGAS_PRODUCTO
   add constraint FK_COM_ENTR_REFERENCE_ALM_ALME foreign key (IDALAMACENES)
      references ALM_ALMECENES (IDALAMACENES)
go

alter table COM_ENTREGAS_PRODUCTO
   add constraint FK_COM_ENTR_REFERENCE_COM_PROD foreign key (IDPRODUCTOSPEDIDOS)
      references COM_PRODUCTOS_PEDIDOS (IDPRODUCTOSPEDIDOS)
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
   add constraint FK_COM_ORD_COM_PRO foreign key (CVEPROVEEDOR)
      references COM_PROVEEDORES (CVEPROVEEDOR)
go

alter table COM_ORDENCOMPRA
   add constraint FK_COM_ORDE_REFERENCE_FAC_CAT_ foreign key (IDCONDICIONESPAGO)
      references FAC_CAT_CONDICIONES_PAGO (IDCONDICIONESPAGO)
go

alter table COM_ORDENCOMPRA
   add constraint FK_COM_ORDE_REFERENCE_COM_CAT_ foreign key (IDESTATUSCOMPRA)
      references COM_CAT_ESTATUS_COMPRA (IDESTATUSCOMPRA)
go

alter table COM_PRODUCTOS_PEDIDOS
   add constraint FK_COM_PROD_REFERENCE_COM_ORDE foreign key (NOORDENCOMPRA)
      references COM_ORDENCOMPRA (NOORDENCOMPRA)
go

alter table COM_PRODUCTOS_PEDIDOS
   add constraint FK_COM_PRO_COMCAT_UNI foreign key (IDUNIDADESMEDIDA)
      references COM_CAT_UNIDADES_MEDIDA (IDUNIDADESMEDIDA)
go

alter table COM_PRODUCTOS_PEDIDOS
   add constraint FK_COM_PRO_COM_CAT_PRE foreign key (IDPRESENTACION)
      references COM_CAT_PRESENTACION (IDPRESENTACION)
go

alter table COM_PRODUCTOS_PEDIDOS
   add constraint FK_COM_PROD_REFERENCE_COM_CAT_ foreign key (IDESTAUSPRODUCTO)
      references COM_CAT_ESTATUS_PRODUCTO (IDESTAUSPRODUCTO)
go

alter table COM_PRODUCTOS_PEDIDOS
   add constraint FK_COM_PROD_REFERENCE_ALM_ALME foreign key (IDALAMACENES)
      references ALM_ALMECENES (IDALAMACENES)
go

alter table COM_PRODUCTOS_PEDIDOS
   add constraint FK_COM_PROD_REFERENCE_COM_PROD foreign key (IDPRODUCTO)
      references COM_PRODUCTOS (IDPRODUCTO)
go

alter table COM_PROVEEDORES
   add constraint FK_COM_PROV_REFERENCE_COM_PROV foreign key (IDPROVEEDORESCONTATOS)
      references COM_PROVEEDORES_CONTACTOS (IDPROVEEDORESCONTATOS)
go

alter table FAC_TIPO_CAMBIO_HISTORICO
   add constraint FK_FAC_TIPO_REFERENCE_FAC_CAT_ foreign key (IDMONEDA)
      references FAC_CAT_MONEDA (IDMONEDA)
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

alter table US_USUARIOS
   add constraint FK_US_USUAR_REFERENCE_PER_PERS foreign key (IDPERSONA)
      references PER_PERSONA (IDPERSONA)
go

