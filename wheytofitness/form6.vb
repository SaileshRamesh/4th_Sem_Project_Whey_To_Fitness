Public Class Form6
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing) Then
            MsgBox("enter all details", "wheytofitness")
        Else
            bmical(TextBox1.Text, TextBox2.Text)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (TextBox4.Text = Nothing) Then
            MessageBox.Show("calculate BMI first", "wheytofitness")
        Else
            fat_percent(TextBox3.Text, ComboBox1.SelectedItem, TextBox4.Text)
        End If
    End Sub
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("male")
        ComboBox1.Items.Add("female")
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub
    Public Sub fat_percent(ByVal a As Integer, ByVal g As String, ByVal b As Double)
        Dim f As Double
        If (g = "male") Then
            f = (1.2 * b) + (0.23 * a) - 13.2
        ElseIf (g = "female") Then
            f = (1.2 * b) + (0.23 * a) - 5.4
        End If
        If (forprofile = 1) Then
            fat_percentage = f
        Else
            TextBox5.Text = f
        End If
    End Sub
    Public Sub bmical(ByVal h As Double, ByVal w As Double)
        Dim b As Double
        b = w / (h * h)
        'MessageBox.Show("bmi" & b & "w" & w & "h" & h)
        If (forprofile = 1) Then
            bmi = b
        Else
            TextBox4.Text = b
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        PictureBox1.Image = My.Resources.bmi
        Button7.Visible = True
        Button5.Visible = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        PictureBox1.Image = My.Resources.fat_
        Button7.Visible = False
        Button5.Visible = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Panel1.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Panel1.Show()
    End Sub
End Class