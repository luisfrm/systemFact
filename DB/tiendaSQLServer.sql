create table usuario(
id int IDENTITY(1,1) primary key,
usuario varchar(13) not null unique,
pass varchar(13) null,
nombre varchar(30) null,
question1 varchar(50) null,
question2 varchar(50) null,
answer1 varchar(50) null,
answer2 varchar(50) null,
valor int not null
);

create table cliente(
id int primary key IDENTITY(1,1), 
dni int unique not null,
nombre varchar(15) not null,
apellido varchar(15) not null,
telefono varchar(20) null,
direccion varchar(50) null
);

create table categoria(
id int primary key IDENTITY(1,1),
nombre varchar(50) not null
);

create table producto(
id int IDENTITY(1,1) primary key,
descripcion	varchar(50) not null,
precioCompra decimal(18,2) not null,
precioVenta decimal(18,2) not null,
stock int not null,
idCategoria int null,
fechaVencimiento date not null,
CONSTRAINT fkCat FOREIGN KEY(idCategoria) REFERENCES categoria(id) ON DELETE CASCADE ON UPDATE CASCADE 
);

create table venta(
idVenta int primary key IDENTITY(1,1),
idCliente int not null,
fechaVenta date not null,
tipoDocumento varchar(50) not null,
numDocumento varchar(50) not null,
CONSTRAINT fkCliente FOREIGN KEY (idCliente) REFERENCES cliente(id) ON DELETE CASCADE ON UPDATE CASCADE
);

create table detalle_venta(
idDetalle int primary key IDENTITY(1,1),
idVenta int not null,
idProducto int not null,
cantidad int not null,
precioUnitario decimal (18,2) not null,
CONSTRAINT fkVenta FOREIGN KEY(idVenta) REFERENCES venta(idVenta)
);

insert into usuario(usuario, pass, valor) VALUES('admin', 'admin', 0);