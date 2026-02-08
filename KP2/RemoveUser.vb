Imports System.Data.SqlClient

Public Class RemoveUser
    Private ReadOnly connectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=KP2;Integrated Security=True"
    Public LoggedInUsername As String

    Private Sub RemoveUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupDataGridView()
        LoadUsers()
    End Sub

    Private Sub SetupDataGridView()
        With DataGridView1
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    Private Sub LoadUsers()
        Try
            Dim dt As New DataTable
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand("SELECT Username, Role FROM TB_User", conn)
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                        DataGridView1.DataSource = dt
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading users: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        ' Validasi apakah ada baris yang dipilih
        If DataGridView1.CurrentRow Is Nothing OrElse DataGridView1.CurrentRow.Index < 0 Then
            MessageBox.Show("Silakan pilih user yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim username As String = DataGridView1.CurrentRow.Cells("Username").Value.ToString()
        Dim userRole As String = DataGridView1.CurrentRow.Cells("Role").Value.ToString()

        ' Cek apakah user yang dipilih adalah user yang sedang login
        If username = LoggedInUsername Then
            MessageBox.Show("Anda tidak dapat menghapus akun yang sedang login.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Jika user adalah admin, cek apakah admin terakhir
        If userRole = "Admin" Then
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Using countCmd As New SqlCommand("SELECT COUNT(*) FROM TB_User WHERE Role='Admin'", conn)
                    Dim adminCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())
                    If adminCount <= 1 Then
                        MessageBox.Show("Tidak dapat menghapus admin terakhir!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using
            End Using
        End If

        ' Pesan konfirmasi penghapusan
        Dim confirmMessage As String = "Apakah Anda yakin ingin menghapus user '" & username & "'?"
        If userRole = "Admin" Then
            confirmMessage = "PERHATIAN! Anda akan menghapus user Admin '" & username & "'. Lanjutkan?"
        End If

        Dim result As DialogResult = MessageBox.Show(confirmMessage, "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Try
                Using conn As New SqlConnection(connectionString)
                    conn.Open()
                    Using cmd As New SqlCommand("DELETE FROM TB_User WHERE Username=@Username", conn)
                        cmd.Parameters.AddWithValue("@Username", username)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("User berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadUsers()
            Catch ex As Exception
                MessageBox.Show("Error saat menghapus user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        AdminDashboard.Show()
        Me.Hide()
    End Sub

    ' Removed problematic CellContentClick handler - data is already loaded in LoadUsers()
    ' This handler was causing issues by clearing rows and querying wrong table name
End Class
