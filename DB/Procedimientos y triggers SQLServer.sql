CREATE PROCEDURE mostrarClientes AS
BEGIN
SET NOCOUNT ON
SELECT id, nombre AS 'Nombre', apellido AS 'Apellido', dni AS 'Cedula', telefono AS 'Telefono', direccion AS 'Direccion' FROM cliente
END
GO

CREATE PROCEDURE insertCliente
@dni int,
@nombre varchar(15),
@apellido varchar(15),
@telefono varchar(20),
@direccion varchar(50)
AS
BEGIN
INSERT INTO cliente(dni, nombre, apellido, telefono, direccion) VALUES(@dni, @nombre, @apellido, @telefono, @direccion)
END
GO

CREATE PROCEDURE updateCliente
@id int,
@dni int,
@nombre varchar(15),
@apellido varchar(15),
@telefono varchar(20),
@direccion varchar(50)
AS
BEGIN
UPDATE cliente SET dni=@dni, nombre=@nombre, apellido=@apellido, telefono=@telefono, direccion=@direccion WHERE id=@id
END
GO

CREATE PROCEDURE deleteCliente
@id int
AS
BEGIN
DELETE FROM cliente WHERE id=@id
END
GO

CREATE PROCEDURE someCliente(@campo varchar(20), @valor varchar(20))
AS
BEGIN
SET NOCOUNT ON
SELECT id, nombre AS 'Nombre', apellido AS 'Apellido', dni AS 'Cedula', telefono AS 'Telefono', direccion AS 'Direccion' FROM cliente WHERE @campo LIKE '%' + @valor + '%'
END
GO

CREATE PROCEDURE mostrarProducto AS
BEGIN
SET NOCOUNT ON
SELECT producto.id as 'ID Producto', categoria.id as 'ID Categoria', categoria.nombre as 'Categoria', producto.descripcion as 'Producto', producto.stock as 'Stock', producto.precioCompra as 'Precio de compra', producto.precioVenta as 'Precio de venta', producto.fechaVencimiento as 'Fecha de vencimiento' FROM producto INNER JOIN categoria ON producto.idCategoria=categoria.id ORDER BY producto.id
END
GO

CREATE PROCEDURE insertProducto
@idCategoria int,
@descripcion varchar(15),
@stock varchar(15),
@precioCompra varchar(20),
@precioVenta varchar(50),
@fechaVencimiento date
AS
BEGIN
INSERT INTO producto(idCategoria, descripcion, stock, precioCompra, precioVenta, fechaVencimiento) VALUES (@idCategoria, @descripcion, @stock, @precioCompra, @precioVenta, @fechaVencimiento)
END
GO

CREATE PROCEDURE updateProducto
@id int,
@idCategoria int,
@descripcion varchar(15),
@stock varchar(15),
@precioCompra varchar(20),
@precioVenta varchar(50),
@fechaVencimiento date
AS
BEGIN
UPDATE producto SET idCategoria=@idCategoria, descripcion=@descripcion, stock=@stock, precioCompra=@precioCompra, precioVenta=@precioVenta, fechaVencimiento=@fechaVencimiento WHERE id=@id
END
GO

CREATE PROCEDURE deleteProducto
@id int
AS
BEGIN
DELETE FROM producto WHERE id=@id
END
GO

CREATE PROCEDURE someProducto(@campo varchar(20), @valor varchar(20))
AS
BEGIN
SET NOCOUNT ON
SELECT producto.id as 'ID Producto', categoria.id as 'ID Categoria', categoria.nombre as 'Categoria', producto.descripcion as 'Producto', producto.stock as 'Stock', producto.precioCompra as 'Precio de compra', producto.precioVenta as 'Precio de venta', producto.fechaVencimiento as 'Fecha de vencimiento' FROM producto INNER JOIN categoria ON producto.idCategoria=categoria.id WHERE @campo LIKE '%' + @valor + '%'
END
GO

CREATE PROCEDURE mostrarCategorias
AS
BEGIN
SET NOCOUNT ON
SELECT id as 'ID', nombre as 'Nombre' FROM categoria
END
GO

CREATE PROCEDURE insertCategoria(@nombre varchar(50))
AS
BEGIN
SET NOCOUNT ON
INSERT INTO categoria(nombre) VALUES(@nombre)
END
GO

CREATE PROCEDURE updateCategoria(@id int, @nombre varchar(50))
AS
BEGIN
SET NOCOUNT ON
UPDATE categoria SET nombre=@nombre WHERE id=@id
END
GO

CREATE PROCEDURE deleteCategoria(@id int)
AS
BEGIN
SET NOCOUNT ON
DELETE FROM categoria WHERE id=@id
END
GO

CREATE PROCEDURE mostrarVenta
AS
BEGIN
SET NOCOUNT ON
SELECT venta.idVenta as 'id Venta', venta.idCliente as 'id Cliente', cliente.nombre as 'Nombre', cliente.apellido as 'Apellido', cliente.dni as 'DNI', venta.fechaVenta as 'Fecha de venta', venta.tipoDocumento as 'Tipo de documento', venta.numDocumento as 'Numero de documento' FROM cliente INNER JOIN venta ON cliente.id=venta.idCliente ORDER BY idVenta desc
END
GO

