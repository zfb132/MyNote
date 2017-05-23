Public Class Login
    Dim times As Integer = 0
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        times = times + 1
        If times >= 3 Then
            Me.Close()
            MsgBox("3次登陆错误，退出程序！", MessageBoxButtons.OK + MessageBoxIcon.Information, "提示")
            End
        End If
        If TextBox1.Text = "ABC" And TextBox2.Text = "123" Then
            Me.Hide()
            Info.Show()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                times = times - 1
                MsgBox("信息不完整，请重新填写", MessageBoxButtons.OK + MessageBoxIcon.Information, "提示")
            Else
                Dim resutlCode As Integer = MsgBox("用户名或密码错误，要重新填写吗？", MessageBoxButtons.YesNo + MessageBoxIcon.Information + MessageBoxDefaultButton.Button1, "提示")
                Select Case resutlCode
                    Case MsgBoxResult.Yes
                        TextBox1.Text = ""
                        TextBox2.Text = ""
                    Case MsgBoxResult.No
                End Select
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        End
    End Sub
End Class