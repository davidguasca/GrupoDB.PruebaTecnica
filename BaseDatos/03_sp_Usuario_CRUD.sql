-- Procedimiento CRUD: única puerta de entrada a dbo.Usuario.
USE GrupoDigitalBank;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_CRUD
    @Accion          CHAR(1),               -- C = Crear, R = Consultar, U = Actualizar, D = Eliminar
    @Id              INT           = NULL,
    @Nombre          NVARCHAR(100) = NULL,
    @FechaNacimiento DATE          = NULL,
    @Sexo            CHAR(1)       = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Accion = 'C'
    BEGIN
        INSERT INTO dbo.Usuario (Nombre, FechaNacimiento, Sexo)
        VALUES (@Nombre, @FechaNacimiento, @Sexo);

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
    END
    ELSE IF @Accion = 'R'
    BEGIN
        SELECT Id, Nombre, FechaNacimiento, Sexo
        FROM dbo.Usuario
        WHERE @Id IS NULL OR Id = @Id
        ORDER BY Nombre;
    END
    ELSE IF @Accion = 'U'
    BEGIN
        UPDATE dbo.Usuario
           SET Nombre = @Nombre, FechaNacimiento = @FechaNacimiento, Sexo = @Sexo
         WHERE Id = @Id;

        SELECT @@ROWCOUNT AS FilasAfectadas;
    END
    ELSE IF @Accion = 'D'
    BEGIN
        DELETE FROM dbo.Usuario WHERE Id = @Id;

        SELECT @@ROWCOUNT AS FilasAfectadas;
    END
    ELSE
    BEGIN
        THROW 50001, N'Acción no válida. Use C, R, U o D.', 1;
    END
END
GO
