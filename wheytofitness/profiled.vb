Imports System.Data.SqlClient
Public Class profiled
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Public constring As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Or ComboBox1.SelectedItem = Nothing Or TextBox4.Text = Nothing Or Button3.Text = "select activity level ") Then
            MessageBox.Show("enter all details correctly")
        Else
            If (TextBox4.Text < 1 Or TextBox4.Text > 2.8) Then
                MessageBox.Show("invalid height", "profile", MessageBoxButtons.OK)
            ElseIf (ComboBox1.SelectedItem = "weight loss" And TextBox1.Text < TextBox2.Text) Then
                MessageBox.Show("you current weight is less than your goal weight please check again as you have chosen weight loss", "profile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf (ComboBox1.SelectedItem = "weight gain" And TextBox1.Text > TextBox2.Text) Then
                MessageBox.Show("you current weight is greater than your goal weight please check again as you have chosen weight gain", "profile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf (TextBox1.Text <> TextBox3.Text) Then
                MessageBox.Show("current weight and initial weight should be same as your are starting your fitness journey")
            ElseIf (TextBox1.Text = TextBox2.Text) Then
                MessageBox.Show("current weight and goal weight cant be same", "profile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                constring = "Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\wheytofitness\Database1.mdf;Integrated Security=True"
                con = New SqlConnection(constring)
                con.Open()
                cmd.Connection = con
                cmd.Parameters.Clear()
                cmd.CommandText = "INSERT INTO profile(name,p_id,current_w,goal_w,initial_w,height,activity_level,tdee,age,bmi,fat,update_d,update_d1) VALUES(@name,@p_id,@current_w,@goal_w,@initial_w,@height,@activity_level,@tdee,@age,@bmi,@fat,@update_d,@update_d1)"
                Dim paramname As New SqlParameter("@name", SqlDbType.VarChar, 50)
                paramname.Value = username
                Dim paramplan As New SqlParameter("@p_id", SqlDbType.Int)
                If (ComboBox1.SelectedItem = "weight loss") Then
                    paramplan.Value = 1
                Else
                    paramplan.Value = 2
                End If
                Dim paramc_w As New SqlParameter("@current_w", SqlDbType.Int)
                paramc_w.Value = TextBox1.Text
                c_weight = TextBox1.Text
                Dim goal_w As New SqlParameter("@goal_w", SqlDbType.Int)
                goal_w.Value = TextBox2.Text
                goal_weight = TextBox2.Text
                Dim initial_w As New SqlParameter("@initial_w", SqlDbType.Int)
                initial_w.Value = TextBox3.Text
                initial_weight = TextBox3.Text
                Dim paramheight As New SqlParameter("@height", SqlDbType.Real)
                paramheight.Value = TextBox4.Text
                heightt = TextBox4.Text
                Dim paramactivity As New SqlParameter("@activity_level", SqlDbType.VarChar, 50)
                paramactivity.Value = Button3.Text
                activity_level = Button3.Text
                Dim paramage As New SqlParameter("@age", SqlDbType.Int)
                agecalculator()
                paramage.Value = age
                forprofile = 1
                Dim parambmi As New SqlParameter("@bmi", SqlDbType.Real)
                Form6.bmical(heightt, initial_weight)
                parambmi.Value = bmi
                Dim paramfat As New SqlParameter("@fat", SqlDbType.Real)
                Form6.fat_percent(age, gender, bmi)
                paramfat.Value = fat_percentage
                Dim paramtdee As New SqlParameter("@tdee", SqlDbType.Int)
                TDEEcalculator()
                paramtdee.Value = tdee
                Dim paramupdate_d As New SqlParameter("@update_d", SqlDbType.Date)
                paramupdate_d.Value = Date.Today
                Dim paramupdate_d1 As New SqlParameter("@update_d1", SqlDbType.Date)
                paramupdate_d1.Value = Date.Today
                cmd.Parameters.Add(paramname)
                cmd.Parameters.Add(paramplan)
                cmd.Parameters.Add(paramc_w)
                cmd.Parameters.Add(goal_w)
                cmd.Parameters.Add(initial_w)
                cmd.Parameters.Add(paramheight)
                cmd.Parameters.Add(paramactivity)
                cmd.Parameters.Add(paramtdee)
                cmd.Parameters.Add(paramage)
                cmd.Parameters.Add(parambmi)
                cmd.Parameters.Add(paramfat)
                cmd.Parameters.Add(paramupdate_d)
                cmd.Parameters.Add(paramupdate_d1)
                Dim da As New SqlDataAdapter
                da.InsertCommand = cmd
                da.InsertCommand.ExecuteNonQuery()
                MessageBox.Show("profile created Sucessfully", "wheytofitness")
                profile_initial()
                Me.Hide()
                profile_created = True
                connection.Open()
                cmd.CommandText = "update login set profile_created = '" & profile_created & "' where name = '" & username & "'"
                cmd.Connection = connection
                cmd.ExecuteNonQuery()
                connection.Close()
            End If
        End If
    End Sub

    Private Sub profiled_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.Hide()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (RadioButton1.Checked = True) Then
            Button3.Text = RadioButton1.Text
        ElseIf (RadioButton2.Checked = True) Then
            Button3.Text = RadioButton2.Text
        ElseIf (RadioButton3.Checked = True) Then
            Button3.Text = RadioButton3.Text
        ElseIf (RadioButton4.Checked = True) Then
            Button3.Text = RadioButton4.Text
        End If
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        RadioButton4.Checked = False
        Panel1.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel1.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        profile_initial()
        Form1.form5position()
        Form5.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Panel1.Hide()
    End Sub
    Public Sub TDEEcalculator()
        'MessageBox.Show("i_w" & initial_weight & "activity level" & activity_level & "heightt" & heightt & "gender" & gender)
        If (gender = "male") Then
            'MessageBox.Show("i_w" & initial_weight & "activity level" & activity_level & "heightt" & heightt)
            bmr1 = (10 * initial_weight) + (6.25 * (heightt * 100)) + 5 - (5 * age)
        ElseIf (gender = "female") Then
            bmr1 = (10 * initial_weight) + (6.25 * (heightt * 100)) - 161 - (5 * age)
        End If
        If (activity_level = "not very active") Then
            bmr2 = bmr1 * 1.2
        ElseIf (activity_level = "lightly active") Then
            bmr2 = bmr1 * 1.375
        ElseIf (activity_level = "active") Then
            bmr2 = bmr1 * 1.55
        ElseIf (activity_level = "very active") Then
            bmr2 = bmr1 * 1.725
        End If
        tdee = (bmr1 + bmr2) / 2
        ' MessageBox.Show("bmr1 " & bmr1 & " bmr2 " & bmr2)
    End Sub
    Public Sub agecalculator()
        today = Date.Today
        con = New SqlConnection(constring)
        con.Open()
        Dim command As New SqlCommand("select dob from login where name = '" & username & "'", con)
        Dim dr1 As SqlDataReader
        dr1 = command.ExecuteReader()
        Do While dr1.Read()
            bday = dr1("dob")
        Loop
        dr1.Close()
        con.Close()
        Dim days As TimeSpan = today.Subtract(bday)
        age = days.TotalDays
        age = age / 365
    End Sub
    Public Sub profile_initial()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Button3.Text = "select activity level"
        ComboBox1.SelectedItem = Nothing
    End Sub
End Class