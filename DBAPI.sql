create database DBVENTA


use DBVENTA

Create table Rol(
idRol int primary key identity(1,1),
nombre varchar(50),
fechaRegistro datetime default getdate()
)

create table Menu(
idMenu int primary key identity(1,1),
nombre varchar(50),
icono varchar(50),
url varchar(50)
)

create table MenuRol(
idMenuRol int primary key identity(1,1),
idMenu int references Menu(idMenu),
idRol int references Rol(idRol)
)

create table Usuario(
idUsuario int primary key identity(1,1),
nombreCompleto varchar(100),
correo varchar(255),
telefono varchar(20),
puesto varchar (100),
idRol int references Rol(idRol),
clave varchar(40),
esActivo bit default 1,
fechaRegistro datetime default getdate()
);

create table Categoria(
idCategoria int primary key identity(1,1),
nombre varchar(50),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

Create table Producto(
idProducto int primary key identity(1,1),
sku varchar(100),
nombre varchar(100),
tipo int references Categoria(idCategoria),
etiquetas varchar(500),
precio decimal(10,2),
unidadMedida varchar(50),
esActivo bit default 1,
fechaRegistro datetime default getdate()
);

create table NumeroDocumento(
idNumeroDocumento int primary key identity(1,1),
ultimo_Numero int not null,
fechaRegistro datetime default getdate()
)

create table Venta(
idVenta int primary key identity(1,1),
numeroDocumento varchar(40),
total decimal(10,2),
fechaRegistro datetime default getdate()
)

create table Estado(
idEstado int primary key identity(1,1),
descripcion varchar(50)
);

insert into Estado(descripcion) values ('Por atender'),('En proceso'),('En delivery'),('Recibido')

create table Pedido(
idPedido int primary key identity(1,1),
numeroDocumento varchar(40),
fechaPedido datetime default getdate(),
fechaRecepcion datetime null,
fechaDespacho datetime null,
fechaEntrega datetime null,
idVendedor int references Usuario(idUsuario),
idRepartidor int references Usuario(idUsuario),
idEstado int references Estado(idEstado),
total decimal(10,2)
)

insert into Pedido(numeroDocumento,fechaPedido,fechaRecepcion,fechaDespacho,fechaEntrega,idVendedor,idRepartidor,idEstado,total) values
('000001', getdate(),null,null,null,2,4,1,6)

--Consulta para obtener la key utilizada para la generacion del token
select NEWID()

create table DetallePedido(
idDetallePedido int primary key identity(1,1),
idPedido int references Pedido(idPedido),
idProducto int references Producto(idProducto),
cantidad int,
precio decimal(10,2),
total decimal(10,2)
)
select * from Producto
insert into DetallePedido(idPedido,idProducto,cantidad,precio,total) values (1,5,1,4.5,6),(1,7,1,1.5,6)

insert into Rol(nombre) values('Encargado'),('Vendedor'),('Delivery'),('Repartidor')

insert into Usuario(nombreCompleto,correo,telefono,puesto,idRol,clave) values ('Jeff Silva','jsilva@gmail.com','345-6763','Encargado',1,'jsilva')
insert into Usuario(nombreCompleto,correo,telefono,puesto,idRol,clave) values ('Junior Quin','jquin@gmail.com','345-6755','Vendedor',2,'jquin')
insert into Usuario(nombreCompleto,correo,telefono,puesto,idRol,clave) values ('Naty Silva','nsilva@gmail.com','345-6777','Delivery',3,'nsilva')
insert into Usuario(nombreCompleto,correo,telefono,puesto,idRol,clave) values ('Carlos Silva','csilva@gmail.com','345-6788','Repartidor',4,'csilva')

insert into Categoria(nombre,esActivo) values('Sodas',1),('Menestras',1),('Lacteos',1),('Detergente',1),('Galletas',1)

insert into Producto(sku,nombre,tipo,etiquetas,precio,unidadMedida) values('1-I-P-ML','Gaseosa Inca kola personal',1,'etiqueta1, etiqueta2,etiqueta5',2.5,'litros')
,('2-I-M-1L','Gaseosa Inca kola 1L',1,'etiqueta1, etiqueta2,etiqueta5',4.5,'litros'),('3-I-G-3L','Gaseosa Inca kola 3L',1,'etiqueta1, etiqueta2,etiqueta5',10.5,'litros'),
('4-M-G-30G','Galleta Morochas 30gr',5,'etiqueta1, etiqueta2,etiqueta3',1.5,'Kilogramos')

insert into Menu(nombre,icono,url) values ('Usuarios','group','/pages/usuarios'),('Productos','collections_bookmark','/pages/productos'),
('Venta','currency_exchange','/pages/venta'),('Historial Ventas','edit_note','/pages/historial_venta'),('Reportes','receipt','/pages/reportes')

insert into MenuRol(idMenu,idRol) values (1,1),(2,1),(3,1),(4,1),(5,1)

insert into NumeroDocumento(ultimo_Numero,fechaRegistro) values (0,GETDATE())

