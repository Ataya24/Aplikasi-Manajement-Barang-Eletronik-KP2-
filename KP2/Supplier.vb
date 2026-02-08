Imports System.Data.SqlClient

Public Class Supplier
    Private ReadOnly connectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=KP2;Integrated Security=True"

    Private Sub Supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSuppliers()
        txt_idproduct.Enabled = False
        ListView1.View = View.Details
        ListView1.FullRowSelect = True
    End Sub

    Private Sub Supplier_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        LoadSuppliers()
    End Sub

    Private Function GenerateProductId() As String
        Using conn As New SqlConnection(connectionString)
            conn.Open()

            Dim cmd As New SqlCommand("
            SELECT ISNULL(MAX(CAST(SUBSTRING(ProductId, 4, LEN(ProductId)-3) AS INT)),0)
            FROM CRUD_Product
            WHERE ProductId LIKE 'PRD%'", conn)

            Dim nextNumber As Integer = Convert.ToInt32(cmd.ExecuteScalar()) + 1

            Return "PRD" & nextNumber.ToString("D3")
        End Using
    End Function

    Private Sub LoadSuppliers()
        ListView1.Items.Clear()
        ListView1.Columns.Clear()

        ListView1.Columns.Add("IdProduct", 120)
        ListView1.Columns.Add("TypeProduct", 150)
        ListView1.Columns.Add("Quantity", 100)
        ListView1.Columns.Add("Categories", 120)
        ListView1.Columns.Add("Date", 100)

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim cmd As New SqlCommand("SELECT * FROM Suppliers", conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim item As New ListViewItem(reader("IdProduct").ToString())
                        item.SubItems.Add(reader("TypeProduct").ToString())
                        item.SubItems.Add(reader("Quantity").ToString())
                        item.SubItems.Add(reader("Categories").ToString())
                        item.SubItems.Add(If(reader("Date") IsNot DBNull.Value, Convert.ToDateTime(reader("Date")).ToString("dd/MM/yyyy"), ""))
                        ListView1.Items.Add(item)
                    End While
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading suppliers: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        AdminDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add.Click
        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()

                ' Cek apakah produk sudah ada
                Dim cmdCheckProduct As New SqlCommand("SELECT ProductId FROM CRUD_Product WHERE ProductName = @name", conn)
                cmdCheckProduct.Parameters.AddWithValue("@name", txt_productname.Text)

                Dim productId As String = Nothing
                Dim result = cmdCheckProduct.ExecuteScalar()

                If result Is Nothing Then
                    ' Kalau belum ada → buat produk baru dulu
                    productId = GenerateProductId()

                    Dim cmdInsertProduct As New SqlCommand("INSERT INTO CRUD_Product (ProductId, ProductName, Stock, Price) VALUES (@id, @name, @stock, @price)", conn)
                    cmdInsertProduct.Parameters.AddWithValue("@id", productId)
                    cmdInsertProduct.Parameters.AddWithValue("@name", txt_productname.Text)
                    cmdInsertProduct.Parameters.AddWithValue("@stock", Convert.ToInt32(txt_quantity.Text))
                    cmdInsertProduct.Parameters.AddWithValue("@price", 0) ' default dulu
                    cmdInsertProduct.ExecuteNonQuery()
                Else
                    productId = result.ToString()

                    ' Kalau sudah ada → update stock
                    Dim cmdUpdateStock As New SqlCommand("UPDATE CRUD_Product SET Stock = Stock + @qty WHERE ProductId = @id", conn)
                    cmdUpdateStock.Parameters.AddWithValue("@qty", Convert.ToInt32(txt_quantity.Text))
                    cmdUpdateStock.Parameters.AddWithValue("@id", productId)
                    cmdUpdateStock.ExecuteNonQuery()
                End If

                ' Lalu tetap insert ke Suppliers
                Dim cmdInsertSupplier As New SqlCommand("INSERT INTO Suppliers (IdProduct, TypeProduct, Quantity, Categories, Date) VALUES (@id, @type, @qty, @cat, @date)", conn)

                cmdInsertSupplier.Parameters.AddWithValue("@id", productId)
                cmdInsertSupplier.Parameters.AddWithValue("@type", txt_productname.Text)
                cmdInsertSupplier.Parameters.AddWithValue("@qty", Convert.ToInt32(txt_quantity.Text))
                cmdInsertSupplier.Parameters.AddWithValue("@cat", cbx_categories.Text)
                cmdInsertSupplier.Parameters.AddWithValue("@date", DateTimePicker1.Value)

                cmdInsertSupplier.ExecuteNonQuery()

                MessageBox.Show("Data berhasil disimpan!")

                LoadSuppliers()
                clearfields()

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using

    End Sub

    Private Sub btn_edit_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        If String.IsNullOrEmpty(txt_idproduct.Text) Then
            MessageBox.Show("Pilih produk yang akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrEmpty(txt_productname.Text) Then
            MessageBox.Show("Tipe Produk harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt_productname.Focus()
            Return
        End If

        If String.IsNullOrEmpty(txt_quantity.Text) Then
            MessageBox.Show("Quantity harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt_quantity.Focus()
            Return
        End If

        If String.IsNullOrEmpty(cbx_categories.Text) Then
            MessageBox.Show("Kategori harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cbx_categories.Focus()
            Return
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                
                ' Cek jumlah stok saat ini
                Dim cmdCheck As New SqlCommand("SELECT Quantity FROM Suppliers WHERE IdProduct = @idproduct", conn)
                cmdCheck.Parameters.AddWithValue("@idproduct", txt_idproduct.Text)
                Dim currentQuantity As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                
                ' Validasi jumlah stok baru
                Dim newQuantity As Integer = Convert.ToInt32(txt_quantity.Text)
                If newQuantity < currentQuantity Then
                    MessageBox.Show("Jumlah stok baru tidak boleh kurang dari jumlah stok saat ini (" & currentQuantity & ")!", 
                                  "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_quantity.Text = currentQuantity.ToString()
                    txt_quantity.Focus()
                    Return
                End If

                Dim cmd As New SqlCommand("UPDATE Suppliers SET TypeProduct=@typeproduct, Quantity=@quantity, Categories=@categories, Date=@date WHERE IdProduct=@idproduct", conn)
                cmd.Parameters.AddWithValue("@idproduct", txt_idproduct.Text)
                cmd.Parameters.AddWithValue("@typeproduct", txt_productname.Text)
                cmd.Parameters.AddWithValue("@quantity", txt_quantity.Text)
                cmd.Parameters.AddWithValue("@categories", cbx_categories.Text)
                cmd.Parameters.AddWithValue("@date", DateTimePicker1.Value)
                cmd.ExecuteNonQuery()

                LoadSuppliers()
                clearfields()
                MessageBox.Show("Data produk berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Gagal mengubah data: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        If String.IsNullOrEmpty(txt_idproduct.Text) Then
            MessageBox.Show("Pilih produk yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus data produk ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then
            Return
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim cmd As New SqlCommand("DELETE FROM Suppliers WHERE IdProduct=@idproduct", conn)
                cmd.Parameters.AddWithValue("@idproduct", txt_idproduct.Text)
                cmd.ExecuteNonQuery()
                
                LoadSuppliers()
                clearfields()
                MessageBox.Show("Data produk berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Gagal menghapus data: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 Then
            Dim item = ListView1.SelectedItems(0)
            txt_idproduct.Text = item.SubItems(0).Text
            txt_productname.Text = item.SubItems(1).Text
            txt_quantity.Text = item.SubItems(2).Text
            cbx_categories.Text = item.SubItems(3).Text
            If Not String.IsNullOrEmpty(item.SubItems(4).Text) Then
                DateTimePicker1.Value = DateTime.ParseExact(item.SubItems(4).Text, "dd/MM/yyyy", Nothing)
            End If
        End If
    End Sub

    Private Sub clearfields()
        txt_idproduct.Clear()
        txt_productname.Clear()
        txt_quantity.Clear()
        DateTimePicker1.Value = DateTime.Now
    End Sub
End Class