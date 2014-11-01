/* tb K_CREDITO_SUSTITUCION --> Fg_Tipo_Centro_Disp; M o B         --- Id_Centro_Disp
 
M = MAtriz CAyD   --> CAT_CENTRO_DISP
B = Sucursal CAyD  --> CAT_CENTRO_DISP_SUCURSAL

 Id_Centro_Disp

M   campo Id_Centro_Disp
FROM   CAT_CENTRO_DISP c INNER JOIN
       CAT_REGION r ON c.Cve_Region = r.Cve_Region INNER JOIN
       CAT_ZONA z ON c.Cve_Zona = z.Cve_Zona AND r.Cve_Region = z.Cve_Region

B     campo Id_Centro_Disp_Sucursal   
FROM       CAT_CENTRO_DISP_SUCURSAL AS s INNER JOIN
             CAT_REGION r ON s.Cve_Region = r.Cve_Region INNER JOIN
             CAT_ZONA z ON s.Cve_Zona = z.Cve_Zona AND r.Cve_Region = z.Cve_Region


--*****/
/****** Script for SelectTopNRows command from SSMS  ******/
--select * from CRE_FOTOS where No_Credito = 'PAEEEMDM25C18038' 
--and idTipoFoto = 2

--SELECT *  FROM CAT_TIPO_FOTO
  
