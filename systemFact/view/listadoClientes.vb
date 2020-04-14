Public Class listadoClientes
    Public user As usuario
    Private idCliente As Integer
    Private ex, ey As Integer
    Private arrastre As Boolean
    Private dt As DataTable


    Private Sub listadoClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvListado.DefaultCellStyle.ForeColor = Color.Black
        dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        mostrar()
    End Sub

    Private Sub mostrar()
        Try
            Dim func As New fcliente
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

            dv.RowFilter = "'" & cbCampo.Text.ToLower() & "' like '%" & txtBuscar.Text & "%'"

            If (dv.Count) <> 0 Then
                inexistente.Visible = False
                dgvListado.DataSource = dv
                ocultarColumnas()
            Else
                inexistente.Visible = False
                dgvListado.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ocultarColumnas()
        dgvListado.Columns(1).Visible = False
    End Sub

    Public Sub limpiar()
        btnGuardar.Visible = True
        btnEditar.Visible = False
        txtNombre.Text = ""
        txtApellido.Text = ""
        txtDni.Text = ""
        txtDireccion.Text = ""
        txtTelefono.Text = ""
    End Sub



    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        limpiar()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If (Me.ValidateChildren = True And txtNombre.Text <> "" And txtApellido.Text <> "" And txtDni.Text <> "" And txtDireccion.Text <> "") Then
            Try
                Dim dts As New cliente
                Dim func As New fcliente

                dts.gnombre = txtNombre.Text
                dts.gapellido = txtApellido.Text
                dts.gdni = txtDni.Text
                dts.gtelefono = txtTelefono.Text
                dts.gdireccion = txtDireccion.Text
                If (func.insertar(dts)) Then
                    MessageBox.Show("Cliente registrado correctamente.", "Cliente registrado.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()
                Else
                    MessageBox.Show("No se pudo completar el registro, intente de nuevo.", "Cliente no se pudo registrar.", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Me.ErrorProvider.SetError(sender, "Ingrese el nombre del cliente, por favor.")
        End If
    End Sub

    Private Sub txtApellido_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtApellido.Validating
        If (DirectCast(sender, TextBox).Text.Length > 0) Then
            Me.ErrorProvider.SetError(sender, "")
        Else
            Me.ErrorProvider.SetError(sender, "Ingrese el apellido del cliente, por favor.")
        End If
    End Sub

    Private Sub txtDni_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDni.Validating
        If (DirectCast(sender, TextBox).Text.Length > 0) Then
            Me.ErrorProvider.SetError(sender, "")
        Else
            Me.ErrorProvider.SetError(sender, "Ingrese el DNI del cliente, por favor.")
        End If
    End Sub

    Private Sub txtDireccion_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDireccion.Validating
        If (DirectCast(sender, TextBox).Text.Length > 0) Then
            Me.ErrorProvider.SetError(sender, "")
        Else
            Me.ErrorProvider.SetError(sender, "Ingrese la direccion del cliente, por favor.")
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
            idCliente = dgvListado.SelectedCells.Item(1).Value
            txtDni.Text = dgvListado.SelectedCells.Item(2).Value
            txtNombre.Text = dgvListado.SelectedCells.Item(3).Value
            txtApellido.Text = dgvListado.SelectedCells.Item(4).Value
            txtTelefono.Text = dgvListado.SelectedCells.Item(5).Value
            txtDireccion.Text = dgvListado.SelectedCells.Item(6).Value
            btnGuardar.Visible = False
            btnEditar.Visible = True
        End If
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click

        If (Me.ValidateChildren = True And txtNombre.Text <> "" And txtApellido.Text <> "" And txtDni.Text <> "" And txtDireccion.Text <> "") Then
            Try
                Dim result As DialogResult
                result = MessageBox.Show("¿Esta seguro de realizar la modificacion de datos?", "Modificacion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                If (result = Windows.Forms.DialogResult.OK) Then
                    Dim dts As New cliente
                    Dim func As New fcliente
                    dts.gidcliente = idCliente
                    dts.gnombre = txtNombre.Text
                    dts.gapellido = txtApellido.Text
                    dts.gdni = txtDni.Text
                    dts.gtelefono = txtTelefono.Text
                    dts.gdireccion = txtDireccion.Text
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
        Dim result As DialogResult = MessageBox.Show("¿Esta seguro de realizar la eliminacion de datos?", "Eliminacion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In dgvListado.Rows
                    Dim marcado As Boolean = Convert.ToBoolean(row.Cells("Eliminar").Value)

                    If marcado Then
                        Dim onekey As Integer = Convert.ToInt32(row.Cells("ID").Value)
                        Dim vdg As New cliente
                        Dim func As New fcliente
                        vdg.gidcliente = onekey
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

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            mostrar()
            txtBuscar.Text = ""
            cbCampo.SelectedItem = "DNI"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNombre.KeyPress
        Metodos.soloLetras(e)
    End Sub

    Private Sub txtApellido_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApellido.KeyPress
        Metodos.soloLetras(e)
    End Sub

    Private Sub txtDni_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDni.KeyPress
        Metodos.soloNumeros(e)
    End Sub

    Private Sub cbCampo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbCampo.KeyPress
        e.Handled = True
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

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub dgvListado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListado.CellDoubleClick
        If txtFlag.Text = "1" Then
            Inicio.fv.txtIdCli.Text = Convert.ToString(dgvListado.SelectedCells(1).Value)
            Inicio.fv.txtNombreCli.Text = Convert.ToString(dgvListado.SelectedCells(2).Value & " " & dgvListado.SelectedCells(3).Value)
            Me.Close()
        End If
    End Sub
End Class