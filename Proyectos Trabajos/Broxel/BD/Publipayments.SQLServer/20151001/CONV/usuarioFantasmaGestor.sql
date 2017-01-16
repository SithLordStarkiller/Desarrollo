SET IDENTITY_INSERT Usuario ON
INSERT INTO Usuario
           ([idUsuario]
           ,[idDominio]
           ,[idRol]
           ,[Usuario]
           ,[Nombre]
           ,[Email]
           ,[Password]
           ,[Alta]
           ,[UltimoLogin]
           ,[Estatus]
           ,[Intentos]
           ,[Bloqueo])
     VALUES
           (-111,1,4,'GestorCC','Gestionado por CC','p.jaimes@publipayments.com',
           '7C00A54A411F4F5FCB5F85DDA179BE4560213C6568896B06C99C13C3833BCAD7BD1281E04754185B61E1E29D067AFE36EECC05376B09BFC2EF978E925778619E',
           GETDATE(),GETDATE(),1,0,null)

           
           INSERT INTO Usuario
           ([idUsuario]
           ,[idDominio]
           ,[idRol]
           ,[Usuario]
           ,[Nombre]
           ,[Email]
           ,[Password]
           ,[Alta]
           ,[UltimoLogin]
           ,[Estatus]
           ,[Intentos]
           ,[Bloqueo])
     VALUES
           (-110,1,3,'SupervisorCC','Gestionado por CC','p.jaimes@publipayments.com',
           '7C00A54A411F4F5FCB5F85DDA179BE4560213C6568896B06C99C13C3833BCAD7BD1281E04754185B61E1E29D067AFE36EECC05376B09BFC2EF978E925778619E',
           GETDATE(),GETDATE(),1,0,null)
           
SET IDENTITY_INSERT Usuario OFF



