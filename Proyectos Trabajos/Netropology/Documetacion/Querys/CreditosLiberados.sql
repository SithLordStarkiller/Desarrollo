--SELECT * FROM CRE_Credito 

--SELECT * FROM CRE_Facturacion

--SELECT * FROM CLI_Cliente

--SELECT * FROM CLI_Negocio

--SELECT * FROM CAT_TIPO_INDUSTRIA

--SELECT * FROM CAT_TARIFA

--SELECT * FROM K_CREDITO_PRODUCTO

--SELECT * FROM CAT_PRODUCTO

--SELECT * FROM DIR_Direcciones

--SELECT * FROM Tipo_Domicilio

--SELECT * FROM CAT_ESTADO

--SELECT * FROM CAT_DELEG_MUNICIPIO

--SELECT * FROM K_CREDITO_AMORTIZACION

--SELECT * from K_CREDITO_Costo

--SELECT * FROM CAT_PROVEEDOR

--SELECT * FROM CAT_PROVEEDORBRANCH

--SELECT * FROM K_CREDITO_SUSTITUCION

--SELECT * FROM K_CREDITO_PRODUCTO

--SELECT * from CAT_ESTATUS_CREDITO

--SELECT * FROM CAT_PRODUCTO

--SELECT * FROM CAT_TECNOLOGIA

--SELECT * FROM US_USUARIO

--***

DECLARE @No_Credito varchar(max) = 'PAEEEMDF45A03098'

SELECT 
--CC.No_Credito, KCP.*
CC.No_Credito as No_Credito ,CC.Fecha_Pendiente as Fecha_Ingreso,CC.RPU,CC.Afectacion_SICOM_fecha as Fecha_Liberado, CC.ID_Intelisis as No_Intelisis
, CC.Monto_Solicitado AS Monto_Finanaciado, CC.Gastos_Instalacion AS Gastos_Instalacion 
, CC.Consumo_Promedio AS KWH_Promedio , CC.Demanda_Maxima AS KW_Demanda, CC.Factor_Potencia , CC.No_Ahorro_Consumo AS KWH_Ahorro, CC.No_Ahorro_Demanda AS KW_horro
,CT_A.Dx_Tarifa Tarifa
,CT_O.Dx_Tarifa Tarifa_Origen
,CASE WHEN CCLI.Cve_Tipo_Sociedad = 1 then CCLI.Nombre + ' ' + CCLI.Ap_Paterno + ' ' + CCLI.Ap_Materno WHEN CCLI.Cve_Tipo_Sociedad = 2 then CCLI.Razon_Social end as Razon_Zocial, CCLI.RFC
,CN.Nombre_Comercial
,CTI.DESCRIPCION_SCIAN as Giro_Comercial
,KCA.Dt_Fecha AS P_Amortizacion, KCA.Mt_Pago AS Amortizacion
,CE.Dx_Nombre_Estado AS ESTADO_NC
,CDM.Dx_Deleg_Municipio AS MUNICIPIO_NC
,CPB.Dx_Razon_Social as RazonSocialDist ,CPB.Dx_Nombre_Comercial AS NombreDomercialDist, CPB.Tipo_Sucursal AS TipoSucursalDist
,CZ.Dx_Nombre_Zona as ZONA
,CR.Dx_Nombre_Region AS REGION
--SUM(KPPRC.No_Cantidad)
--,SUM(KPPAA.No_Cantidad)
--,SUM(KPPIL.No_Cantidad)
--,SUM(KPPME.No_Cantidad)
--,SUM(KPPSE.No_Cantidad)
--,SUM(KPPILED.No_Cantidad)
--,SUM(KPPBC.No_Cantidad)
--,SUM(KPPII.No_Cantidad)
--,SUM(KPPCR.No_Cantidad)
--,SUM(KCRC.No_Unidades)
--,SUM(KCAA.No_Unidades)
--,SUM(KCME.No_Unidades)
--,SUM(KCCR.No_Unidades)
From Cre_Credito AS CC
INNER JOIN CLI_Negocio AS CN ON CC.IdNegocio = cn.IdNegocio and cc.IdCliente = cn.IdCliente and CC.Id_Branch = CN.Id_Branch
LEFT OUTER JOIN CRE_Facturacion AS CF_A ON CC.No_Credito = CF_A.No_Credito and CF_A.IdTipoFacturacion = 2
LEFT OUTER JOIN CAT_TARIFA AS CT_A ON CF_A.Cve_Tarifa = CT_A.Cve_Tarifa
LEFT OUTER JOIN CRE_Facturacion AS CF_O ON CC.No_Credito = CF_O.No_Credito AND CF_O.IdTipoFacturacion = 1
LEFT OUTER JOIN CAT_TARIFA AS CT_O ON CF_O.Cve_Tarifa = CT_O.Cve_Tarifa
INNER JOIN CLI_Cliente AS CCLI ON CC.Id_Branch = CCLI.Id_Branch and CC.IdCliente = CCLI.IdCliente
LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS CPB ON CC.Id_Branch = CPB.Id_Branch
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS CTI ON CN.Cve_Tipo_Industria = CTI.Cve_Tipo_Industria
INNER JOIN K_CREDITO_AMORTIZACION AS KCA ON CC.No_Credito = KCA.No_Credito and KCA.No_Pago = 1
INNER JOIN DIR_Direcciones AS DD ON CN.Id_Branch = DD.Id_Branch AND CN.IdCliente = DD.IdCliente AND CN.IdNegocio = DD.IdNegocio AND DD.IdTipoDomicilio =1
INNER JOIN CAT_ESTADO AS CE ON DD.Cve_Estado = CE.Cve_Estado
INNER JOIN CAT_DELEG_MUNICIPIO AS CDM ON DD.Cve_Deleg_Municipio = CDM.Cve_Deleg_Municipio
INNER JOIN CAT_ZONA AS CZ ON CPB.Cve_Zona = CZ.Cve_Zona
INNER JOIN CAT_REGION AS CR ON CPB.Cve_Region = CR.Cve_Region

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPRC on CC.No_Credito = KPPRC.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPRC ON KPPRC.Cve_Producto = CPRC.Cve_Producto and CPRC.Cve_Tecnologia = 1 
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTRC on CPRC.Cve_Tecnologia = CTRC.Cve_Tecnologia 

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPAA on CC.No_Credito = KPPAA.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPAA ON KPPAA.Cve_Producto = CPAA.Cve_Producto and CPAA.Cve_Tecnologia = 2
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTAA on CPAA.Cve_Tecnologia = CTAA.Cve_Tecnologia

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPIL on CC.No_Credito = KPPIL.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPIL ON KPPIL.Cve_Producto = CPIL.Cve_Producto and CPIL.Cve_Tecnologia = 3
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTIL on CPIL.Cve_Tecnologia = CTIL.Cve_Tecnologia

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPME on CC.No_Credito = KPPME.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPME ON KPPME.Cve_Producto = CPME.Cve_Producto and CPME.Cve_Tecnologia = 4
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTME on CPME.Cve_Tecnologia = CTME.Cve_Tecnologia

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPSE on CC.No_Credito = KPPSE.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPSE ON KPPSE.Cve_Producto = CPSE.Cve_Producto and CPSE.Cve_Tecnologia = 5
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTSE on CPSE.Cve_Tecnologia = CTSE.Cve_Tecnologia

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPILED on CC.No_Credito = KPPILED.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPILED ON KPPILED.Cve_Producto = CPILED.Cve_Producto and CPILED.Cve_Tecnologia = 6
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTILED on CPILED.Cve_Tecnologia = CTME.Cve_Tecnologia

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPBC on CC.No_Credito = KPPBC.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPBC ON KPPBC.Cve_Producto = CPBC.Cve_Producto and CPBC.Cve_Tecnologia = 7
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTBC on CPBC.Cve_Tecnologia = CTBC.Cve_Tecnologia

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPII on CC.No_Credito = KPPII.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPII ON KPPII.Cve_Producto = CPII.Cve_Producto and CPII.Cve_Tecnologia = 8
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTII on CPII.Cve_Tecnologia = CTII.Cve_Tecnologia

