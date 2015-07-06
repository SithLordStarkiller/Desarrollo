/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     06/07/2015 01:17:30 p. m.                    */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_CAT_TIPO_OPERACION') and o.name = 'FK_COM_CAT__REFERENCE_LOG_COM_')
alter table COM_CAT_TIPO_OPERACION
   drop constraint FK_COM_CAT__REFERENCE_LOG_COM_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ENTREGAS_PRODUCTO') and o.name = 'FK_COM_ENTR_COM_PROD')
alter table COM_ENTREGAS_PRODUCTO
   drop constraint FK_COM_ENTR_COM_PROD
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_ENTREGAS_PRODUCTO') and o.name = 'FK_COM_ENTR_REFERENCE_ALM_ALME')
alter table COM_ENTREGAS_PRODUCTO
   drop constraint FK_COM_ENTR_REFERENCE_ALM_ALME
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
   where r.fkeyid = object_id('COM_PRODUCTOS_PEDIDOS') and o.name = 'FK_COM_PROD_REFERENCE_COM_PROD')
alter table COM_PRODUCTOS_PEDIDOS
   drop constraint FK_COM_PROD_REFERENCE_COM_PROD
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PRODUCTOS_PEDIDOS') and o.name = 'FK_COM_PROD_REFERENCE_ALM_ALME')
alter table COM_PRODUCTOS_PEDIDOS
   drop constraint FK_COM_PROD_REFERENCE_ALM_ALME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COM_PROVEEDORES') and o.name = 'FK_COM_PROV_REFERENCE_COM_PROV')
alter table COM_PROVEEDORES
   drop constraint FK_COM_PROV_REFERENCE_COM_PROV
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_COLONIAS') and o.name = 'FK_DIR_CAT__COLONIAS__DIR_CAT_')
alter table DIR_CAT_COLONIAS
   drop constraint FK_DIR_CAT__COLONIAS__DIR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_COLONIAS') and o.name = 'FK_DIR_CAT__REFERENCE_DIR_CAT_')
alter table DIR_CAT_COLONIAS
   drop constraint FK_DIR_CAT__REFERENCE_DIR_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_COLONIAS') and o.name = 'Reference_26')
alter table DIR_CAT_COLONIAS
   drop constraint Reference_26
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_COLONIAS') and o.name = 'Reference_29')
alter table DIR_CAT_COLONIAS
   drop constraint Reference_29
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_CAT_DELG_MUNICIPIO') and o.name = 'Reference_27')
alter table DIR_CAT_DELG_MUNICIPIO
   drop constraint Reference_27
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_DIRECCIONES') and o.name = 'Reference_28')
alter table DIR_DIRECCIONES
   drop constraint Reference_28
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DIR_DIRECCIONES') and o.name = 'Reference_32')
alter table DIR_DIRECCIONES
   drop constraint Reference_32
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FAC_TIPO_CAMBIO_HISTORICO') and o.name = 'FK_FAC_TIPO_REFERENCE_FAC_CAT_')
alter table FAC_TIPO_CAMBIO_HISTORICO
   drop constraint FK_FAC_TIPO_REFERENCE_FAC_CAT_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONA') and o.name = 'FK_PER_PERS_REFERENCE_DIR_DIRE')
alter table PER_PERSONA
   drop constraint FK_PER_PERS_REFERENCE_DIR_DIRE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONA') and o.name = 'FK_PER_PERS_REFERENCE_TEL_TELE')
alter table PER_PERSONA
   drop constraint FK_PER_PERS_REFERENCE_TEL_TELE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PER_PERSONA') and o.name = 'FK_PER_PERS_REFERENCE_CON_CONT')
alter table PER_PERSONA
   drop constraint FK_PER_PERS_REFERENCE_CON_CONT
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
           where  id = object_id('CON_CONTACTO')
            and   type = 'U')
   drop table CON_CONTACTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_COLONIAS')
            and   type = 'U')
   drop table DIR_CAT_COLONIAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_DELG_MUNICIPIO')
            and   type = 'U')
   drop table DIR_CAT_DELG_MUNICIPIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_ESTADO')
            and   type = 'U')
   drop table DIR_CAT_ESTADO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_TIPO_ASENTAMIENTO')
            and   type = 'U')
   drop table DIR_CAT_TIPO_ASENTAMIENTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_CAT_TIPO_ZONA')
            and   type = 'U')
   drop table DIR_CAT_TIPO_ZONA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIR_DIRECCIONES')
            and   type = 'U')
   drop table DIR_DIRECCIONES
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
           where  id = object_id('LOG_OPERACIONES')
            and   type = 'U')
   drop table LOG_OPERACIONES
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
           where  id = object_id('TEL_TELEFONOS')
            and   type = 'U')
   drop table TEL_TELEFONOS
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
   CVEALAMACENES        varchar(20)          not null,
   NOMBREALMACEN        varchar(50)          not null,
   DESCRIPCION          varchar(100)         null,
   BORRADO              bit                  not null,
   constraint PK_ALM_ALMECENES primary key (CVEALAMACENES)
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
   IDPRODUCTOSPEDIDOS   int                  null,
   CVEALAMACENES        varchar(20)          null,
   FECHAENTREGA         datetime             not null,
   PARTIDASENTREGADAS   decimal(10,4)        not null,
   PAGOREALIZADO        decimal(10,2)        not null,
   PARTIDA              int                  not null,
   BORRADO              bit                  not null,
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
   CVEPRODUCTO          varchar(20)          not null,
   NOMBREPRODUCTO       varchar(100)         not null,
   DESCRIPCION          varchar(200)         not null,
   BORRADO              bit                  not null,
   constraint PK_COM_PRODUCTOS primary key (CVEPRODUCTO)
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
   CVEPRODUCTO          varchar(20)          null,
   CVEALAMACENES        varchar(20)          null,
   PARTIDA              int                  not null,
   CANTIDAD             decimal(10,4)        not null,
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
   BORRADO              bit                  not null,
   constraint PK_COM_PROVEEDORES primary key (CVEPROVEEDOR)
)
go

