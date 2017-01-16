

ALTER TABLE [dbo].[SubFormulario] DROP CONSTRAINT [FK_SubFormulario_Formulario]
GO

ALTER TABLE [dbo].[SubFormulario]  WITH CHECK ADD  CONSTRAINT [FK_SubFormulario_Formulario] FOREIGN KEY([idFormulario])
REFERENCES [dbo].[Formulario] ([idFormulario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SubFormulario] CHECK CONSTRAINT [FK_SubFormulario_Formulario]
GO


ALTER TABLE [dbo].[FuncionesXCampos] DROP CONSTRAINT [FK_FuncionesXCampos_CatFuncionesJS]
GO

ALTER TABLE [dbo].[FuncionesXCampos] DROP CONSTRAINT [FK_FuncionesXCampos_CamposXSubFormulario]
GO

ALTER TABLE [dbo].[FuncionesXCampos]  WITH CHECK ADD  CONSTRAINT [FK_FuncionesXCampos_CamposXSubFormulario] FOREIGN KEY([idCampoFormulario])
REFERENCES [dbo].[CamposXSubFormulario] ([idCampoFormulario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[FuncionesXCampos] CHECK CONSTRAINT [FK_FuncionesXCampos_CamposXSubFormulario]

GO

ALTER TABLE [dbo].[FuncionesXCampos]  WITH CHECK ADD  CONSTRAINT [FK_FuncionesXCampos_CatFuncionesJS] FOREIGN KEY([idFuncionJS])
REFERENCES [dbo].[CatFuncionesJS] ([idFuncionJS])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[FuncionesXCampos] CHECK CONSTRAINT [FK_FuncionesXCampos_CatFuncionesJS]
GO



