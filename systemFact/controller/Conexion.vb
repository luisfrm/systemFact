Imports System.Data.SqlClient
Public Class Conexion
    Protected conn As New SqlConnection()

    Protected Function conectar()
        Try
            conn = New SqlConnection("Server=DESKTOP-09AROM6\SQLSERVER;Database=tienda;User Id=sa;Password=;")
            conn.Open()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function

    Protected Function desconectar()
        Try
            If (conn.State = ConnectionState.Open) Then
                conn.Close()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

End Class
