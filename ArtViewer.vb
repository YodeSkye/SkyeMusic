
Public Class ArtViewer

    ' Declarations
    Private FadeInTimer As Timer
    Private FadeOutTimer As Timer

    ' Form Events
    Public Sub New(img As Image, Optional loc As Point = Nothing)
        InitializeComponent()
        Text = My.Application.Info.Title & " Art Viewer"
        Opacity = 0
        If img IsNot Nothing Then
            PicBox.Image = img

            ' Clamp size to screen with uniform scale
            Dim screenBounds = Screen.FromPoint(If(loc.IsEmpty, Cursor.Position, loc)).WorkingArea
            Dim scaleW As Double = screenBounds.Width / img.Width
            Dim scaleH As Double = screenBounds.Height / img.Height
            Dim scale As Double = Math.Min(1.0, Math.Min(scaleW, scaleH)) ' shrink if too big

            Dim newWidth As Integer = CInt(img.Width * scale)
            Dim newHeight As Integer = CInt(img.Height * scale)
            ClientSize = New Size(newWidth, newHeight)

            ' Clamp location
            Dim newX = If(loc.IsEmpty, screenBounds.Left + (screenBounds.Width - Width) \ 2, loc.X)
            Dim newY = If(loc.IsEmpty, screenBounds.Top + (screenBounds.Height - Height) \ 2, loc.Y)

            If newX + Width > screenBounds.Right Then newX = screenBounds.Right - Width
            If newY + Height > screenBounds.Bottom Then newY = screenBounds.Bottom - Height
            If newX < screenBounds.Left Then newX = screenBounds.Left
            If newY < screenBounds.Top Then newY = screenBounds.Top

            Location = New Point(newX, newY)
        End If
    End Sub
    Private Sub ArtViewer_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        FadeInTimer = New Timer With {.Interval = 20}
        AddHandler FadeInTimer.Tick, AddressOf FadeInStep
        FadeInTimer.Start()
    End Sub
    Private Sub ArtViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If FadeInTimer IsNot Nothing Then
            FadeInTimer.Stop()
            RemoveHandler FadeInTimer.Tick, AddressOf FadeInStep
            FadeInTimer.Dispose()
            FadeInTimer = Nothing
        End If
        If Opacity > 0 Then
            e.Cancel = True
            FadeOutTimer = New Timer With {.Interval = 20}
            AddHandler FadeOutTimer.Tick, AddressOf FadeOutStep
            FadeOutTimer.Start()
        End If
    End Sub
    Private Sub ArtViewer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If FadeOutTimer IsNot Nothing Then
            FadeOutTimer.Stop()
            RemoveHandler FadeOutTimer.Tick, AddressOf FadeOutStep
            FadeOutTimer.Dispose()
            FadeOutTimer = Nothing
        End If
    End Sub

    ' Control Events
    Private Sub FadeInStep(sender As Object, e As EventArgs)
        If IsDisposed Then
            FadeInTimer.Stop()
        Else
            If Opacity < 1.0 Then
                Opacity += 0.1
            Else
                FadeInTimer.Stop()
                RemoveHandler FadeInTimer.Tick, AddressOf FadeInStep
                FadeInTimer.Dispose()
            End If
        End If
    End Sub
    Private Sub FadeOutStep(sender As Object, e As EventArgs)
        If Opacity > 0 Then
            Opacity -= 0.1
        Else
            FadeOutTimer.Stop()
            RemoveHandler FadeOutTimer.Tick, AddressOf FadeOutStep
            FadeOutTimer.Dispose()
            FadeOutTimer = Nothing
            Dispose()
        End If
    End Sub
    Private Sub PicBox_MouseDown(sender As Object, e As MouseEventArgs) Handles PicBox.MouseDown
        Close()
    End Sub

End Class
