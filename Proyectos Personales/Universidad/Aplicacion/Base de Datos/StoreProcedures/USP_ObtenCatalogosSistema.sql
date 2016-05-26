USE [Universidad]
GO
/****** Object:  StoredProcedure [dbo].[Usp_ObtenCatalogosSistema]    Script Date: 10/02/2016 01:13:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 07/12/2015
-- Description:	Se encargara de realizarun listado de los catalogos que se tienenen la base de datos
-- =============================================
CREATE PROCEDURE [dbo].[Usp_ObtenCatalogosSistema] 
	
AS
BEGIN
	DECLARE @Server varchar(50) 
	SET @Server = @@SERVERNAME 
	EXEC sp_serveroption  @Server,'DATA ACCESS','TRUE'

	SELECT [TableName] = so.name 
                FROM sysobjects so, sysindexes si 
                WHERE so.xtype = 'U' AND si.id = OBJECT_ID(so.name) --AND so.name Like '%CAT%'
				AND so.name in ('AUL_CAT_TIPO_AULA','HOR_CAT_TURNO','HOR_CAT_DIAS_SEMANA','CAR_CAT_ESPECIALIDAD','HOR_CAT_HORAS','CAR_CAT_CARRERAS','MAT_CAT_MATERIAS','MAT_CAT_CREDITOS_POR_HORAS','HOR_HORAS_POR_DIA')
                GROUP BY so.name
END
