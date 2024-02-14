
--CREACION DB
CREATE DATABASE DB_PRUEBA_AFP

--USO DB
USE DB_PRUEBA_AFP

--CREACION DE TABLAS
CREATE TABLE VEHICULO(
   ID_VEHICULO INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
   NUMERO_PLACA VARCHAR(20),
   VIN VARCHAR(17),
   MARCA VARCHAR(50),
   SERIE VARCHAR(50),
   ANIO INT,
   COLOR VARCHAR(20),
   CANTIDAD_PUERTAS INT
)

--INSERTS
INSERT INTO VEHICULO
VALUES('1', '1', 'A','A',2011, 'Azul',4)

--SELECT
SELECT * FROM VEHICULO