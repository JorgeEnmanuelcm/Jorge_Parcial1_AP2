create table Materiales(
MaterialId int primary key identity(1,1),
Descripcion varchar(32),
Precio int
)

create table Solicitudes(
SolicitudId int primary key identity(1,1),
Fecha varchar(7),
Razon varchar(32),
Total int
)

create table SolicitudesDetalle(
Id int primary key identity(1,1),
SolicitudId int,
MaterialId int,
Material varchar(32),
Cantidad int
)

