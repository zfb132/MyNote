Public Class Info

    Private Sub Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub
    Private Sub ToolStripStatusLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel1.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ''HH表示24小时制；hh表示12小时制
        ToolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Title = "选择学生照片"
        OpenFileDialog1.Filter = "全部图片|*.bmp;*.jpg;*.gif|BMP文件|*.bmp|JPEG文件|*.jpg|GIF文件|*.gif"
        OpenFileDialog1.FilterIndex = 3
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Student.path = OpenFileDialog1.FileName
        ''因为某些原因找不到LoadPicture()函数
        Dim image As Image = Image.FromFile(Student.path)
        Dim cloneImage As Image = New Bitmap(image)
        ''释放原文件资源防止占用
        image.Dispose()
        PictureBox1.Image = cloneImage
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ''获取学号
        Student.info = Student.info + "学号：" + TextBox1.Text + vbCrLf
        ''获取姓名
        Student.info = Student.info + "姓名：" + TextBox2.Text + vbCrLf
        ''获取性别：当没有选择性别时，不预览性别这一项（根据老师的demo）
        For Each myradio As RadioButton In GroupBox1.Controls
            If myradio.Checked Then
                Student.info = Student.info + "性别：" + myradio.Text + vbCrLf
            End If
        Next
        ''获得出生日期
        Student.info = Student.info + "出生日期：" + DateTimePicker1.Text + vbCrLf
        ''获得专业
        Student.info = Student.info + "专业："
        If Not ComboBox1.SelectedIndex = -1 Then
            Student.info = Student.info + ComboBox1.SelectedItem.ToString
        End If
        Student.info = Student.info + vbCrLf
        ''获得爱好
        Student.info = Student.info + "爱好："
        For Each mycheck As CheckBox In GroupBox2.Controls
            If mycheck.Checked Then
                Student.info = Student.info + mycheck.Text + " "
            End If
        Next
        Student.info = Student.info + vbCrLf
        Student.info = Student.info + "奖惩情况：" + RichTextBox1.Text
        ''不预览备注
        ''Student.info = Student.info + "备注： " + RichTextBox2.Text
        ''Me.Hide()
        ''在 VBA 中，可以将窗体显示为 vbModal 或 vbModeless
        ''在Visual Basic.NET 中， ShowDialog 方法用于以模式方式显示窗体；Show 方法用于以无模式方式显示窗体。
        Preview.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        End
    End Sub
End Class