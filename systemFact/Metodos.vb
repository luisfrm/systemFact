Imports System.Text.RegularExpressions
Module Metodos
    'Author  LuisRivas
    'Version 1.2

    'Funcion que valida que un email sea valido.
    Private Function validar_Mail(ByVal sMail As String) As Boolean
        ' retorna true o false   
        Return Regex.IsMatch(sMail, _
                             "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")
    End Function

    'Metodo que solo permite escribir letras en un componente.
    Public Sub soloLetras(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = False
        End If

    End Sub

    'Metodo que solo permite escribir numeros en un componente.
    Public Sub soloNumeros(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    'Funcion que recibe dos numeros y retorna uno aleatorio en ese rango.
    Function numAleatorio(ByVal minimo As Integer, ByVal maximo As Integer) As Integer
        Randomize()
        Return CLng((minimo - maximo) * Rnd() + maximo)
    End Function

    Public Function calcularEdad(ByVal nacimiento As Date) As Integer
        Dim edad As Integer
        edad = Today.Year - nacimiento.Year
        If (nacimiento > Today.AddYears(-edad)) Then edad -= 1
        If edad = -1 Then
            edad = 0
        End If
        Return edad
    End Function

    'Funcion que retorna un string con la fecha y hora actual.
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

End Module
