
/****** Object:  StoredProcedure [dbo].[ObtenerOrdenesCapturaWeb]    Script Date: 22/11/2016 12:31:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ObtenerOrdenesCapturaWeb]
	@Credito varchar(15) = NULL,
	@NSS varchar(15) = null,
	@RFC varchar(15) = null,	
	@Nombre nvarchar(200) = null,
	@Municipio nvarchar(200) = null,
	@Delegacion varchar(100) = null,
	@IdUsuario int = null
As
Begin
	Declare 
		@sql nvarchar(4000),
		@cond Nvarchar(8) = ' WHERE ',
		@NomCorto nvarchar(40) = '',
		@slqFiltroCCExtra nvarchar(100)='',
		@esCallCenterOut bit

	select 
	@Credito = ltrim(rtrim(@Credito))
	,@NSS = ltrim(rtrim(@NSS))
	,@RFC = ltrim(rtrim(@RFC))
	,@Nombre = ltrim(rtrim(@Nombre))
	,@Municipio = ltrim(rtrim(@Municipio))
	,@Delegacion = ltrim(rtrim(@Delegacion))

	select @NomCorto = nom_corto,@esCallCenterOut= EsCallCenterOut from usuario usr
	inner join dominio dom
	on usr.idDominio = dom.idDominio
	where idUsuario = @IdUsuario

	IF  @esCallCenterOut IS  NULL 
		BEGIN
			SET @slqFiltroCCExtra = ' cr.CC_DESPACHO = ''' + @NomCorto +''' and CV_RUTA = ''CSD'''
		END
	ELSE
		BEGIN
			SET @slqFiltroCCExtra = ' cr.tx_nombre_despacho = ''' + @NomCorto +''' and CV_RUTA = ''RDST'''
		END

	

	Set @sql = 	'select top 20 isnull(idOrden,-1) idOrden,CV_CREDITO Credito,tx_soluciones Soluciones, dom.NombreDominio Despacho,TX_NOMBRE_ACREDITADO Nombre '+
				',TX_CALLE Calle,TX_COLONIA Colonia'+
				',TX_MUNICIPIO Municipio,CV_CODIGO_POSTAL CP'+
				',cd.Descripcion Delegacion'+
				',CASE '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''1S'' THEN ''Asignada SMS'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''15S'' THEN ''Asignada SMS'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''2S'' THEN ''Cancelada SMS'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''3S'' THEN ''Respondida SMS'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''6S'' THEN ''Sincronizando'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''4S'' THEN ''Autorizada SMS'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''1C'' THEN ''Asignada CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''11C'' THEN ''Asignada CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''12C'' THEN ''Asignada CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''15C'' THEN ''Asignada CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''2C'' THEN ''Cancelada CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''5C'' THEN ''Reasignada CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''6C'' THEN ''Sincronizando CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''1CS'' THEN ''Asignada SMS CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''3C'' THEN ''Respondida CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''3CS'' THEN ''Respondida SMS CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''4C'' THEN ''Autorizada CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''4CS'' THEN ''Autorizada SMS CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''15CS'' THEN ''Asignada SMS CC'''+
				'WHEN convert(varchar,ORD.ESTATUS) + convert(varchar,Tipo) = ''2CS'' THEN ''Cancelada SMS CC'' '+
				'WHEN convert(varchar,ORD.ESTATUS) = ''1'' THEN ''Asignada'''+
				'WHEN convert(varchar,ORD.ESTATUS) = ''2'' THEN ''Cancelada'''+
				'WHEN convert(varchar,ORD.ESTATUS) = ''3'' THEN ''Respondida'' '+
				'WHEN convert(varchar,ORD.ESTATUS) = ''4'' THEN ''Autorizada'' '+
				'WHEN convert(varchar,ORD.ESTATUS) = ''5'' THEN ''Reasignada'' '+
				'WHEN convert(varchar,ORD.ESTATUS) = ''6'' THEN ''Sincronizando'' '+
				'WHEN convert(varchar,ORD.ESTATUS) = ''11'' THEN ''Asignada'' '+
				'WHEN convert(varchar,ORD.ESTATUS) = ''12'' THEN ''Asignada'' '+
				'WHEN convert(varchar,ORD.ESTATUS) = ''15'' THEN ''Asignada'' '+
				'ELSE ''No asiganda'' END EstatusTxt'+
				',CV_NSS '+
				',CV_RFC '+
				',ISNULL(idVisita,1) idVisita'+
				',ord.Estatus Estatus'+
				',row_number() over (ORDER BY CV_CREDITO) as num'+
				',ISNULL(CASE WHEN (ord.estatus = 3 or ord.estatus = 4) '+
							' THEN '+
							' ('+
								' SELECT CASE '+
									 'WHEN  (cr.Nombre = ''DictamenProrrogaP''  OR cr.Nombre = ''DictamenCONFPRParcial'')'+	 
									' THEN  ''1'''+	 
									 ' ELSE ''0'''+
									 ' END'+ 
								 ' FROM '+ 
								' Respuestas r WITH (NOLOCK) '+
								' LEFT JOIN CamposRespuesta cr WITH (NOLOCK) ON cr.idCampo = r.idCampo'+
								' WHERE cr.Nombre LIKE ''dictamen%'' and ord.idOrden = r.idOrden'+
							')	 '+
							' ELSE ''NA'' '+
						   ' END, ''NA'' ) Convenio'+
			    ',ISNULL(CASE WHEN (ord.estatus = 3 or ord.estatus = 4) THEN  ( SELECT r.valor FROM  Respuestas r WITH (NOLOCK) LEFT JOIN CamposRespuesta cr WITH (NOLOCK) ON cr.idCampo = r.idCampo WHERE cr.Nombre LIKE ''dictamen%'' and ord.idOrden = r.idOrden)ELSE (SELECT lower(ctll.RESULTADO) FROM llamadassinexito ll WITH(NOLOCK) INNER JOIN catResultadollamadassinexito ctll WITH(NOLOCK) ON ll.id_resultado=ctll.id_resultado WHERE ll.CV_CREDITO=cr.CV_CREDITO) END, '''' ) AS FinRama '+
				' from creditos cr with (nolock) '+
				' inner join Dominio dom with (nolock) '+
				' on dom.nom_corto = cr.TX_NOMBRE_DESPACHO and '+
				@slqFiltroCCExtra+
				' left join ordenes ord with (nolock) '+
				' on ord.num_cred = cr.CV_CREDITO '+
				' inner join CatDelegaciones cd with (nolock) '+
				' on cd.Delegacion = cv_delegacion '+
				' left join CatEstatusOrdenes ceo '+
				' on ceo.Codigo = ord.Estatus '
	If @Credito Is Not Null
	begin
		set @sql = @sql + @cond + ' CV_CREDITO like ''%' + @Credito + '%'''
		set @cond = ' AND '
	end
	
	If @NSS Is Not Null
	begin
		set @sql = @sql + @cond + ' CV_NSS like ''%' + @NSS + '%'''
		set @cond = ' AND '
	end
	
	If @RFC Is Not Null
	begin
		set @sql = @sql + @cond + ' CV_RFC like ''%' + @RFC + '%'''
		set @cond = ' AND '
	end

	If @Nombre Is Not Null
	begin
		set @sql = @sql + @cond + ' TX_NOMBRE_ACREDITADO like ''%' + @Nombre + '%'''
		set @cond = ' AND '
	end

	If @Municipio Is Not Null
	begin
		set @sql = @sql + @cond + ' TX_MUNICIPIO like ''' + @Municipio + '%'''
		set @cond = ' AND '
	end

	If @Delegacion Is Not Null
	begin
		set @sql = @sql + @cond + ' cd.Descripcion like ''' + @Delegacion + '%'''
		set @cond = ' AND '
	end
	
	set @sql = @sql + ' order by num ' 

	Execute sp_Executesql @sql

end