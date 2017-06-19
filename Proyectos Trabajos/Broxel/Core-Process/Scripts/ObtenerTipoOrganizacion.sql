/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [ID_ORGANIZATION]
      ,[ID_COUNTRY]
      ,[ID_DOCUMENT_TYPE]
      ,[DOCUMENT_NUMBER]
      ,[LEGAL_NAME]
      ,[FANTASY_NAME]
      ,[STATUS]
      ,[EXTERNAL_ID]
      ,[ID_PARENT_ORGANIZATION]
      ,[STATE_INSCRIPTION_NUMBER]
      ,[CITY_INSCRIPTION_NUMBER]
      ,[ORGANIZATION_TYPE]
  FROM [brx_pruebas_bo_20170321].[dbo].[C_ORGANIZATION] where  [ORGANIZATION_TYPE]  = 2