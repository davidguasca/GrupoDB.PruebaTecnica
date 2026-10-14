-- Crea la base solo si no existe, para que el script pueda ejecutarse varias veces sin error.
IF DB_ID('GrupoDigitalBank') IS NULL
    CREATE DATABASE GrupoDigitalBank;
GO
