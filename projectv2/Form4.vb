Imports System.Data.SqlClient
Public Class addaccounts
    Dim key As Integer = 0
    Dim cn As New SqlConnection(Module1.sqlcon)

    Sub Display()
        Dim cn As New SqlConnection(Module1.sqlcon)
        cn.Open()
        Dim da As New SqlDataAdapter("select * from AccountTbl", cn)
        Dim dt As New DataTable
        da.Fill(dt)
        dvgAccount.DataSource = dt
        cn.Close()
    End Sub

    Sub reset()
        txtName.Text = ""
        txtPhon.Text = ""
        txtAddress.Text = ""
        cbbGender.Text = "ระบุเพศ"
        cbbEducation.Text = "ระบุการศึกษา"
        cbbGender.SelectedIndex = -1
        cbbEducation.SelectedIndex = -1
        txtOccupation.Clear()
        txtIncom.Text = ""
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtName.Text = " " Or txtPhon.Text = " " Or txtAddress.Text = " " Or txtIncom.Text = " " Or txtOccupation.Text = " " Or cbbEducation.SelectedIndex = -1 Or cbbGender.SelectedIndex = -1 Then
            MessageBox.Show(" ไม่พบข้อมูล ")
        Else
            Try
                Dim cmd As New SqlCommand("insert into AccountTbl(Name, Phon, Address, Gen, Occup, Educ, Inc, Bal)values(@n, @p, @a, @g, @o, @e, @i, @b)", cn)
                cn.Open()
                cmd.Parameters.AddWithValue("@n", txtName.Text)
                cmd.Parameters.AddWithValue("@p", txtPhon.Text)
                cmd.Parameters.AddWithValue("@a", txtAddress.Text)
                cmd.Parameters.AddWithValue("@g", cbbGender.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@o", txtOccupation.Text)
                cmd.Parameters.AddWithValue("@e", cbbEducation.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@i", txtIncom.Text)
                cmd.Parameters.AddWithValue("@b", 0)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Account created")
                Display()
                reset()
                cn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If key = 0 Then
            MessageBox.Show(" select the Account ")
        Else
            Try
                Dim cmd As New SqlCommand("Delete from AccountTbl where id = @key", cn)
                cn.Open()
                cmd.Parameters.AddWithValue("@key", key)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Account Delete")
                reset()
                Display()
                cn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub dvgAccount_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dvgAccount.CellContentClick
        txtName.Text = dvgAccount.Rows(0).Cells(1).Value.ToString()
        txtPhon.Text = dvgAccount.Rows(0).Cells(2).Value.ToString()
        txtAddress.Text = dvgAccount.Rows(0).Cells(3).Value.ToString()
        cbbGender.SelectedItem = dvgAccount.Rows(0).Cells(4).Value.ToString()
        txtOccupation.Text = dvgAccount.Rows(0).Cells(5).Value.ToString()
        cbbEducation.SelectedItem = dvgAccount.Rows(0).Cells(6).Value.ToString()
        txtIncom.Text = dvgAccount.Rows(0).Cells(7).Value.ToString()
        If txtName.Text = " " Then
            key = 0
        Else
            key = Convert.ToInt32(dvgAccount.Rows(0).Cells(0).Value.ToString())
            Display()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtName.Text = " " Or txtPhon.Text = " " Or txtAddress.Text = " " Or txtIncom.Text = " " Or txtOccupation.Text = " " Or cbbEducation.SelectedIndex = -1 Or cbbGender.SelectedIndex = -1 Then
            MessageBox.Show(" ไม่พบข้อมูล Gender and Education")
        Else
            Try
                Dim cmd As New SqlCommand("Update AccountTbl set Name = @n, Phon= @p, Address = @a, Gen = @g, Occup = @o, Educ = @e, Inc = @i, Bal = @b", cn)
                cn.Open()
                cmd.Parameters.AddWithValue("@n", txtName.Text)
                cmd.Parameters.AddWithValue("@p", txtPhon.Text)
                cmd.Parameters.AddWithValue("@a", txtAddress.Text)
                cmd.Parameters.AddWithValue("@g", cbbGender.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@o", txtOccupation.Text)
                cmd.Parameters.AddWithValue("@e", cbbEducation.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@i", txtIncom.Text)
                cmd.Parameters.AddWithValue("@b", 0)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Account Update")
                Display()
                reset()
                cn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub addaccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Me.Hide()
        MainMenu.Show()
    End Sub
End Class