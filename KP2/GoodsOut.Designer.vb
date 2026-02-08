<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GoodsOut
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GoodsOut))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbx_product = New System.Windows.Forms.ComboBox()
        Me.txt_quantity = New System.Windows.Forms.TextBox()
        Me.cbx_reason = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.lv_goodsout = New System.Windows.Forms.ListView()
        Me.btn_export = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Teal
        Me.Panel2.Controls.Add(Me.Button7)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 100)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 790)
        Me.Panel2.TabIndex = 3
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Poppins", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(29, 730)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(138, 39)
        Me.Button7.TabIndex = 15
        Me.Button7.Text = "Back"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Teal
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1280, 100)
        Me.Panel1.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(636, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(179, 29)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Barang Keluar"
        '
        'cbx_product
        '
        Me.cbx_product.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbx_product.FormattingEnabled = True
        Me.cbx_product.Location = New System.Drawing.Point(467, 294)
        Me.cbx_product.Name = "cbx_product"
        Me.cbx_product.Size = New System.Drawing.Size(234, 44)
        Me.cbx_product.TabIndex = 4
        '
        'txt_quantity
        '
        Me.txt_quantity.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_quantity.Location = New System.Drawing.Point(467, 364)
        Me.txt_quantity.Name = "txt_quantity"
        Me.txt_quantity.Size = New System.Drawing.Size(234, 37)
        Me.txt_quantity.TabIndex = 5
        '
        'cbx_reason
        '
        Me.cbx_reason.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbx_reason.FormattingEnabled = True
        Me.cbx_reason.Location = New System.Drawing.Point(467, 430)
        Me.cbx_reason.Name = "cbx_reason"
        Me.cbx_reason.Size = New System.Drawing.Size(234, 44)
        Me.cbx_reason.TabIndex = 6
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Location = New System.Drawing.Point(467, 503)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(234, 37)
        Me.DateTimePicker1.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(272, 296)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(159, 36)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Product Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(272, 365)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 36)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Quantity"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(272, 433)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 36)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Reason"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(272, 504)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 36)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Date"
        '
        'btn_save
        '
        Me.btn_save.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.Location = New System.Drawing.Point(278, 653)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(101, 41)
        Me.btn_save.TabIndex = 12
        Me.btn_save.Text = "Save"
        Me.btn_save.UseVisualStyleBackColor = True
        '
        'lv_goodsout
        '
        Me.lv_goodsout.HideSelection = False
        Me.lv_goodsout.Location = New System.Drawing.Point(779, 294)
        Me.lv_goodsout.Name = "lv_goodsout"
        Me.lv_goodsout.Size = New System.Drawing.Size(453, 400)
        Me.lv_goodsout.TabIndex = 13
        Me.lv_goodsout.UseCompatibleStateImageBehavior = False
        '
        'btn_export
        '
        Me.btn_export.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_export.Location = New System.Drawing.Point(524, 653)
        Me.btn_export.Name = "btn_export"
        Me.btn_export.Size = New System.Drawing.Size(177, 41)
        Me.btn_export.TabIndex = 14
        Me.btn_export.Text = "Export As PDF"
        Me.btn_export.UseVisualStyleBackColor = True
        '
        'GoodsOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1280, 890)
        Me.Controls.Add(Me.btn_export)
        Me.Controls.Add(Me.lv_goodsout)
        Me.Controls.Add(Me.btn_save)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.cbx_reason)
        Me.Controls.Add(Me.txt_quantity)
        Me.Controls.Add(Me.cbx_product)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GoodsOut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GoodsOut"
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cbx_product As ComboBox
    Friend WithEvents txt_quantity As TextBox
    Friend WithEvents cbx_reason As ComboBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_save As Button
    Friend WithEvents lv_goodsout As ListView
    Friend WithEvents Button7 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents btn_export As Button
End Class
