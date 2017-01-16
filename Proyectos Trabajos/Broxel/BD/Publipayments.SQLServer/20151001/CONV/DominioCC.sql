declare @idDominio int=0

insert into Dominio(NombreDominio,nom_corto,FechaAlta,Estatus)
values('MunozCC','MU?OZCC',getdate(),1)

set @idDominio=scope_identity()


insert into Usuario(idDominio,idRol,USuario,Nombre,Email,Password,Alta,UltimoLogin,Estatus,Intentos)
values(@idDominio,3,'MunozCCSupervisor','Supervisor CC','p.jaimes@publipayments.com',
'7C00A54A411F4F5FCB5F85DDA179BE4560213C6568896B06C99C13C3833BCAD7BD1281E04754185B61E1E29D067AFE36EECC05376B09BFC2EF978E925778619E',dateadd(dd,-1,getdate()),getdate(),1,0)


insert into Usuario(idDominio,idRol,USuario,Nombre,Email,Password,Alta,UltimoLogin,Estatus,Intentos)
values(@idDominio,4,'MunozCCGestor','Gestor CC','p.jaimes@publipayments.com',
'7C00A54A411F4F5FCB5F85DDA179BE4560213C6568896B06C99C13C3833BCAD7BD1281E04754185B61E1E29D067AFE36EECC05376B09BFC2EF978E925778619E',dateadd(dd,-1,getdate()),getdate(),1,0)

/*
select top 5 * from Usuario order by idUsuario desc

insert into RelacionUsuarios(idPadre,idHijo,Estatus)
values(839,838,1)
*/



--- agreagar la relacion en la tabla de relacionusuarios