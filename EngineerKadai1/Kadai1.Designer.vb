<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKadai
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmbOpen = New System.Windows.Forms.Button()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.dgvDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmbExit = New System.Windows.Forms.Button()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbOpen
        '
        Me.cmbOpen.Location = New System.Drawing.Point(12, 12)
        Me.cmbOpen.Name = "cmbOpen"
        Me.cmbOpen.Size = New System.Drawing.Size(178, 48)
        Me.cmbOpen.TabIndex = 0
        Me.cmbOpen.Text = "実行"
        Me.cmbOpen.UseVisualStyleBackColor = True
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvDate})
        Me.dgvList.Location = New System.Drawing.Point(12, 66)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.Size = New System.Drawing.Size(387, 309)
        Me.dgvList.TabIndex = 1
        '
        'dgvDate
        '
        Me.dgvDate.HeaderText = "日付"
        Me.dgvDate.Name = "dgvDate"
        '
        'cmbExit
        '
        Me.cmbExit.Location = New System.Drawing.Point(221, 12)
        Me.cmbExit.Name = "cmbExit"
        Me.cmbExit.Size = New System.Drawing.Size(178, 48)
        Me.cmbExit.TabIndex = 0
        Me.cmbExit.Text = "終了"
        Me.cmbExit.UseVisualStyleBackColor = True
        '
        'frmKadai
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 387)
        Me.ControlBox = False
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.cmbExit)
        Me.Controls.Add(Me.cmbOpen)
        Me.MinimumSize = New System.Drawing.Size(425, 389)
        Me.Name = "frmKadai"
        Me.Text = "Kadai1"
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmbOpen As Button
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents cmbExit As Button
    Friend WithEvents dgvDate As DataGridViewTextBoxColumn
End Class
