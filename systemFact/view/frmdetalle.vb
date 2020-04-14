Public Class frmdetalle
    Private dt As New DataTable
    Dim ex, ey As Integer
    Dim arrastre As Boolean

    Private Sub frmdetalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvListado.DefaultCellStyle.ForeColor = Color.Black
        btnMaximize.Visible = True
        btnRestore.Visible = False
        dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        mostrar()
    End Sub

    Private Sub pnlPrincipal_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlPrincipal.MouseDown
        ex = e.X
        ey = e.Y
        arrastre = True
    End Sub

    Private Sub pnlPrincipal_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlPrincipal.MouseMove
        If arrastre Then
            Me.Location = Me.PointToScreen(New Point(MousePosition.X - Me.Location.X - ex, MousePosition.Y - Me.Location.Y - ey))
        End If
    End Sub

    Private Sub pnlPrincipal_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlPrincipal.MouseUp
        arrastre = False
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        Me.WindowState = FormWindowState.Normal
        btnMaximize.Visible = True
        btnRestore.Visible = False
    End Sub

    Private Sub btnMinimizar_Click(sender As Object, e As EventArgs) Handles btnMinimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
        Me.WindowState = FormWindowState.Maximized
        btnMaximize.Visible = False
        btnRestore.Visible = True
    End Sub

    Private Sub pnlPrincipal_DoubleClick(sender As Object, e As EventArgs) Handles pnlPrincipal.DoubleClick
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
            btnMaximize.Visible = True
            btnRestore.Visible = False
        ElseIf Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
            btnMaximize.Visible = False
            btnRestore.Visible = True
        End If
    End Sub


    Private Sub txtCantidad_ValueChanged(sender As Object, e As EventArgs) Handles txtCantidad.ValueChanged
        If txtCantidad.Value > 100000 Then
            MessageBox.Show("Error. La cantidad no puede exceder los 100000", "Cantidad maxima excedida.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCantidad.Value = 100000
        End If

        If txtCantidad.Value > txtStock.Value Then
            MessageBox.Show("Error. La cantidad no puede ser mayor que el stock", "Cantidad maxima excedida.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCantidad.Value = txtStock.Value
        End If

        If txtCantidad.Value = 0 Then
            txtCantidad.Value = 1
        End If
    End Sub

    Private Sub mostrar()
        Try
            Dim func As New fdetalle
            dt = func.mostrar(txtId.Text)
            dgvListado.Columns.Item("Eliminar").Visible = False

            If dt.Rows.Count <> 0 Then
                dgvListado.DataSource = dt
                dgvListado.ColumnHeadersVisible = True
                inexistente.Visible = False
                ocultarColumnas()
            Else
                dgvListado.DataSource = Nothing
                dgvListado.ColumnHeadersVisible = False
                inexistente.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub vaciarCarrito(value As Integer)
        For Each row As DataGridViewRow In dgvListado.Rows

            Dim dts As New detalle
            Dim func As New fdetalle
            dts.gidD = Convert.ToInt32(row.Cells(3).Value)
            If (func.eliminar(dts)) Then
                If (value = 1) Then
                    MessageBox.Show("El carrito ha sido vaciado exitosamente.", "Carrito vaciado.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtIva.Text = ""
                    txtSub.Text = ""
                    txtTotal.Text = ""
                    actualizarCarrito()
                End If
            End If

        Next
    End Sub

    Private Function convertToDivisa(value As Decimal) As String
        Dim arreglo(), monto As String
        monto = value
        arreglo = monto.Split(",")
        If arreglo.Length > 1 Then
            Return arreglo(0) & "," & arreglo(1).Substring(0, 2)
        Else
            Return monto
        End If
    End Function

    Private Sub actualizarCarrito()
        Dim iva As Decimal = 0
        Dim subtotal As Decimal = 0
        Dim total As Decimal = 0
        For Each row As DataGridViewRow In dgvListado.Rows
            iva += Convert.ToDecimal(row.Cells(7).Value) / 1.16 * 0.16
            subtotal += Convert.ToDecimal(row.Cells(7).Value) / 1.16
            total += Convert.ToDecimal(row.Cells(7).Value)
        Next
        txtSub.Text = convertToDivisa(subtotal)
        txtIva.Text = convertToDivisa(iva)
        txtTotal.Text = convertToDivisa(total)
    End Sub

    Private Sub ocultarColumnas()
        dgvListado.Columns(1).Visible = False
        dgvListado.Columns(2).Visible = False
        dgvListado.Columns(3).Visible = False
    End Sub

    Public Sub limpiar()
        btnGuardar.Visible = True
        txtIdPro.Text = ""
        txtNomPro.Text = ""
        txtCantidad.Value = 1
        txtStock.Value = 1
        txtPU.Text = ""
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If (Me.ValidateChildren = True And txtIdPro.Text <> "" And txtPU.Text <> "") Then
            Try
                Dim dts As New detalle
                Dim func As New fdetalle
                dts.gidV = txtId.Text
                dts.gidP = txtIdPro.Text
                dts.gPU = txtPU.Text
                dts.gcantidad = txtCantidad.Value
                If (func.insertar(dts)) Then
                    mostrar()
                    actualizarCarrito()
                    limpiar()
                    MessageBox.Show("Articulo registrado correctamente.", "Venta registrada.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No se pudo completar el registro, intente de nuevo.", " El articulo no se pudo agregar.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Hay campos obligatorios vacios.", "Rellene todos los campos.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs)

        If (Me.ValidateChildren = True And txtId.Text <> "" And txtNumDoc.Text <> "" And txtIdCli.Text <> "") Then
            Try
                Dim result As DialogResult
                result = MessageBox.Show("¿Esta seguro de realizar la modificacion de datos?", "Modificacion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                If (result = Windows.Forms.DialogResult.OK) Then
                    Dim dts As New cliente
                    Dim func As New fcliente

                    If (func.editar(dts)) Then
                        MessageBox.Show("Cliente modificado correctamente.", "Cliente modificado.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mostrar()
                        limpiar()
                    Else
                        MessageBox.Show("No se pudo completar la modificacion, intente de nuevo.", "Cliente no se pudo modificar.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Hay campos obligatorios vacios.", "Rellene todos los campos.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    

    Private Sub chbEliminar_CheckedChanged(sender As Object, e As EventArgs) Handles chbEliminar.CheckedChanged
        If chbEliminar.Checked Then
            dgvListado.Columns.Item("Eliminar").Visible = True
        Else
            dgvListado.Columns.Item("Eliminar").Visible = False
        End If
    End Sub

    Private Sub dgvListado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListado.CellContentClick
        If e.ColumnIndex = Me.dgvListado.Columns.Item("Eliminar").Index Then
            Dim chkcell As DataGridViewCheckBoxCell = Me.dgvListado.Rows(e.RowIndex).Cells("Eliminar")
            chkcell.Value = Not chkcell.Value
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim result As DialogResult = MessageBox.Show("¿Esta seguro de quitar los articulos de la venta?", "Eliminacion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In dgvListado.Rows
                    If (chbEliminar.Checked) Then
                        If (Convert.ToBoolean(row.Cells(0).Value)) Then
                            Dim func As New fdetalle
                            Dim dts As New detalle
                            dts.gidD = row.Cells(3).Value
                            If func.eliminar(dts) Then
                                MessageBox.Show("Producto eliminado correctamente.", "Eliminacion de registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Call mostrar()
                                Call limpiar()
                                actualizarCarrito()
                            Else
                                MessageBox.Show("Hubo un error al realizar la eliminacion. Intente de nuevo.", "Eliminacion de registros", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Call mostrar()
                                Call limpiar()
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                chbEliminar.Checked = False
            End Try

        End If
    End Sub


    Private Sub txtNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtId.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtApellido_KeyPress(sender As Object, e As KeyPressEventArgs)
        Metodos.soloLetras(e)
    End Sub

    Private Sub txtDni_KeyPress(sender As Object, e As KeyPressEventArgs)
        Metodos.soloNumeros(e)
    End Sub

    Private Sub cbCampo_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = True
    End Sub

    Private Sub btnPro_Click(sender As Object, e As EventArgs) Handles btnPro.Click
        listadoProductos.txtFlag.Text = 1
        listadoProductos.Show()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Dim dialog As DialogResult
        dialog = MessageBox.Show("¿Esta seguro de que desea salir? Se perderan los datos registrados.", "MasterFact", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If dialog = Windows.Forms.DialogResult.Yes Then
            Dim dts As New venta
            Dim func As New fventa
            dts.gidV = txtId.Text
            vaciarCarrito(2)
            func.eliminar(dts)
            Inicio.fv.mostrar()
            Me.Close()
        End If

    End Sub

   
    Private Sub txtPU_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotal.KeyPress, txtSub.KeyPress, txtIva.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtNomPro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNomPro.KeyPress, txtStock.KeyPress, txtPU.KeyPress, txtIdPro.KeyPress
        e.Handled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dialog As DialogResult
        dialog = MessageBox.Show("¿Desea vaciar el carrito?", "Vaciar carrito.", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If dialog = Windows.Forms.DialogResult.Yes Then
            vaciarCarrito(1)
            mostrar()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class