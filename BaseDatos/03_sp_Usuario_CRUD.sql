-- Punto único de acceso a dbo.Usuario: @Accion elige la operación y el resto de parámetros se usan según la acción.
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
    SET NOCOUNT ON;   -- omite los mensajes de "filas afectadas" que la aplicación no usa

    IF @Accion = 'C'
    BEGIN
        INSERT INTO dbo.Usuario (Nombre, FechaNacimiento, Sexo)
        VALUES (@Nombre, @FechaNacimiento, @Sexo);

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;   -- Id generado por esta misma inserción
    END
    ELSE IF @Accion = 'R'
    BEGIN
        SELECT Id, Nombre, FechaNacimiento, Sexo
        FROM dbo.Usuario
        WHERE @Id IS NULL OR Id = @Id                 -- sin @Id devuelve todos; con @Id, solo ese usuario
        ORDER BY Nombre;
    END
    ELSE IF @Accion = 'U'
    BEGIN
        UPDATE dbo.Usuario
           SET Nombre = @Nombre, FechaNacimiento = @FechaNacimiento, Sexo = @Sexo
         WHERE Id = @Id;

        SELECT @@ROWCOUNT AS FilasAfectadas;          -- 0 significa que el Id no existe
    END
    ELSE IF @Accion = 'D'
    BEGIN
        DELETE FROM dbo.Usuario WHERE Id = @Id;

        SELECT @@ROWCOUNT AS FilasAfectadas;          -- 0 significa que el Id no existe
    END
    ELSE
    BEGIN
        THROW 50001, N'Acción no válida. Use C, R, U o D.', 1;   -- acción desconocida: error explícito
    END
END
GO
