Imports System.Data.SqlClient
Public Class Agents
    Dim cn As New SqlConnection(Module1.sqlcon)
    Dim key As Integer = 0

    Sub reset()
        txtName.Text = ""
        txtPassword.Text = ""
        txtAddress.Text = ""
        txtPhone.Text = ""

    End Sub

    Sub Display()
        Dim cn As New SqlConnection(Module1.sqlcon)
        cn.Open()
        Dim da As New SqlDataAdapter("select * from EmployeeTbl", cn)
        Dim dt As New DataTable
        da.Fill(dt)
        dvgAgents.DataSource = dt
        cn.Close()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtName.Text = " " Or txtPassword.Text = " " Or txtAddress.Text = " " Or txtPhone.Text = " " Then
            MessageBox.Show(" ไม่พบข้อมูล ")
        Else
            Try
                Dim cmd As New SqlCommand("insert into EmployeeTbl(Name, Pass, phon, Address)values(@n, @pa, @ph, @a)", cn)
                cn.Open()
                cmd.Parameters.AddWithValue("@n", txtName.Text)
                cmd.Parameters.AddWithValue("@pa", txtPassword.Text)
                cmd.Parameters.AddWithValue("@ph", txtPhone.Text)
                cmd.Parameters.AddWithValue("@a", txtAddress.Text)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Employee created")
                Display()
                Reset()
                cn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub


    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click, PictureBox2.Click, PictureBox4.Click
        login.Show()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If key = 0 Then
            MessageBox.Show(" select the Employee ")
        Else
            Try
                Dim cmd As New SqlCommand("Delete from EmployeeTbl where id = @key", cn)
                cn.Open()
                cmd.Parameters.AddWithValue("@key", key)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Employee Delete")
                reset()
                Display()
                cn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub dvgAgents_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dvgAgents.CellContentClick
        txtName.Text = dvgAgents.Rows(0).Cells(1).Value.ToString()
        txtPassword.Text = dvgAgents.Rows(0).Cells(2).Value.ToString
        txtPhone.Text = dvgAgents.Rows(0).Cells(3).Value.ToString()
        txtAddress.Text = dvgAgents.Rows(0).Cells(4).Value.ToString()

        If txtName.Text = " " Then
            key = 0
        Else
            key = Convert.ToInt32(dvgAgents.Rows(0).Cells(0).Value.ToString())
            Display()
        End If
    End Sub

    Private Sub Agents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtName.Text = " " Or txtPassword.Text = " " Or txtAddress.Text = " " Or txtPhone.Text = " " Then
            MessageBox.Show(" ไม่พบข้อมูล Gender and Education")
        Else
            Try
                Dim cmd As New SqlCommand("Update EmployeeTbl set Name = @n, Pass = @pa, phon = @ph, Address = @a", cn)
                cn.Open()
            cmd.Parameters.AddWithValue("@n", txtName.Text)
            cmd.Parameters.AddWithValue("@pa", txtPassword.Text)
            cmd.Parameters.AddWithValue("@ph", txtPhone.Text)
            cmd.Parameters.AddWithValue("@a", txtAddress.Text)
            cmd.ExecuteNonQuery()
                MessageBox.Show("Employee Update")
                Display()
                reset()
                cn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Settings.Show()
    End Sub

    Private Sub PictureBox4_Click_1(sender As Object, e As EventArgs) Handles PictureBox4.Click
        login.Show()
        Me.Hide()
    End Sub
End Class