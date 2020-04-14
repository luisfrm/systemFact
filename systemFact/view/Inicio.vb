Public Class Inicio
    Dim ex, ey As Integer
    Dim arrastre As Boolean
    Public fh As listadoProductos
    Public fv As frmventa
    Public fdv As frmdetalle


    Private Sub btnReportes_Click(sender As Object, e As EventArgs) Handles btnReportes.Click
        If pnlReporte.Visible Then
            pnlReporte.Visible = False
        Else
            pnlReporte.Visible = True
        End If
    End Sub

    Private Sub Inicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblHora.Text = DateAndTime()
        btnMaximize.Visible = True
        btnRestore.Visible = False
    End Sub

    Private Sub abrirFormProductos(formHija As Object)
        If (Me.panelContenedor.Controls.Count > 0) Then
            Me.panelContenedor.Controls.RemoveAt(0)
        End If
        fh = formHija
        fh.TopLevel = False
        fh.Dock = DockStyle.Fill
        Me.panelContenedor.Controls.Add(fh)
        Me.panelContenedor.Tag = fh
        fh.Show()
    End Sub

    Private Sub abrirFormHija(formHija As Object)
        Dim formh As Form
        If (Me.panelContenedor.Controls.Count > 0) Then
            Me.panelContenedor.Controls.RemoveAt(0)
        End If
        formh = formHija
        formh.TopLevel = False
        formh.Dock = DockStyle.Fill
        Me.panelContenedor.Controls.Add(formh)
        Me.panelContenedor.Tag = formh
        formh.Show()
    End Sub

    Private Sub abrirFormVenta(formHija As Object)
        If (Me.panelContenedor.Controls.Count > 0) Then
            Me.panelContenedor.Controls.RemoveAt(0)
        End If
        fv = formHija
        fv.TopLevel = False
        fv.Dock = DockStyle.Fill
        Me.panelContenedor.Controls.Add(fv)
        Me.panelContenedor.Tag = fv
        fv.Show()
    End Sub

    Private Sub btnProductos_Click(sender As Object, e As EventArgs) Handles btnProductos.Click
        abrirFormProductos(New listadoProductos())
    End Sub

    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
        abrirFormHija(New listadoClientes())
    End Sub

    Private Sub btnCategorias_Click(sender As Object, e As EventArgs) Handles btnCategorias.Click
        abrirFormHija(New listadoCategorias())
    End Sub

    Private Sub btnVentas_Click(sender As Object, e As EventArgs) Handles btnVentas.Click
        abrirFormVenta(New frmventa)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblHora.Text = DateAndTime()
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
        Me.Hide()
        Login.Show()
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

    Private Sub btnCerrar_MouseEnter(sender As Object, e As EventArgs) Handles btnCerrar.MouseEnter
        btnCerrar.Image = My.Resources.Close_Windows_Over
    End Sub

    Private Sub btnCerrar_MouseLeave(sender As Object, e As EventArgs) Handles btnCerrar.MouseLeave
        btnCerrar.Image = My.Resources.Close_Windows_Out
    End Sub

    Private Sub btnRestore_MouseEnter(sender As Object, e As EventArgs) Handles btnRestore.MouseEnter
        btnRestore.Image = My.Resources.Windows_Restore_Over
    End Sub

    Private Sub btnRestore_MouseLeave(sender As Object, e As EventArgs) Handles btnRestore.MouseLeave
        btnRestore.Image = My.Resources.Windows_Restore_Out
    End Sub

    Private Sub btnMaximize_MouseEnter(sender As Object, e As EventArgs) Handles btnMaximize.MouseEnter
        btnMaximize.Image = My.Resources.Maximizar_windows_Over
    End Sub

    Private Sub btnMaximize_MouseLeave(sender As Object, e As EventArgs) Handles btnMaximize.MouseLeave
        btnMaximize.Image = My.Resources.Maximizar_windows_out
    End Sub

    Private Sub btnMinimizar_MouseEnter(sender As Object, e As EventArgs) Handles btnMinimizar.MouseEnter
        btnMinimizar.Image = My.Resources.Windows_Minimize_Over
    End Sub

    Private Sub btnMinimizar_MouseLeave(sender As Object, e As EventArgs) Handles btnMinimizar.MouseLeave
        btnMinimizar.Image = My.Resources.Windows_Minimize_Out
    End Sub

   
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class