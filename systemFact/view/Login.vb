
Public Class Login

    Public dts As usuario
    Private Sub txtUser_Enter(sender As Object, e As EventArgs) Handles txtUser.Enter
        If (txtUser.Text = "Usuario") Then
            txtUser.Text = ""
        End If


    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Hide()
        Application.Exit()
    End Sub


    Private Sub btnMinimizar_Click(sender As Object, e As EventArgs) Handles btnMinimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnCerrar_MouseEnter(sender As Object, e As EventArgs) Handles btnCerrar.MouseEnter
        btnCerrar.Image = My.Resources.Close_Windows_Over
    End Sub

    Private Sub btnCerrar_MouseLeave(sender As Object, e As EventArgs) Handles btnCerrar.MouseLeave
        btnCerrar.Image = My.Resources.Close_Windows_Out
    End Sub


    Private Sub btnMinimizar_MouseEnter(sender As Object, e As EventArgs) Handles btnMinimizar.MouseEnter
        btnMinimizar.Image = My.Resources.Windows_Minimize_Over
    End Sub

    Private Sub btnMinimizar_MouseLeave(sender As Object, e As EventArgs) Handles btnMinimizar.MouseLeave
        btnMinimizar.Image = My.Resources.Windows_Minimize_Out
    End Sub

    Private Sub txtUser_Leave(sender As Object, e As EventArgs) Handles txtUser.Leave
        If (txtUser.Text = "") Then
            txtUser.Text = "Usuario"
        End If
    End Sub

    Private Sub txtPass_Enter(sender As Object, e As EventArgs) Handles txtPass.Enter
        If (txtPass.Text = "Contraseña") Then
            txtPass.Text = ""
            txtPass.UseSystemPasswordChar = True
        End If
    End Sub



    Private Sub txtPass_Leave(sender As Object, e As EventArgs) Handles txtPass.Leave
        If (txtPass.Text = "") Then
            txtPass.Text = "Contraseña"
            txtPass.UseSystemPasswordChar = False
        End If
    End Sub

    Private Sub linkPass_MouseEnter(sender As Object, e As EventArgs) Handles linkPass.MouseEnter
        linkPass.LinkColor = Color.Aquamarine
    End Sub

    Private Sub linkPass_MouseLeave(sender As Object, e As EventArgs) Handles linkPass.MouseLeave
        linkPass.LinkColor = Color.MediumAquamarine
    End Sub

    Private Sub limpiar()
        txtUser.Text = "Usuario"
        txtPass.Text = "Contraseña"
        txtPass.UseSystemPasswordChar = False
    End Sub

    Private Sub btnAcceder_Click(sender As Object, e As EventArgs) Handles btnAcceder.Click
        Try
            dts = New usuario
            Dim func As New fusuario
            dts.guser = txtUser.Text
            dts.gpass = txtPass.Text
            If (func.selec(dts)) Then
                MessageBox.Show("Bienvenido Sr/a " & dts.guser, "Inicio de sesion exitoso.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Inicio.lblUser.Text = dts.guser
                limpiar()
                Me.Hide()
                Inicio.Show()
            Else
                MessageBox.Show("Usuario y/o contraseña invalidos.", "Error al iniciar sesion.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblHora.Text = DateAndTime()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblHora.Text = DateAndTime()
    End Sub

    Public Function DateAndTime() As String
        Dim fecha
        Dim day$, dia$, mes$, year$, hora$

        Select Case Now.DayOfWeek
            Case 0 : day = "Domingo"
            Case 1 : day = "Lunes"
            Case 2 : day = "Martes"
            Case 3 : day = "Miercoles"
            Case 4 : day = "Jueves"
            Case 5 : day = "Viernes"
            Case 6 : day = "Sabado"
            Case Else
                day = Nothing
        End Select

        Select Case Now.Month
            Case 1 : mes = "Enero"
            Case 2 : mes = "Febrero"
            Case 3 : mes = "Marzo"
            Case 4 : mes = "Abril"
            Case 5 : mes = "Mayo"
            Case 6 : mes = "Junio"
            Case 7 : mes = "Julio"
            Case 8 : mes = "Agosto"
            Case 9 : mes = "Septiembre"
            Case 10 : mes = "Octubre"
            Case 11 : mes = "Noviembre"
            Case 12 : mes = "Diciembre"
            Case Else
                mes = Nothing
        End Select

        dia = Now.Day
        year = Now.Year
        hora = TimeOfDay
        fecha = day & ", " & dia & " de " & mes & " del " & year & vbLf & hora
        Return fecha
    End Function

    Private Sub txtPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPass.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            btnAcceder_Click(Nothing, e)
        End If
    End Sub

End Class
