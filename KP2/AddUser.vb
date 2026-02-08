Imports System.Data.SqlClient

Public Class AddUser
    Private ReadOnly connectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=KP2;Integrated Security=True"

    Private Sub AddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsersToDataGridView()
        cb_role.Items.Clear()
        cb_role.Items.Add("Admin")
        cb_role.Items.Add("Cashier")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        AdminDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        ' Validasi input terlebih dahulu
        If txt_name.Text = "" OrElse txt_password.Text = "" OrElse cb_role.Text = "" Then
            MessageBox.Show("Semua field harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "INSERT INTO TB_User (Username, Password, Role) VALUES (@Username, @Password, @Role)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Username", txt_name.Text)
                    cmd.Parameters.AddWithValue("@Password", txt_password.Text) ' Kalau Password int, ubah ke Convert.ToInt32(txt_password.Text)
                    cmd.Parameters.AddWithValue("@Role", cb_role.Text)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Data pengguna berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUsersToDataGridView()
            ResetForm()
        Catch ex As Exception
            MessageBox.Show("Gagal menambahkan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Fungsi untuk mereset form input
    Private Sub ResetForm()
        txt_name.Text = ""
        txt_password.Text = ""
        cb_role.SelectedIndex = -1
    End Sub

    Private Sub btn_reset_Click(sender As Object, e As EventArgs) Handles btn_reset.Click
        ResetForm()
    End Sub

    ' Fungsi untuk load data user dari database ke DataGridView
    Private Sub LoadUsersToDataGridView()
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT Username, Password, Role FROM TB_User"
                Using adapter As New SqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Gagal memuat data pengguna: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
