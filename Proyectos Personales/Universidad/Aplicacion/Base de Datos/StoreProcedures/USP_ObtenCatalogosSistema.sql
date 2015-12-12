-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 07/12/2015
-- Description:	Se encargara de realizarun listado de los catalogos que se tienenen la base de datos
-- =============================================
CREATE PROCEDURE Usp_ObtenCatalogosSistema 
	
AS
BEGIN
	DECLARE @Server varchar(50) 
	SET @Server = @@SERVERNAME 
	EXEC sp_serveroption  @Server,'DATA ACCESS','TRUE'

	SELECT [TableName] = so.name 
                FROM sysobjects so, sysindexes si 
                WHERE so.xtype = 'U' AND si.id = OBJECT_ID(so.name) AND so.name Like '%CAT%'
                GROUP BY so.name
END
GO