/*==============================================================*/
/* Table: COM_PROVEEDORES_CONTACTOS                             */
/*==============================================================*/
create table COM_PROVEEDORES_CONTACTOS (
   IDPROVEEDORESCONTATOS int                  not null,
   NOMBRECOMPLETO       varchar(100)         not null,
   TELEFONOFIJO         varchar(10)          null,
   TELEFONOMOVIL        varchar(10)          null,
   CORREOELECTRONICO    varchar(50)          null,
   BORRADO              bit                  not null,
   constraint PK_COM_PROVEEDORES_CONTACTOS primary key (IDPROVEEDORESCONTATOS)
)
go

/*==============================================================*/
/* Table: CON_CONTACTO                                          */
/*==============================================================*/
create table CON_CONTACTO (
   IDCONTACTOS          int                  identity,
   CORREOELECTRONICOPERSONAL varchar(100)         null,
   CORREOELECTRONICOTRABAJO varchar(100)         null,
   ALTERNATIVO1         varchar(100)         null,
   ALTERNATIVO2         varchar(100)         null,
   BORRADO              bit                  not null,
   constraint PK_CON_CONTACTO primary key (IDCONTACTOS)
)
go

/*==============================================================*/
/* Table: DIR_CAT_COLONIAS                                      */
/*==============================================================*/
create table DIR_CAT_COLONIAS (
   IDCOLONIA            int                  not null,
   IDESTADO             int                  null,
   IDDELGMUNICIPIO      int                  null,
   IDTIPOASENTAMIENTO   int                  null,
   IDTIPOZONA           int                  null,
   IDMUNICIPIO          int                  null,
   CODIGOPOSTAL         int                  null,
   NOMBRECOLONIA        varchar(100)         null,
   constraint PK_DIR_CAT_COLONIAS primary key (IDCOLONIA)
)
go

/*==============================================================*/
/* Table: DIR_CAT_DELG_MUNICIPIO                                */
/*==============================================================*/
create table DIR_CAT_DELG_MUNICIPIO (
   IDDELGMUNICIPIO      int                  not null,
   IDESTADO             int                  null,
   IDMUNICIPIO          int                  null,
   NOMBREDELGMUNICIPIO  varchar(50)          null,
   constraint PK_DIR_CAT_DELG_MUNICIPIO primary key (IDDELGMUNICIPIO)
)
go

/*==============================================================*/
/* Table: DIR_CAT_ESTADO                                        */
/*==============================================================*/
create table DIR_CAT_ESTADO (
   IDESTADO             int                  not null,
   NOMBREESTADO         varchar(50)          not null,
   NOMBREESTADOOFICIAL  varchar(50)          null,
   ESTADO               varchar(50)          null,
   constraint PK_DIR_CAT_ESTADO primary key (IDESTADO)
)
go

/*==============================================================*/
/* Table: DIR_CAT_TIPO_ASENTAMIENTO                             */
/*==============================================================*/
create table DIR_CAT_TIPO_ASENTAMIENTO (
   IDTIPOASENTAMIENTO   int                  not null,
   TIPOASENTAMIENTO     varchar(100)         not null,
   constraint PK_DIR_CAT_TIPO_ASENTAMIENTO primary key (IDTIPOASENTAMIENTO)
)
go

/*==============================================================*/
/* Table: DIR_CAT_TIPO_ZONA                                     */
/*==============================================================*/
create table DIR_CAT_TIPO_ZONA (
   IDTIPOZONA           int                  not null,
   TIPOZONA             varchar(100)         null,
   constraint PK_DIR_CAT_TIPO_ZONA primary key (IDTIPOZONA)
)
go

