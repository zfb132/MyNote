Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel

Public Class MDIParent1

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click
        ' 创建此子窗体的一个新实例。
        Dim ChildForm As New MDIParent2
        ChildForm.m_openType = True
        ' 在显示该窗体前使其成为此 MDI 窗体的子窗体。
        ChildForm.MdiParent = Me
        m_ChildFormNumber += 1
        ChildForm.Text = "新文档" & m_ChildFormNumber
        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        ''此处筛选全部文件
        ''OpenFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"
        OpenFileDialog.Filter = "全部文件(*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            ''获得选中文件的路径
            Dim FileName As String = OpenFileDialog.FileName
            ''流读取
            Dim SR As New StreamReader(FileName)
            Dim content As String
            content = SR.ReadToEnd
            SR.Close()
            ' 创建此子窗体的一个新实例。
            Dim ChildForm As New MDIParent2
            ChildForm.m_openType = False
            ' 在显示该窗体前使其成为此 MDI 窗体的子窗体。
            ChildForm.MdiParent = Me
            ChildForm.Text = FileName
            ChildForm.RichTextBox1.Text = content
            ChildForm.Show()
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialog.Filter = "全部文件(*.*)|*.*"
        SaveFileDialog.DefaultExt = "txt"
        SaveFileDialog.AddExtension = True
        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: 在此处添加代码，将窗体的当前内容保存到一个文件中。
            ''true是指以追加的方式打开指定文件
            Dim sw As StreamWriter = New StreamWriter（FileName， False）
            ''获取当前活动的子窗体
            Dim activate As MDIParent2 = Me.ActiveMdiChild()
            activate.m_openType = False
            For i = 0 To activate.RichTextBox1.Lines.Length - 1
                sw.WriteLine（activate.RichTextBox1.Lines(i)）
            Next
            sw.Flush（）
            sw.Close（）
            sw = Nothing
            activate.Text = FileName
            activate.m_isModified = False
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        ' 关闭此父窗体的所有子窗体。
        For Each ChildForm As MDIParent2 In Me.MdiChildren
            'If ChildForm.m_isModified Then
            '    Dim resutl As Integer = MsgBox("是否保存所做的修改？", MessageBoxButtons.YesNo + MessageBoxIcon.Question + MessageBoxDefaultButton.Button1, ChildForm.Text)
            '    Select Case resutl
            '        Case MsgBoxResult.Yes
            '            Call save()
            '        Case MsgBoxResult.No
            '    End Select
            'End If
            ChildForm.Close()
        Next
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' 使用 My.Computer.Clipboard 将选择的文本或图像插入剪贴板
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' 使用 My.Computer.Clipboard 将选择的文本或图像插入剪贴板
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        '使用 My.Computer.Clipboard.GetText() 或 My.Computer.Clipboard.GetData 从剪贴板检索信息。
    End Sub



    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' 关闭此父窗体的所有子窗体。
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer


    Private Sub MDIParent1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ''HH表示24小时制；hh表示12小时制
        ToolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Call save()
    End Sub

    ''保存文档的函数：若为新文档则保存时弹出保存对话框，否则直接保存
    Public Sub save()
        ''获取当前活动的子窗体
        Dim activate As MDIParent2 = Me.ActiveMdiChild()
        ''如果是新建true的话，就弹出保存文件的对话框并保存；否则直接保存
        If activate.m_openType Then
            Dim SaveFileDialog As New SaveFileDialog
            SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            SaveFileDialog.Filter = "全部文件(*.*)|*.*"
            SaveFileDialog.DefaultExt = "txt"
            SaveFileDialog.AddExtension = True
            If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                Dim FileName As String = SaveFileDialog.FileName
                ' TODO: 在此处添加代码，将窗体的当前内容保存到一个文件中。
                ''true是指以追加的方式打开指定文件
                Dim sw As StreamWriter = New StreamWriter（FileName， False）
                For i = 0 To activate.RichTextBox1.Lines.Length - 1
                    sw.WriteLine（activate.RichTextBox1.Lines(i)）
                Next
                sw.Flush（）
                sw.Close（）
                sw = Nothing
                activate.Text = FileName
                activate.m_openType = False
                activate.m_isModified = False
            End If
        Else
            ' TODO: 在此处添加代码，将窗体的当前内容保存到一个文件中。
            ''true是指以追加的方式打开指定文件
            Dim sw As StreamWriter = New StreamWriter（activate.Text， False）
            For i = 0 To activate.RichTextBox1.Lines.Length - 1
                sw.WriteLine（activate.RichTextBox1.Lines(i)）
            Next
            sw.Flush（）
            sw.Close（）
            sw = Nothing
            activate.m_isModified = False
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub FileMenu_Click(sender As Object, e As EventArgs) Handles FileMenu.Click
        If Me.MdiChildren.Length = 0 Then
            Me.SaveToolStripMenuItem.Enabled = False
            Me.SaveAsToolStripMenuItem.Enabled = False
        Else
            Me.SaveToolStripMenuItem.Enabled = True
            Me.SaveAsToolStripMenuItem.Enabled = True
        End If
    End Sub
End Class