CREATE PROCEDURE insertVenta
@idCliente int,
@fechaVenta date,
@tipoDocumento varchar(50),
@numDocumento varchar(50)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO venta(idCliente, fechaVenta, tipoDocumento, numDocumento) VALUES(@idCliente, @fechaVenta, @tipoDocumento, @numDocumento)
END
GO

CREATE PROCEDURE updateVenta
@idVenta int,
@idCliente int,
@fechaVenta date,
@tipoDocumento varchar(50),
@numDocumento varchar(50)
AS
BEGIN
SET NOCOUNT ON
UPDATE venta SET idCliente=@idCliente, fechaVenta=@fechaVenta, tipoDocumento=@tipoDocumento, numDocumento=@numDocumento WHERE idVenta=@idVenta
END
GO

CREATE PROCEDURE deleteVenta
@idVenta int
AS
BEGIN
SET NOCOUNT ON
DELETE FROM venta WHERE idVenta=@idVenta
END
GO

CREATE PROCEDURE mostrarDetalle
@idVenta int
AS
BEGIN
SET NOCOUNT ON
SELECT detalle_venta.idVenta, detalle_venta.idProducto, detalle_venta.idDetalle, producto.descripcion AS 'Producto', detalle_venta.cantidad AS 'Cantidad', detalle_venta.precioUnitario AS 'PU', (detalle_venta.precioUnitario * detalle_venta.cantidad) AS 'Precio total' FROM detalle_venta INNER JOIN producto ON producto.id = detalle_venta.idProducto WHERE idVenta=@idVenta
END
GO

CREATE PROC insertDetalle
@idVenta int,
@idProducto int,
@cantidad decimal(18,2),
@precioUnitario as decimal(18,2)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO detalle_venta (idVenta, idProducto, cantidad, precioUnitario) VALUES (@idVenta, @idProducto, @cantidad, @precioUnitario)
END
GO

CREATE PROC updateDetalle
@idDetalle int,
@idVenta int,
@idProducto int,
@cantidad decimal(18,2),
@precioUnitario decimal(18,2)
AS
BEGIN
SET NOCOUNT ON
UPDATE detalle_venta SET idVenta= @idVenta, idProducto= @idProducto, cantidad=@cantidad, precioUnitario=@precioUnitario WHERE idDetalle=@idDetalle
END
GO

CREATE PROC deleteDetalle
@idDetalle int
AS
BEGIN
SET NOCOUNT ON
DELETE FROM detalle_venta WHERE idDetalle=@idDetalle
END
GO

CREATE PROC comprobante
@idVenta int
AS
BEGIN
SET NOCOUNT ON
SELECT        dbo.venta.idVenta, dbo.cliente.nombre, dbo.cliente.apellido, dbo.cliente.dni, dbo.venta.fechaVenta, dbo.venta.tipoDocumento, dbo.venta.numDocumento, dbo.producto.descripcion, dbo.detalle_venta.cantidad, 
                         dbo.detalle_venta.precioUnitario as 'PU',  (dbo.detalle_venta.cantidad * dbo.detalle_venta.precioUnitario) as 'Precio total'

FROM            dbo.venta INNER JOIN
                         dbo.detalle_venta ON dbo.venta.idVenta = dbo.detalle_venta.idVenta INNER JOIN
                         dbo.producto ON dbo.detalle_venta.idProducto = dbo.producto.id INNER JOIN
                         dbo.cliente ON dbo.venta.idCliente = dbo.cliente.id

WHERE			dbo.venta.idVenta=@idVenta
END
GO

--  Creacion de Triggers
CREATE TRIGGER reducirStock 
ON detalle_venta 
AFTER INSERT
AS
BEGIN
SET NOCOUNT ON
DECLARE @cantidad int
DECLARE @idProducto int
SELECT @cantidad = detalle_venta.cantidad FROM detalle_venta INNER JOIN inserted ON inserted.idDetalle = detalle_venta.idDetalle WHERE detalle_venta.idDetalle=inserted.idDetalle
SELECT @idProducto = detalle_venta.idProducto FROM detalle_venta INNER JOIN inserted ON inserted.idDetalle = detalle_venta.idDetalle WHERE detalle_venta.idDetalle=inserted.idDetalle
UPDATE producto SET stock = producto.stock - @cantidad WHERE producto.id = @idProducto;
END
GO

CREATE TRIGGER aumentarStock 
ON detalle_venta 
AFTER DELETE 
AS
BEGIN 
SET NOCOUNT ON
DECLARE @cantidad int
DECLARE @idProducto int
SELECT @cantidad = deleted.cantidad FROM deleted
SELECT @idProducto = deleted.idProducto FROM deleted
UPDATE producto SET stock = producto.stock + @cantidad WHERE producto.id = @idProducto;
END
GO






