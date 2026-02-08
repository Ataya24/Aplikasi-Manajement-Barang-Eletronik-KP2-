<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CRUD_Product
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CRUD_Product))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.SuppliersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DB_KP2DataSet = New KP2.DB_KP2DataSet()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_productname = New System.Windows.Forms.ComboBox()
        Me.txt_price = New System.Windows.Forms.TextBox()
        Me.txt_addstock = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_delete = New System.Windows.Forms.Button()
        Me.btn_add = New System.Windows.Forms.Button()
        Me.btn_edit = New System.Windows.Forms.Button()
        Me.btn_deleteall = New System.Windows.Forms.Button()
        Me.btn_addstock = New System.Windows.Forms.Button()
        Me.SuppliersTableAdapter = New KP2.DB_KP2DataSetTableAdapters.SuppliersTableAdapter()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SuppliersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DB_KP2DataSet, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Button7.Location = New System.Drawing.Point(30, 716)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(138, 39)
        Me.Button7.TabIndex = 16
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
        Me.Label5.Font = New System.Drawing.Font("Poppins Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(527, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(253, 40)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "CRUD Data Barang"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(241, 189)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(488, 363)
        Me.DataGridView1.TabIndex = 4
        '
        'SuppliersBindingSource
        '
        Me.SuppliersBindingSource.DataMember = "Suppliers"
        Me.SuppliersBindingSource.DataSource = Me.DB_KP2DataSet
        '
        'DB_KP2DataSet
        '
        Me.DB_KP2DataSet.DataSetName = "DB_KP2DataSet"
        Me.DB_KP2DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ListView1
        '
        Me.ListView1.Font = New System.Drawing.Font("Poppins", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(762, 189)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(488, 363)
        Me.ListView1.TabIndex = 5
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(247, 604)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 36)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Product Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(247, 677)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 36)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Price"
        '
        'cb_productname
        '
        Me.cb_productname.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_productname.FormattingEnabled = True
        Me.cb_productname.Location = New System.Drawing.Point(487, 604)
        Me.cb_productname.Name = "cb_productname"
        Me.cb_productname.Size = New System.Drawing.Size(242, 44)
        Me.cb_productname.TabIndex = 10
        '
        'txt_price
        '
        Me.txt_price.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_price.Location = New System.Drawing.Point(487, 677)
        Me.txt_price.Name = "txt_price"
        Me.txt_price.Size = New System.Drawing.Size(242, 37)
        Me.txt_price.TabIndex = 11
        '
        'txt_addstock
        '
        Me.txt_addstock.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_addstock.Location = New System.Drawing.Point(1008, 604)
        Me.txt_addstock.Name = "txt_addstock"
        Me.txt_addstock.Size = New System.Drawing.Size(242, 37)
        Me.txt_addstock.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(768, 604)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 36)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Add Stock"
        '
        'btn_delete
        '
        Me.btn_delete.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_delete.Location = New System.Drawing.Point(1149, 677)
        Me.btn_delete.Name = "btn_delete"
        Me.btn_delete.Size = New System.Drawing.Size(101, 41)
        Me.btn_delete.TabIndex = 16
        Me.btn_delete.Text = "Delete"
        Me.btn_delete.UseVisualStyleBackColor = True
        '
        'btn_add
        '
        Me.btn_add.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_add.Location = New System.Drawing.Point(961, 677)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(101, 41)
        Me.btn_add.TabIndex = 15
        Me.btn_add.Text = "Add"
        Me.btn_add.UseVisualStyleBackColor = True
        '
        'btn_edit
        '
        Me.btn_edit.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_edit.Location = New System.Drawing.Point(774, 677)
        Me.btn_edit.Name = "btn_edit"
        Me.btn_edit.Size = New System.Drawing.Size(101, 41)
        Me.btn_edit.TabIndex = 14
        Me.btn_edit.Text = "Edit"
        Me.btn_edit.UseVisualStyleBackColor = True
        '
        'btn_deleteall
        '
        Me.btn_deleteall.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_deleteall.Location = New System.Drawing.Point(774, 750)
        Me.btn_deleteall.Name = "btn_deleteall"
        Me.btn_deleteall.Size = New System.Drawing.Size(144, 41)
        Me.btn_deleteall.TabIndex = 17
        Me.btn_deleteall.Text = "Delete All"
        Me.btn_deleteall.UseVisualStyleBackColor = True
        '
        'btn_addstock
        '
        Me.btn_addstock.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_addstock.Location = New System.Drawing.Point(1106, 750)
        Me.btn_addstock.Name = "btn_addstock"
        Me.btn_addstock.Size = New System.Drawing.Size(144, 41)
        Me.btn_addstock.TabIndex = 18
        Me.btn_addstock.Text = "Add Stock"
        Me.btn_addstock.UseVisualStyleBackColor = True
        '
        'SuppliersTableAdapter
        '
        Me.SuppliersTableAdapter.ClearBeforeFill = True
        '
        'CRUD_Product
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1280, 890)
        Me.Controls.Add(Me.btn_addstock)
        Me.Controls.Add(Me.btn_deleteall)
        Me.Controls.Add(Me.btn_delete)
        Me.Controls.Add(Me.btn_add)
        Me.Controls.Add(Me.btn_edit)
        Me.Controls.Add(Me.txt_addstock)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_price)
        Me.Controls.Add(Me.cb_productname)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CRUD_Product"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CRUD_Product"
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SuppliersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DB_KP2DataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button7 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cb_productname As ComboBox
    Friend WithEvents txt_price As TextBox
    Friend WithEvents txt_addstock As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_delete As Button
    Friend WithEvents btn_add As Button
    Friend WithEvents btn_edit As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents btn_deleteall As Button
    Friend WithEvents btn_addstock As Button
    Friend WithEvents DB_KP2DataSet As DB_KP2DataSet
    Friend WithEvents SuppliersBindingSource As BindingSource
    Friend WithEvents SuppliersTableAdapter As DB_KP2DataSetTableAdapters.SuppliersTableAdapter
End Class
