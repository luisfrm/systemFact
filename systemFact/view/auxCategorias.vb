Public Class auxCategorias
    Private ex, ey As Integer
    Private arrastre As Boolean
    Private dt As DataTable
    Private Sub txtCerrar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub mostrar()
        Try
            Dim func As New fcategoria
            dt = func.mostrar()
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
    End Sub


    Private Sub listadoClientes_MouseDown(sender As Object, e As MouseEventArgs)
        ex = e.X
        ey = e.Y
        arrastre = True
    End Sub

    Private Sub auxCategoria_MouseUp(sender As Object, e As MouseEventArgs)
        arrastre = False
    End Sub

    Private Sub auxCategoria_MouseMove(sender As Object, e As MouseEventArgs)
        If arrastre Then
            Me.Location = Me.PointToScreen(New Point(MousePosition.X - Me.Location.X - ex, MousePosition.Y - Me.Location.Y - ey))
        End If
    End Sub

    Private Sub auxCategoria_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        Inicio.fh.txtIdCat.Text = dgvListado.SelectedCells(0).Value
        Inicio.fh.txtNombreCat.Text = dgvListado.SelectedCells(1).Value
        Me.Close()
    End Sub


    Private Sub btnCerrar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnCerrar_MouseEnter(sender As Object, e As EventArgs) Handles btnCerrar.MouseEnter
        btnCerrar.Image = My.Resources.Close_Windows_Over
    End Sub

    Private Sub btnCerrar_MouseLeave(sender As Object, e As EventArgs) Handles btnCerrar.MouseLeave
        btnCerrar.Image = My.Resources.Close_Windows_Out
    End Sub

    Private Sub auxCategorias_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        ex = e.X
        ey = e.Y
        arrastre = True
    End Sub
End Class