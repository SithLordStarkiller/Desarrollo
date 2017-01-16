--UPDATE USUARIO SET Usuario = 'sorbagtoa01' WHERE Usuario = '02120101' AND idRol = 2 and iddominio=74
--UPDATE USUARIO SET Usuario = 'sorbagtos01' WHERE Usuario = '02120201' AND idRol = 3
--UPDATE USUARIO SET Usuario = 'sorbagtog01' WHERE Usuario = '02120301' AND idRol = 4
--UPDATE USUARIO SET Usuario = 'sorbagtog02' WHERE Usuario = '02120302' AND idRol = 4
--UPDATE USUARIO SET Usuario = 'sorbagtog03' WHERE Usuario = '02120303' AND idRol = 4


--(1 row(s) affected)

--(1 row(s) affected)

--(1 row(s) affected)

--(1 row(s) affected)

--(1 row(s) affected)


--UPDATE Respuestas SET Valor = 'sorbagtog01' FROM Respuestas r INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo WHERE Nombre = 'AssignedTo' AND Valor = '02120301'
--UPDATE Respuestas SET Valor = 'sorbagtog02' FROM Respuestas r INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo WHERE Nombre = 'AssignedTo' AND Valor = '02120302'
--UPDATE Respuestas SET Valor = 'sorbagtog03' FROM Respuestas r INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo WHERE Nombre = 'AssignedTo' AND Valor = '02120303'

--(50 row(s) affected)

--(101 row(s) affected)

--(102 row(s) affected)

--UPDATE BitacoraRespuestas SET Valor = 'sorbagtog01' FROM BitacoraRespuestas r INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo WHERE Nombre = 'AssignedTo' AND Valor = '02120301'
--UPDATE BitacoraRespuestas SET Valor = 'sorbagtog02' FROM BitacoraRespuestas r INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo WHERE Nombre = 'AssignedTo' AND Valor = '02120302'
--UPDATE BitacoraRespuestas SET Valor = 'sorbagtog03' FROM BitacoraRespuestas r INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo WHERE Nombre = 'AssignedTo' AND Valor = '02120303'


--(2 row(s) affected)

--(2 row(s) affected)

--(4 row(s) affected)
