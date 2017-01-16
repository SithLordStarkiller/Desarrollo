-- 20150720 - MJNS - Cambio del salario minimo para cobranza
--                   Se actualizan todos los formularios
USE SistemasCobranza

UPDATE CamposXML2
SET Valor = '70.10'
WHERE Nombre = 'salarioMinimo'
