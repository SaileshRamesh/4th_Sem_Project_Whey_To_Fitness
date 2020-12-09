Imports System.Data.SqlClient
Imports System.IO
Public Class Form1
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Form2.Show()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.initial()
        Panel2.Hide()
        Panel3.Hide()
        Panel4.Hide()
    End Sub

    Public Sub initial()
        If (l = 0) Then
            Label2.Visible = False
            Label1.Visible = True
            Form2.TextBox1.Text = ""
            Form2.TextBox2.Text = ""
            Label3.Visible = False
            Label4.Visible = False
        ElseIf (l = 1) Then
            Label1.Visible = False
            Label2.Visible = True
            Label3.Text = username.ToUpper
            Label3.Visible = True
            Me.Show()
            If (profile_created = True) Then
                Dim command As New SqlCommand("select name,p_id,current_w,goal_w,initial_w,height,activity_level,tdee,age,bmi,fat,update_d,update_d1 from profile where name = @name", connection)
                Dim parametersprofile As New SqlParameter("@name", SqlDbType.VarChar)
                parametersprofile.Direction = ParameterDirection.Input
                parametersprofile.Value = username
                command.Parameters.Add(parametersprofile)
                connection.Open()
                Dim dr1 As SqlDataReader
                dr1 = command.ExecuteReader()
                Do While dr1.Read()
                    p_id = dr1("p_id")
                    c_weight = dr1("current_w")
                    goal_weight = dr1("goal_w")
                    initial_weight = dr1("initial_w")
                    activity_level = dr1("activity_level")
                    tdee = dr1("tdee")
                    age = dr1("age")
                    bmi = dr1("bmi")
                    fat_percentage = dr1("fat")
                    update_d = dr1("update_d")
                    update_d1 = dr1("update_d1")
                Loop
                dr1.Close()
                connection.Close()
            Else
                MessageBox.Show("no profile")
            End If
        End If
    End Sub

    Private Sub ProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfileToolStripMenuItem.Click
        If (l = 1) Then
            form5position()
            Form5.Show()
        Else
            MessageBox.Show("please login", "wheytofitness")
        End If
    End Sub
    Private Sub Label1_MouseHover(sender As Object, e As EventArgs) Handles Label1.MouseHover
        Label1.BackColor = Color.White
        Label1.ForeColor = Color.Black
    End Sub

    Private Sub Label1_MouseLeave(sender As Object, e As EventArgs) Handles Label1.MouseLeave
        Label1.BackColor = Color.WhiteSmoke
        Label1.ForeColor = Color.DarkOliveGreen
    End Sub
    Private Sub Label2_MouseHover(sender As Object, e As EventArgs) Handles Label2.MouseHover
        Label2.BackColor = Color.White
        Label2.ForeColor = Color.Black
    End Sub

    Private Sub Label2_MouseLeave(sender As Object, e As EventArgs) Handles Label2.MouseLeave
        Label2.BackColor = Color.WhiteSmoke
        Label2.ForeColor = Color.DarkOliveGreen
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        If (MessageBox.Show("are you sure you want to log out", "log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
            l = 0
            Me.initial()
        Else
            MessageBox.Show("continue to enjoy our app")
        End If

    End Sub

    Private Sub FoodToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FoodToolStripMenuItem.Click
        If (l = 1) Then
            'food.Width = 800
            'food.Height = 350
            'food.StartPosition = FormStartPosition.CenterParent
            'food.Location = New Point(470, 100)
            'food.Show()
            If (profile_created = True) Then
                Panel2.Show()
                foodcal()
            Else
                MessageBox.Show("please create profile to get custom food details", "food", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("please login", "wheytofitness")
        End If
    End Sub

    Private Sub ExerciseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExerciseToolStripMenuItem.Click
        If (l = 1) Then
            'exercise.Width = 800
            'exercise.Height = 370
            'exercise.StartPosition = FormStartPosition.Manual
            'exercise.Location = New Point(470, 460)
            'exercise.Show()
            If (profile_created = True) Then
                Panel3.Show()
            Else
                MessageBox.Show("please create profile to get custom exercise details", "food", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("please login", "wheytofitness")
        End If
    End Sub
    Public Sub form5position()
        Form5.Width = 550
        Form5.Height = 390
        Form5.StartPosition = FormStartPosition.CenterScreen
    End Sub
    Public Sub form6position()
        Form6.Width = 500
        Form6.Height = 380
        Form6.StartPosition = FormStartPosition.CenterScreen
    End Sub
    Private Sub UtilityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UtilityToolStripMenuItem.Click
        form6position()
        Form6.Panel1.Hide()
        Form6.Show()
    End Sub
    Public Sub foodcal()
        Dim carb, protein, fat, total, carbcal, pounds, fw, lw, fatt As Double
        'MessageBox.Show()
        pounds = c_weight * 2.20462
        fatt = fat_percentage / 100
        fw = fatt * pounds
        lw = pounds - fw
        protein = lw * 1
        fat = lw * 0.35
        total = protein * 4 + fat * 9
        carbcal = tdee - total
        carb = carbcal / 4
        TextBox1.Text = Convert.ToInt32(carb) & " grams"
        TextBox2.Text = Convert.ToInt32(protein) & " grams"
        TextBox3.Text = Convert.ToInt32(fat) & " grams"
        TextBox4.Text = tdee & " cal"
        If (p_id = 1) Then
            Label10.Text = "required calories to lose weight"
            TextBox5.Text = (tdee - 250) & " cal"
        ElseIf (p_id = 2) Then
            TextBox5.Text = (tdee + 250) & " cal"
            Label10.Text = "required calories to gain weight"
        End If
        TextBox6.Text = fat_percentage
        TextBox7.Text = Convert.ToInt32((fw / 2.205)) & "kg"
        TextBox8.Text = Convert.ToInt32((lw / 2.205)) & "kg"
        ' If (ComboBox1.SelectedItem = "fat") Then
        'ty = "fat"
        'ElseIf (ComboBox1.SelectedItem = "protein") Then
        'ty = prote
        'End If
    End Sub
    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted
        Dim ty As String
        Dim c As New SqlCommand("select f_name from food where type = '" & ComboBox1.SelectedItem & "'")
        ComboBox2.Items.Clear()
        c.Connection = connection
        connection.Open()
        Dim dr1 As SqlDataReader
        dr1 = c.ExecuteReader()
        Do While dr1.Read()
            ty = dr1("f_name")
            ComboBox2.Items.Add(ty)
        Loop
        connection.Close()
    End Sub

    Private Sub ComboBox2_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox2.SelectionChangeCommitted
        Dim ty As String
        Dim c As New SqlCommand("select calorie from food where f_name = '" & ComboBox2.SelectedItem & "'")
        c.Connection = connection
        connection.Open()
        Dim dr2 As SqlDataReader
        dr2 = c.ExecuteReader()
        Do While dr2.Read()
            ty = dr2("calorie")
            TextBox11.Text = ty
        Loop
        connection.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel2.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel3.Hide()
    End Sub
    Private Sub ComboBox4_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox4.SelectionChangeCommitted
        Dim ty As String
        Dim c As New SqlCommand("select e_name from exercise where intensity = '" & ComboBox4.SelectedItem & "'")
        ComboBox3.Items.Clear()
        c.Connection = connection
        connection.Open()
        Dim dr2 As SqlDataReader
        dr2 = c.ExecuteReader()
        Do While dr2.Read()
            ty = dr2("e_name")
            ComboBox3.Items.Add(ty)
        Loop
        connection.Close()
    End Sub
    Private Sub ComboBox3_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox3.SelectionChangeCommitted
        Dim c As New SqlCommand("select met from exercise where e_name = '" & ComboBox3.SelectedItem & "'")
        c.Connection = connection
        connection.Open()
        Dim dr2 As SqlDataReader
        dr2 = c.ExecuteReader()
        Do While dr2.Read()
            met = dr2("met")
        Loop
        connection.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (ComboBox4.SelectedItem = Nothing Or ComboBox4.SelectedItem = Nothing Or TextBox9.Text = Nothing) Then
            MessageBox.Show("select exercise and intensity before calculating")
        Else
            TextBox10.Text = met * c_weight * ((TextBox9.Text) / 60)
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        WebBrowser1.Navigate(RichTextBox1.Text)
    End Sub

    Private Sub ShopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShopToolStripMenuItem.Click
        Panel4.Show()
        WebBrowser1.Navigate("https://www.bodybuilding.com/")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Panel4.Hide()
        RichTextBox1.Text = ""
    End Sub
End Class
