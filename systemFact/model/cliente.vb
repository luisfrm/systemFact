Public Class cliente
    Dim idCliente As Integer
    Dim nombre, apellido, telefono, dni, direccion As String

    'Constructores
    Public Sub New()

    End Sub

    Public Sub New(ByVal idCliente As Integer, ByVal nombre As String, ByVal apellido As String, ByVal telefono As String, ByVal dni As String, ByVal direccion As String)
        gidcliente = idCliente
        gnombre = nombre
        gapellido = apellido
        gtelefono = telefono
        gdni = dni
        gdireccion = direccion
    End Sub





    'Getter and Setter
    Public Property gidcliente
        Get
            Return idCliente
        End Get
        Set(value)
            idCliente = value
        End Set
    End Property

    Public Property gnombre
        Get
            Return nombre
        End Get
        Set(value)
            nombre = value
        End Set
    End Property

    Public Property gapellido
        Get
            Return apellido
        End Get
        Set(value)
            apellido = value
        End Set
    End Property
    Public Property gdni
        Get
            Return dni
        End Get
        Set(value)
            dni = value
        End Set
    End Property
    Public Property gtelefono
        Get
            Return telefono
        End Get
        Set(value)
            telefono = value
        End Set
    End Property

    Public Property gdireccion
        Get
            Return direccion
        End Get
        Set(value)
            direccion = value
        End Set
    End Property




End Class
