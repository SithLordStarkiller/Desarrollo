-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE Usp_GetListUsZonaPageList
	@i_Page_Index int ,
	@i_Page_Count int ,
	@includeDelete bit,
	@o_total_rows int output
AS
BEGIN
	
--SELECT TOP (@i_Page_Count) a.* FROM
--(
--	SELECT p.*,ROW_NUMBER() OVER (ORDER BY p.IdZona) AS num FROM dbo.UsZona p 
--) AS a WHERE num > @i_Page_Index * @i_Page_Count AND Borrado LIKE
--  CASE WHEN @includeDelete = 1 THEN 
--    @includeDelete
--  ELSE
--    '%'
--  END;

---- Get Total Rows

--SET @o_total_rows =  (SELECT  COUNT(1) FROM UsZona);
SELECT (
CASE @includeDelete 

WHEN  0 THEN (select 'no')
	
WHEN  1 THEN (select 'si')

END
)
END
GO