--  SELECT * FROM K_CREDITO_SUSTITUCION
  
  --Baja Eficiencia
  SELECT 
  --*
  EBA.NO_CREDITO, EBA.GRUPO,TEC.DX_NOMBRE_GENERAL,EBA.DX_MODELO_PRODUCTO,EBA.CAPACIDADSISTEMA,EBA.UNIDAD,EBA.NO_UNIDADES,
  CRES.Dx_Marca,CRES.dx_Modelo_Producto,CRES.Dx_Color,CRES.Dx_Antiguedad,CRES.Id_Pre_Folio, CRES.Id_Folio,CRES.DT_FECHA_RECEPCION,
  CRES.Id_Centro_Disp, CRES.Fg_Tipo_Centro_Disp,--1 CAyD Comercial, 
  /*zona y region*/case CRES.FG_TIPO_CENTRO_DISP WHEN 'M' THEN z.DX_NOMBRE_ZONA WHEN 'B' THEN zo.DX_NOMBRE_ZONA END as Zona ,CASE CRES.FG_TIPO_CENTRO_DISP WHEN 'M' THEN  r.DX_NOMBRE_REGION WHEN 'B' THEN  re.DX_NOMBRE_REGION end as Region, FOTO.FOTO

  FROM CRE_CREDITO_EQUIPOS_BAJA  AS EBA INNER JOIN CAT_TECNOLOGIA AS TEC ON EBA.CVE_TECNOLOGIA = TEC.CVE_TECNOLOGIA 
  INNER JOIN K_CREDITO_SUSTITUCION AS CRES ON EBA.NO_CREDITO = CRES.NO_CREDITO
  INNER JOIN CRE_FOTOS AS FOTO ON FOTO.IdCreditoSustitucion = CRES.Id_Credito_Sustitucion AND FOTO.IDTIPOFOTO = 2
  
  LEFT OUTER JOIN CAT_CENTRO_DISP  AS CCD  on CRES.ID_CENTRO_DISP = CCD.ID_CENTRO_DISP --AND CRES.FG_TIPO_CENTRO_DISP = 'M'  
  LEFT OUTER JOIN CAT_CENTRO_DISP_SUCURSAL AS CCDS on CRES.ID_CENTRO_DISP = CCDS.ID_CENTRO_DISP --and CRES.FG_TIPO_CENTRO_DISP = 'B'
  
  LEFT OUTER JOIN CAT_REGION as r ON CCD.Cve_Region = r.Cve_Region 
  LEFT OUTER JOIN CAT_ZONA z ON CCD.Cve_Zona = z.Cve_Zona AND r.Cve_Region = z.Cve_Region
  
  LEFT OUTER JOIN CAT_REGION as re ON CCDS.Cve_Region = re.Cve_Region 
  LEFT OUTER JOIN CAT_ZONA zo ON CCDS.Cve_Zona = zo.Cve_Zona AND re.Cve_Region = zo.Cve_Region
  
  WHERE EBA.NO_CREDITO in ('PAEEEMDA01A18022','PAEEEMDA01A18073')
  
  --Alta eficiencia
  SELECT 
  *
  --CRE.NO_CREDITO, PRO.Grupo, TEC.DX_NOMBRE_GENERAL,CFA.DX_NOMBRE_FABRICANTE, CM.DX_Marca, CPRO.DX_Modelo_Producto, PRO.No_Cantidad, PPRO.MT_PRECIO_UNITARIO,
  --PRO.MT_PRECIO_UNITARIO_SIN_IVA * PRO.NO_CANTIDAD AS IMPORTE_SIN_IVA, pro.mt_gastos_instalacion_mano_obra, /*COSTO ACOPIO Y DESTRUCCION*/
  --PRO.INCENTIVO,CT1.Dx_Tarifa ,CT2.Dx_Tarifa
  
   FROM CRE_CREDITO AS CRE INNER JOIN K_CREDITO_PRODUCTO AS PRO ON
  CRE.NO_CREDITO = PRO.NO_CREDITO INNER JOIN K_PROVEEDOR_PRODUCTO AS PPRO ON
  CRE.ID_PROVEEDOR = PPRO.ID_PROVEEDOR INNER JOIN CAT_PRODUCTO AS CPRO ON 
  PRO.CVE_PRODUCTO = CPRO.CVE_PRODUCTO INNER JOIN CAT_TECNOLOGIA AS TEC ON--
  CPRO.CVE_TECNOLOGIA = TEC.CVE_TECNOLOGIA INNER JOIN CAT_MARCA AS CM ON
 CPRO.CVE_MARCA = CM.CVE_MARCA left JOIN CRE_FOTOS AS FT ON
 CRE.NO_CREDITO = FT.NO_CREDITO AND FT.IDTIPOFOTO = 3 and FT.IdCreditoProducto = Pro.ID_CREDITO_Producto
 left join CRE_Facturacion as Fac1 on cre.no_credito = Fac1.No_credito and fac1.idTipoFacturacion = 1
  left join CRE_Facturacion as Fac2 on cre.no_credito = Fac2.No_credito and fac2.idTipoFacturacion = 2
  left join CAT_FABRICANTE as CFA on cPRO.CVE_Fabricante = CFA.CVe_Fabricante
  left join CAT_TARIFA as CT1 on Fac1.Cve_Tarifa = CT1.Cve_Tarifa
  left join CAT_TARIFA as CT2 on Fac1.Cve_Tarifa = CT2.Cve_Tarifa
   WHERE CPRO.CVE_PRODUCTO = PPRO.CVE_PRODUCTO and CRE.No_Credito in('PAEEEMDA01A18022','PAEEEMDA01A18049')
   
   --DetalleBalanceMensual
   
   select neg.Gastos_Mes, neg.Ventas_Mes 
   from cre_credito as cre  join 
   CLI_Negocio as neg on
  -- cre.No_Credito = neg.No_Credito 
    cre.IdCliente = neg.IdCliente 
   and cre.idnegocio = neg.idnegocio
   and  cre.Id_Branch =neg.Id_Branch 
   and cre.Id_Proveedor = neg.Id_Proveedor
   where cre.No_Credito = 'PAEEEMDA01A18022'
  
  --select * from cre_credito where no_credito= 'PAEEEMDA08A18060'
  
  --select * from cli_negocio where idcliente =15906
  
  select
   ft.* 
   from cre_credito as CRE inner join  cre_fotos as FT on cre.No_credito = ft.No_credito 
   where  cre.no_credito= 'PAEEEMDA01A18022'

   
   select * from CRE_Credito where no_credito= 'PAEEEMDA01A18022'

   select * FROM US_USUARIO 
   WHERE Id_Departamento = 359


   --creditos viejos

   SELECT * from K_CREDITO_PRODUCTO  as KCP 
   left join CAT_PRODUCTO AS CP on KCP.Cve_Producto = CP.Cve_Producto
   INNER join CAT_TIPO_PRODUCTO AS CTP on CP.Ft_Tipo_Producto = CTP.Ft_Tipo_Producto
   INNER JOIN CRE_Credito AS CC on KCP.No_Credito = CC.No_Credito
   inner join K_CREDITO_COSTO AS KCC on KCP.No_Credito = KCC.No_Credito
   inner join k_CREDITO_DEsCUENTO AS KCD on KCP.No_Credito = KCD.No_Credito
   WHERE KCP.No_Credito = 'PAEEEMDA01A18022'

   SELECT * FROM K_CREDITO_PRODUCTO WHERE No_Credito = 'PAEEEMDA01A18022'

   SELECT * FROM CAT_PROVEEDOR WHERE Id_Proveedor = 359

