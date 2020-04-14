Imports System.Data.SqlClient
Public Class fdetalle
    Inherits Conexion
    Dim cmd As New SqlCommand


    Public Function mostrar(ByVal idVenta As Integer) As DataTable
        Try
            conectar()
            Dim Sql As String = "mostrarDetalle"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idVenta", idVenta)
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


    Public Function insertar(dts As detalle) As Boolean
        Try
            conectar()
            Dim Sql As String = "insertDetalle"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idVenta", dts.gidV)
            cmd.Parameters.AddWithValue("@idProducto", dts.gidP)
            cmd.Parameters.AddWithValue("@Cantidad", dts.gcantidad)
            cmd.Parameters.AddWithValue("@precioUnitario", dts.gPU)
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

    Public Function editar(dts As detalle) As Boolean
        Try
            conectar()
            Dim Sql As String = "updateDetalle"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idDetalle", dts.gidD)
            cmd.Parameters.AddWithValue("@idVenta", dts.gidV)
            cmd.Parameters.AddWithValue("@idProducto", dts.gidP)
            cmd.Parameters.AddWithValue("@cantidad", dts.gcantidad)
            cmd.Parameters.AddWithValue("@precioUnitario", dts.gPU)
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

    Public Function eliminar(dts As detalle) As Boolean
        Try
            Dim sql As String = "deleteDetalle"
            conectar()
            cmd = New SqlCommand(sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idDetalle", dts.gidD)
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

    'Public Function aumentarStock(dts As detalle) As Boolean
    '    Try
    '        Dim Sql As String = "UPDATE producto SET stock=stock+" & dts.gcantidad & " WHERE id=" & dts.gidP
    '        conectar()
    '        cmd = New MySqlCommand(sql, conn)
    '        cmd.CommandType = CommandType.Text
    '        If (cmd.ExecuteNonQuery) Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        Return False
    '    Finally
    '        desconectar()
    '    End Try
    'End Function

    'Public Function disminuirStock(dts As detalle) As Boolean
    '    Try
    '        Dim Sql As String = "UPDATE producto SET stock=stock-" & dts.gcantidad & " WHERE id=" & dts.gidP
    '        conectar()
    '        cmd = New MySqlCommand(Sql, conn)
    '        cmd.CommandType = CommandType.Text
    '        If (cmd.ExecuteNonQuery) Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        Return False
    '    Finally
    '        desconectar()
    '    End Try
    'End Function

End Class
