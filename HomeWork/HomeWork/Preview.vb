Public Class Preview
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Preview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Student.path = "" Then
            ''因为某些原因找不到LoadPicture()函数
            Dim image As Image = Image.FromFile(Student.path)
            Dim cloneImage As Image = New Bitmap(image)
            ''释放原文件资源防止占用
            image.Dispose()
            PictureBox1.Image = cloneImage
        End If
        RichTextBox1.Text = Student.info
    End Sub
    Private Sub Preview_UnLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        Student.path = ""
        Student.info = ""
    End Sub
End Class