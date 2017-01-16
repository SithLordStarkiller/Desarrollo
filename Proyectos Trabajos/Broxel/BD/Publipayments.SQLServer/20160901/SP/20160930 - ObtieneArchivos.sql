/****************************************************************************
* Proyecto:	portal.publipayments.com
* Autor:	Laura Anayeli Dotor Mejia
* Fecha de creación:	13/09/2016
* Descripción:	Inserta/Actualiza los registros en la tabla de Llamadas sin exito CC
*****************************************************************************/
CREATE PROCEDURE [dbo].[ObtieneArchivos] (
@PAUSUARIO INT
,@PAPROCESO INT
)
AS
BEGIN
DECLARE @IDROL INT= 0;

	SELECT @IDROL = idRol FROM USUARIO WITH (NOLOCK) WHERE IDUSUARIO = @PAUSUARIO
	
	IF(@PAPROCESO = 1)
	BEGIN
		SELECT 
			a.id, 
			a.Archivo, 
			a.Tipo, 
			a.Tiempo, 
			a.Registros, 
			a.Fecha, 
			a.Estatus, 
			b.id_archivo as Error 
		FROM Archivos a WITH (NOLOCK)
		LEFT OUTER JOIN ArchivosError b  WITH (NOLOCK) on a.id = b.id_archivo
		WHERE a.tipoArchivo IS NULL ORDER BY a.id DESC
	END
	ELSE
	BEGIN
		IF(@IDROL = 0 OR @IDROL = 1 )
		BEGIN
			SELECT 
			a.id, 
			e.NombreDominio,
			d.Usuario,
			a.Archivo, 
			a.Tipo, 
			a.Tiempo, 
			a.Registros, 
			a.Fecha, 
			a.Estatus, 
			b.id_archivo as Error 
		FROM Archivos a WITH (NOLOCK)
		LEFT OUTER JOIN ArchivosError b  WITH (NOLOCK) on a.id = b.id_archivo
		LEFT JOIN ArchivoXUsuario c  WITH (NOLOCK) on a.ID = c.ID_ARCHIVO 
		INNER JOIN Usuario d  WITH (NOLOCK) on c.ID_USUARIO = d.IDUSUARIO
		INNER JOIN Dominio e  WITH (NOLOCK) on d.idDominio = e.idDominio
		WHERE a.tipoArchivo = 100 ORDER BY a.id DESC
		END
		ELSE IF (@IDROL = 2)
		BEGIN
			SELECT 
				a.id, 
				a.Archivo, 
				a.Tipo, 
				a.Tiempo, 
				a.Registros, 
				a.Fecha, 
				a.Estatus, 
				b.id_archivo as Error 
			FROM Archivos a WITH (NOLOCK)
			LEFT OUTER JOIN ArchivosError b  WITH (NOLOCK) on a.id = b.id_archivo
			LEFT JOIN ArchivoXUsuario c  WITH (NOLOCK) on a.ID = c.ID_ARCHIVO 
			WHERE a.tipoArchivo = 100 and c.ID_USUARIO = @PAUSUARIO ORDER BY a.id DESC
		END
	END
END