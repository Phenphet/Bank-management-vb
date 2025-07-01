Imports System.Data.SqlClient
Public Class Settings
    Dim cn As New SqlConnection(Module1.sqlcon)
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click, Label1.Click
        txtNewpassword.Text = " "
        CbTheme.SelectedIndex = -1
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If CbTheme.SelectedIndex = -1 Then
            MessageBox.Show("Select A Thame")
        ElseIf CbTheme.SelectedIndex = 0 Then
            Panel1.BackColor = Color.Red
            Agents.Panel1.BackColor = Color.Red
        ElseIf CbTheme.SelectedIndex = 1 Then
            Panel1.BackColor = Color.Blue
            Agents.Panel1.BackColor = Color.Blue
        Else
            Panel1.BackColor = Color.Green
            Agents.Panel1.BackColor = Color.Green
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtNewpassword.Text = " " Then
            MessageBox.Show(" ไม่พบข้อมูล Gender and Education")
        Else
            Try
                Dim cmd As New SqlCommand("Update AdminTbl set pass = @p where id = @key", cn)
                cn.Open()
                cmd.Parameters.AddWithValue("@p", txtNewpassword.Text)
                cmd.Parameters.AddWithValue("@key", 1)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Password Update")
                cn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub


End Class