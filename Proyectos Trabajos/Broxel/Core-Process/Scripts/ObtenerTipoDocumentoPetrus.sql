/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [ID_DOCUMENT_TYPE]
      ,[ID_COUNTRY]
      ,[DESCRIPTION]
      ,[MASK]
      ,[VALIDATION_ALGORITHM]
      ,[APPEARANCE_ORDER]
      ,[APPLY_TYPE]
  FROM [brx_pruebas_bo_20170321].[dbo].[TGD_DOCUMENT_TYPE]   where [ID_DOCUMENT_TYPE] in (
SELECT 
      [ID_DOCUMENT_TYPE]

  FROM [brx_pruebas_bo_20170321].[dbo].[IBC_ISSUER_DOCUMENT_TYPE]  where [ID_ISSUER] = 1) 