--LEFT OUTER JOIN K_CREDITO_PRODUCTO AS KPPCR on CC.No_Credito = KPPCR.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CPCR ON KPPCR.Cve_Producto = CPCR.Cve_Producto and CPCR.Cve_Tecnologia = 9
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTCR on CPCR.Cve_Tecnologia = CTCR.Cve_Tecnologia

--LEFT OUTER JOIN K_CREDITO_SUSTITUCION As KCRC ON CC.No_Credito = KCRC.No_Credito
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTBRC ON KCRC.Cve_Tecnologia=CTBRC.Cve_Tecnologia and CTBRC.Cve_Tecnologia=1

--LEFT OUTER JOIN K_CREDITO_SUSTITUCION As KCAA ON CC.No_Credito = KCAA.No_Credito
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTBAA ON KCAA.Cve_Tecnologia=CTBAA.Cve_Tecnologia and CTBAA.Cve_Tecnologia=2

--LEFT OUTER JOIN K_CREDITO_SUSTITUCION As KCME ON CC.No_Credito = KCME.No_Credito
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTBME ON KCME.Cve_Tecnologia=CTBME.Cve_Tecnologia and CTBME.Cve_Tecnologia=4

--LEFT OUTER JOIN K_CREDITO_SUSTITUCION As KCCR ON CC.No_Credito = KCCR.No_Credito
--LEFT OUTER JOIN CAT_TECNOLOGIA AS CTBCR ON KCCR.Cve_Tecnologia=CTBCR.Cve_Tecnologia and CTBCR.Cve_Tecnologia=9


WHERE 
CC.Cve_Estatus_Credito = 1 
--AND CC.No_Credito = @No_Credito

--Group by CTAA.Cve_Tecnologia,CTRC.Cve_Tecnologia,CTIL.Cve_Tecnologia,CTME.Cve_Tecnologia, CTSE.Cve_Tecnologia, CTILED.Cve_Tecnologia,CTBC.Cve_Tecnologia, CTII.Cve_Tecnologia,
--CTCR.Cve_Tecnologia,CTBRC.Cve_Tecnologia, CTBAA.Cve_Tecnologia, CTBME.Cve_Tecnologia, CTBCR.Cve_Tecnologia

--Por cada una de las tecnologias de alta eficienciad
--DECLARE @No_Credito varchar(max) = 'PAEEEMDF45A03098'


--SELECT
--KPP.No_Credito, KPP.No_Cantidad,CP.Cve_Tecnologia
--FROM K_CREDITO_PRODUCTO KPP 
--INNER JOIN CAT_PRODUCTO CP ON KPP.Cve_Producto = CP.Cve_Producto and CP.Cve_Tecnologia = 1
--WHERE KPP.No_Credito = @No_Credito


--SELECT *
--FROM CRE_CREDITO CC
--LEFT OUTER JOIN K_CREDITO_PRODUCTO KCP_RC ON CC.No_Credito = KCP_RC.No_Credito
--LEFT OUTER JOIN CAT_PRODUCTO CP_RC ON KCP_RC.Cve_Producto = CP_RC.Cve_Producto AND CP_RC.Cve_Tecnologia = 1
--where KCP_RC.No_Credito = @No_Credito




