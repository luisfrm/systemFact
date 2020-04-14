Public Class detalle
    Private idDetalle, idVenta, idProducto As Integer
    Private PU, cantidad As Double

    'Constructores
    Public Sub New()

    End Sub

    Public Sub New(ByVal idDetalle As Integer, ByVal idVenta As Integer, ByVal idProducto As Integer, ByVal PU As Double, ByVal cantidad As Double)

    End Sub

    Public Property gidD
        Get
            Return idDetalle
        End Get
        Set(value)
            idDetalle = value
        End Set
    End Property

    Public Property gidV
        Get
            Return idVenta
        End Get
        Set(value)
            idVenta = value
        End Set
    End Property

    Public Property gidP
        Get
            Return idProducto
        End Get
        Set(value)
            idProducto = value
        End Set
    End Property

    Public Property gPU
        Get
            Return PU
        End Get
        Set(value)
            PU = value
        End Set
    End Property

    Public Property gcantidad
        Get
            Return cantidad
        End Get
        Set(value)
            cantidad = value
        End Set
    End Property

End Class
