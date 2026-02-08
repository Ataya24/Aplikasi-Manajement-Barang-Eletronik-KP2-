Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.draw
Imports System.IO
Imports System.Data.SqlClient

Public Class GoodsInHistory
    Private ReadOnly connectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=KP2;Integrated Security=True"

    Private Sub LoadGoodsData()
        Using conn As New SqlConnection(connectionString)
            Dim query As String = "SELECT IdProduct AS [Product ID], " &
                             "TypeProduct AS [Product Type], " &
                             "Quantity AS [Quantity], " &
                             "Categories AS [Category], " &
                             "FORMAT(Date, 'dd MMM yyyy') AS [Entry Date] " &
                             "FROM dbo.Suppliers " &
                             "ORDER BY Date DESC"

            Dim adapter As New SqlDataAdapter(query, conn)
            Dim table As New DataTable()

            Try
                conn.Open()
                adapter.Fill(table)
                DataGridView1.DataSource = table
                DataGridView1.Columns("Product ID").Width = 100
                DataGridView1.Columns("Product Type").Width = 150
                DataGridView1.Columns("Quantity").Width = 80
                DataGridView1.Columns("Category").Width = 150
                DataGridView1.Columns("Entry Date").Width = 100

            Catch ex As Exception
                MessageBox.Show("Error loading data: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ExportToPDF()
        If DataGridView1.Rows.Count = 0 Then
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

            Dim titleFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD)
            Dim headerFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL)
            Dim cellFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL)
            Dim smallFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)

            Dim header As New Paragraph("ATAYA ELECTRONIC’S", titleFont)
            header.Alignment = Element.ALIGN_CENTER
            doc.Add(header)

            doc.Add(New Paragraph("Jalan Pulau Saparua 12 No. 139, Kec. Bekasi Timur, Kota Bekasi,", headerFont) With {.Alignment = Element.ALIGN_CENTER})
            doc.Add(New Paragraph("Jawa Barat 17111", headerFont) With {.Alignment = Element.ALIGN_CENTER})
            doc.Add(New Paragraph("Telepon: +62 851-6168-8367 | Email: atayanafisrizqulah@gmail.com", headerFont) With {.Alignment = Element.ALIGN_CENTER})

            Dim line As New iTextSharp.text.pdf.draw.LineSeparator(1.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, -2.0F)
            doc.Add(New Chunk(line))

            Dim title As New Paragraph(vbCrLf & "Daftar Barang Masuk", titleFont)
            title.Alignment = Element.ALIGN_CENTER
            title.SpacingAfter = 10
            doc.Add(title)

            Dim dateText As New Paragraph("Tanggal Cetak: " & DateTime.Now.ToString("dd MMM yyyy HH:mm:ss"), smallFont)
            dateText.Alignment = Element.ALIGN_RIGHT
            dateText.SpacingAfter = 10
            doc.Add(dateText)

            Dim table As New PdfPTable(DataGridView1.Columns.Count)
            table.WidthPercentage = 100
            table.SpacingBefore = 10
            table.SpacingAfter = 10

            Dim colWidths() As Single = {1.0F, 1.5F, 0.8F, 1.5F, 1.2F}
            table.SetWidths(colWidths)

            For Each column As DataGridViewColumn In DataGridView1.Columns
                Dim cell As New PdfPCell(New Phrase(column.HeaderText, headerFont))
                cell.HorizontalAlignment = Element.ALIGN_CENTER
                cell.BackgroundColor = BaseColor.LIGHT_GRAY
                cell.Padding = 5
                table.AddCell(cell)
            Next

            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    For Each cell As DataGridViewCell In row.Cells
                        Dim value As String = If(cell.Value Is Nothing, "", cell.Value.ToString())
                        Dim pdfCell As New PdfPCell(New Phrase(value, cellFont))

                        If cell.OwningColumn.Name = "Jumlah" Then
                            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT
                        Else
                            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT
                        End If

                        pdfCell.Padding = 5
                        table.AddCell(pdfCell)
                    Next
                End If
            Next

            doc.Add(table)

            doc.Add(New Paragraph(vbCrLf & vbCrLf & vbCrLf))

            Dim signatureTable As New PdfPTable(1)
            signatureTable.WidthPercentage = 30
            signatureTable.HorizontalAlignment = Element.ALIGN_RIGHT

            Dim cell1 As New PdfPCell(New Phrase("Yang Bertanggung Jawab,", headerFont))
            cell1.Border = Rectangle.NO_BORDER
            cell1.HorizontalAlignment = Element.ALIGN_CENTER
            cell1.PaddingTop = 20
            signatureTable.AddCell(cell1)

            signatureTable.AddCell(New PdfPCell(New Phrase(vbCrLf & vbCrLf & vbCrLf)) With {
                .Border = Rectangle.NO_BORDER
            })

            Dim cell3 As New PdfPCell(New Phrase("(.................................)", headerFont))
            cell3.Border = Rectangle.NO_BORDER
            cell3.HorizontalAlignment = Element.ALIGN_CENTER
            signatureTable.AddCell(cell3)

            doc.Add(signatureTable)

            doc.Close()
            MessageBox.Show("Data berhasil diexport ke PDF!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ExportToPDF()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        AdminDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub GoodsInHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGoodsData()
    End Sub

    Private Sub GoodsInHistory_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ' Refresh goods data when form becomes active to show latest changes
        LoadGoodsData()
    End Sub
End Class