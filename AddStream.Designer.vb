<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddStream
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddStream))
        BtnOK = New Button()
        TxtBoxStreamName = New TextBox()
        TxtBoxStreamPath = New TextBox()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        BtnOK.Location = New Point(211, 90)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 8
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxStreamName
        ' 
        TxtBoxStreamName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxStreamName.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxStreamName.Location = New Point(12, 12)
        TxtBoxStreamName.Name = "TxtBoxStreamName"
        TxtBoxStreamName.Size = New Size(463, 29)
        TxtBoxStreamName.TabIndex = 4
        ' 
        ' TxtBoxStreamPath
        ' 
        TxtBoxStreamPath.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxStreamPath.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TxtBoxStreamPath.Location = New Point(12, 47)
        TxtBoxStreamPath.Name = "TxtBoxStreamPath"
        TxtBoxStreamPath.Size = New Size(463, 29)
        TxtBoxStreamPath.TabIndex = 5
        ' 
        ' AddStream
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(487, 166)
        Controls.Add(TxtBoxStreamPath)
        Controls.Add(TxtBoxStreamName)
        Controls.Add(BtnOK)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AddStream"
        Text = "Add Stream"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents TxtBoxStreamName As TextBox
    Friend WithEvents TxtBoxStreamPath As TextBox
End Class
