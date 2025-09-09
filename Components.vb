
Imports System.ComponentModel

Namespace My.Components

	''' <summary>
	''' Extended Windows progress bar control.
	''' </summary>
	<ToolboxItem(True)>
	<DesignerCategory("Code")>
	<DefaultProperty("PercentageMode")>
	Public Class ProgressEX
		Inherits System.Windows.Forms.UserControl

		'Declarations
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

		'Properties
		''' <summary>
		''' Gets or Sets a value determine how to display Percentage value
		''' </summary>
		<Category("Behavior"), Description("Specify how to display the Percentage value"), DefaultValue(percentageDrawModes.Center)>
		Public Property PercentageMode As percentageDrawModes
			Get
				Return percentageDrawMode
			End Get
			Set
				percentageDrawMode = Value
				'Me.Refresh()
			End Set
		End Property
		''' <summary>
		''' Gets or Sets a value to determine use of a color gradient
		''' </summary>
		<Category("Appearance"), Description("Specify how to display the Drawing Color"), DefaultValue(colorDrawModes.Gradient)>
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
		<Category("Appearance"), Description("Specify the color used to draw the progress activities"), DefaultValue(GetType(Color), "Red")>
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
				_Brush?.Dispose()
				_Brush = New SolidBrush(Value)
				Me.Invalidate(False)
			End Set
		End Property
		''' <summary>
		'''  Gets or sets the maximum value of the range of the control. 
		''' </summary>
		<Category("Layout"), Description("Specify the maximum value the progress can increased to"), DefaultValue(100)>
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
		<Category("Layout"), Description("Specify the minimum value the progress can decreased to"), DefaultValue(0)>
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
		<Category("Layout"), Description("Specify the amount by which a call to the System.Windows.Forms.ProgressBar.StepForword method increases the current position of the progress bar"), DefaultValue(5)>
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
		<Category("Layout"), Description("Specify the current position of the progress bar"), DefaultValue(0)>
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
		<Category("Appearance"), Description("Specify the font used to draw the Percentage value")>
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
		<Category("Appearance"), Description("Specify the color used to draw the Percentage value")>
		Public Overrides Property ForeColor As Color
			Get
				Return writingBrush.Color
			End Get
			Set
				writingBrush.Color = Value
				Me.Invalidate(False)
			End Set
		End Property

		'Events
		''' <summary>
		''' Initialize new instance of the ProgressEx control
		''' </summary>
		Public Sub New()

			'Initialize
			MyBase.New()
			Me.Name = "ProgressEx"
			Me.DoubleBuffered = True
			Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True) 'Cancel Reflection while drawing
			Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True) 'Allow Transparent backcolor
			Me.BackColor = Color.Transparent
			Me.MinimumSize = New Size(50, 5)
			Me.MaximumSize = New Size(Integer.MaxValue, 40)
			Me.Size = New System.Drawing.Size(96, 24)

			'Designer Stuff
			maxValue = 100
			minValue = 0
			stepValue = 5
			percentageDrawMode = percentageDrawModes.Center
			colorDrawMode = colorDrawModes.Gradient

			'ProgressEx Stuff
			m_drawingColor = Color.Red
			gradientBlender = New Drawing2D.ColorBlend() With {
				.Positions = {0.0F, 0.5F, 1.0F},
				.Colors = {ControlPaint.Dark(m_drawingColor), ControlPaint.Light(m_drawingColor), ControlPaint.Dark(m_drawingColor)}}
			writingFont = New Font("Arial", 10, FontStyle.Bold)
			writingBrush = New SolidBrush(Color.Black)

		End Sub
		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Sub Dispose(disposing As Boolean)
			_Brush.Dispose()
			writingBrush.Dispose() 'Release Percentage writer brush
			writingFont.Dispose() 'Release Percentage font
			_Drawer.Dispose()
			MyBase.Dispose(disposing)
		End Sub
		Protected Overrides Sub OnPaint(e As PaintEventArgs)
			MyBase.OnPaint(e)
			If LicenseManager.UsageMode = LicenseUsageMode.Runtime AndAlso _value > 0 Then
				percentageValue = (_value - minValue) / (maxValue - minValue) * 100
				Dim w = CInt((Me.Width - 1) * (_value - minValue) / (maxValue - minValue))
				Select Case colorDrawMode
					Case colorDrawModes.Gradient
						_Drawer.InterpolationColors = gradientBlender
						e.Graphics.FillRectangle(_Drawer, 0, 0, w, Me.Height)
					Case Else
						e.Graphics.FillRectangle(_Brush, 0, 0, w, Me.Height)
				End Select

				If percentageDrawMode <> percentageDrawModes.None Then
					Dim txt = CInt(Math.Truncate(percentageValue)).ToString() & "%"
					Dim sz = e.Graphics.MeasureString(txt, writingFont)
					Debug.Print(percentageDrawMode.ToString)
					Dim x = If(percentageDrawMode = percentageDrawModes.Movable, w, (Me.Width - sz.Width) / 2)
					Dim y = (Me.Height - sz.Height) / 2
					e.Graphics.DrawString(txt, writingFont, writingBrush, x, y)
				End If
			End If
		End Sub
		Protected Overrides Sub OnResize(e As EventArgs)
			MyBase.OnResize(e)
			If LicenseManager.UsageMode = LicenseUsageMode.Runtime Then
				_Drawer?.Dispose()
				_Drawer = New Drawing2D.LinearGradientBrush(New Rectangle(Point.Empty, Me.ClientSize), Color.Black, Color.White, Drawing2D.LinearGradientMode.Vertical)
				If gradientBlender IsNot Nothing Then _Drawer.InterpolationColors = gradientBlender
				Me.Invalidate()
			End If
		End Sub

		'Procedures
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
	<ToolboxItem(True)>
	<DesignerCategory("Code")>
	Public Class ListViewEX
		Inherits ListView

		'Declarations
		Private _LineBefore As Integer = -1
		Private _LineAfter As Integer = -1
		Private _InsertionLineColor As Color = Color.Teal

		'Properties
		<DefaultValue(-1)>
		Public Property LineBefore As Integer
			Get
				Return _LineBefore
			End Get
			Set(ByVal value As Integer)
				_LineBefore = value
			End Set
		End Property
		<DefaultValue(-1)>
		Public Property LineAfter As Integer
			Get
				Return _LineAfter
			End Get
			Set(ByVal value As Integer)
				_LineAfter = value
			End Set
		End Property
		<Category("Appearance"), Description("Specify the color used to draw the Insertion Line"), DefaultValue(GetType(Color), "Color.Teal")>
		Public Property InsertionLineColor As Color
			Get
				Return _InsertionLineColor
			End Get
			Set(ByVal value As Color)
				_InsertionLineColor = value
				Me.Invalidate()
			End Set
		End Property

		'Events
		Public Sub New()
			SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
		End Sub
		Protected Overrides Sub WndProc(ByRef m As Message)
			MyBase.WndProc(m)

			If m.Msg = WinAPI.WM_PAINT AndAlso Me.IsHandleCreated AndAlso Not DesignMode Then
				Using g As Graphics = Graphics.FromHwnd(Me.Handle)
					If LineBefore >= 0 AndAlso LineBefore < Items.Count Then
						Dim rc As Rectangle = Items(LineBefore).GetBounds(ItemBoundsPortion.Label)
						DrawInsertionLine(g, rc.Left, rc.Right, rc.Top)
					End If
					If LineAfter >= 0 AndAlso LineAfter < Items.Count Then
						Dim rc As Rectangle = Items(LineAfter).GetBounds(ItemBoundsPortion.Label)
						DrawInsertionLine(g, rc.Left, rc.Right, rc.Bottom)
					End If
				End Using
			End If
		End Sub

		'Procedures
		Private Sub DrawInsertionLine(g As Graphics, X1 As Integer, X2 As Integer, Y As Integer)
			Using p As New Pen(_InsertionLineColor) With {.Width = 3}
				g.DrawLine(p, X1, Y, X2 - 1, Y)
				Dim leftTriangle As Point() = {New Point(X1, Y - 4), New Point(X1 + 7, Y), New Point(X1, Y + 4)}
				Dim rightTriangle As Point() = {New Point(X2, Y - 4), New Point(X2 - 8, Y), New Point(X2, Y + 4)}
				Using b As New SolidBrush(_InsertionLineColor)
					g.FillPolygon(b, leftTriangle)
					g.FillPolygon(b, rightTriangle)
				End Using
			End Using
		End Sub

	End Class

	''' <summary>
	''' Changes basic .NET label to OPTIONALLY copy on double-click
	''' </summary>
	<ToolboxItem(True)>
	<DesignerCategory("Code")>
	Public Class Label
		Inherits System.Windows.Forms.Label

		'Properties
		<DefaultValue(False)>
		Public Property CopyOnDoubleClick As Boolean

		'Events
		Protected Overrides Sub DefWndProc(ByRef m As Message)
			If LicenseManager.UsageMode = LicenseUsageMode.Runtime Then
				Select Case m.Msg
					Case WinAPI.WM_LBUTTONDBLCLK
						'Suppress default double-click behavior unless explicitly allowed
						If CopyOnDoubleClick Then
							MyBase.DefWndProc(m)
						Else
							m.Result = IntPtr.Zero
						End If
					Case Else
						MyBase.DefWndProc(m)
				End Select
			End If
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
	<ToolboxItem(True)>
	<DesignerCategory("Code")>
	Public Class TextBoxContextMenu
		Inherits System.Windows.Forms.ContextMenuStrip

		'Declarations
		Private WithEvents miUndo As ToolStripMenuItem
		Private WithEvents miCut As ToolStripMenuItem
		Private WithEvents miCopy As ToolStripMenuItem
		Private WithEvents miPaste As ToolStripMenuItem
        Private WithEvents miDelete As ToolStripMenuItem
		Private miSeparatorProperCase As New ToolStripSeparator
		Private WithEvents miProperCase As ToolStripMenuItem
		Private WithEvents miSelectAll As ToolStripMenuItem

		'Properties
		<DefaultValue(False)>
		Public Property ShowExtendedTools As Boolean

		'Events
		Public Sub New()
			MyBase.New
			miUndo = New ToolStripMenuItem("Undo", Resources.ImageEditUndo16, AddressOf UndoClick)
			Me.Items.Add(miUndo)
			Me.Items.Add(New ToolStripSeparator())
			miCut = New ToolStripMenuItem("Cut", Resources.ImageEditCut16, AddressOf CutClick)
			Me.Items.Add(miCut)
			miCopy = New ToolStripMenuItem("Copy", Resources.ImageEditCopy16, AddressOf CopyClick)
			Me.Items.Add(miCopy)
			miPaste = New ToolStripMenuItem("Paste", Resources.ImageEditPaste16, AddressOf PasteClick)
			Me.Items.Add(miPaste)
			miDelete = New ToolStripMenuItem("Delete", Resources.ImageEditDelete16, AddressOf DeleteClick)
			Me.Items.Add(miDelete)
			Me.Items.Add(miSeparatorProperCase)
			miProperCase = New ToolStripMenuItem("Proper Case", Resources.ImageEditProperCase16, AddressOf ProperCaseClick)
			Me.Items.Add(miProperCase)
			Me.Items.Add(New ToolStripSeparator())
			miSelectAll = New ToolStripMenuItem("Select All", Resources.ImageEditSelectAll16, AddressOf SelectAllClick)
			Me.Items.Add(miSelectAll)
		End Sub
		Protected Overrides Sub OnOpening(e As CancelEventArgs)
			MyBase.OnOpening(e)

			Dim txbx As TextBox = TryCast(SourceControl, TextBox)
			If txbx Is Nothing Then Return
			miUndo.Enabled = txbx.CanUndo AndAlso Not txbx.ReadOnly
			miCut.Enabled = txbx.SelectedText.Length > 0 AndAlso Not txbx.ReadOnly
			miCopy.Enabled = txbx.SelectedText.Length > 0
			miPaste.Enabled = Clipboard.ContainsText() AndAlso Not txbx.ReadOnly
			miDelete.Enabled = txbx.SelectedText.Length > 0 AndAlso Not txbx.ReadOnly
			miSeparatorProperCase.Visible = ShowExtendedTools
			miProperCase.Visible = ShowExtendedTools
			miSelectAll.Enabled = txbx.Text.Length > 0 AndAlso txbx.SelectedText.Length < txbx.Text.Length

			If txbx.SelectedText.Length > 0 Then txbx.Focus()

		End Sub
		Public Sub ShortcutKeys(ByRef sender As TextBox, e As PreviewKeyDownEventArgs)
			If e.Control Then
				Select Case e.KeyCode
					Case Keys.A : SelectAll(sender)
					Case Keys.C : Copy(sender)
					Case Keys.D : Delete(sender)
					Case Keys.V : Paste(sender)
					Case Keys.X : Cut(sender)
					Case Keys.Z : Undo(sender)
				End Select
			End If
		End Sub
		Private Sub UndoClick(ByVal sender As Object, ByVal e As EventArgs)
			Undo(TryCast(SourceControl, TextBox))
		End Sub
		Private Sub CutClick(ByVal sender As Object, ByVal e As EventArgs)
			Cut(TryCast(SourceControl, TextBox))
		End Sub
		Private Sub CopyClick(ByVal sender As Object, ByVal e As EventArgs)
			Copy(TryCast(SourceControl, TextBox))
		End Sub
		Private Sub PasteClick(ByVal sender As Object, ByVal e As EventArgs)
			Paste(TryCast(SourceControl, TextBox))
		End Sub
		Private Sub DeleteClick(ByVal sender As Object, ByVal e As EventArgs)
			Delete(TryCast(SourceControl, TextBox))
		End Sub
		Private Sub ProperCaseClick(ByVal sender As Object, ByVal e As EventArgs)
			ProperCase(TryCast(SourceControl, TextBox))
		End Sub
		Private Sub SelectAllClick(ByVal sender As Object, ByVal e As EventArgs)
			SelectAll(TryCast(SourceControl, TextBox))
		End Sub

		'Procedures
		Private Sub Undo(txbx As TextBox)
			txbx.Undo()
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub Cut(txbx As TextBox)
			txbx.Cut()
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub Copy(txbx As TextBox)
			txbx.Copy()
		End Sub
		Private Sub Paste(txbx As TextBox)
			txbx.Paste()
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub Delete(txbx As TextBox)
			If Not txbx.ReadOnly Then
				txbx.SelectedText = String.Empty
				txbx.FindForm()?.Validate()
			End If
		End Sub
		Private Sub ProperCase(txbx As TextBox)
			txbx.Focus()
			txbx.Text = StrConv(txbx.Text, VbStrConv.ProperCase)
			If txbx.FindForm IsNot Nothing Then txbx.FindForm.Validate()
		End Sub
		Private Sub SelectAll(txbx As TextBox)
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
	<ToolboxItem(True)>
	<DesignerCategory("Code")>
	Public Class RichTextBoxContextMenu
		Inherits System.Windows.Forms.ContextMenuStrip

		'Declarations
		Private WithEvents miUndo As ToolStripMenuItem
		Private WithEvents miCut As ToolStripMenuItem
		Private WithEvents miCopy As ToolStripMenuItem
		Private WithEvents miPaste As ToolStripMenuItem
		Private WithEvents miDelete As ToolStripMenuItem
		Private WithEvents miSelectAll As ToolStripMenuItem

		'Events
		Public Sub New()
			MyBase.New
			miUndo = New ToolStripMenuItem("Undo", Resources.ImageEditUndo16, AddressOf UndoClick)
			Me.Items.Add(miUndo)
			Me.Items.Add(New ToolStripSeparator())
			miCut = New ToolStripMenuItem("Cut", Resources.ImageEditCut16, AddressOf CutClick)
			Me.Items.Add(miCut)
			miCopy = New ToolStripMenuItem("Copy", Resources.ImageEditCopy16, AddressOf CopyClick)
			Me.Items.Add(miCopy)
			miPaste = New ToolStripMenuItem("Paste", Resources.ImageEditPaste16, AddressOf PasteClick)
			Me.Items.Add(miPaste)
			miDelete = New ToolStripMenuItem("Delete", Resources.ImageEditDelete16, AddressOf DeleteClick)
			Me.Items.Add(miDelete)
			Me.Items.Add(New ToolStripSeparator())
			miSelectAll = New ToolStripMenuItem("Select All", Resources.ImageEditSelectAll16, AddressOf SelectAllClick)
			Me.Items.Add(miSelectAll)
		End Sub
		Protected Overrides Sub OnOpening(e As CancelEventArgs)
			MyBase.OnOpening(e)
			Dim rtb As RichTextBox = TryCast(SourceControl, RichTextBox)
			If rtb Is Nothing Then Return
			miUndo.Enabled = rtb.CanUndo AndAlso Not rtb.ReadOnly
			miCut.Enabled = rtb.SelectedText.Length > 0 AndAlso Not rtb.ReadOnly
			miCopy.Enabled = rtb.SelectedText.Length > 0
			miPaste.Enabled = Clipboard.ContainsText() AndAlso Not rtb.ReadOnly
			miDelete.Enabled = rtb.SelectedText.Length > 0 AndAlso Not rtb.ReadOnly
			miSelectAll.Enabled = rtb.Text.Length > 0 AndAlso rtb.SelectedText.Length < rtb.Text.Length
			If rtb.SelectedText.Length > 0 Then rtb.Focus()
		End Sub
		'Public Sub Display(ByRef sender As RichTextBox, ByVal e As MouseEventArgs)
		'	If e.Button = MouseButtons.Right Then
		'		rtb = sender
		'		Me.Items(0).Enabled = rtb.CanUndo And Not rtb.ReadOnly
		'		Me.Items(2).Enabled = rtb.SelectedText.Length > 0 And Not rtb.ReadOnly
		'		Me.Items(3).Enabled = rtb.SelectedText.Length > 0
		'		Me.Items(4).Enabled = Clipboard.ContainsText And Not rtb.ReadOnly
		'		Me.Items(5).Enabled = rtb.SelectedText.Length > 0 And Not rtb.ReadOnly
		'		Me.Items(7).Enabled = rtb.Text.Length > 0 And rtb.SelectedText.Length < rtb.Text.Length
		'		If rtb.SelectedText.Length > 0 Then rtb.Focus()
		'		MyBase.Show(rtb, e.Location)
		'	End If
		'End Sub
		Public Sub ShortcutKeys(ByRef sender As RichTextBox, e As PreviewKeyDownEventArgs)
			If e.Control Then
				Select Case e.KeyCode
					Case Keys.A : SelectAll(sender)
					Case Keys.C : Copy(sender)
					Case Keys.D : Delete(sender)
					Case Keys.V : Paste(sender)
					Case Keys.X : Cut(sender)
					Case Keys.Z : Undo(sender)
				End Select
			End If
		End Sub
		Private Sub UndoClick(ByVal sender As Object, ByVal e As EventArgs)
			Undo(TryCast(SourceControl, RichTextBox))
		End Sub
		Private Sub CutClick(ByVal sender As Object, ByVal e As EventArgs)
			Cut(TryCast(SourceControl, RichTextBox))
		End Sub
		Private Sub CopyClick(ByVal sender As Object, ByVal e As EventArgs)
			Copy(TryCast(SourceControl, RichTextBox))
		End Sub
		Private Sub PasteClick(ByVal sender As Object, ByVal e As EventArgs)
			Paste(TryCast(SourceControl, RichTextBox))
		End Sub
		Private Sub DeleteClick(ByVal sender As Object, ByVal e As EventArgs)
			Delete(TryCast(SourceControl, RichTextBox))
		End Sub
		Private Sub SelectAllClick(ByVal sender As Object, ByVal e As EventArgs)
			SelectAll(TryCast(SourceControl, RichTextBox))
		End Sub

		'Procedures
		Private Sub Undo(rtb As RichTextBox)
			rtb.Undo()
			If rtb.FindForm IsNot Nothing Then rtb.FindForm.Validate()
		End Sub
		Private Sub Cut(rtb As RichTextBox)
			rtb.Cut()
			If rtb.FindForm IsNot Nothing Then rtb.FindForm.Validate()
		End Sub
		Private Sub Copy(rtb As RichTextBox)
			rtb.Copy()
		End Sub
		Private Sub Paste(rtb As RichTextBox)
			rtb.Paste()
			If rtb.FindForm IsNot Nothing Then rtb.FindForm.Validate()
		End Sub
		Private Sub Delete(rtb As RichTextBox)
			If Not rtb.ReadOnly Then
				rtb.SelectedText = String.Empty
				rtb.FindForm()?.Validate()
			End If
		End Sub
		Private Sub SelectAll(rtb As RichTextBox)
			rtb.SelectAll()
			rtb.Focus()
		End Sub

	End Class

End Namespace
