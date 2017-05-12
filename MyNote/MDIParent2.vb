Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.IO
Public Class MDIParent2

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' 创建此子窗体的一个新实例。
        Dim ChildForm As New System.Windows.Forms.Form
        ' 在显示该窗体前使其成为此 MDI 窗体的子窗体。
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "窗口 " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: 在此处添加打开文件的代码。
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: 在此处添加代码，将窗体的当前内容保存到一个文件中。
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        ' 使用 My.Computer.Clipboard 将选择的文本或图像插入剪贴板
        My.Computer.Clipboard.SetText(Me.RichTextBox1.SelectedText)
        Me.RichTextBox1.SelectedText = ""
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        ' 使用 My.Computer.Clipboard 将选择的文本或图像插入剪贴板
        My.Computer.Clipboard.SetText(Me.RichTextBox1.SelectedText)

    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click
        '使用 My.Computer.Clipboard.GetText() 或 My.Computer.Clipboard.GetData 从剪贴板检索信息。
        ''获取当前光标所在控件内的行号和列号
        ''Dim row As Integer = (1 + RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart))
        ''Dim column As Integer = (1 + RichTextBox1.SelectionStart - (RichTextBox1.GetFirstCharIndexFromLine(1 + RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart) - 1)))
        Me.RichTextBox1.Focus()
        Me.RichTextBox1.Text = Me.RichTextBox1.Text.Insert(Me.RichTextBox1.SelectionStart, My.Computer.Clipboard.GetText())
        Me.RichTextBox1.Select(Me.RichTextBox1.SelectionStart + My.Computer.Clipboard.GetText().Length, 0)
    End Sub



    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
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
    ''True:新建     False:打开
    Public m_openType As Boolean
    ''文档修改标记
    Public m_isModified As Boolean = False

    Private Sub ViewMenu_Click(sender As Object, e As EventArgs) Handles ViewMenu.Click

    End Sub

    Private Sub MDIParent2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''用于定时检测剪切板上是否有文字内容
        Timer1.Interval = 5000
        Timer1.Start()
        ''用于初始禁用剪切、复制、粘贴选项
        Me.CutToolStripMenuItem.Enabled = False
        Me.CopyToolStripMenuItem.Enabled = False
        Me.ToolStripMenuItem1.Enabled = False
    End Sub

    Private Sub MDIParent2_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        RichTextBox1.Width = Me.Width
        RichTextBox1.Height = Me.Height
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        m_isModified = True
    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If My.Computer.Clipboard.ContainsText() Then
            Me.PasteToolStripMenuItem.Enabled = True
        Else
            Me.PasteToolStripMenuItem.Enabled = False
        End If
    End Sub

    ''属性里面设置为不显示此按钮
    Private Sub FontDialog1_Apply(sender As Object, e As EventArgs) Handles FontDialog1.Apply

    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Dim se As DialogResult
        ''设置打开前的默认值
        FontDialog1.Font = RichTextBox1.Font
        FontDialog1.Color = RichTextBox1.ForeColor
        se = FontDialog1.ShowDialog
        If se = DialogResult.OK Then
            RichTextBox1.Font = FontDialog1.Font
            RichTextBox1.ForeColor = FontDialog1.Color
        End If
    End Sub

    ''根据此函数禁用菜单项
    Private Sub RichTextBox1_SelectionChanged(sender As Object, e As EventArgs) Handles RichTextBox1.SelectionChanged
        If Not RichTextBox1.SelectedText = "" Then
            Me.CutToolStripMenuItem.Enabled = True
            Me.CopyToolStripMenuItem.Enabled = True
            Me.ToolStripMenuItem1.Enabled = True
        Else
            Me.CutToolStripMenuItem.Enabled = False
            Me.CopyToolStripMenuItem.Enabled = False
            Me.ToolStripMenuItem1.Enabled = False
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        Me.RichTextBox1.SelectAll()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Form1.ShowDialog()
    End Sub

    Private Sub MDIParent2_FormClosing(sender As Object, e As CancelEventArgs) Handles Me.FormClosing
        If m_isModified Then
            Dim resutl As Integer = MsgBox("是否保存所做的修改？", MessageBoxButtons.YesNo + MessageBoxIcon.Question + MessageBoxDefaultButton.Button1, Me.Text)
            Select Case resutl
                Case MsgBoxResult.Yes
                    Call saveIt()
                Case MsgBoxResult.No
            End Select
        End If
    End Sub

    Public Sub saveIt()
        ''如果是新建true的话，就弹出保存文件的对话框并保存；否则直接保存
        If Me.m_openType Then
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
                For i = 0 To Me.RichTextBox1.Lines.Length - 1
                    sw.WriteLine（Me.RichTextBox1.Lines(i)）
                Next
                sw.Flush（）
                sw.Close（）
                sw = Nothing
                Me.Text = FileName
            End If
            Me.m_openType = False
        Else
            ' TODO: 在此处添加代码，将窗体的当前内容保存到一个文件中。
            ''true是指以追加的方式打开指定文件
            Dim sw As StreamWriter = New StreamWriter（Me.Text， False）
            For i = 0 To Me.RichTextBox1.Lines.Length - 1
                sw.WriteLine（Me.RichTextBox1.Lines(i)）
            Next
            sw.Flush（）
            sw.Close（）
            sw = Nothing
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Me.RichTextBox1.SelectedText = ""
    End Sub
End Class
