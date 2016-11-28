--Crear usuario
CREATE USER C##OracleConnectionTest IDENTIFIED BY Aq141516182235

--Conceder permisos de coneccion
GRANT CONNECT TO C##OracleConnectionTest

GRANT DBA TO C##OracleConnectionTest

--Concender permisos de acceso a elementos de la BD
GRANT CONNECT, RESOURCE, DBA TO C##OracleConnectionTest

--Permisos a el espacio de tablas
GRANT UNLIMITED TABLESPACE TO C##OracleConnectionTest

--GRANT SELECT,INSERT,UPDATE,DELETE ON schema.GamerShopStore TO C##Starkiller