Imports System.Data.SqlClient
Public Class Transaction
    Dim cn As New SqlConnection(Module1.sqlcon)
    Dim Balance As Integer
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click, PictureBox5.Click
        MainMenu.Show()
        Me.Hide()
    End Sub


    Sub CheckBalance()
        cn.Open()
        Dim Query As String = "select * from AccountTbl Where Id = '" & txtCheckBLN.Text & "'"
        Dim cmd As New SqlCommand(Query, cn)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Dim dr As DataRow
        For Each dr In dt.Rows
            lblBalance.Text = "Th " & dr("Bal").ToString()
            Balance = Convert.ToInt32(dr("Bal").ToString())
        Next
        cn.Close()
    End Sub

    Sub CheckAvailablebBal()
        'cn.Open()
        Dim Query As String = "select * from AccountTbl Where Id = '" & txtFrom.Text & "'"
        Dim cmd As New SqlCommand(Query, cn)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Dim dr As DataRow
        For Each dr In dt.Rows
            lbl2Balance.Text = "Th " & dr("Bal").ToString()
            Balance = Convert.ToInt32(dr("Bal").ToString())
        Next
        'cn.Close()
    End Sub

    Private Sub GetNewBalance(sql As String)
        cn.Open()
        Dim Query As String = "select * from AccountTbl Where Id = '" & sql & "'"
        Dim cmd As New SqlCommand(Query, cn)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Dim dr As DataRow
        For Each dr In dt.Rows
            'lblBalance.Text = "Rs" & dr("Bal").ToString()
            Balance = Convert.ToInt32(dr("Bal").ToString())
        Next
        cn.Close()
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        If txtCheckBLN.Text = " " Then
            MessageBox.Show("Enter Account Number")
        Else
            CheckBalance()
            If lblBalance.Text = "ยอดเงินของคุณ" Then
                MessageBox.Show("Account Not found")
                txtCheckBLN.Text = ""
            End If
        End If

    End Sub

    Sub Deposit()
        Try
            Dim cmd As New SqlCommand("insert into TransactionTbl(Name, Date, Amt, ACID)values(@TN, @TD, @TA, @TAC)", cn)
            cn.Open()
            cmd.Parameters.AddWithValue("@TN", "Deposit")
            cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date)
            cmd.Parameters.AddWithValue("@TA", txtDpAmont.Text)
            cmd.Parameters.AddWithValue("@TAC", txtDpAmont.Text)
            cmd.ExecuteNonQuery()

            cn.Close()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            cn.Close()
        End Try

    End Sub
    Sub Withdraw()
        Try
            Dim cmd As New SqlCommand("insert into TransactionTbl(Name, Date, Amt, ACID)values(@TN, @TD, @TA, @TAC)", cn)
            cn.Open()
            cmd.Parameters.AddWithValue("@TN", "Witgdarw")
            cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date)
            cmd.Parameters.AddWithValue("@TA", txtDpAmont.Text)
            cmd.Parameters.AddWithValue("@TAC", txtWdAmount.Text)
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            cn.Close()
        End Try

    End Sub

    Private Sub btnDeposit_Click(sender As Object, e As EventArgs) Handles btnDeposit.Click
        If txtDpAccount.Text = " " Or txtDpAmont.Text = " " Then
            MessageBox.Show(" ไม่พบข้อมูล ")
        Else
            Deposit()
            GetNewBalance(txtDpAccount.Text)
            Dim newbal = Balance + Convert.ToInt32(txtDpAmont.Text)
            Try
                Dim cmd As New SqlCommand("Update AccountTbl set Bal = @bal where ID =  @b", cn)
                cn.Open()
                cmd.Parameters.AddWithValue("@bal", newbal)
                cmd.Parameters.AddWithValue("@b", txtDpAccount.Text)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Money Deposit")
                cn.Close()
                txtDpAmont.Text = " "
                txtDpAccount.Text = " "
                lblBalance.Text = "ยอดเงินของคุณ"
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                'lblBalance.Text = "YourBalance"
            End Try
        End If
    End Sub

    Private Sub btnWithdraw_Click(sender As Object, e As EventArgs) Handles btnWithdraw.Click
        If txtWdAccount.Text = " " Or txtWdAmount.Text = " " Then
            MessageBox.Show(" ไม่พบข้อมูล ")
        Else
            Withdraw()
            GetNewBalance(txtWdAccount.Text)
            If Balance < Convert.ToInt32(txtWdAmount.Text) Then
                MessageBox.Show("Iinsufisiant Balance")
            Else
                Dim newbal = Balance - Convert.ToInt32(txtWdAmount.Text)
                Try
                    Dim cmd As New SqlCommand("Update AccountTbl set Bal = @bal where ID =  @b", cn)
                    cn.Open()
                    cmd.Parameters.AddWithValue("@bal", newbal)
                    cmd.Parameters.AddWithValue("@b", txtWdAccount.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Money Deposit")
                    cn.Close()
                    txtWdAccount.Text = " "
                    txtWdAmount.Text = " "
                    lblBalance.Text = "ยอดเงินของคุณ"
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    'lblBalance.Text = "YourBalance"
                End Try
            End If
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If txtFrom.Text = " " Then
            MessageBox.Show("Enter Source Account")
        Else
            cn.Open()
            Dim da As New SqlDataAdapter("select count(*) from AccountTbl where ID = '" & txtFrom.Text & "' ", cn)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows(0)(0).ToString() = 1 Then
                CheckAvailablebBal()
                cn.Close()
            Else
                MessageBox.Show("Account Does not Exist")
                txtFrom.Text = " "

            End If
            cn.Close()
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If txtTOO.Text = " " Then
            MessageBox.Show("Enter Destination Account")
        Else
            cn.Open()
            Dim da As New SqlDataAdapter("select count(*) from AccountTbl where ID = '" & txtTOO.Text & "' ", cn)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows(0)(0).ToString() = 1 Then
                'CheckAvailablebBal()
                MessageBox.Show("Account Found")
                cn.Close()
                If txtTOO.Text = txtFrom.Text Then
                    MessageBox.Show("Source And Destination Accounts are same")
                    txtTOO.Text = " "
                End If
            Else
                MessageBox.Show("Account Does not Exist")
                txtTOO.Text = " "

            End If
            cn.Close()
        End If
    End Sub

    Sub transfer()
        Try
            Dim cmd As New SqlCommand("insert into TransferTbl(TrSrc, TrDest, TrAmt, TrDate)values(@Ts, @Td, @TA,@Tda)", cn)
            cn.Open()
            cmd.Parameters.AddWithValue("@Ts", txtFrom.Text)
            cmd.Parameters.AddWithValue("@Td", txtTOO.Text)
            cmd.Parameters.AddWithValue("@TA", txtTRamount.Text)
            cmd.Parameters.AddWithValue("@Tda", DateTime.Now.Date)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Money Transfer")
            cn.Close()
        Catch ex As Exception
            ''MessageBox.Show(ex.Message)
            cn.Close()
        End Try

    End Sub

    Sub Substran()
        GetNewBalance(txtFrom.Text)
        Dim newbal = Balance - Convert.ToInt32(txtTRamount.Text)
        Try
            Dim cmd As New SqlCommand("Update AccountTbl set Bal = @bal where ID =  @b", cn)
            cn.Open()
            cmd.Parameters.AddWithValue("@bal", newbal)
            cmd.Parameters.AddWithValue("@b", txtFrom.Text)
            cmd.ExecuteNonQuery()

            cn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'lblBalance.Text = "YourBalance"
        End Try

    End Sub
    Sub AddBal()
        GetNewBalance(txtTOO.Text)
        Dim newbal = Balance + Convert.ToInt32(txtTRamount.Text)
        Try
            Dim cmd As New SqlCommand("Update AccountTbl set Bal = @bal where ID =  @b", cn)
            cn.Open()
            cmd.Parameters.AddWithValue("@bal", newbal)
            cmd.Parameters.AddWithValue("@b", txtTOO.Text)
            cmd.ExecuteNonQuery()

            cn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'lblBalance.Text = "YourBalance"
        End Try

    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        If txtTOO.Text = "" Or txtFrom.Text = " " Or txtTRamount.Text = " " Then
            MessageBox.Show("Missing Information")
        ElseIf Convert.ToInt16(txtTRamount.Text) > Balance Then
            MessageBox.Show("Insufisiant Balance")
        Else
            transfer()
            Substran()
            AddBal()
            txtFrom.Text = " "
            txtTOO.Text = " "
            txtTRamount.Text = " "
        End If
    End Sub

    Private Sub PictureBox5_Click_1(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Me.Hide()
        MainMenu.Show()
    End Sub
End Class