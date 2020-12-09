Imports System.Data.SqlClient
Public Class Form5
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label4.Text = username
        Dim dr1 As SqlDataReader
       
        'MessageBox.Show("profile_created :" & profile_created)
        connection.Close()
        If (profile_created = True) Then
            Dim command As New SqlCommand("select name,p_id,current_w,goal_w,initial_w,height,activity_level,tdee,age,bmi,fat,update_d,update_d1 from profile where name = @name", connection)
            connection.Open()
            Dim parametersprofile As New SqlParameter("@name", SqlDbType.VarChar)
            parametersprofile.Direction = ParameterDirection.Input
            parametersprofile.Value = username
            command.Parameters.Add(parametersprofile)
            dr1 = command.ExecuteReader()
            Do While dr1.Read()
                TextBox2.Text = dr1("p_id")
                p_id = TextBox2.Text
                TextBox3.Text = dr1("current_w")
                c_weight = TextBox3.Text
                TextBox4.Text = dr1("goal_w")
                goal_weight = TextBox4.Text
                initial_weight = dr1("initial_w")
                activity_level = dr1("activity_level")
                tdee = dr1("tdee")
                TextBox1.Text = tdee
                age = dr1("age")
                bmi = dr1("bmi")
                fat_percentage = dr1("fat")
                update_d = dr1("update_d")
                update_d1 = dr1("update_d1")
            Loop
            dr1.Close()
            connection.Close()
            Dim dr2 As SqlDataReader
            connection.Open()
            Dim command11 As New SqlCommand("select plan_name from planprofile where p_id='" & TextBox2.Text & "'", connection)
            dr2 = command11.ExecuteReader
            Do While dr2.Read
                TextBox2.Text = dr2("plan_name")
            Loop
            dr2.Close()
            Dim dr3 As SqlDataReader
            command11.Connection = connection
            command11.CommandText = "select gender from login where name = '" & username & "'"
            dr3 = command11.ExecuteReader
            Do While dr3.Read
                gender = dr3("gender")
            Loop
            dr3.Close()
            connection.Close()
            Progress()
            'Else
            '   MessageBox.Show("no profile")
        End If
    End Sub
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If (profile_created = False) Then
            profiled.Show()
            Me.Close()
        Else
            MessageBox.Show(username + " you have already created a profile. Instead you can update your existing profile")
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        connection.Open()
        If (profile_created = True) Then
            If (MessageBox.Show("Do you really want to delete your profile?", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
                MessageBox.Show("operation canceled", "profile")
            Else
                Dim command As New SqlCommand("delete from profile where name = '" & username & "'", connection)
                Dim i As Integer = command.ExecuteNonQuery()
                If (i > 0) Then
                    MessageBox.Show("your profile has been deleted", "profile")
                End If
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox1.Text = ""
                profile_created = 0
                command.CommandText = "update login set profile_created = '" & profile_created & "' where name = '" & username & "'"
                command.Connection = connection
                command.ExecuteNonQuery()
            End If
        Else
            MessageBox.Show(username & " there is no existing profile ", "delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        connection.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (profile_created = True) Then
            today = Date.Today
            Dim days As TimeSpan = today.Subtract(update_d1)
            dno = days.TotalDays
            If (dno < 7) Then
                MessageBox.Show("update your weight on a weekly basis ")
            Else
                TextBox3.ReadOnly = False
                MessageBox.Show("enter your current weight to update progress", "current weight", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Button4.Visible = True
            End If
        Else
            MessageBox.Show("you have to create a profile", "profile", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        update_d1 = today
        If (TextBox2.Text = "weight loss") Then
            If (TextBox3.Text > c_weight) Then
                MessageBox.Show("you have gained weight. please work towards your goal weight", "current weight", MessageBoxButtons.OK, MessageBoxIcon.Information)
                diff = 1
                update_query()
            ElseIf (dno < 14 And (c_weight - TextBox3.Text) > 4) Then
                MessageBox.Show("accelerated weight loss not recommended. gradual weight loss is healthy", "current weight", MessageBoxButtons.OK, MessageBoxIcon.Information)
                diff = 2
                update_query()
            Else
                diff = 2
                update_query()
            End If
            progress()
            If (c_weight <= goal_weight) Then
                progress()
                If ((MessageBox.Show("congratulations " & username & " you have reached your goal. please create a new fitness profile", "profile", MessageBoxButtons.OK, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK)) Then
                    Dim command As New SqlCommand("delete from profile where profile.name = '" & username & "'", connection)
                    connection.Open()
                    command.ExecuteNonQuery()
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox1.Text = ""
                    profile_created = 0
                    command.CommandText = "update login set profile_created = '" & profile_created & "' where name = '" & username & "'"
                    command.Connection = connection
                    command.ExecuteNonQuery()
                    connection.Close()
                    profiled.Show()
                    Me.Close()
                End If
            End If
        ElseIf (TextBox2.Text = "weight gain") Then
            If (TextBox3.Text < c_weight) Then
                MessageBox.Show("you have lost weight. please work towards your goal weight", "current weight", MessageBoxButtons.OK, MessageBoxIcon.Information)
                diff = 1
                update_query()
            ElseIf (dno < 14 And (TextBox3.Text - c_weight) > 4) Then
                MessageBox.Show("accelerated weight gain not recommended. muscle gain is healthy", "current weight", MessageBoxButtons.OK, MessageBoxIcon.Information)
                diff = 2
                update_query()
            Else
                diff = 2
                update_query()
            End If
            progress()
            If (c_weight >= goal_weight) Then
                progress()
                If ((MessageBox.Show("congratulations " & username & " you have reached your goal. please create a new fitness profile", "profile", MessageBoxButtons.OK, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK)) Then
                    Dim command As New SqlCommand("delete from profile where profile.name = '" & username & "'", connection)
                    connection.Open()
                    command.ExecuteNonQuery()
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox1.Text = ""
                    profile_created = 0
                    command.CommandText = "update login set profile_created = '" & profile_created & "' where name = '" & username & "'"
                    command.Connection = connection
                    command.ExecuteNonQuery()
                    connection.Close()
                    profiled.Show()
                    Me.Close()
                End If
            End If
        End If
    End Sub
    Public Sub update_query()
        connection.Open()
        Dim cmd As New SqlCommand
        If (diff = 1) Then
            cmd.CommandText = "update profile set current_w = " & Val(TextBox3.Text) & ",initial_w = " & Val(TextBox3.Text) & ",update_d1 = '" & update_d1 & "' where name = '" & username & "'"
            c_weight = Val(TextBox3.Text)
            initial_weight = c_weight
            progress()
        ElseIf (diff = 2) Then
            cmd.CommandText = "update profile set current_w = " & Val(TextBox3.Text) & ",update_d1 = '" & update_d1 & "' where name = '" & username & "'"
            c_weight = Val(TextBox3.Text)
            progress()
        End If
        cmd.Connection = connection
        cmd.ExecuteNonQuery()
        MessageBox.Show("succesful updation")
        connection.Close()
        Form1.initial()
    End Sub
    Public Sub progress()
        Dim diff1, prog As Double
        If (p_id = 1) Then
            diff1 = initial_weight - goal_weight
            prog = c_weight - goal_weight
            prog = ((diff1 - prog) / diff1) * 100
            ProgressBar1.Maximum = 100
            ProgressBar1.Minimum = 0
            ProgressBar1.Value = prog
        ElseIf (p_id = 2) Then
            diff1 = goal_weight - initial_weight
            prog = goal_weight - c_weight
            prog = ((diff1 - prog) / diff1) * 100
            ProgressBar1.Maximum = 100
            ProgressBar1.Minimum = 0
            ProgressBar1.Value = prog
        End If
    End Sub
End Class