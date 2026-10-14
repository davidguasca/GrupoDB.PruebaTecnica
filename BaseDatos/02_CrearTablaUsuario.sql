-- Tabla de usuarios. Id no lo pide el enunciado, pero es necesario para modificar y eliminar sin ambigüedad.
USE GrupoDigitalBank;
GO

IF OBJECT_ID('dbo.Usuario', 'U') IS NULL
CREATE TABLE dbo.Usuario
(
    Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Usuario PRIMARY KEY,
    Nombre          NVARCHAR(100)     NOT NULL,   -- Texto, longitud 100 (Unicode: tildes y ñ)
    FechaNacimiento DATE              NOT NULL,   -- Tipo fecha
    Sexo            CHAR(1)           NOT NULL    -- Texto fijo, longitud 1
        CONSTRAINT CK_Usuario_Sexo CHECK (Sexo IN ('M', 'F'))
);
GO
