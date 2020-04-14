Public Class categoria
    Private id As Integer
    Private desc As String
    'Constructores
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal desc As String)
        gid = id
        gdesc = desc
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

    Public Property gdesc
        Get
            Return desc
        End Get
        Set(value)
            desc = value
        End Set
    End Property
End Class
