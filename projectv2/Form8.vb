Public Class setting2
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If CbTheme.SelectedIndex = -1 Then
            MessageBox.Show("Select A Thame")
        ElseIf CbTheme.SelectedIndex = 0 Then
            Panel1.BackColor = Color.Red
            MainMenu.Panel1.BackColor = Color.Red
            addaccounts.Panel1.BackColor = Color.Red
            Transaction.Panel1.BackColor = Color.Red
        ElseIf CbTheme.SelectedIndex = 1 Then
            Panel1.BackColor = Color.Blue
            MainMenu.Panel1.BackColor = Color.Blue
            addaccounts.Panel1.BackColor = Color.Blue
            Transaction.Panel1.BackColor = Color.Blue
        Else
            Panel1.BackColor = Color.Green
            MainMenu.Panel1.BackColor = Color.Green
            addaccounts.Panel1.BackColor = Color.Green
            Transaction.Panel1.BackColor = Color.Green
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        CbTheme.SelectedIndex = -1
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Hide()
    End Sub
End Class