GO
ALTER TABLE formulario ADD EnviarMovil bit 

GO
UPDATE formulario SET EnviarMovil=1 WHERE Ruta NOT IN ('RDST','RA')

GO
UPDATE formulario SET EnviarMovil=0 WHERE EnviarMovil IS NULL
