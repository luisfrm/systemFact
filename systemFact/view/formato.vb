Public Class formato
    Dim ex, ey As Integer, arrastre As Boolean
    
    Private Sub formato_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnRestore.Visible = False
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        Me.WindowState = FormWindowState.Normal
        btnRestore.Visible = False
        btnMaximize.Visible = True
    End Sub

   
    Private Sub btnMinimizar_Click(sender As Object, e As EventArgs) Handles btnMinimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
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

    Private Sub listadoClientes_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlPrincipal.MouseDown
        ex = e.X
        ey = e.Y
        arrastre = True
    End Sub

    Private Sub listadoClientes_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlPrincipal.MouseUp
        arrastre = False
    End Sub

    Private Sub listadoClientes_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlPrincipal.MouseMove
        If arrastre Then
            Me.Location = Me.PointToScreen(New Point(MousePosition.X - Me.Location.X - ex, MousePosition.Y - Me.Location.Y - ey))
        End If
    End Sub

End Class