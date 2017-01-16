
/****** Object:  Table [dbo].[UsuariosServicios]    Script Date: 10/08/2015 02:11:52 p.m. ******/

DROP MASTER KEY;

--If there is no master key, create one now. 
IF NOT EXISTS 
    (SELECT * FROM sys.symmetric_keys WHERE symmetric_key_id = 101)
    CREATE MASTER KEY ENCRYPTION BY 
    PASSWORD = '23987hxJKL95QYV4369#ghf0%lekjg5k3fd117r$$#1946kcj$n44ncjhdlj'
GO
/************************************************************
-----------PARA EL CASO EN QUE SE CAMBIE A OTRO SERVIDOR DE BD SE DEBERA DE EJECUTAR ESTE COMANDO PARA QUE REACTIVE LA ECRIPCION TODO------------

open master key decryption by password = '23987hxJKL95QYV4369#ghf0%lekjg5k3fd117r$$#1946kcj$n44ncjhdlj'
alter master key add encryption by service master key
*************************************************************/
CREATE CERTIFICATE ServiciosPW
   WITH SUBJECT = 'Contrasenia de servicios externos';
GO

CREATE SYMMETRIC KEY ServiciosPW_Key11
    WITH ALGORITHM = AES_256
    ENCRYPTION BY CERTIFICATE ServiciosPW;
GO

GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsuariosServicios](
	[idUsuarioServicios] [int] IDENTITY(1,1) NOT NULL,
	[idAplicacion] [int] NOT NULL,
	[Usuario] [nvarchar](50) NOT NULL,
	[Password] [varbinary](128) NOT NULL,
	[Orden] [int] NULL,
	[Tipo] [int] NOT NULL,
 CONSTRAINT [PK_UsuariosServicios] PRIMARY KEY CLUSTERED 
(
	[idUsuarioServicios] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UsuariosServicios]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosServicios_Aplicacion] FOREIGN KEY([idAplicacion])
REFERENCES [dbo].[Aplicacion] ([idAplicacion])
GO

ALTER TABLE [dbo].[UsuariosServicios] CHECK CONSTRAINT [FK_UsuariosServicios_Aplicacion]
GO


