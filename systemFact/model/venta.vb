Public Class venta
    Private idVenta, idCliente As Integer
    Private fechaVenta, tipoDocumento, numDocumento As String

    'Constructores
    Public Sub New()

    End Sub

    Public Sub New(ByVal idVenta As Integer, ByVal idCliente As Integer, ByVal fechaVenta As String, ByVal tipoDocumento As String, ByVal numDocumento As String)
        gidV = idVenta
        gidC = idCliente
        gfv = fechaVenta
        gtDoc = tipoDocumento
        gnDoc = numDocumento
    End Sub

    'Setters and getters
    Public Property gidV
        Get
            Return idVenta
        End Get
        Set(value)
            idVenta = value
        End Set
    End Property

    Public Property gidC
        Get
            Return idCliente
        End Get
        Set(value)
            idCliente = value
        End Set
    End Property

    Public Property gfv
        Get
            Return fechaVenta
        End Get
        Set(value)
            fechaVenta = value
        End Set
    End Property

    Public Property gtDoc
        Get
            Return tipoDocumento
        End Get
        Set(value)
            tipoDocumento = value
        End Set
    End Property

    Public Property gnDoc
        Get
            Return numDocumento
        End Get
        Set(value)
            numDocumento = value
        End Set
    End Property

End Class
