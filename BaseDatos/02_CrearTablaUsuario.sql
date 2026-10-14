-- Tabla de usuarios. Id es una llave sustituta: identifica cada fila aunque dos personas compartan nombre y fecha.
USE GrupoDigitalBank;
GO

IF OBJECT_ID('dbo.Usuario', 'U') IS NULL
CREATE TABLE dbo.Usuario
(
    Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Usuario PRIMARY KEY,   -- el motor lo genera al insertar
    Nombre          NVARCHAR(100)     NOT NULL,   -- Unicode: conserva tildes y ñ
    FechaNacimiento DATE              NOT NULL,   -- solo la fecha, sin hora
    Sexo            CHAR(1)           NOT NULL    -- código de un carácter; el CHECK solo admite M o F
        CONSTRAINT CK_Usuario_Sexo CHECK (Sexo IN ('M', 'F'))
);
GO
