Imports System.Data.SqlClient

Public Class fproducto
    Inherits Conexion
    Dim cmd As New SqlCommand


    Public Function mostrar() As DataTable
        Try
            conectar()
            Dim Sql As String = "mostrarProducto"
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
            Dim Sql As String = "SELECT producto.id as 'ID Producto', categoria.id as 'ID Categoria', categoria.nombre as 'Categoria', producto.descripcion as 'Producto', producto.stock as 'Stock', producto.precioCompra as 'Precio de compra', producto.precioVenta as 'Precio de venta', producto.fechaVencimiento as 'Fecha de vencimiento' FROM producto INNER JOIN categoria ON producto.idCategoria=categoria.id WHERE " & campo & "like '%" & valor & "%' ORDER BY producto.id"
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

    Public Function insertar(dts As producto) As Boolean
        Try
            conectar()
            Dim Sql As String = "insertProducto"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idCategoria", dts.gidCat)
            cmd.Parameters.AddWithValue("@descripcion", dts.gdesc)
            cmd.Parameters.AddWithValue("@stock", dts.gstock)
            cmd.Parameters.AddWithValue("@precioCompra", dts.gPCompra)
            cmd.Parameters.AddWithValue("@precioVenta", dts.gPVenta)
            cmd.Parameters.AddWithValue("@fechaVencimiento", dts.gfVenc)
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Duplicate entry ") And ex.Message.Contains("for key 'PRIMARY'") Then
                MessageBox.Show("El ID ingresado ya existe.", "Registro de producto.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MsgBox(ex.Message)
            End If
            Return False
        Finally
            desconectar()
        End Try
    End Function

    Public Function editar(dts As producto) As Boolean
        Try
            conectar()
            Dim Sql As String = "updateProducto"
            cmd = New SqlCommand(Sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", dts.gid)
            cmd.Parameters.AddWithValue("@idCategoria", dts.gidCat)
            cmd.Parameters.AddWithValue("@descripcion", dts.gdesc)
            cmd.Parameters.AddWithValue("@stock", dts.gstock)
            cmd.Parameters.AddWithValue("@precioCompra", dts.gPCompra)
            cmd.Parameters.AddWithValue("@precioVenta", dts.gPVenta)
            cmd.Parameters.AddWithValue("@fechaVencimiento", dts.gfVenc)
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Duplicate entry ") And ex.Message.Contains("for key 'PRIMARY'") Then
                MessageBox.Show("El ID ingresado ya existe.", "Registro de producto.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MsgBox(ex.Message)
            End If
            Return False
        Finally
            desconectar()
        End Try
    End Function

    Public Function eliminar(dts As producto) As Boolean
        Try
            Dim sql As String = "deleteProducto"
            conectar()
            cmd = New SqlCommand(sql, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", dts.gid)
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
