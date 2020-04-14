Public Class usuario

    Private id%, valor%
    Private user$, pass$

    'Constructores
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal user As String, ByVal pass As String, ByVal valor As Integer)
        gid = id
        gvalor = valor
        guser = user
        gpass = pass
    End Sub

    'Getters and setters
    Public Property gid
        Get
            Return id
        End Get
        Set(value)
            id = value
        End Set
    End Property

    Public Property gvalor
        Get
            Return valor
        End Get
        Set(value)
            valor = value
        End Set
    End Property

    Public Property guser
        Get
            Return user
        End Get
        Set(value)
            user = value
        End Set
    End Property

    Public Property gpass
        Get
            Return pass
        End Get
        Set(value)
            pass = value
        End Set
    End Property



End Class
