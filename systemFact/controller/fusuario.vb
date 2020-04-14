Imports System.Data.SqlClient
Public Class fusuario
    Inherits Conexion
    Private cmd As New SqlCommand
    Public Function selec(dts As usuario) As Boolean
        Try
            conectar()
            Dim Sql As String = "SELECT id, user, pass, valor FROM usuario WHERE user='" & dts.guser & "' and pass='" & dts.gpass & "'"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.Text
            If (cmd.ExecuteReader.HasRows) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error al iniciar sesion.1234", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            desconectar()
        End Try
    End Function


    Public Function insertar(dts As usuario) As Boolean
        Try
            conectar()
            Dim Sql As String = "INSERT INTO usuario(user, pass, valor) VALUES('" & dts.guser & "', '" & dts.gpass & "', " & dts.gvalor & ")"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.Text
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Duplicate entry ") And ex.Message.Contains("for key 'user'") Then
                MessageBox.Show("El usuario ingresado ya existe.", "Registro de usuario.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MsgBox(ex.Message)
            End If
            Return False
        Finally
            desconectar()
        End Try
    End Function

    Public Function editar(dts As usuario) As Boolean
        Try
            conectar()
            Dim Sql As String = "UPDATE usuario SET user=" & dts.guser & ", pass='" & dts.gpass & "', valor='" & dts.gvalor & " WHERE id=" & dts.gid
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.Text
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Duplicate entry ") And ex.Message.Contains("for key 'user'") Then
                MessageBox.Show("El USER ingresado ya existe.", "Registro de usuario.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MsgBox(ex.Message)
            End If
            Return False
        Finally
            desconectar()
        End Try
    End Function

    Public Function eliminar(dts As usuario) As Boolean
        Try
            Dim sql As String = "DELETE FROM usuario WHERE id=" & dts.gid
            conectar()
            cmd = New SqlCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            If (cmd.ExecuteNonQuery) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            desconectar()
        End Try

    End Function
End Class
