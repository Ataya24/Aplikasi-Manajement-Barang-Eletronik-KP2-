Imports System.Data.SqlClient

Public Class CRUD_Product
    Private ReadOnly connectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=KP2;Integrated Security=True"

    Private Sub CRUD_Product_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllData()
    End Sub

    Private Sub CRUD_Product_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ' Refresh all data when form becomes active to show latest changes
        LoadAllData()
    End Sub

    ' Fungsi utama untuk memuat semua data
    Private Sub LoadAllData()
        LoadGoodsInComboBox()
        LoadListView()
        LoadSuppliersToGrid()
    End Sub

    ' Memuat data Suppliers ke DataGridView
    Private Sub LoadSuppliersToGrid()
        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand("SELECT IdProduct, TypeProduct, Quantity, Categories, Date FROM Suppliers", conn)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    DataGridView1.DataSource = dt

                    ' Format kolom
                    DataGridView1.Columns("IdProduct").HeaderText = "ID Produk"
                    DataGridView1.Columns("TypeProduct").HeaderText = "Nama Produk"
                    DataGridView1.Columns("Quantity").HeaderText = "Stok"
                    DataGridView1.Columns("Categories").HeaderText = "Kategori"
                    DataGridView1.Columns("Date").HeaderText = "Tanggal"
                End Using
            End Using
        Catch ex As Exception
            ShowError("Error loading suppliers: " & ex.Message)
        End Try
    End Sub

    ' Memuat data ke ComboBox
    Private Sub LoadGoodsInComboBox()
        cb_productname.Items.Clear()
        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand("SELECT DISTINCT TypeProduct FROM Suppliers", conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            cb_productname.Items.Add(reader("TypeProduct").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ShowError("Error loading products: " & ex.Message)
        End Try
    End Sub

    ' Memuat data ke ListView
    Private Sub LoadListView()
        ListView1.Items.Clear()
        ListView1.View = View.Details
        ListView1.Columns.Clear()

        ' Tambahkan kolom
        ListView1.Columns.Add("ProductId", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("ProductName", 150, HorizontalAlignment.Center)
        ListView1.Columns.Add("Stock", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Price", 100, HorizontalAlignment.Center)

        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand("SELECT ProductId, ProductName, Stock, Price FROM CRUD_Product", conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New ListViewItem(reader("ProductId").ToString())
                            item.SubItems.Add(reader("ProductName").ToString())
                            item.SubItems.Add(reader("Stock").ToString())
                            item.SubItems.Add(If(IsDBNull(reader("Price")), "", reader("Price").ToString()))
                            ListView1.Items.Add(item)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ShowError("Error loading products: " & ex.Message)
        End Try
    End Sub

    ' Fungsi untuk menambahkan produk baru
    Private Sub AddProduct()
        If String.IsNullOrEmpty(cb_productname.Text) Then
            ShowWarning("Nama produk harus diisi!")
            Return
        End If

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()

                ' Cek duplikasi produk
                Using cmdCheck As New SqlCommand("SELECT COUNT(*) FROM CRUD_Product WHERE ProductName = @ProductName", conn)
                    cmdCheck.Parameters.AddWithValue("@ProductName", cb_productname.Text)
                    If CInt(cmdCheck.ExecuteScalar()) > 0 Then
                        ShowWarning("Produk sudah ada di daftar!")
                        Return
                    End If
                End Using

                ' Generate ID baru
                Dim newId = GenerateProductId()

                ' Ambil stok awal dari Suppliers
                Dim initialStock As Integer = GetInitialStock(cb_productname.Text)

                ' Tambahkan produk baru
                Using cmd As New SqlCommand("INSERT INTO CRUD_Product (ProductId, ProductName, Stock, Price) VALUES (@ProductId, @ProductName, @Stock, @Price)", conn)
                    cmd.Parameters.AddWithValue("@ProductId", newId)
                    cmd.Parameters.AddWithValue("@ProductName", cb_productname.Text)
                    cmd.Parameters.AddWithValue("@Stock", initialStock)
                    cmd.Parameters.AddWithValue("@Price", If(String.IsNullOrEmpty(txt_price.Text), DBNull.Value, Decimal.Parse(txt_price.Text)))
                    cmd.ExecuteNonQuery()
                End Using

                ShowSuccess("Data berhasil ditambahkan.")
                LoadAllData()
                ClearFields()
            End Using
        Catch ex As Exception
            ShowError("Error adding product: " & ex.Message)
        End Try
    End Sub

    ' Fungsi untuk mengedit harga
    Private Sub EditPrice()
        If ListView1.SelectedItems.Count = 0 Then
            ShowWarning("Pilih item yang ingin diedit.")
            Return
        End If

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim productId As String = ListView1.SelectedItems(0).Text

                Using cmd As New SqlCommand("UPDATE CRUD_Product SET Price = @Price WHERE ProductId = @ProductId", conn)
                    cmd.Parameters.AddWithValue("@Price", If(String.IsNullOrEmpty(txt_price.Text), DBNull.Value, Decimal.Parse(txt_price.Text)))
                    cmd.Parameters.AddWithValue("@ProductId", productId)
                    cmd.ExecuteNonQuery()
                End Using

                ShowSuccess("Harga berhasil diupdate!")
                LoadListView()
                ClearFields()
            End Using
        Catch ex As Exception
            ShowError("Error updating price: " & ex.Message)
        End Try
    End Sub

    ' Fungsi untuk menghapus produk
    Private Sub DeleteProduct()
        If ListView1.SelectedItems.Count = 0 Then
            ShowWarning("Pilih item yang ingin dihapus.")
            Return
        End If

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim productId As String = ListView1.SelectedItems(0).Text

                Using cmd As New SqlCommand("DELETE FROM CRUD_Product WHERE ProductId = @ProductId", conn)
                    cmd.Parameters.AddWithValue("@ProductId", productId)
                    cmd.ExecuteNonQuery()
                End Using

                ShowSuccess("Data berhasil dihapus.")
                LoadListView()
                ClearFields()
            End Using
        Catch ex As Exception
            ShowError("Error deleting product: " & ex.Message)
        End Try
    End Sub

    ' Fungsi untuk menambah stok
    Private Sub AddStock()
        If ListView1.SelectedItems.Count = 0 Then
            ShowWarning("Pilih produk terlebih dahulu!")
            Return
        End If

        If Not IsNumeric(txt_addstock.Text) OrElse CInt(txt_addstock.Text) <= 0 Then
            ShowWarning("Masukkan jumlah stok yang valid!")
            Return
        End If

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim selectedItem = ListView1.SelectedItems(0)
                Dim productId As String = selectedItem.Text
                Dim productName As String = selectedItem.SubItems(1).Text
                Dim addStock As Integer = CInt(txt_addstock.Text)

                ' Update stok di CRUD_Product
                Using cmdProduct As New SqlCommand("UPDATE CRUD_Product SET Stock = Stock + @AddStock WHERE ProductId = @ProductId", conn)
                    cmdProduct.Parameters.AddWithValue("@AddStock", addStock)
                    cmdProduct.Parameters.AddWithValue("@ProductId", productId)
                    cmdProduct.ExecuteNonQuery()
                End Using

                ' Update stok di Suppliers
                Using cmdSupplier As New SqlCommand("UPDATE Suppliers SET Quantity = Quantity + @AddStock WHERE TypeProduct = @TypeProduct", conn)
                    cmdSupplier.Parameters.AddWithValue("@AddStock", addStock)
                    cmdSupplier.Parameters.AddWithValue("@TypeProduct", productName)
                    cmdSupplier.ExecuteNonQuery()
                End Using

                ' Refresh ListView dan DataGridView untuk menampilkan data terbaru
                LoadListView()
                LoadSuppliersToGrid()

                ShowSuccess("Stok berhasil ditambahkan!")
                txt_addstock.Clear()
            End Using
        Catch ex As Exception
            ShowError("Error adding stock: " & ex.Message)
        End Try
    End Sub

    ' ===== FUNGSI PENDUKUNG =====
    Private Function GenerateProductId() As String
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Using cmd As New SqlCommand("SELECT MAX(CAST(SUBSTRING(ProductId, 5, LEN(ProductId)) AS INT)) FROM CRUD_Product", conn)
                Dim result = cmd.ExecuteScalar()
                Dim lastId As Integer = If(IsDBNull(result), 0, CInt(result))
                Return "PROD" & (lastId + 1).ToString("D3")
            End Using
        End Using
    End Function

    Private Function GetInitialStock(productName As String) As Integer
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Using cmd As New SqlCommand("SELECT TOP 1 Quantity FROM Suppliers WHERE TypeProduct = @TypeProduct", conn)
                cmd.Parameters.AddWithValue("@TypeProduct", productName)
                Dim result = cmd.ExecuteScalar()
                Return If(IsDBNull(result), 0, CInt(result))
            End Using
        End Using
    End Function

    Private Sub ClearFields()
        cb_productname.SelectedIndex = -1
        txt_price.Clear()
        txt_addstock.Clear()
    End Sub

    ' ===== FUNGSI TAMPILAN PESAN =====
    Private Sub ShowError(message As String)
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub ShowWarning(message As String)
        MessageBox.Show(message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub ShowSuccess(message As String)
        MessageBox.Show(message, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ===== EVENT HANDLERS =====
    Private Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add.Click
        AddProduct()
    End Sub

    Private Sub btn_edit_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        EditPrice()
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        DeleteProduct()
    End Sub

    Private Sub btn_addstock_Click(sender As Object, e As EventArgs) Handles btn_addstock.Click
        AddStock()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 Then
            Dim selectedItem = ListView1.SelectedItems(0)
            cb_productname.Text = selectedItem.SubItems(1).Text
            txt_price.Text = selectedItem.SubItems(3).Text
        End If
    End Sub

    Private Sub txt_price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_price.KeyPress, txt_addstock.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        If e.KeyChar = "." AndAlso DirectCast(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        AdminDashboard.Show()
        Me.Hide()
    End Sub
End Class