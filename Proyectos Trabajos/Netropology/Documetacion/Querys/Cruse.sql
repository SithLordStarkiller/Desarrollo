/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT Tipo_Usuario, *
--  FROM [paeeem_produccion19_06].[dbo].[K_CREDITO] WHERE No_Credito = 'PAEEEMDU07E03792'
  
  SELECT co.No_Credito,co.Id_Proveedor,co.Tipo_Usuario,cm.Id_Proveedor,cm.Id_Branch,cm.Tipo_Usuario
  FROM PAEEEM_PROD.dbo.CRE_Credito cm INNER JOIN 
  paeeem_produccion19_06.dbo.K_CREDITO co ON cm.No_Credito = co.No_Credito WHERE co.Tipo_Usuario ='S'
  