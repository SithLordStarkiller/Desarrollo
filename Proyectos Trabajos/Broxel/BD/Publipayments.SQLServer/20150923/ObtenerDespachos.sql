/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Pablo Rendon
* Fecha de creación:	08/09/2015
* Descripción:			Obtener despachos
**********************************************************************/
CREATE PROCEDURE ObtenerDespachos
(
@nombre nvarchar(100),
@nCorto nvarchar(40),
@estatus int
)
AS
BEGIN
	IF @estatus >= 0
	BEGIN
	SELECT
		d.idDominio,
        d.NombreDominio,
        d.nom_corto,
        d.FechaAlta,
        d.Estatus,
        e.Descripcion EstatusTxt
        from Dominio d
        join CatEstatusUsuario e
        on d.Estatus = e.Estatus
        where d.idDominio > 2
            and e.Estatus = @estatus
            and d.NombreDominio like '%' + @nombre + '%'
            and d.nom_corto like '%' + @nCorto + '%'
        order by d.Estatus desc
	END
	ELSE
		SELECT 
		d.idDominio,
        d.NombreDominio,
        d.nom_corto,
        d.FechaAlta,
        d.Estatus,
        e.Descripcion EstatusTxt
		from Dominio d
		join CatEstatusUsuario e
		on d.Estatus = e.Estatus
		where d.idDominio > 2
            and d.NombreDominio like '%' + @nombre + '%'
            and d.nom_corto like '%' + @nCorto + '%'
        order by d.Estatus desc
END





