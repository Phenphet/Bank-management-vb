Public Class Form1
    Dim start As Integer = 0
    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(3)
        If ProgressBar1.Value = 100 Then
            ProgressBar1.Value = 0
            Timer1.Stop()
            login.Show()
            Me.Hide()

        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub


End Class
