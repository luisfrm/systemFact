Public Class listadoProductos
    Dim dt As New DataTable
    Dim ex, ey As Integer
    Dim arrastre As Boolean
    Dim idProducto As Integer
    Dim dts As producto

    Private Sub formato_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dtpVencimiento.Value = Now
        mostrar()

    End Sub

    Public Sub limpiar()
        btnGuardar.Visible = True
        btnEditar.Visible = False
        txtIdCat.Text = ""
        txtDescripcion.Text = ""
        txtStock.Text = ""
        txtCompra.Text = ""
        txtVenta.Text = ""
        txtId.Text = ""
        dtpVencimiento.Value = Now
        txtNombreCat.Text = ""
    End Sub

    Private Sub mostrar()
        Try
            Dim func As New fproducto
            dt = func.mostrar()
            dgvListado.Columns.Item("Eliminar").Visible = False

            If dt.Rows.Count <> 0 Then
                dgvListado.DataSource = dt
                txtBuscar.Enabled = True
                dgvListado.ColumnHeadersVisible = True
                inexistente.Visible = False
                ocultarColumnas()
            Else
                dgvListado.DataSource = Nothing
                txtBuscar.Enabled = False
                dgvListado.ColumnHeadersVisible = False
                inexistente.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        btnNuevo.Visible = True
        btnEditar.Visible = False
        buscar()

    End Sub

    Private Sub ocultarColumnas()
        dgvListado.Columns(1).Visible = False
        dgvListado.Columns(2).Visible = False
    End Sub

    Private Sub buscar()
        Try

            Dim ds As New DataSet
            ds.Tables.Add(dt.Copy)
            Dim dv As New DataView(ds.Tables(0))

            dv.RowFilter = "'" & cbCampo.Text.ToLower() & "' like '" & txtBuscar.Text & "%'"

            If (dv.Count) <> 0 Then
                inexistente.Visible = False
                dgvListado.DataSource = dv

            Else
                inexistente.Visible = False
                dgvListado.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        limpiar()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If (Me.ValidateChildren = True And txtIdCat.Text <> "" And txtDescripcion.Text <> "" And txtStock.Text <> "" And txtCompra.Text <> "") Then
            Try
                Dim dts As New producto
                Dim func As New fproducto
                dts.gidCat = txtIdCat.Text
                dts.gdesc = txtDescripcion.Text
                dts.gstock = txtStock.Text
                dts.gPCompra = txtCompra.Text
                dts.gPVenta = txtVenta.Text
                dts.gfVenc = dtpVencimiento.Value
                If (func.insertar(dts)) Then
                    MessageBox.Show("Producto registrado correctamente.", "Producto registrado.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()
                Else
                    MessageBox.Show("No se pudo completar el registro, intente de nuevo.", "Producto no se pudo registrar.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Hay campos obligatorios vacios.", "Rellene todos los campos.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub dgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListado.CellClick
        If Not chbEliminar.Checked Then
            txtId.Text = Convert.ToInt32(dgvListado.SelectedCells(1).Value)
            txtIdCat.Text = Convert.ToString(dgvListado.SelectedCells(2).Value)
            txtNombreCat.Text = Convert.ToString(dgvListado.SelectedCells(3).Value)
            txtDescripcion.Text = Convert.ToString(dgvListado.SelectedCells(4).Value)
            txtStock.Text = Convert.ToString(dgvListado.SelectedCells(5).Value)
            txtCompra.Text = Convert.ToString(dgvListado.SelectedCells(6).Value)
            txtVenta.Text = Convert.ToString(dgvListado.SelectedCells(7).Value)
            dtpVencimiento.Value = dgvListado.SelectedCells(8).Value
            btnGuardar.Visible = False
            btnEditar.Visible = True
        End If
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click

        If (Me.ValidateChildren = True And txtCompra.Text <> "" And txtDescripcion.Text <> "" And txtId.Text <> "" And txtIdCat.Text <> "" And txtStock.Text <> "" And txtVenta.Text <> "") Then
            Try
                Dim result As DialogResult
                result = MessageBox.Show("¿Esta seguro de realizar la modificacion de datos?", "Modificacion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                If (result = Windows.Forms.DialogResult.OK) Then
                    Dim dts As New producto
                    Dim func As New fproducto
                    dts.gid = txtId.Text
                    dts.gidCat = txtIdCat.Text
                    dts.gdesc = txtDescripcion.Text
                    dts.gstock = txtStock.Text
                    dts.gPCompra = txtCompra.Text
                    dts.gPVenta = txtVenta.Text
                    dts.gfVenc = dtpVencimiento.Text
                    If (func.editar(dts)) Then
                        MessageBox.Show("Producto modificado correctamente.", "Producto modificado.", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Dim result As DialogResult = MessageBox.Show("¿Esta seguro de realizar la eliminacion de datos?", "Eliminacion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In dgvListado.Rows
                    Dim marcado As Boolean = Convert.ToBoolean(row.Cells("Eliminar").Value)
                    If marcado Then
                        Dim onekey As Integer = Convert.ToInt32(row.Cells("ID").Value)
                        Dim vdg As New producto
                        Dim func As New fproducto
                        vdg.gid = onekey
                        If func.eliminar(vdg) Then
                            MessageBox.Show("Producto eliminado correctamente.", "Eliminacion de registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Call mostrar()
                            Call limpiar()
                        Else
                            MessageBox.Show("Hubo un error al realizar la eliminacion. Intente de nuevo.", "Eliminacion de registros", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Call mostrar()
                            Call limpiar()
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

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
       Try
            mostrar()
            txtBuscar.Text = ""
            cbCampo.SelectedItem = "ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cbCampo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbCampo.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

   
    Private Sub txtVenta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVenta.KeyPress
        soloNumeros(e)
    End Sub

    Private Sub txtCompra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCompra.KeyPress
        soloNumeros(e)
    End Sub

    Private Sub txtStock_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStock.KeyPress
        soloNumeros(e)
    End Sub

    Private Sub txtNombreCat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNombreCat.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtIdCat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdCat.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtId.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnCat_Click(sender As Object, e As EventArgs) Handles btnCat.Click
        auxCategorias.Show()
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        Try
            If txtBuscar.Text <> "" Then
                Dim func As New fcliente
                dt = func.mostrarW(cbCampo.Text.ToLower, txtBuscar.Text)
                dgvListado.Columns.Item("Eliminar").Visible = False

                If dt.Rows.Count <> 0 Then
                    dgvListado.DataSource = dt
                    dgvListado.ColumnHeadersVisible = True
                    inexistente.Visible = False
                Else
                    dgvListado.DataSource = Nothing
                    dgvListado.ColumnHeadersVisible = False
                    inexistente.Visible = True
                End If
            Else
                mostrar()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvListado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListado.CellDoubleClick
        If txtFlag.Text = "1" Then
            If Convert.ToInt32(dgvListado.SelectedCells(5).Value) > 0 Then
                frmdetalle.txtIdPro.Text = dgvListado.SelectedCells(1).Value
                frmdetalle.txtNomPro.Text = dgvListado.SelectedCells(4).Value
                frmdetalle.txtStock.Value = Convert.ToInt32(dgvListado.SelectedCells(5).Value)
                frmdetalle.txtPU.Text = Convert.ToDouble(dgvListado.SelectedCells(7).Value)
                Me.Close()
            Else
                MessageBox.Show("Error. Ahorita mismo no quedan articulos de este producto en Stock", "Producto no disponible", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub
End Class