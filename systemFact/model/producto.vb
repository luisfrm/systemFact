Public Class producto
    Private id, idCategoria, stock As Integer
    Private precioCompra, precioVenta As Double
    Private fechaVencimiento As String
    Private descripcion As String

    'Constructores
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal idcategoria As Integer, ByVal stock As Integer, ByVal precioCompra As Double, ByVal precioVenta As Double, ByVal fechaVencimiento As Date, ByVal descripcion As String)
        gid = id
        gidCat = idcategoria
        gstock = stock
        gPCompra = precioCompra
        gPVenta = precioVenta
        gfVenc = fechaVencimiento
        gdesc = descripcion

    End Sub

    'Getters and Setters

    Public Property gid
        Get
            Return id
        End Get
        Set(value)
            id = value
        End Set
    End Property

    Public Property gidCat
        Get
            Return idCategoria
        End Get
        Set(value)
            idCategoria = value
        End Set
    End Property

    Public Property gstock
        Get
            Return stock
        End Get
        Set(value)
            stock = value
        End Set
    End Property

    Public Property gPCompra
        Get
            Return precioCompra
        End Get
        Set(value)
            precioCompra = value
        End Set
    End Property

    Public Property gPVenta
        Get
            Return precioVenta
        End Get
        Set(value)
            precioVenta = value
        End Set
    End Property

    Public Property gfVenc
        Get
            Return fechaVencimiento
        End Get
        Set(value)
            fechaVencimiento = value
        End Set
    End Property

    Public Property gdesc
        Get
            Return descripcion
        End Get
        Set(value)
            descripcion = value
        End Set
    End Property
End Class
