Public Class listadoCategorias
    Private id As Integer
    Private ex, ey As Integer
    Private arrastre As Boolean
    Private dt As DataTable
    Private Sub txtCerrar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub txtMinimizar_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub


    Private Sub mostrar()
        Try
            Dim func As New fcategoria
            dt = func.mostrar()
            dgvListado.Columns.Item("Eliminar").Visible = False

            If dt.Rows.Count <> 0 Then
                dgvListado.DataSource = dt
                txtBuscar.Enabled = True
                dgvListado.ColumnHeadersVisible = True
                inexistente.Visible = False
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

    Private Sub buscar()
        Try

            Dim ds As New DataSet
            ds.Tables.Add(dt.Copy)
            Dim dv As New DataView(ds.Tables(0))

            dv.RowFilter = "'" & cbCampo.Text.ToLower & "' like '" & txtBuscar.Text & "%'"

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

    

    Public Sub limpiar()
        btnGuardar.Visible = True
        btnEditar.Visible = False
        txtNombre.Text = ""
        txtId.Text = ""
    End Sub



    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        limpiar()
        mostrar()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If (Me.ValidateChildren = True And txtNombre.Text <> "") Then
            Try
                Dim dts As New categoria
                Dim func As New fcategoria
                dts.gdesc = txtNombre.Text
                If (func.insertar(dts)) Then
                    MessageBox.Show("Categoria registrada correctamente.", "Categoria registrado.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()
                    txtNombre.Focus()
                Else
                    MessageBox.Show("No se pudo completar el registro, intente de nuevo.", "Categoria no se pudo registrar.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Hay campos obligatorios vacios.", "Rellene todos los campos.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub txtNombre_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNombre.Validating
        If (DirectCast(sender, TextBox).Text.Length > 0) Then
            Me.ErrorProvider.SetError(sender, "")
        Else
            Me.ErrorProvider.SetError(sender, "Ingrese el nombre de la categoria, por favor.")
        End If
    End Sub

    Private Sub listadoClientes_MouseDown(sender As Object, e As MouseEventArgs)
        ex = e.X
        ey = e.Y
        arrastre = True
    End Sub

    Private Sub listadoClientes_MouseUp(sender As Object, e As MouseEventArgs)
        arrastre = False
    End Sub

    Private Sub listadoClientes_MouseMove(sender As Object, e As MouseEventArgs)
        If arrastre Then
            Me.Location = Me.PointToScreen(New Point(MousePosition.X - Me.Location.X - ex, MousePosition.Y - Me.Location.Y - ey))
        End If
    End Sub

    Private Sub dgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListado.CellClick
        If Not chbEliminar.Checked Then
            txtId.Text = dgvListado.SelectedCells.Item(1).Value
            txtNombre.Text = dgvListado.SelectedCells.Item(2).Value
            btnGuardar.Visible = False
            btnEditar.Visible = True
        End If
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click

        If (Me.ValidateChildren = True And txtNombre.Text <> "" And txtId.Text <> "") Then
            Try
                Dim result As DialogResult
                result = MessageBox.Show("¿Esta seguro de realizar la modificacion de datos?", "Modificacion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                If (result = Windows.Forms.DialogResult.OK) Then
                    Dim dts As New categoria
                    Dim func As New fcategoria
                    dts.gid = txtId.Text
                    dts.gdesc = txtNombre.Text
                    If (func.editar(dts)) Then
                        MessageBox.Show("Categoria modificada correctamente.", "Categoria modificado.", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub listadoCategorias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnEditar.Visible = False
        dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvListado.DefaultCellStyle.ForeColor = Color.Black
        mostrar()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs)
        Try
            mostrar()
            txtBuscar.Text = ""
            cbCampo.SelectedItem = "ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

    Private Sub cbCampo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbCampo.KeyPress
        e.Handled = True
    End Sub

    Private Sub dgvListado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListado.CellDoubleClick
        If txtProducto.Text = 1 Then
            Inicio.fh.txtIdCat.Text = dgvListado.SelectedCells(1).Value
            Inicio.fh.txtNombreCat.Text = dgvListado.SelectedCells(2).Value
            Me.Close()
        End If
    End Sub


   
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim result As DialogResult = MessageBox.Show("¿Esta seguro de realizar la eliminacion de datos?", "Eliminacion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In dgvListado.Rows
                    Dim marcado As Boolean = Convert.ToBoolean(row.Cells("Eliminar").Value)
                    If marcado Then
                        Dim onekey As Integer = Convert.ToInt32(row.Cells("id").Value)
                        Dim vdg As New categoria
                        Dim func As New fcategoria
                        vdg.gid = onekey
                        If func.eliminar(vdg) Then
                            MessageBox.Show("Cliente eliminado correctamente.", "Eliminacion de registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub txtNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNombre.KeyPress
        If AscW(e.KeyChar) = 13 Then
            btnGuardar_Click(Nothing, e)
        End If
    End Sub
End Class