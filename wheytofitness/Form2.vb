Imports System.Data.SqlClient
Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim connection As New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\wheytofitness\Database1.mdf;Integrated Security=True")
        Dim command As New SqlCommand("select * from login where name = @name and password = @password", connection)
        command.Parameters.Add("@name", SqlDbType.VarChar).Value = TextBox1.Text
        command.Parameters.Add("@password", SqlDbType.VarChar).Value = TextBox2.Text
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        If table.Rows.Count() <= 0 Then
            MessageBox.Show("Username Or Password are invalid")
        Else
            l = 1
            username = TextBox1.Text
            MessageBox.Show("hello " & username)
            connection.Open()
            Dim cmd As New SqlCommand
            cmd.CommandText = "select gender,profile_created from login where name = '" & username & "'"
            cmd.Connection = connection
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader()
            Do While dr1.Read()
                gender = dr1("gender")
                profile_created = dr1("profile_created")
            Loop
            connection.Close()
            'MessageBox.Show(gender)
            Me.Hide()
            Form1.initial()
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub
End Class

Public Module globalvar
    Public l As Integer = 0
    Public forprofile As Integer = 0
    Public username, gender As String
    Public initial_weight, fat_percentage, c_weight, goal_weight, age, p_id As Integer
    Public dno, diff As Integer
    Public activity_level As String
    Public profile_created As Boolean
    Public heightt, tdee, bmr1, bmr2, bmi, met As Double
    Public today, bday, update_d, update_d1 As Date
    Public connection As New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\wheytofitness\Database1.mdf;Integrated Security=True")
End Module