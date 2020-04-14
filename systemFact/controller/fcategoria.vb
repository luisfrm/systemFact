Imports System.Data.SqlClient
Public Class fcategoria
    Inherits Conexion

    Private cmd As New SqlCommand
    Public Function mostrar() As DataTable
        Try
            conectar()
            Dim Sql As String = "mostrarCategorias"
            cmd = New sqlcommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            If cmd.ExecuteNonQuery Then
                Dim dt As New DataTable
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function

    Public Function mostrarW(ByVal campo As String, valor As String) As DataTable
        Try
            conectar()
            Dim Sql As String = "SELECT id as 'ID', nombre as 'Nombre' FROM categoria WHERE " & campo & "='" & valor & "'"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.Text
            If (cmd.ExecuteNonQuery) Then
                Dim dt As New DataTable
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function

    Public Function insertar(dts As categoria) As Boolean
        Try
            conectar()
            Dim sql As String = "insertCategoria"
            cmd = New SqlCommand(sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@nombre", dts.gdesc)
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Duplicate entry ") And ex.Message.Contains("for key 'PRIMARY'") Then
                MsgBox("El ID ingresado ya esta registrado.")
            Else
                MsgBox(ex.Message)
            End If
            Return False
        Finally
            desconectar()
        End Try
    End Function

    Public Function editar(dts As categoria) As Boolean
        Try
            conectar()
            Dim sql As String = "updateCategoria"
            cmd = New SqlCommand(sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", dts.gid)
            cmd.Parameters.AddWithValue("@nombre", dts.gdesc)
            If cmd.ExecuteNonQuery Then
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

    Public Function eliminar(dts As categoria) As Boolean
        Try
            conectar()
            Dim sql As String = "deleteCategoria"
            cmd = New SqlCommand(sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", dts.gid)
            If cmd.ExecuteNonQuery Then
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
