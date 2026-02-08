Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class GoodsOut

    Private ReadOnly connectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=KP2;Integrated Security=True"
    Private Sub GoodsOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts()
        LoadReasons()
        LoadGoodsOutList()
    End Sub

    Private Sub GoodsOut_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ' Refresh products and goods out list when form becomes active to show latest changes
        LoadProducts()
        LoadGoodsOutList()
    End Sub
    Private Sub LoadProducts()
        Try
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand("SELECT ProductId, ProductName FROM CRUD_Product", conn)
                    Dim dt As New DataTable()
                    conn.Open()
                    dt.Load(cmd.ExecuteReader())
                    cbx_product.DataSource = dt
                    cbx_product.DisplayMember = "ProductName"
                    cbx_product.ValueMember = "ProductId"
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading products: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub LoadReasons()
        cbx_reason.Items.Clear()
        cbx_reason.Items.Add("Dipakai")
        cbx_reason.Items.Add("Rusak")
        cbx_reason.SelectedIndex = 0
    End Sub

    Private Sub LoadGoodsOutList()
        lv_goodsout.Items.Clear()
        lv_goodsout.View = View.Details
        lv_goodsout.Columns.Clear()
        lv_goodsout.Columns.Add("Date", 100)
        lv_goodsout.Columns.Add("Product Name", 150)
        lv_goodsout.Columns.Add("Quantity", 70)
        lv_goodsout.Columns.Add("Reason", 100)

        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT go.DateOut, p.ProductName, go.Quantity, go.Reason " &
                                    "FROM GoodsOut go " &
                                    "JOIN CRUD_Product p ON go.ProductId = p.ProductId " &
                                    "ORDER BY go.DateOut DESC"
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New ListViewItem(reader("DateOut").ToString())
                            item.SubItems.Add(reader("ProductName").ToString())
                            item.SubItems.Add(reader("Quantity").ToString())
                            item.SubItems.Add(reader("Reason").ToString())
                            lv_goodsout.Items.Add(item)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading goods out list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        AdminDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If cbx_product.SelectedIndex = -1 OrElse cbx_reason.SelectedIndex = -1 Then
            MessageBox.Show("Pilih produk dan alasan terlebih dahulu.")
            Return
        End If

        Dim productId As String = cbx_product.SelectedValue.ToString()
        Dim quantity As Integer
        If Not Integer.TryParse(txt_quantity.Text, quantity) OrElse quantity <= 0 Then
            MessageBox.Show("Jumlah tidak valid.")
            Return
        End If

        Dim reason As String = cbx_reason.Text
        Dim dateOut As Date = DateTimePicker1.Value

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()

                ' Check stock availability
                Dim stock As Integer = 0
                Using cmdCheck As New SqlCommand("SELECT Stock FROM CRUD_Product WHERE ProductId=@id", conn)
                    cmdCheck.Parameters.AddWithValue("@id", productId)
                    Dim result = cmdCheck.ExecuteScalar()
                    If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                        Integer.TryParse(result.ToString(), stock)
                    End If
                End Using

                If quantity > stock Then
                    MessageBox.Show("Stok tidak mencukupi.")
                    Return
                End If

                ' Save to GoodsOut and update stock in transaction
                Using transaction As SqlTransaction = conn.BeginTransaction()
                    Try
                        ' Insert into GoodsOut
                        Using cmdInsert As New SqlCommand("INSERT INTO GoodsOut (ProductId, Quantity, DateOut, Reason) VALUES (@id, @qty, @date, @reason)", conn, transaction)
                            cmdInsert.Parameters.AddWithValue("@id", productId)
                            cmdInsert.Parameters.AddWithValue("@qty", quantity)
                            cmdInsert.Parameters.AddWithValue("@date", dateOut)
                            cmdInsert.Parameters.AddWithValue("@reason", reason)
                            cmdInsert.ExecuteNonQuery()
                        End Using

                        ' Update stock
                        Using cmdUpdate As New SqlCommand("UPDATE CRUD_Product SET Stock = Stock - @qty WHERE ProductId=@id", conn, transaction)
                            cmdUpdate.Parameters.AddWithValue("@qty", quantity)
                            cmdUpdate.Parameters.AddWithValue("@id", productId)
                            cmdUpdate.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Data barang keluar berhasil disimpan.")
                        LoadGoodsOutList()
                        txt_quantity.Clear()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_quantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_quantity.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        If e.KeyChar = "." AndAlso DirectCast(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub ExportToPDF()
        If lv_goodsout.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data untuk diexport ke PDF.")
            Return
        End If

        Dim saveFile As New SaveFileDialog()
        saveFile.Filter = "PDF Files (*.pdf)|*.pdf"
        saveFile.Title = "Simpan sebagai PDF"

        If saveFile.ShowDialog() = DialogResult.OK Then
            Dim doc As New iTextSharp.text.Document(PageSize.A4, 40, 40, 40, 40)
            PdfWriter.GetInstance(doc, New FileStream(saveFile.FileName, FileMode.Create))
            doc.Open()

            ' Font settings mirip dengan DataGridView version
            Dim titleFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD)
            Dim headerFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL)
            Dim cellFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL)
            Dim smallFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)

            ' Header perusahaan (mirip dengan DataGridView)
            Dim header As New Paragraph("ATAYA ELECTRONIC’S", titleFont)
            header.Alignment = Element.ALIGN_CENTER
            doc.Add(header)

            doc.Add(New Paragraph("Jalan Pulau Saparua 12 No. 139, Kec. Bekasi Timur, Kota Bekasi,", headerFont) With {.Alignment = Element.ALIGN_CENTER})
            doc.Add(New Paragraph("Jawa Barat 17111", headerFont) With {.Alignment = Element.ALIGN_CENTER})
            doc.Add(New Paragraph("Telepon: 085161688367 | Email: atayanafis2007@gmail.com", headerFont) With {.Alignment = Element.ALIGN_CENTER})

            Dim line As New iTextSharp.text.pdf.draw.LineSeparator(1.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, -2.0F)
            doc.Add(New Chunk(line))

            ' Judul dokumen
            Dim title As New Paragraph(vbCrLf & "Daftar Riwayat Barang Keluar", titleFont)
            title.Alignment = Element.ALIGN_CENTER
            title.SpacingAfter = 10
            doc.Add(title)

            ' Tanggal cetak
            Dim dateText As New Paragraph("Tanggal Cetak: " & DateTime.Now.ToString("dd MMM yyyy HH:mm:ss"), smallFont)
            dateText.Alignment = Element.ALIGN_RIGHT
            dateText.SpacingAfter = 10
            doc.Add(dateText)

            ' Buat tabel sesuai kolom ListView (misal 4 kolom)
            Dim columnCount As Integer = 4 ' Sesuaikan dengan jumlah kolom ListView kamu
            Dim table As New PdfPTable(columnCount)
            table.WidthPercentage = 100
            table.SpacingBefore = 10
            table.SpacingAfter = 10

            ' Atur lebar kolom (bisa disesuaikan proporsinya)
            Dim colWidths() As Single = {1.5F, 3.0F, 1.0F, 3.0F}
            table.SetWidths(colWidths)

            ' Header tabel (bisa ambil langsung dari header ListView)
            Dim headers() As String = {"Date", "Product Name", "Quantity", "Reason"}
            For Each headerText As String In headers
                Dim cell As New PdfPCell(New Phrase(headerText, headerFont))
                cell.HorizontalAlignment = Element.ALIGN_CENTER
                cell.BackgroundColor = BaseColor.LIGHT_GRAY
                cell.Padding = 5
                table.AddCell(cell)
            Next

            ' Isi data dari ListView
            For Each item As ListViewItem In lv_goodsout.Items
                ' Kolom pertama (Text utama item)
                Dim cell1 As New PdfPCell(New Phrase(item.Text, cellFont))
                cell1.HorizontalAlignment = Element.ALIGN_CENTER
                cell1.Padding = 5
                table.AddCell(cell1)

                ' Kolom-kolom berikutnya (SubItems)
                For i As Integer = 1 To item.SubItems.Count - 1
                    Dim cellSub As New PdfPCell(New Phrase(item.SubItems(i).Text, cellFont))
                    cellSub.HorizontalAlignment = Element.ALIGN_CENTER
                    cellSub.Padding = 5
                    table.AddCell(cellSub)
                Next
            Next

            doc.Add(table)

            doc.Add(New Paragraph(vbCrLf & vbCrLf & vbCrLf))

            Dim signatureTable As New PdfPTable(1)
            signatureTable.WidthPercentage = 30
            signatureTable.HorizontalAlignment = Element.ALIGN_RIGHT

            Dim cellSign1 As New PdfPCell(New Phrase("Yang Bertanggung Jawab,", headerFont))
            cellSign1.Border = Rectangle.NO_BORDER
            cellSign1.HorizontalAlignment = Element.ALIGN_CENTER
            cellSign1.PaddingTop = 20
            signatureTable.AddCell(cellSign1)

            signatureTable.AddCell(New PdfPCell(New Phrase(vbCrLf & vbCrLf & vbCrLf)) With {
            .Border = Rectangle.NO_BORDER
        })

            Dim cellSign3 As New PdfPCell(New Phrase("(.................................)", headerFont))
            cellSign3.Border = Rectangle.NO_BORDER
            cellSign3.HorizontalAlignment = Element.ALIGN_CENTER
            signatureTable.AddCell(cellSign3)

            doc.Add(signatureTable)

            doc.Close()

            MessageBox.Show("Data berhasil diexport ke PDF!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btn_export_Click(sender As Object, e As EventArgs) Handles btn_export.Click
        ExportToPDF()
    End Sub
End Class