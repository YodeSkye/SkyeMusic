
Imports System.Runtime.InteropServices
Imports System.Text
Imports Syncfusion.Windows.Forms.Tools

Namespace My.Components

	''' <summary>
	''' Extended Windows progress bar control.
	''' </summary>
	<System.ComponentModel.DefaultProperty("PercentageMode")>
	Friend Class ProgressEX
		Inherits System.Windows.Forms.UserControl
		''' <summary>
		''' Specifies how the percentage value should be drawn
		''' </summary>
		Public Enum percentageDrawModes As Integer
			None = 0 'No Percentage shown
			Center 'Percentage alwayes centered
			Movable 'Percentage moved with the progress activities
		End Enum
		Public Enum colorDrawModes As Integer
			Gradient = 0
			Smooth
		End Enum
		Private maxValue As Integer 'Maximum value
		Private minValue As Integer 'Minimum value
		Private _value As Single 'Value property value
		Private stepValue As Integer 'Step value
		Private percentageValue As Single 'Percent value
		Private drawingWidth As Integer 'Drawing width according to the logical Value property
		Private m_drawingColor As Color 'Color used for drawing activities
		Private gradientBlender As Drawing2D.ColorBlend 'Color mixer object
		Private percentageDrawMode As percentageDrawModes 'Percent Drawing type
		Private colorDrawMode As colorDrawModes
		Private _Brush As SolidBrush
		Private writingBrush As SolidBrush 'Percent writing brush
		Private writingFont As Font 'Font to write Percent with
		Private _Drawer As Drawing2D.LinearGradientBrush
		''' <summary>
		''' Gets or Sets a value determine how to display Percentage value
		''' </summary>
		<System.ComponentModel.Category("Behavior"), System.ComponentModel.Description("Specify how to display the Percentage value")>
		Public Property PercentageMode As percentageDrawModes
			Get
				Return percentageDrawMode
			End Get
			Set
				percentageDrawMode = Value
				Me.Refresh()
			End Set
		End Property
		''' <summary>
		''' Gets or Sets a value to determine use of a color gradient
		''' </summary>
		<System.ComponentModel.Category("Appearance"), System.ComponentModel.Description("Specify how to display the Drawing Color")>
		Public Property DrawingColorMode As colorDrawModes
			Get
				Return colorDrawMode
			End Get
			Set
				colorDrawMode = Value
				Me.Refresh()
			End Set
		End Property
		''' <summary>
		''' Gets or Sets the color used to draw the Progress activities
		''' </summary>
		<System.ComponentModel.Category("Appearance"), System.ComponentModel.Description("Specify the color used to draw the progress activities")>
		Public Property DrawingColor As Color
			Get
				Return m_drawingColor
			End Get
			Set
				'If assigned then remix the colors used for gradient display
				m_drawingColor = Value
				gradientBlender.Colors(0) = ControlPaint.Dark(Value)
				gradientBlender.Colors(1) = ControlPaint.Light(Value)
				gradientBlender.Colors(2) = ControlPaint.Dark(Value)
				_Brush = New SolidBrush(Value)
				Me.Refresh()
			End Set
		End Property
		''' <summary>
		'''  Gets or sets the maximum value of the range of the control. 
		''' </summary>
		<System.ComponentModel.Category("Layout"), System.ComponentModel.Description("Specify the maximum value the progress can increased to")>
		Public Property Maximum As Integer
			Get
				Return maxValue
			End Get
			Set
				maxValue = Value
				Me.Refresh()
			End Set
		End Property
		''' <summary>
		''' Gets or sets the minimum value of the range of the control.
		''' </summary>
		<System.ComponentModel.Category("Layout")>
		Public Property Minimum As Integer
			Get
				Return minValue
			End Get
			Set
				minValue = Value
				Me.Refresh()
			End Set
		End Property
		''' <summary>
		'''  Gets or sets the amount by which a call to the System.Windows.Forms.ProgressBar.
		'''  StepForword method increases the current position of the progress bar.
		''' </summary>
		<System.ComponentModel.Category("Layout")>
		Public Property [Step] As Integer
			Get
				Return stepValue
			End Get
			Set
				stepValue = Value
				Me.Refresh()
			End Set
		End Property
		''' <summary>
		''' Gets or sets the current position of the progress bar. 
		''' </summary>
		''' <exception cref="System.ArgumentException">The value specified is greater than the value of
		''' the System.Windows.Forms.ProgressBar.Maximum property.  -or- The value specified is less
		''' than the value of the System.Windows.Forms.ProgressBar.Minimum property</exception>
		<System.ComponentModel.Category("Layout")>
		Public Property Value As Integer
			Get
				Return CInt(Math.Truncate(_value))
			End Get
			Set
				'Protect the value and refuse any invalid values
				'Here we may just handle invalid values and dont bother the client by exceptions
				If Value > maxValue Or Value < minValue Then
					Throw New ArgumentException("Invalid value used")
				End If
				_value = Value
				Me.Refresh()
			End Set
		End Property
		''' <summary>
		''' Gets the Percent value the Progress activities reached
		''' </summary>
		Public ReadOnly Property Percent As Integer
			Get
				Return CInt(Math.Truncate(Math.Round(percentageValue))) 'Its float value, so to be accurate round it then return
			End Get
		End Property
		'This property exist in the parent, overide it for our own good
		''' <summary>
		''' Gets or Sets the color used to draw the Precentage value
		''' </summary>
		<System.ComponentModel.Category("Appearance"), System.ComponentModel.Description("Specify the font used to draw the Percentage value")>
		Public Overrides Property Font As Font
			Get
				Return writingFont
			End Get
			Set
				writingFont = Value
				Me.Invalidate(False)
			End Set
		End Property
		'This property exist in the parent, overide it for our own good
		''' <summary>
		''' Gets or Sets the color used to draw the Precentage value
		''' </summary>
		<System.ComponentModel.Category("Appearance"), System.ComponentModel.Description("Specify the color used to draw the Percentage value")>
		Public Overrides Property ForeColor As Color
			Get
				Return writingBrush.Color
			End Get
			Set
				writingBrush.Color = Value
				Me.Invalidate(False)
			End Set
		End Property

		''' <summary>
		''' Initialize new instance of the ProgressEx control
		''' </summary>
		Public Sub New()
			'Initialize
			Me.BackColor = System.Drawing.SystemColors.Control
			Me.Name = "ProgressEx"
			Me.Size = New System.Drawing.Size(96, 24)
			AddHandler Me.Resize, New System.EventHandler(AddressOf Me.ProgressEXResize)
			AddHandler Me.Paint, New System.Windows.Forms.PaintEventHandler(AddressOf Me.ProgressEXPaint)
			'Designer Stuff
			maxValue = Me.Width
			minValue = 0
			stepValue = 1
			percentageDrawMode = percentageDrawModes.Center
			'ProgressEx Stuff
			gradientBlender = New Drawing2D.ColorBlend(3) 'Color Mixer contain 3 colors for (top, center, bottom)
			gradientBlender.Positions = New Single() {0F, 0.5F, 1.0F} 'Position of mixing pints is (top, middle, bottom)
			DrawingColor = Color.Blue
			writingBrush = New SolidBrush(Color.Black)
			writingFont = New Font("Arial", 10, FontStyle.Bold)
			_Drawer = New Drawing2D.LinearGradientBrush(Me.ClientRectangle, Color.Black, Color.White, Drawing2D.LinearGradientMode.Vertical)
			Me.SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.DoubleBuffer, True) 'Cancel Reflection while drawing
			Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True) 'Allow Transparent backcolor
			Me.BackColor = Color.Transparent
		End Sub
		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Sub Dispose(disposing As Boolean)
			_Brush.Dispose()
			writingBrush.Dispose() 'Release Percentage writer brush
			writingFont.Dispose() 'Release Percentage font
			MyBase.Dispose(disposing)
		End Sub
		Private Sub ProgressEXPaint(sender As Object, e As System.Windows.Forms.PaintEventArgs)
			'Why draw outside the control !! ?
			If _value <> 0 And _value <= maxValue Then
				percentageValue = (_value / maxValue) * 100 'Calculate the Percentage according to the logical value reached
				drawingWidth = CInt(Math.Truncate(Me.Width * _value)) \ (maxValue - minValue) 'Calculate the right side of drawing edge
				Select Case Me.DrawingColorMode 'Now we ready to draw, so do the actual drawing
					Case colorDrawModes.Gradient
						_Drawer.InterpolationColors = gradientBlender 'Tie our color mixer with the just created brush
						e.Graphics.FillRectangle(_Drawer, 0, 0, drawingWidth, Me.Height)
					Case colorDrawModes.Smooth : e.Graphics.FillRectangle(_Brush, 0, 0, drawingWidth, Me.Height)
				End Select
				'Prepare for Percentage writing only when required
				If percentageDrawMode <> percentageDrawModes.None Then
					Dim st As String = CInt(Math.Truncate(percentageValue)).ToString() & "%"
					Dim s As SizeF = e.Graphics.MeasureString(st, writingFont) 'Calculate Percentage rectangle size
					If percentageDrawMode = percentageDrawModes.Movable Then
						e.Graphics.DrawString(st, writingFont, writingBrush, drawingWidth, (e.ClipRectangle.Height \ 2 - s.Height / 2))
					ElseIf percentageDrawMode = percentageDrawModes.Center Then
						e.Graphics.DrawString(st, writingFont, writingBrush, New PointF((e.ClipRectangle.Width \ 2 - s.Width / 2), (e.ClipRectangle.Height \ 2 - s.Height / 2)))
					End If
				End If
			End If
		End Sub
		Private Sub ProgressEXResize(sender As Object, e As System.EventArgs)
			If Me.Height > 40 Then
				Me.Height = 40
			End If
			'Prevent a Progress shorter than its own percentag
			'used to be set to 15
			If Me.Height < 5 Then
				Me.Height = 5
			End If
			'Prevent a Progress smaller than its own percentage
			If Me.Width < 50 Then
				Me.Width = 50
			End If
			Me.Refresh()
		End Sub
		''' <summary>
		''' Increment the progress one step
		''' </summary>
		Public Sub StepForward()
			If (_value + stepValue) < maxValue Then
				'If valid increment the value by step size
				_value += stepValue
				Me.Refresh()
			Else
				'If not dont exceed the maximum allowed
				_value = maxValue
				Me.Refresh()
			End If
		End Sub
		''' <summary>
		''' Decrement the progress one step
		''' </summary>
		Public Sub StepBackward()
			If (_value - stepValue) > minValue Then
				'If valid decrement the value by step size
				_value -= stepValue
				Me.Refresh()
			Else
				'If not dont exceed the minimum allowed
				_value = minValue
				Me.Refresh()
			End If
		End Sub
	End Class

	''' <summary>
	''' Extended Listview control with Insertion Line for drag/drop operations.
	''' </summary>
	Friend Class ListViewEX

		Inherits ListView

		Private _LineBefore As Integer = -1
		Private _LineAfter As Integer = -1
		Private _InsertionLineColor As Color = Color.Teal

		Public Property LineBefore As Integer
			Get
				Return _LineBefore
			End Get
			Set(ByVal value As Integer)
				_LineBefore = value
			End Set
		End Property
		Public Property LineAfter As Integer
			Get
				Return _LineAfter
			End Get
			Set(ByVal value As Integer)
				_LineAfter = value
			End Set
		End Property
		<ComponentModel.Category("Appearance"), ComponentModel.Description("Specify the color used to draw the Insertion Line")>
		Public Property InsertionLineColor As Color
			Get
				Return _InsertionLineColor
			End Get
			Set(ByVal value As Color)
				_InsertionLineColor = value
			End Set
		End Property

		Public Sub New()
			SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
		End Sub
		Protected Overrides Sub WndProc(ByRef m As Message)
			MyBase.WndProc(m)
			If m.Msg = WinAPI.WM_PAINT And m.HWnd = Handle Then
				If LineBefore >= 0 AndAlso LineBefore < Items.Count Then
					Dim rc As Rectangle = Items(LineBefore).GetBounds(ItemBoundsPortion.Label)
					DrawInsertionLine(rc.Left, rc.Right, rc.Top)
				End If
				If LineAfter >= 0 AndAlso LineBefore < Items.Count Then
					Dim rc As Rectangle = Items(LineAfter).GetBounds(ItemBoundsPortion.Label)
					DrawInsertionLine(rc.Left, rc.Right, rc.Bottom)
				End If
			End If
		End Sub
		Private Sub DrawInsertionLine(ByVal X1 As Integer, ByVal X2 As Integer, ByVal Y As Integer)
			Using g As Graphics = Me.CreateGraphics()
				Dim p As New Pen(_InsertionLineColor)
				p.Width = 3
				g.DrawLine(p, X1, Y, X2 - 1, Y)
				Dim leftTriangle As Point() = New Point(2) {New Point(X1, Y - 4), New Point(X1 + 7, Y), New Point(X1, Y + 4)}
				Dim rightTriangle As Point() = New Point(2) {New Point(X2, Y - 4), New Point(X2 - 8, Y), New Point(X2, Y + 4)}
				Dim b As New SolidBrush(_InsertionLineColor)
				g.FillPolygon(b, leftTriangle)
				g.FillPolygon(b, rightTriangle)
				b.Dispose()
				p.Dispose()
			End Using
		End Sub

	End Class

	''' <summary>
	''' Changes basic .NET label to OPTIONALLY copy on double-click
	''' </summary>
	Public Class LabelCSY

		Inherits System.Windows.Forms.Label
		Public Property CopyOnDoubleClick As Boolean
		Protected Overrides Sub DefWndProc(ByRef m As Message)
			Select Case m.Msg
				Case WinAPI.WM_LBUTTONDBLCLK
					If CopyOnDoubleClick Then
						MyBase.DefWndProc(m)
					Else
						m.Result = 0
					End If
				Case Else
					MyBase.DefWndProc(m)
			End Select
		End Sub

	End Class

	''' <summary>
	''' Simple, Old-Style Context Menu for TextBoxes.
	''' Contains everything needed for basic Cut & Paste functionality from ContextMenu & Keyboard.
	''' 1.	Must create New Instance of TextBoxContextMenu either in Designer or Programmatically.
	''' 2.	Handle MouseUpEvent on TextBox & call Display Function.
	''' 3.	Handle PreviewKeyDownEvent on TextBox & call ShortcutKeys Function.
	''' 4.	Disable all built in contextmenus. !Does Not Work by assigning to ContextMenu Property of TextBox!
	''' 5.	Set ShortcutsEnabled property to False.
	''' </summary>
	Friend Class TextBoxContextMenu
		Inherits System.Windows.Forms.ContextMenuStrip

		'Declarations
		Public Property ShowExtendedTools As Boolean = False
		Private txbx As TextBox

		'Control Events
		Public Sub New()
			MyBase.New
			Me.Items.Add("Undo", Nothing, AddressOf UndoClick)
			Me.Items.Add("-")
			Me.Items.Add("Cut", Nothing, AddressOf CutClick)
			Me.Items.Add("Copy", Nothing, AddressOf CopyClick)
			Me.Items.Add("Paste", Nothing, AddressOf PasteClick)
			Me.Items.Add("Delete", Nothing, AddressOf DeleteClick)
			Me.Items.Add("-")
			Me.Items.Add("Proper Case", Nothing, AddressOf ProperCaseClick)
			Me.Items.Add("-")
			Me.Items.Add("Select All", Nothing, AddressOf SelectAllClick)
			Me.Items(0).Image = Resources.ImageEditUndo16
			Me.Items(2).Image = Resources.ImageEditCut16
			Me.Items(3).Image = Resources.ImageEditCopy16
			Me.Items(4).Image = Resources.ImageEditPaste16
			Me.Items(5).Image = Resources.ImageEditDelete16
			Me.Items(7).Image = Resources.ImageEditSelectAll16
			Me.Items(9).Image = Resources.ImageEditSelectAll16
		End Sub
		Friend Sub Display(ByRef sender As TextBox, ByVal e As MouseEventArgs)
			If e.Button = MouseButtons.Right Then
				txbx = sender
				Me.Items(0).Enabled = txbx.CanUndo
				Me.Items(2).Enabled = txbx.SelectedText.Length > 0 And Not txbx.ReadOnly
				Me.Items(3).Enabled = txbx.SelectedText.Length > 0
				Me.Items(4).Enabled = Clipboard.ContainsText And Not txbx.ReadOnly
				Me.Items(5).Enabled = txbx.SelectedText.Length > 0 And Not txbx.ReadOnly
				Me.Items(7).Enabled = txbx.Text.Length > 0
				If ShowExtendedTools Then
					Me.Items(6).Visible = True
					Me.Items(7).Visible = True
				Else
					Me.Items(6).Visible = False
					Me.Items(7).Visible = False
				End If
				Me.Items(9).Enabled = txbx.Text.Length > 0 And txbx.SelectedText.Length < txbx.Text.Length
				If txbx.SelectedText.Length > 0 Then txbx.Focus()
				MyBase.Show(txbx, e.Location)
			End If
		End Sub
		Friend Sub ShortcutKeys(ByRef sender As TextBox, e As PreviewKeyDownEventArgs)
			txbx = sender
			If e.Control Then
				Select Case e.KeyCode
					Case Keys.A : SelectAll()
					Case Keys.C : Copy()
					Case Keys.D : Delete()
					Case Keys.V : Paste()
					Case Keys.X : Cut()
					Case Keys.Z : Undo()
				End Select
			End If
			txbx = Nothing
		End Sub
		Private Sub UndoClick(ByVal sender As Object, ByVal e As EventArgs)
			Undo()
			txbx = Nothing
		End Sub
		Private Sub CutClick(ByVal sender As Object, ByVal e As EventArgs)
			Cut()
			txbx = Nothing
		End Sub
		Private Sub CopyClick(ByVal sender As Object, ByVal e As EventArgs)
			Copy()
			txbx = Nothing
		End Sub
		Private Sub PasteClick(ByVal sender As Object, ByVal e As EventArgs)
			Paste()
			txbx = Nothing
		End Sub
		Private Sub DeleteClick(ByVal sender As Object, ByVal e As EventArgs)
			Delete()
			txbx = Nothing
		End Sub
		Private Sub ProperCaseClick(ByVal sender As Object, ByVal e As EventArgs)
			ProperCase()
			txbx = Nothing
		End Sub
		Private Sub SelectAllClick(ByVal sender As Object, ByVal e As EventArgs)
			SelectAll()
			txbx = Nothing
		End Sub

		'Procedures
		Private Sub Undo()
			txbx.Undo()
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub Cut()
			txbx.Cut()
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub Copy()
			txbx.Copy()
		End Sub
		Private Sub Paste()
			txbx.Paste()
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub Delete()
			If Not txbx.ReadOnly Then
				Dim pos As Integer = txbx.SelectionStart
				txbx.Text = txbx.Text.Substring(0, pos) + txbx.Text.Substring(pos + txbx.SelectionLength)
				txbx.SelectionStart = pos
				txbx.SelectionLength = 0
				pos = Nothing
			End If
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub ProperCase()
			txbx.Focus()
			txbx.Text = StrConv(txbx.Text, VbStrConv.ProperCase)
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub SelectAll()
			txbx.SelectAll()
			txbx.Focus()
		End Sub
	End Class

	''' <summary>
	''' Simple, Old-Style Context Menu for RichTextBoxes.
	''' Contains everything needed for basic Cut & Paste functionality from ContextMenu & Keyboard.
	''' 1.	Must create New Instance of RichTextBoxContextMenu either in Designer or Programmatically.
	''' 2.	Handle MouseUpEvent on RichTextBox & call Display Function.
	''' 3.	Handle PreviewKeyDownEvent on RichTextBox & call ShortcutKeys Function.
	''' 4.	Disable all built in contextmenus. !Does Not Work by assigning to ContextMenu Property of RichTextBox!
	''' 5.	Set ShortcutsEnabled property to False.
	''' </summary>
	Friend Class RichTextBoxContextMenu

		Inherits System.Windows.Forms.ContextMenuStrip

		'Declarations
		Private rtb As RichTextBox

		'Control Events
		Public Sub New()
			MyBase.New
			Me.Items.Add("Undo", Nothing, AddressOf UndoClick)
			Me.Items.Add("-")
			Me.Items.Add("Cut", Nothing, AddressOf CutClick)
			Me.Items.Add("Copy", Nothing, AddressOf CopyClick)
			Me.Items.Add("Paste", Nothing, AddressOf PasteClick)
			Me.Items.Add("Delete", Nothing, AddressOf DeleteClick)
			Me.Items.Add("-")
			Me.Items.Add("Select All", Nothing, AddressOf SelectAllClick)
			Me.Items(0).Image = My.Resources.ImageEditUndo16
			Me.Items(2).Image = My.Resources.ImageEditCut16
			Me.Items(3).Image = My.Resources.ImageEditCopy16
			Me.Items(4).Image = My.Resources.ImageEditPaste16
			Me.Items(5).Image = My.Resources.ImageEditDelete16
			Me.Items(7).Image = My.Resources.ImageEditSelectAll16
		End Sub
		Friend Sub Display(ByRef sender As RichTextBox, ByVal e As MouseEventArgs)
			If e.Button = MouseButtons.Right Then
				rtb = sender
				Me.Items(0).Enabled = rtb.CanUndo And Not rtb.ReadOnly
				Me.Items(2).Enabled = rtb.SelectedText.Length > 0 And Not rtb.ReadOnly
				Me.Items(3).Enabled = rtb.SelectedText.Length > 0
				Me.Items(4).Enabled = Clipboard.ContainsText And Not rtb.ReadOnly
				Me.Items(5).Enabled = rtb.SelectedText.Length > 0 And Not rtb.ReadOnly
				Me.Items(7).Enabled = rtb.Text.Length > 0 And rtb.SelectedText.Length < rtb.Text.Length
				If rtb.SelectedText.Length > 0 Then rtb.Focus()
				MyBase.Show(rtb, e.Location)
			End If
		End Sub
		Friend Sub ShortcutKeys(ByRef sender As RichTextBox, e As PreviewKeyDownEventArgs)
			rtb = sender
			If e.Control Then
				Select Case e.KeyCode
					Case Keys.A : SelectAll()
					Case Keys.C : Copy()
					Case Keys.D : Delete()
					Case Keys.V : Paste()
					Case Keys.X : Cut()
					Case Keys.Z : Undo()
				End Select
			End If
			rtb = Nothing
		End Sub
		Private Sub UndoClick(ByVal sender As Object, ByVal e As EventArgs)
			Undo()
			rtb = Nothing
		End Sub
		Private Sub CutClick(ByVal sender As Object, ByVal e As EventArgs)
			Cut()
			rtb = Nothing
		End Sub
		Private Sub CopyClick(ByVal sender As Object, ByVal e As EventArgs)
			Copy()
			rtb = Nothing
		End Sub
		Private Sub PasteClick(ByVal sender As Object, ByVal e As EventArgs)
			Paste()
			rtb = Nothing
		End Sub
		Private Sub DeleteClick(ByVal sender As Object, ByVal e As EventArgs)
			Delete()
			rtb = Nothing
		End Sub
		Private Sub SelectAllClick(ByVal sender As Object, ByVal e As EventArgs)
			SelectAll()
			rtb = Nothing
		End Sub

		'Procedures
		Private Sub Undo()
			rtb.Undo()
			If rtb.FindForm IsNot Nothing Then rtb.FindForm.Validate()
		End Sub
		Private Sub Cut()
			rtb.Cut()
			If rtb.FindForm IsNot Nothing Then rtb.FindForm.Validate()
		End Sub
		Private Sub Copy()
			rtb.Copy()
		End Sub
		Private Sub Paste()
			rtb.Paste()
			If rtb.FindForm IsNot Nothing Then rtb.FindForm.Validate()
		End Sub
		Private Sub Delete()
			If Not rtb.ReadOnly Then
				Dim pos As Integer = rtb.SelectionStart
				rtb.Text = rtb.Text.Substring(0, pos) + rtb.Text.Substring(pos + rtb.SelectionLength)
				rtb.SelectionStart = pos
				rtb.SelectionLength = 0
				pos = Nothing
			End If
			If rtb.FindForm IsNot Nothing Then rtb.FindForm.Validate()
		End Sub
		Private Sub SelectAll()
			rtb.SelectAll()
			rtb.Focus()
		End Sub

	End Class

End Namespace