﻿-- ==============================================================================================================================================
-- --------------------------------------------------------------Title---------------------------------------------------------------------------
--
-- Author:					Esteban Cruz Lagunes
-- Project:					Usp_Logs_InsertLog
-- Module:					Logger

-- Type:					StoredProcedured
-- FileName:				Usp_Logs_InsertLog.sql
-- Create date:				05/02/2018
-- Description:				InsertLog in the of movement in the aplication
--
-- ----------------------------------------------------------------------------------------------------------------------------------------------
-- ==============================================================================================================================================

CREATE PROCEDURE [Logs].[Usp_InsertLog] 
	@tipoLog int,
    @proyecto VarChar(MAX),
    @clase VarChar(MAX),
    @metodo VarChar(MAX),
    @mensage varchar(MAX),
    @log VarChar(MAX),
    @excepcion varchar(MAX),
    @auxiliar VarChar(MAX)
AS
BEGIN

	INSERT INTO [Logs].LogLogger (IdTipoLog, Proyecto, Clase, Metodo,Mensage,Log,Excepcion,Auxiliar,FechaCreacion)  
	VALUES (@tipoLog , @proyecto, @clase,@metodo,@mensage,@log ,@Excepcion,@auxiliar, GETDATE());

	SELECT @@IDENTITY
END


-- ==============================================================================================================================================
-- ------------------------------------------------------------Modifications---------------------------------------------------------------------
--
-- **********************************************************************************************************************************************
--
-- Autor of modification:	AutorMod
-- Modification Date:		DateMod
-- Description:				DescMod
-- 
-- **********************************************************************************************************************************************
--
-- ----------------------------------------------------------------------------------------------------------------------------------------------
-- ==============================================================================================================================================