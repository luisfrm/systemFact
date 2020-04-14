Imports System.Data.SqlClient
Public Class fventa
    Inherits Conexion
    Dim cmd As New SqlCommand


    Public Function mostrar() As DataTable
        Try
            conectar()
            Dim Sql As String = "mostrarVenta"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
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

    Public Function mostrarW(ByVal campo As String, valor As String) As DataTable
        Try
            conectar()
            Dim Sql As String = "SELECT venta.idVenta as 'id Venta', venta.idCliente as 'id Cliente', cliente.nombre as 'Nombre', cliente.apellido as 'Apellido', cliente.dni as 'DNI', venta.fechaVenta as 'Fecha de venta', venta.tipoDocumento as 'Tipo de documento', venta.numDocumento as 'Numero de documento' FROM cliente INNER JOIN venta ON cliente.id=venta.idCliente " & campo & " like '%" & valor & "%'" & " ORDER BY idVenta desc"
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

    Public Function insertar(dts As venta) As Boolean
        Try
            conectar()
            Dim Sql As String = "insertVenta"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idCliente", dts.gidC)
            cmd.Parameters.AddWithValue("@fechaVenta", dts.gfv)
            cmd.Parameters.AddWithValue("@tipoDocumento", dts.gtDoc)
            cmd.Parameters.AddWithValue("@numDocumento", dts.gnDoc)
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

    Public Function editar(dts As venta) As Boolean
        Try
            conectar()
            Dim Sql As String = "updateVenta"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idVenta", dts.gidV)
            cmd.Parameters.AddWithValue("@idCliente", dts.gidC)
            cmd.Parameters.AddWithValue("@fechaVenta", dts.gfv)
            cmd.Parameters.AddWithValue("@tipoDocumento", dts.gtDoc)
            cmd.Parameters.AddWithValue("@numDocumento", dts.gnDoc)
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Duplicate entry ") And ex.Message.Contains("for key 'numDocumento'") Then
                MessageBox.Show("El numero de documento ingresado ya existe.", "Registro de venta.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MsgBox(ex.Message)
            End If
            Return False
        Finally
            desconectar()
        End Try
    End Function

    Public Function eliminar(dts As venta) As Boolean
        Try
            Dim sql As String = "deleteVenta"
            conectar()
            cmd = New SqlCommand(sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idVenta", dts.gidV)
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
