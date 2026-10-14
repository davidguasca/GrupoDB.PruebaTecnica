-- Datos de ejemplo; solo se insertan si la tabla está vacía.
USE GrupoDigitalBank;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Usuario)
BEGIN
    EXEC dbo.sp_Usuario_CRUD @Accion = 'C', @Nombre = N'Ana María Pérez', @FechaNacimiento = '1990-05-10', @Sexo = 'F';
    EXEC dbo.sp_Usuario_CRUD @Accion = 'C', @Nombre = N'Carlos Gómez',    @FechaNacimiento = '1985-11-23', @Sexo = 'M';
END
GO
