USE [Suncorp]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 07/12/2015
-- Description:	Se encargara de realizarun listado de los catalogos que se tienenen la base de datos
-- =============================================
ALter PROCEDURE [dbo].[Usp_GetListCatalogsSystem] 
	
AS
BEGIN
	DECLARE @Server varchar(50) 
	SET @Server = @@SERVERNAME 
	EXEC sp_serveroption  @Server,'DATA ACCESS','TRUE'

	SELECT
	ROW_NUMBER() OVER(order by so.name) AS IdTable, 
	[TableName] = so.name ,
	'System table' As Descriptions
                FROM sysobjects so, sysindexes si 
                WHERE so.xtype = 'U' AND si.id = OBJECT_ID(so.name) 
	--AND so.name Like '%CAT%'
				AND so.name in ('UsZona','UsTipoUsuario')
                GROUP BY so.name
END


--SELECT [TableName] = so.name + ', '
--                FROM sysobjects so, sysindexes si 
--                WHERE so.xtype = 'U' AND si.id = OBJECT_ID(so.name)
--                GROUP BY so.name