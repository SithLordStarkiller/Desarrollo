
update camposxsubformulario set ClasesLinea='CWCentrado CWRec',ClasesValor='CWAncho300 CWReadonly' where idsubformulario in (
select idsubformulario from subformulario where idformulario in (select idformulario from formulario where nombre like 'CobSocial%' and captura=2 and estatus=1)
)
and nombrecampo in ('factorSinFee','Res_factorDCP')