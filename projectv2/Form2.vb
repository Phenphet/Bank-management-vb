Imports System.Data.SqlClient
Public Class login
    Dim cn As New SqlConnection(Module1.sqlcon)
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If cbbRole.SelectedIndex = -1 Then
            MessageBox.Show("คุณต้องเลือกผู้ใช้ก่อนน้ะ")
        ElseIf cbbRole.SelectedIndex = 0 Then
            If txtUsername.Text = "" Or txtpassword.Text = "" Then
                MessageBox.Show("คุณต้องกรอกข้อมูล")
            Else
                cn.Open()
                Dim da As New SqlDataAdapter("select count(*) from AdminTbl where Name = '" & txtUsername.Text & "' and pass = '" & txtpassword.Text & "'", cn)
                Dim dt As New DataTable
                da.Fill(dt)
                If dt.Rows(0)(0).ToString() = 1 Then
                    Agents.Show()
                    Me.Hide()
                    cn.Close()
                    txtUsername.Text = ""
                    txtpassword.Text = ""
                    cbbRole.SelectedIndex = -1
                    cbbRole.Text = "ระบุผู้ใช้"
                Else
                    MessageBox.Show("รหัสผ่านหรือบัญชีผู้ใช้ไม่ถูกต้อง")
                    txtUsername.Text = ""
                    txtpassword.Text = ""
                End If
                cn.Close()
            End If
        Else
            If txtUsername.Text = "" Or txtpassword.Text = "" Then
                MessageBox.Show("คุณต้องกรอกข้อมูล")
            Else
                cn.Open()
                Dim da As New SqlDataAdapter("select count(*) from EmployeeTbl where Name = '" & txtUsername.Text & "' and pass = '" & txtpassword.Text & "'", cn)
                Dim dt As New DataTable
                da.Fill(dt)
                If dt.Rows(0)(0).ToString() = 1 Then
                    MainMenu.Show()
                    Me.Hide()
                    cn.Close()
                    txtUsername.Text = ""
                    txtpassword.Text = ""
                    cbbRole.SelectedIndex = -1
                    cbbRole.Text = "ระบุผู้ใช้"
                Else
                    MessageBox.Show("รหัสผ่านหรือผบัยชีผู้ใช้ไม่ถูกต้อง")
                    txtUsername.Text = ""
                    txtpassword.Text = ""
                End If
                cn.Close()
            End If
        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        txtUsername.Text = ""
        txtpassword.Text = ""
        cbbRole.SelectedIndex = -1
        cbbRole.Text = "ระบุผู้ใช้"
    End Sub


End Class