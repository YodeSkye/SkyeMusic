
Imports System.IO

Public Class PlayerQueue

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private QueueItemMove As ListViewItem 'Item being moved in the playlist

    'Form Events
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Select Case m.Msg
                Case Skye.WinAPI.WM_DWMCOLORIZATIONCOLORCHANGED
                    SetAccentColor()
            End Select
        Catch ex As Exception
            My.App.WriteToLog("Queue WndProc Handler Error" + Chr(13) + ex.ToString)
        Finally
            MyBase.WndProc(m)
        End Try
    End Sub
    Private Sub PlayerQueue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetAccentColor()
        SetTheme()
        App.ThemeMenu(CMQueue)
        Populate()
    End Sub
    Private Sub PlayerQueue_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FixedFrameBorderSize.Width - 5, -e.Y - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 5)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 5, -e.Y - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 5)
            End If
        End If
    End Sub
    Private Sub PlayerQueue_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub PlayerQueue_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub PlayerQueue_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub
    Private Sub PlayerQueue_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        LVQueue.Columns(1).Width = -2
    End Sub
    Private Sub PlayerQueue_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Control Then
                    For Each item As ListViewItem In LVQueue.Items
                        item.Selected = True
                    Next
                End If
            Case Keys.Delete
                RemoveFromQueue()
        End Select
    End Sub

    'Control Events
    Private Sub LVQueue_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles LVQueue.DrawColumnHeader
        Static b As Rectangle
        'Draw Background
        b = e.Bounds
        Using br As Brush = New SolidBrush(App.CurrentTheme.BackColor)
            e.Graphics.FillRectangle(br, b)
        End Using
        'Draw Borders
        b.Width -= 1
        b.Height -= 1
        e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, b)
        b.Width -= 1
        b.Height -= 1
        e.Graphics.DrawLine(SystemPens.ControlLightLight, b.X, b.Y, b.Right, b.Y)
        e.Graphics.DrawLine(SystemPens.ControlLightLight, b.X, b.Y, b.X, b.Bottom)
        e.Graphics.DrawLine(SystemPens.ControlDark, (b.X + 1), b.Bottom, b.Right, b.Bottom)
        e.Graphics.DrawLine(SystemPens.ControlDark, b.Right, (b.Y + 1), b.Right, b.Bottom)
        'Draw Text
        b = e.Bounds
        Dim width As Integer = TextRenderer.MeasureText(" ", e.Font).Width
        b = Rectangle.Inflate(e.Bounds, -2, 0)
        If e.Header.TextAlign = HorizontalAlignment.Center Then
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, b, App.CurrentTheme.TextColor, TextFormatFlags.HorizontalCenter)
        Else
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, b, App.CurrentTheme.TextColor, TextFormatFlags.VerticalCenter)
        End If
    End Sub
    Private Sub LVQueue_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles LVQueue.DrawItem
        e.DrawDefault = True
    End Sub
    Private Sub LVQueue_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles LVQueue.DrawSubItem
        e.DrawDefault = True
    End Sub
    Private Sub LVQueue_MouseDown(sender As Object, e As MouseEventArgs) Handles LVQueue.MouseDown
        If e.Clicks = 1 Then QueueItemMove = LVQueue.GetItemAt(e.X, e.Y)
    End Sub
    Private Sub LVQueue_MouseMove(sender As Object, e As MouseEventArgs) Handles LVQueue.MouseMove
        If QueueItemMove IsNot Nothing Then
            Cursor = Cursors.Hand
            Dim lastItemBottom = Math.Min(e.Y, LVQueue.Items(LVQueue.Items.Count - 1).GetBounds(ItemBoundsPortion.Entire).Bottom - 1)
            Dim itemover = LVQueue.GetItemAt(0, lastItemBottom)
            If itemover IsNot Nothing Then
                Dim rc = itemover.GetBounds(ItemBoundsPortion.Entire)
                If e.Y < rc.Top + rc.Height / 2 Then
                    LVQueue.LineBefore = itemover.Index
                    LVQueue.LineAfter = -1
                Else
                    LVQueue.LineBefore = -1
                    LVQueue.LineAfter = itemover.Index
                End If
                LVQueue.Invalidate()
            End If
        End If
    End Sub
    Private Sub LVQueue_MouseUp(sender As Object, e As MouseEventArgs) Handles LVQueue.MouseUp
        If QueueItemMove IsNot Nothing Then
            Dim lastItemBottom = Math.Min(e.Y, LVQueue.Items(LVQueue.Items.Count - 1).GetBounds(ItemBoundsPortion.Entire).Bottom - 1)
            Dim itemover = LVQueue.GetItemAt(0, lastItemBottom)
            If itemover IsNot Nothing And itemover IsNot QueueItemMove Then
                Dim insertbefore As Boolean
                Dim rc = itemover.GetBounds(ItemBoundsPortion.Entire)
                If e.Y < rc.Top + rc.Height / 2 Then
                    insertbefore = True
                Else
                    insertbefore = False
                End If
                LVQueue.Items.Remove(QueueItemMove)
                If Not QueueItemMove.Index = itemover.Index Then
                    If insertbefore Then
                        LVQueue.Items.Insert(itemover.Index, QueueItemMove)
                    Else
                        LVQueue.Items.Insert(itemover.Index + 1, QueueItemMove)
                    End If
                End If
                SaveLVToQueue()
            End If
            QueueItemMove = Nothing
        End If
        QueueItemMove = Nothing
        Cursor = Cursors.Default
        LVQueue.LineBefore = -1
        LVQueue.LineAfter = -1
        LVQueue.Invalidate()
    End Sub
    Private Sub LVQueue_DragEnter(sender As Object, e As DragEventArgs) Handles LVQueue.DragEnter
        Activate()
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim filedrop = DirectCast(e.Data.GetData(DataFormats.FileDrop, True), String())
            Dim files As New List(Of String)
            For Each s In filedrop
                If Computer.FileSystem.FileExists(s) AndAlso App.ExtensionDictionary.ContainsKey(IO.Path.GetExtension(s)) Then files.Add(s)
            Next
            If files.Count > 0 Then : e.Effect = DragDropEffects.Link
            Else : e.Effect = DragDropEffects.None
            End If
            files.Clear()
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub LVQueue_DragDrop(sender As Object, e As DragEventArgs) Handles LVQueue.DragDrop
        If e.Effect = DragDropEffects.Link Then
            Dim filedrop = DirectCast(e.Data.GetData(DataFormats.FileDrop, True), String())
            Dim files As New List(Of String)
            For Each s In filedrop
                If Computer.FileSystem.FileExists(s) AndAlso App.ExtensionDictionary.ContainsKey(IO.Path.GetExtension(s)) Then files.Add(s)
            Next
            If files.Count > 0 Then
                WriteToLog("Queue Drag&Drop Performed (" + files.Count.ToString + " " + IIf(files.Count = 1, "File", "Files").ToString + ")")
                Dim lvi As ListViewItem
                Dim clientpoint = LVQueue.PointToClient(New System.Drawing.Point(e.X, e.Y))
                Dim itemover = LVQueue.GetItemAt(clientpoint.X, clientpoint.Y)
                For x = 0 To files.Count - 1
                    'add new playlist entry
                    If ExtensionDictionary.ContainsKey(Path.GetExtension(files(x))) Then
                        lvi = New ListViewItem
                        lvi.SubItems(0).Text = IO.Path.GetFileNameWithoutExtension(files(x))
                        lvi.SubItems.Add(files(x))
                        App.AddToHistoryFromPlaylist(files(x))
                        If itemover Is Nothing Then
                            LVQueue.Items.Add(lvi)
                        Else
                            LVQueue.Items.Insert(itemover.Index, lvi)
                        End If
                    End If
                Next
                SaveLVToQueue()
                Player.SetPlaylistCountText()
            End If
            files.Clear()
        End If
    End Sub
    Private Sub CMQueue_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMQueue.Opening
        CMIMoveBottom.Enabled = False
        CMIMoveDown.Enabled = False
        CMIMoveTop.Enabled = False
        CMIMoveUp.Enabled = False
        If LVQueue.SelectedItems.Count = 1 Then
            If LVQueue.SelectedItems(0).Index >= 1 Then
                CMIMoveTop.Enabled = True
                CMIMoveUp.Enabled = True
            End If
            If LVQueue.SelectedItems(0).Index <= LVQueue.Items.Count - 2 Then
                CMIMoveBottom.Enabled = True
                CMIMoveDown.Enabled = True
            End If
        End If
        If LVQueue.SelectedItems.Count = 0 Then
            CMIRemove.Text = CMIRemove.Text.TrimEnd(App.TrimEndSearch)
            CMIRemove.Enabled = False
        Else
            CMIRemove.Text = CMIRemove.Text.TrimEnd(App.TrimEndSearch) + " (" + LVQueue.SelectedItems.Count.ToString + ")"
            CMIRemove.Enabled = True
        End If
    End Sub
    Private Sub CMIMoveTop_Click(sender As Object, e As EventArgs) Handles CMIMoveTop.Click
        Dim lvi As ListViewItem = LVQueue.SelectedItems(0)
        LVQueue.Items.RemoveAt(lvi.Index)
        LVQueue.Items.Insert(0, lvi)
        SaveLVToQueue()
    End Sub
    Private Sub CMIMoveUp_Click(sender As Object, e As EventArgs) Handles CMIMoveUp.Click
        Dim lvi As ListViewItem = LVQueue.SelectedItems(0)
        Dim index As Integer = LVQueue.SelectedItems(0).Index
        LVQueue.Items.RemoveAt(index)
        LVQueue.Items.Insert(index - 1, lvi)
        SaveLVToQueue()
    End Sub
    Private Sub CMIMoveDown_Click(sender As Object, e As EventArgs) Handles CMIMoveDown.Click
        Dim lvi As ListViewItem = LVQueue.SelectedItems(0)
        Dim index As Integer = LVQueue.SelectedItems(0).Index
        LVQueue.Items.RemoveAt(index)
        LVQueue.Items.Insert(index + 1, lvi)
        SaveLVToQueue()
    End Sub
    Private Sub CMIMoveBottom_Click(sender As Object, e As EventArgs) Handles CMIMoveBottom.Click
        Dim lvi As ListViewItem = LVQueue.SelectedItems(0)
        LVQueue.Items.RemoveAt(lvi.Index)
        LVQueue.Items.Insert(LVQueue.Items.Count, lvi)
        SaveLVToQueue()
    End Sub
    Private Sub CMIRemove_Click(sender As Object, e As EventArgs) Handles CMIRemove.Click
        RemoveFromQueue()
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
    Private Sub BtnPrune_Click(sender As Object, e As EventArgs) Handles BtnPrune.Click
        Player.PruneQueue()
        Populate()
    End Sub

    'Procedures
    Private Sub Populate()
        LVQueue.Items.Clear()
        For Each s As String In Player.Queue
            Dim playlistlvi As ListViewItem = Player.LVPlaylist.FindItemWithText(s, True, 0)
            Dim lvi As New ListViewItem
            If playlistlvi Is Nothing Then
                lvi.SubItems(0).Text = "Not Found In Playlist"
                lvi.SubItems.Add(s)
            Else
                lvi.SubItems(0).Text = playlistlvi.SubItems(0).Text
                lvi.SubItems.Add(s)
            End If
            LVQueue.Items.Add(lvi)
        Next
        LVQueue.Columns(1).Width = -2
    End Sub
    Private Sub SaveLVToQueue()
        Player.Queue.Clear()
        For Each lvi As ListViewItem In LVQueue.Items
            Player.Queue.Add(lvi.SubItems(1).Text)
        Next
    End Sub
    Private Sub RemoveFromQueue()
        If LVQueue.SelectedItems.Count > 0 Then
            For Each item As ListViewItem In LVQueue.SelectedItems
                Player.RemoveFromQueue(item.SubItems(1).Text)
            Next
            Populate()
        End If
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > My.Computer.Screen.WorkingArea.Right Then location.X = My.Computer.Screen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > My.Computer.Screen.WorkingArea.Bottom Then location.Y = My.Computer.Screen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < My.Computer.Screen.WorkingArea.Left Then location.X = My.Computer.Screen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = My.Computer.Screen.WorkingArea.Top
    End Sub
    Private Sub SetAccentColor()
        Static c As Color
        If App.CurrentTheme.IsAccent Then
            c = App.GetAccentColor()
            BackColor = c
            Debug.Print("Player Accent Color Set")
        End If
    End Sub
    Private Sub SetTheme()
        If Not App.CurrentTheme.IsAccent Then
            BackColor = App.CurrentTheme.BackColor
        End If
        LVQueue.BackColor = App.CurrentTheme.BackColor
        LVQueue.ForeColor = App.CurrentTheme.TextColor
        LVQueue.InsertionLineColor = App.CurrentTheme.TextColor
        TipQueue.BackColor = App.CurrentTheme.BackColor
        TipQueue.ForeColor = App.CurrentTheme.TextColor
        TipQueue.BorderColor = App.CurrentTheme.ButtonBackColor
    End Sub

End Class
