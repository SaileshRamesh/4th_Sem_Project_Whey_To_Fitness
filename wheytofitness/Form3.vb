Imports System.Data.SqlClient
Public Class Form3
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim cmd1 As SqlCommand
    Dim dr3 As SqlDataReader
    Dim username_exists As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Or TextBox4.Text = Nothing Or RadioButton1.Checked = False And RadioButton2.Checked = False) Then
            MessageBox.Show("sign up fields are empty", "sign up", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim constring As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\wheytofitness\Database1.mdf;Integrated Security=True"
            con = New SqlConnection(constring)
            con.Open()
            Dim cmd1 As New SqlCommand("select name from login where name = '" & TextBox1.Text & "'", con)
            dr3 = cmd1.ExecuteReader
            Do While dr3.Read
                username_exists = dr3("name")
            Loop
            dr3.Close()
            If (username_exists = TextBox1.Text) Then
                MessageBox.Show("username already exists", "sign up", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If (IsValidEmail(TextBox3.Text) = False) Then
                    MessageBox.Show("invalid Email id", "sign up")
                ElseIf (validpassword(TextBox4.Text) <> False) Then
                    cmd.Connection = con
                    cmd.CommandText = "INSERT INTO login(name,dob,email_id,password,gender,profile_created) VALUES(@name,@dob,@email_id,@password,@gender,@profile_created)"
                    Dim paramname As New SqlParameter("@name", SqlDbType.VarChar, 15)
                    paramname.Value = TextBox1.Text
                    'bday = Convert.ToDateTime(bday)
                    'TextBox2.Text = Convert.ToDateTime(TextBox2.Text)
                    bday = TextBox2.Text
                    Dim paramdob As New SqlParameter("@dob", SqlDbType.Date)
                    paramdob.Value = bday
                    Dim paramemail_id As New SqlParameter("@email_id", SqlDbType.VarChar, 50)
                    paramemail_id.Value = TextBox3.Text
                    Dim parampassword As New SqlParameter("@password", SqlDbType.VarChar, 50)
                    parampassword.Value = TextBox4.Text
                    Dim paramgender As New SqlParameter("@gender", SqlDbType.VarChar, 50)
                    If (RadioButton1.Checked = True) Then
                        paramgender.Value = "male"
                    Else
                        paramgender.Value = "female"
                    End If
                    Dim paramprofile_created As New SqlParameter("@profile_created", SqlDbType.Bit)
                    paramprofile_created.Value = False
                    cmd.Parameters.Add(paramname)
                    cmd.Parameters.Add(paramdob)
                    cmd.Parameters.Add(paramemail_id)
                    cmd.Parameters.Add(parampassword)
                    cmd.Parameters.Add(paramgender)
                    cmd.Parameters.Add(paramprofile_created)
                    Dim da As New SqlDataAdapter
                    da.InsertCommand = cmd
                    da.InsertCommand.ExecuteNonQuery()
                    MessageBox.Show("created Sucessfully", "sign up", MessageBoxButtons.OK)
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    RadioButton1.Checked = False
                    RadioButton1.Checked = True
                    Me.Hide()
                    Form2.Show()
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
        Me.Hide()
    End Sub
    Function IsValidEmail(ByVal s As String) As Boolean
        Try
            Dim a As New System.Net.Mail.MailAddress(s)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Function validpassword(ByVal s As String) As Boolean
        Const max_length = 15, min_length = 8
        Dim l, up, low, dig As Boolean
        l = False
        up = False
        low = False
        dig = False
        If (s.Length() < min_length Or s.Length() > max_length) Then
            l = False
            MessageBox.Show("password should be of length 8-15", "sign up", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            l = True
            For Each c As Char In s
                If (Char.IsLower(c)) Then
                    low = True
                ElseIf (Char.IsUpper(c)) Then
                    up = True
                ElseIf (Char.IsDigit(c)) Then
                    dig = True
                End If
            Next
        End If
        If (l And up And low And dig) Then
            Return True
        Else
            MessageBox.Show("password should contain uppercase,lowercase letters and digits", "sign up", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
    End Function
End Class