/*==============================================================*/
/* Table: DIR_DIRECCIONES                                       */
/*==============================================================*/
create table DIR_DIRECCIONES (
   IDDIRECCION          int                  identity,
   IDESTADO             int                  null,
   IDMUNICIPIO          int                  null,
   IDCOLONIA            int                  null,
   CALLE                varchar(100)         null,
   NOEXT                varchar(30)          null,
   NOINT                varchar(30)          null,
   REFERENCIAS          varchar(150)         null,
   constraint PK_DIR_DIRECCIONES primary key (IDDIRECCION)
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
/* Table: LOG_OPERACIONES                                       */
/*==============================================================*/
create table LOG_OPERACIONES (
   IDOPERACIONES        int                  not null,
   FECHAOPERACION       datetime             not null,
   XMLREGISTRO          xml                  not null,
   XMLREGISTROACTUALIZADO xml                  not null,
   TABLAMODIFICADA      varchar(50)          not null,
   TIPODEMOVIMIENTO     varchar(50)          not null,
   XMLDATOSPC           xml                  not null,
   constraint PK_LOG_OPERACIONES primary key (IDOPERACIONES)
)
go

/*==============================================================*/
/* Table: PER_PERSONA                                           */
/*==============================================================*/
create table PER_PERSONA (
   IDPERSONA            int                  identity,
   IDDIRECCION          int                  null,
   IDTELEFONOS          int                  null,
   IDCONTACTOS          int                  null,
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
   BORRADO              bit                  null,
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
/* Table: TEL_TELEFONOS                                         */
/*==============================================================*/
create table TEL_TELEFONOS (
   IDTELEFONOS          int                  not null,
   TELEFONOFIJOPERSONAL varchar(10)          null,
   TELEFONOMOVILPERSONAL varchar(10)          null,
   TELEFONOFIJOTRABAJO  varchar(10)          null,
   TELEFONOMOVILTRABAJO varchar(10)          null,
   BORRADO              bit                  not null,
   constraint PK_TEL_TELEFONOS primary key (IDTELEFONOS)
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
   add constraint FK_COM_ENTR_COM_PROD foreign key (IDPRODUCTOSPEDIDOS)
      references COM_PRODUCTOS_PEDIDOS (IDPRODUCTOSPEDIDOS)
go

alter table COM_ENTREGAS_PRODUCTO
   add constraint FK_COM_ENTR_REFERENCE_ALM_ALME foreign key (CVEALAMACENES)
      references ALM_ALMECENES (CVEALAMACENES)
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
   add constraint FK_COM_PROD_REFERENCE_COM_PROD foreign key (CVEPRODUCTO)
      references COM_PRODUCTOS (CVEPRODUCTO)
go

alter table COM_PRODUCTOS_PEDIDOS
   add constraint FK_COM_PROD_REFERENCE_ALM_ALME foreign key (CVEALAMACENES)
      references ALM_ALMECENES (CVEALAMACENES)
go

alter table COM_PROVEEDORES
   add constraint FK_COM_PROV_REFERENCE_COM_PROV foreign key (IDPROVEEDORESCONTATOS)
      references COM_PROVEEDORES_CONTACTOS (IDPROVEEDORESCONTATOS)
go

alter table DIR_CAT_COLONIAS
   add constraint FK_DIR_CAT__COLONIAS__DIR_CAT_ foreign key (IDTIPOASENTAMIENTO)
      references DIR_CAT_TIPO_ASENTAMIENTO (IDTIPOASENTAMIENTO)
go

alter table DIR_CAT_COLONIAS
   add constraint FK_DIR_CAT__REFERENCE_DIR_CAT_ foreign key (IDTIPOZONA)
      references DIR_CAT_TIPO_ZONA (IDTIPOZONA)
go

alter table DIR_CAT_COLONIAS
   add constraint Reference_26 foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
go

alter table DIR_CAT_COLONIAS
   add constraint Reference_29 foreign key (IDDELGMUNICIPIO)
      references DIR_CAT_DELG_MUNICIPIO (IDDELGMUNICIPIO)
go

alter table DIR_CAT_DELG_MUNICIPIO
   add constraint Reference_27 foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
go

alter table DIR_DIRECCIONES
   add constraint Reference_28 foreign key (IDESTADO)
      references DIR_CAT_ESTADO (IDESTADO)
go

alter table DIR_DIRECCIONES
   add constraint Reference_32 foreign key (IDCOLONIA)
      references DIR_CAT_COLONIAS (IDCOLONIA)
go

alter table FAC_TIPO_CAMBIO_HISTORICO
   add constraint FK_FAC_TIPO_REFERENCE_FAC_CAT_ foreign key (IDMONEDA)
      references FAC_CAT_MONEDA (IDMONEDA)
go

alter table PER_PERSONA
   add constraint FK_PER_PERS_REFERENCE_DIR_DIRE foreign key (IDDIRECCION)
      references DIR_DIRECCIONES (IDDIRECCION)
go

alter table PER_PERSONA
   add constraint FK_PER_PERS_REFERENCE_TEL_TELE foreign key (IDTELEFONOS)
      references TEL_TELEFONOS (IDTELEFONOS)
go

alter table PER_PERSONA
   add constraint FK_PER_PERS_REFERENCE_CON_CONT foreign key (IDCONTACTOS)
      references CON_CONTACTO (IDCONTACTOS)
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

