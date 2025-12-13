
Public Class TagEditorOnlineSave

    ' Declarations
    Private _filename As String = String.Empty
    Friend ReadOnly Property GetFilename As String
        Get
            Return _filename
        End Get
    End Property

    ' Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
                Case Skye.WinAPI.WM_SYSCOMMAND
                    If m.WParam.ToInt32 = Skye.WinAPI.SC_CLOSE Then
                        DialogResult = DialogResult.Cancel
                    End If
            End Select
        Catch ex As Exception
            My.App.WriteToLog("TagEditorOnlineSave WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub TagEditorOnlineSave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
    End Sub

    ' Control Events
    Private Sub BtnLocation_Click(sender As Object, e As EventArgs) Handles BtnLocation.Click
        Dim ofd As New FolderBrowserDialog With {
            .Description = "Select File Location",
            .Multiselect = False}
        Dim result As DialogResult = ofd.ShowDialog(Me)
        If result = DialogResult.OK AndAlso Not String.IsNullOrEmpty(ofd.SelectedPath) Then
            TxtBoxLocation.Text = ofd.SelectedPath
        End If
        ofd.Dispose()
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        _filename = TxtBoxLocation.Text.Trim + "\" + TxtBoxFilename.Text.Trim
        If RadBtnImageFormatJPG.Checked Then
            _filename += ".jpg"
        ElseIf RadBtnImageFormatPNG.Checked Then
            _filename += ".png"
        ElseIf RadBtnImageFormatBMP.Checked Then
            _filename += ".bmp"
        End If
        LblFilename.ResetForeColor()
        LblLocation.ResetForeColor()
        If String.IsNullOrEmpty(TxtBoxFilename.Text) OrElse TxtBoxFilename.Text.Intersect(System.IO.Path.GetInvalidFileNameChars).Any OrElse System.IO.File.Exists(_filename) Then
            TxtBoxFilename.Focus()
            LblFilename.ForeColor = Color.Red
        ElseIf String.IsNullOrEmpty(TxtBoxLocation.Text) OrElse TxtBoxLocation.Text.Intersect(System.IO.Path.GetInvalidPathChars).Any OrElse Not System.IO.Directory.Exists(TxtBoxLocation.Text) Then
            TxtBoxLocation.Focus()
            LblLocation.ForeColor = Color.Red
        Else
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    ' Methods
    Private Sub SetAccentColor()
        Static c As Color
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            BackColor = c
        End If
        ResumeLayout()
    End Sub
    Private Sub SetTheme()
        SuspendLayout()
        If App.CurrentTheme.IsAccent Then
            LblFilename.ForeColor = App.CurrentTheme.AccentTextColor
            LblLocation.ForeColor = App.CurrentTheme.AccentTextColor
        Else
            BackColor = App.CurrentTheme.BackColor
            LblFilename.ForeColor = App.CurrentTheme.TextColor
            LblLocation.ForeColor = App.CurrentTheme.TextColor
        End If
        TxtBoxFilename.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxFilename.ForeColor = App.CurrentTheme.TextColor
        TxtBoxLocation.BackColor = App.CurrentTheme.ControlBackColor
        TxtBoxLocation.ForeColor = App.CurrentTheme.TextColor
        BtnLocation.BackColor = App.CurrentTheme.ButtonBackColor
        BtnLocation.ForeColor = App.CurrentTheme.ButtonTextColor
        BtnSave.BackColor = App.CurrentTheme.ButtonBackColor
        BtnSave.ForeColor = App.CurrentTheme.ButtonTextColor
        tipInfo.BackColor = App.CurrentTheme.BackColor
        tipInfo.ForeColor = App.CurrentTheme.TextColor
        tipInfo.BorderColor = App.CurrentTheme.ButtonBackColor
        ResumeLayout()
        'Debug.Print("Tag Editor Theme Set")
    End Sub

End Class
