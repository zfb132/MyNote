Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        zfbglobal.minNum = CInt(TextBox1.Text)
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress
        If Char.IsDigit(e.KeyChar) = True Or e.KeyChar = Chr(8) = True Or e.KeyChar = "." Then
            If e.KeyChar = "." And InStr(CType(sender, TextBox).Text, ".") > 0 Then
                MsgBox("请输入数字", vbOKOnly + vbExclamation, "输入错误")
                e.Handled = True
            Else
                e.Handled = False
            End If
        Else
            MsgBox("请输入数字", vbOKOnly + vbExclamation, "输入错误")
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case Button1.Text
            Case "开始"
                zfbglobal.minNum = CInt(TextBox1.Text)
                zfbglobal.maxNum = CInt(TextBox2.Text)
                zfbglobal.num = zfbglobal.minNum
                TextBox3.Text = ""
                '启动定时器
                Me.Timer1.Enabled = True
                '设定间隔时间为50ms
                Me.Timer1.Interval = 100
                Button1.Text = "暂停"
                ProgressBar1.Minimum = zfbglobal.minNum
                ProgressBar1.Maximum = zfbglobal.maxNum
                '启动定时器
                Me.Timer2.Enabled = True
                '设定间隔时间为50ms
                Me.Timer2.Interval = 100
            Case "暂停"
                '关闭定时器
                Me.Timer1.Enabled = False
                '关闭定时器
                Me.Timer2.Enabled = False
                Button1.Text = "继续"
            Case "继续"
                '启动定时器
                Me.Timer1.Enabled = True
                '设定间隔时间为50ms
                Me.Timer1.Interval = 100
                '启动定时器
                Me.Timer2.Enabled = True
                '设定间隔时间为50ms
                Me.Timer2.Interval = 100
                Button1.Text = "暂停"
            Case Else
        End Select
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If zfbglobal.num <= zfbglobal.maxNum Then
            Dim j As Integer, i As Integer
            i = zfbglobal.num
            For j = 2 To zfbglobal.num - 1
                If i Mod j = 0 Then
                    Exit For
                End If
                If j = i - 1 Then
                    zfbglobal.times = zfbglobal.times + 1
                    If zfbglobal.times Mod 6 = 0 Then
                        'vbTab  vbCrLf  一些内置的字符
                        TextBox3.Text = TextBox3.Text + CStr(i) + vbTab + vbCrLf
                    Else
                        TextBox3.Text = TextBox3.Text + CStr(i) + vbTab
                    End If
                End If
            Next j
            zfbglobal.num = zfbglobal.num + 1
        Else
            '启动定时器
            Me.Timer1.Enabled = False
            MsgBox("完成计算！", vbOKOnly + vbInformation, "提示")
            Button1.Text = "开始"
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If zfbglobal.num > zfbglobal.maxNum Then
            Me.ProgressBar1.Value = zfbglobal.maxNum
        Else
            Me.ProgressBar1.Value = zfbglobal.num
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class
