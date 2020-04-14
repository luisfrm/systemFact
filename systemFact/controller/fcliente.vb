Imports System.Data.SqlClient
Public Class fcliente
    Inherits Conexion
    Dim cmd As New sqlcommand

    
    Public Function mostrar() As DataTable
        Try
            conectar()
            Dim Sql As String = "mostrarClientes"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim dt As New DataTable
            If (cmd.ExecuteNonQuery) Then
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                Return dt
            Else
                Return dt
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
            Dim Sql As String = "SELECT id, dni as 'DNI', nombre as 'Nombre', apellido as 'Apellido', telefono as 'Telefono', direccion as 'Direccion' FROM cliente WHERE " & campo & " like '%" & valor & "%' ORDER BY id DESC"
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

    Public Function insertar(dts As cliente) As Boolean
        Try
            conectar()
            Dim Sql As String = "insertCliente"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@dni", dts.gdni)
            cmd.Parameters.AddWithValue("@nombre", dts.gnombre)
            cmd.Parameters.AddWithValue("@apellido", dts.gapellido)
            cmd.Parameters.AddWithValue("@telefono", dts.gtelefono)
            cmd.Parameters.AddWithValue("@direccion", dts.gdireccion)
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Duplicate entry ") And ex.Message.Contains("for key 'dni'") Then
                MessageBox.Show("El DNI ingresado ya existe.", "Registro de cliente.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MsgBox(ex.Message)
            End If
            Return False
        Finally
            desconectar()
        End Try
    End Function

    Public Function editar(dts As cliente) As Boolean
        Try
            conectar()
            Dim Sql As String = "updateCliente"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", dts.gidcliente)
            cmd.Parameters.AddWithValue("@dni", dts.gdni)
            cmd.Parameters.AddWithValue("@nombre", dts.gnombre)
            cmd.Parameters.AddWithValue("@apellido", dts.gapellido)
            cmd.Parameters.AddWithValue("@telefono", dts.gtelefono)
            cmd.Parameters.AddWithValue("@direccion", dts.gdireccion)
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Duplicate entry ") And ex.Message.Contains("for key 'dni'") Then
                MessageBox.Show("El DNI ingresado ya existe.", "Registro de cliente.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MsgBox(ex.Message)
            End If
            Return False
        Finally
            desconectar()
        End Try
    End Function

    Public Function eliminar(dts As cliente) As Boolean
        Try
            Dim sql As String = "deleteCliente"
            conectar()
            cmd = New SqlCommand(sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", dts.gidcliente)
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
