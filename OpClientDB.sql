CREATE DATABASE OpClientDB;

CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NULL,
    Email NVARCHAR(100) NULL,
    Pais NVARCHAR(50) NULL
);

CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NULL,
    Categoria NVARCHAR(50) NULL,
    Precio DECIMAL(10,2) NULL
);

CREATE TABLE Fuentes (
    IdFuente INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NULL
);

CREATE TABLE Clasificacion (
    IdClasificacion INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NULL
);

CREATE TABLE Opiniones (
    IdOpinion INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT NOT NULL,
    ProductoId INT NOT NULL,
    FuenteId INT NULL,
    ClasificacionId INT NULL,
    Comentario NVARCHAR(MAX) NULL,
    FechaOpinion DATE NULL,
    PuntajeSatisfaccion INT NULL,
    CONSTRAINT FK_Opiniones_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(IdCliente),
    CONSTRAINT FK_Opiniones_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(IdProducto),
    CONSTRAINT FK_Opiniones_Fuentes FOREIGN KEY (FuenteId) REFERENCES Fuentes(IdFuente),
    CONSTRAINT FK_Opiniones_Clasificacion FOREIGN KEY (ClasificacionId) REFERENCES Clasificacion(IdClasificacion)
);