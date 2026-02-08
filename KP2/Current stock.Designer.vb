<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Current_stock
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
        Me.components = New System.ComponentModel.Container()
        Me.DB_KP2DataSet2 = New KP2.DB_KP2DataSet2()
        Me.CRUDProductBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CRUD_ProductTableAdapter = New KP2.DB_KP2DataSet2TableAdapters.CRUD_ProductTableAdapter()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ProductIdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StockDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PriceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btn_back = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.DB_KP2DataSet2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CRUDProductBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DB_KP2DataSet2
        '
        Me.DB_KP2DataSet2.DataSetName = "DB_KP2DataSet2"
        Me.DB_KP2DataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CRUDProductBindingSource
        '
        Me.CRUDProductBindingSource.DataMember = "CRUD_Product"
        Me.CRUDProductBindingSource.DataSource = Me.DB_KP2DataSet2
        '
        'CRUD_ProductTableAdapter
        '
        Me.CRUD_ProductTableAdapter.ClearBeforeFill = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ProductIdDataGridViewTextBoxColumn, Me.ProductNameDataGridViewTextBoxColumn, Me.StockDataGridViewTextBoxColumn, Me.PriceDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.CRUDProductBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(310, 136)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(430, 370)
        Me.DataGridView1.TabIndex = 18
        '
        'ProductIdDataGridViewTextBoxColumn
        '
        Me.ProductIdDataGridViewTextBoxColumn.DataPropertyName = "ProductId"
        Me.ProductIdDataGridViewTextBoxColumn.HeaderText = "ProductId"
        Me.ProductIdDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.ProductIdDataGridViewTextBoxColumn.Name = "ProductIdDataGridViewTextBoxColumn"
        Me.ProductIdDataGridViewTextBoxColumn.Width = 125
        '
        'ProductNameDataGridViewTextBoxColumn
        '
        Me.ProductNameDataGridViewTextBoxColumn.DataPropertyName = "ProductName"
        Me.ProductNameDataGridViewTextBoxColumn.HeaderText = "ProductName"
        Me.ProductNameDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.ProductNameDataGridViewTextBoxColumn.Name = "ProductNameDataGridViewTextBoxColumn"
        Me.ProductNameDataGridViewTextBoxColumn.Width = 125
        '
        'StockDataGridViewTextBoxColumn
        '
        Me.StockDataGridViewTextBoxColumn.DataPropertyName = "Stock"
        Me.StockDataGridViewTextBoxColumn.HeaderText = "Stock"
        Me.StockDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.StockDataGridViewTextBoxColumn.Name = "StockDataGridViewTextBoxColumn"
        Me.StockDataGridViewTextBoxColumn.Width = 125
        '
        'PriceDataGridViewTextBoxColumn
        '
        Me.PriceDataGridViewTextBoxColumn.DataPropertyName = "Price"
        Me.PriceDataGridViewTextBoxColumn.HeaderText = "Price"
        Me.PriceDataGridViewTextBoxColumn.MinimumWidth = 6
        Me.PriceDataGridViewTextBoxColumn.Name = "PriceDataGridViewTextBoxColumn"
        Me.PriceDataGridViewTextBoxColumn.Width = 125
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Poppins", 10.0!)
        Me.Button1.Location = New System.Drawing.Point(680, 536)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 41)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Export As PDF"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btn_back
        '
        Me.btn_back.Font = New System.Drawing.Font("Poppins", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_back.Location = New System.Drawing.Point(43, 537)
        Me.btn_back.Name = "btn_back"
        Me.btn_back.Size = New System.Drawing.Size(119, 38)
        Me.btn_back.TabIndex = 0
        Me.btn_back.Text = "Back"
        Me.btn_back.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Teal
        Me.Panel2.Controls.Add(Me.btn_back)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 600)
        Me.Panel2.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Poppins Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(188, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(274, 40)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Stok Barang Saat Ini"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Teal
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(200, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(650, 100)
        Me.Panel1.TabIndex = 16
        '
        'Current_stock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 36.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(850, 600)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.Name = "Current_stock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Current_stock"
        CType(Me.DB_KP2DataSet2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CRUDProductBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DB_KP2DataSet2 As DB_KP2DataSet2
    Friend WithEvents CRUDProductBindingSource As BindingSource
    Friend WithEvents CRUD_ProductTableAdapter As DB_KP2DataSet2TableAdapters.CRUD_ProductTableAdapter
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ProductIdDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents ProductNameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents StockDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PriceDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
    Friend WithEvents btn_back As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
End Class
