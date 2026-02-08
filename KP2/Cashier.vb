Imports System.Data.SqlClient
Public Class Cashier
    Private total As Decimal = 0
    Private transactionItems As New List(Of TransactionItem)
    Private productsDataTable As New DataTable()
    Private connectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=KP2;Integrated Security=True"

    Private Class TransactionItem
        Public Property KodeBarang As String
        Public Property NamaBarang As String
        Public Property Harga As Decimal
        Public Property Jumlah As Integer
    End Class

    Private Sub Cashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts()
        AddHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged
        AddHandler DataGridView1.CellClick, AddressOf DataGridView1_CellClick
        AddHandler DataGridView2.CellClick, AddressOf DataGridView2_CellClick
        AddHandler btnAddToCart.Click, AddressOf btnAddToCart_Click
        ' Populate DataGridView1 with all products on initial load
        txtSearch_TextChanged(txtSearch, EventArgs.Empty)
    End Sub

    Private Sub Cashier_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ' Refresh products when form becomes active to show latest data
        LoadProducts()
        ' Refresh the search display
        txtSearch_TextChanged(txtSearch, EventArgs.Empty)
    End Sub

    Private Sub LoadProducts()
        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT ProductId, ProductName, Price FROM CRUD_Product"
                Using cmd As New SqlCommand(query, conn)
                    Using adapter As New SqlDataAdapter(cmd)
                        productsDataTable.Clear()
                        adapter.Fill(productsDataTable)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading products: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim searchText As String = txtSearch.Text.Trim()
            Dim filteredRows As DataRow() = productsDataTable.Select(
                "ProductName LIKE '%" & searchText & "%' OR ProductId LIKE '%" & searchText & "%'")

            DataGridView1.Rows.Clear()
            For Each row As DataRow In filteredRows
                DataGridView1.Rows.Add(
                    row("ProductId"),
                    row("ProductName"),
                    Convert.ToDecimal(row("Price")).ToString("C2"))
            Next
        Catch ex As Exception
            MessageBox.Show("Error searching products: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            txtQuantity.Text = "1"
            DataGridView1.Rows(e.RowIndex).Selected = True

        End If
    End Sub

    Private Sub btnAddToCart_Click(sender As Object, e As EventArgs)
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih item terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim kodeBarang As String = row.Cells(0).Value.ToString()
        Dim namaBarang As String = row.Cells(1).Value.ToString()
        Dim harga As Decimal = Convert.ToDecimal(row.Cells(2).Value.ToString().Replace("$", "").Replace(",", ""))
        
        Dim jumlah As Integer
        If Not Integer.TryParse(txtQuantity.Text, jumlah) OrElse jumlah <= 0 Then
            MessageBox.Show("Masukkan jumlah yang valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim existingItem = transactionItems.FirstOrDefault(Function(x) x.KodeBarang = kodeBarang)
        If existingItem IsNot Nothing Then
            existingItem.Jumlah += jumlah
            UpdateCartDisplay()
        Else
            AddItem(kodeBarang, namaBarang, harga, jumlah)
        End If

        CalculateTotal()
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 4 Then
            transactionItems.RemoveAt(e.RowIndex)
            UpdateCartDisplay()
            CalculateTotal()
        End If
    End Sub

    Private Sub UpdateCartDisplay()
        DataGridView2.Rows.Clear()
        For Each item In transactionItems
            DataGridView2.Rows.Add(
                item.KodeBarang,
                item.NamaBarang,
                item.Harga.ToString("C2"),
                item.Jumlah,
                "Hapus")
        Next
    End Sub

    Private Sub CalculateTotal()
        total = 0
        For Each item In transactionItems
            total += item.Harga * item.Jumlah
        Next
        txtTotal.Text = total.ToString("C2")
    End Sub

    Private Sub SaveTransaction(paymentMethod As String, amountPaid As Decimal, change As Decimal)
        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Using transaction As SqlTransaction = conn.BeginTransaction()
                    Try
                        Dim insertTransactionQuery As String = "INSERT INTO Transactions (TransactionDate, TotalAmount, PaymentMethod, AmountPaid, ChangeAmount) " &
                                                         "VALUES (@TransactionDate, @TotalAmount, @PaymentMethod, @AmountPaid, @ChangeAmount); " &
                                                         "SELECT SCOPE_IDENTITY();"

                        Dim transactionId As Integer
                        Using cmd As New SqlCommand(insertTransactionQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@TransactionDate", DateTime.Now)
                            cmd.Parameters.AddWithValue("@TotalAmount", total)
                            cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod)
                            cmd.Parameters.AddWithValue("@AmountPaid", amountPaid)
                            cmd.Parameters.AddWithValue("@ChangeAmount", change)
                            transactionId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        Dim insertDetailQuery As String = "INSERT INTO TransactionDetails (TransactionID, ProductId, Quantity, Price, Subtotal) " &
                                                     "VALUES (@TransactionID, @ProductId, @Quantity, @Price, @Subtotal)"

                        Using cmd As New SqlCommand(insertDetailQuery, conn, transaction)
                            For Each item In transactionItems
                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@TransactionID", transactionId)
                                cmd.Parameters.AddWithValue("@ProductId", item.KodeBarang)
                                cmd.Parameters.AddWithValue("@Quantity", item.Jumlah)
                                cmd.Parameters.AddWithValue("@Price", item.Harga)
                                cmd.Parameters.AddWithValue("@Subtotal", item.Harga * item.Jumlah)
                                cmd.ExecuteNonQuery()

                                Dim updateStockQuery As String = "UPDATE CRUD_Product SET Stock = Stock - @Quantity WHERE ProductId = @ProductId"
                                Using updateCmd As New SqlCommand(updateStockQuery, conn, transaction)
                                    updateCmd.Parameters.AddWithValue("@Quantity", item.Jumlah)
                                    updateCmd.Parameters.AddWithValue("@ProductId", item.KodeBarang)
                                    updateCmd.ExecuteNonQuery()
                                End Using
                            Next
                        End Using

                        Dim insertGoodsOutQuery As String = "INSERT INTO GoodsOut (ProductId, Quantity, DateOut, Reason) VALUES (@ProductId, @Quantity, @DateOut, @Reason)"
                        Using cmdGoodsOut As New SqlCommand(insertGoodsOutQuery, conn, transaction)
                            For Each item In transactionItems
                                cmdGoodsOut.Parameters.Clear()
                                cmdGoodsOut.Parameters.AddWithValue("@ProductId", item.KodeBarang)
                                cmdGoodsOut.Parameters.AddWithValue("@Quantity", item.Jumlah)
                                cmdGoodsOut.Parameters.AddWithValue("@DateOut", DateTime.Now)
                                cmdGoodsOut.Parameters.AddWithValue("@Reason", "Dijual")
                                cmdGoodsOut.ExecuteNonQuery()
                            Next
                        End Using

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving transaction: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCash_Click(sender As Object, e As EventArgs) Handles btnCash.Click
        If transactionItems.Count = 0 Then
            MessageBox.Show("Tidak ada item dalam keranjang!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim cashAmount As Decimal
        If Decimal.TryParse(InputBox("Masukkan jumlah uang tunai:", "Pembayaran Cash"), cashAmount) Then
            If cashAmount >= total Then
                Dim kembalian As Decimal = cashAmount - total
                SaveTransaction("CASH", cashAmount, kembalian)
                PrintReceipt("CASH", cashAmount, kembalian)
                ClearTransaction()
            Else
                MessageBox.Show("Jumlah uang tidak mencukupi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnEWallet_Click(sender As Object, e As EventArgs) Handles btnEWallet.Click
        If transactionItems.Count = 0 Then
            MessageBox.Show("Tidak ada item dalam keranjang!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim eWalletOptions() As String = {"GoPay", "OVO", "DANA", "LinkAja"}
        Dim selectedWallet = InputBox("Pilih e-wallet:" & vbCrLf & String.Join(vbCrLf, eWalletOptions), "Pembayaran E-Wallet")

        If eWalletOptions.Contains(selectedWallet) Then
            SaveTransaction(selectedWallet, total, 0)
            PrintReceipt(selectedWallet, total, 0)
            ClearTransaction()
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        If transactionItems.Count = 0 Then
            MessageBox.Show("Tidak ada item dalam keranjang!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        PrintReceipt("", total, 0)
    End Sub

    Private Sub PrintReceipt(paymentMethod As String, amountPaid As Decimal, change As Decimal)
        Dim receipt As New System.Text.StringBuilder()
        receipt.AppendLine("ELECTRONICS MANAGEMENT APP")
        receipt.AppendLine("==========================")
        receipt.AppendLine("Tanggal: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
        receipt.AppendLine("--------------------------")

        For Each item In transactionItems
            receipt.AppendLine(item.NamaBarang)
            receipt.AppendLine(item.Harga.ToString("C2") & " x " & item.Jumlah & " = " & (item.Harga * item.Jumlah).ToString("C2"))
        Next

        receipt.AppendLine("--------------------------")
        receipt.AppendLine("Total: " & total.ToString("C2"))

        If paymentMethod <> "" Then
            receipt.AppendLine("Metode Pembayaran: " & paymentMethod)
            receipt.AppendLine("Jumlah Dibayar: " & amountPaid.ToString("C2"))
            If change > 0 Then
                receipt.AppendLine("Kembalian: " & change.ToString("C2"))
            End If
        End If

        receipt.AppendLine("==========================")
        receipt.AppendLine("Terima Kasih Atas Kunjungan Anda!")

        MessageBox.Show(receipt.ToString(), "Struk Pembayaran", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ClearTransaction()
        transactionItems.Clear()
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        total = 0
        txtTotal.Text = "0"
        txtQuantity.Text = "1"
        LoadProducts()
    End Sub

    Public Sub AddItem(kodeBarang As String, namaBarang As String, harga As Decimal, jumlah As Integer)
        Dim item As New TransactionItem With {
            .KodeBarang = kodeBarang,
            .NamaBarang = namaBarang,
            .Harga = harga,
            .Jumlah = jumlah
        }
        transactionItems.Add(item)
        UpdateCartDisplay()
        CalculateTotal()
    End Sub

    Private Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Hide()
            LoginCashier.txt_username.Text = ""
            LoginCashier.txt_password.Text = ""
            LoginCashier.Show()
        End If
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub
End Class