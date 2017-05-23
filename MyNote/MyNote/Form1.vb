Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim activate As MDIParent2 = MDIParent1.ActiveMdiChild()
        Dim str As String = activate.RichTextBox1.Text
        Dim find As String = TextBox1.Text
        Dim replace As String = TextBox2.Text
        Dim startIndex As Integer = 1
        Dim findIndex As Integer = 1
        ''这个判断是防止find=""时的无限替换
        If Not find = "" Then
            While findIndex
                findIndex = InStr(startIndex, str, find, CompareMethod.Text)
                If findIndex = 0 Then
                    Exit While
                End If
                activate.RichTextBox1.SelectionStart = findIndex - 1
                activate.RichTextBox1.SelectionLength = find.Length
                activate.RichTextBox1.SelectedText = replace
                str = activate.RichTextBox1.Text
                startIndex = findIndex + replace.Length
            End While
        End If
        activate.RichTextBox1.Text = str
    End Sub
End Class
