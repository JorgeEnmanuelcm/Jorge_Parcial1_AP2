create table Materiales(
MaterialId int primary key identity(1,1),
Razon varchar(32)
)

create table MaterialesDetalle(
Id int primary key identity(1,1),
MaterialId int,
Material varchar(32),
Cantidad varchar(32)
)