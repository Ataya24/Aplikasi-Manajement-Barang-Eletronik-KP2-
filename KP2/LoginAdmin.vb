Imports System.Data.SqlClient
Public Class LoginAdmin
    Private connectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=KP2;Integrated Security=True"
    Private Sub LoginAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txt_username.Text = ""
        txt_password.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        If String.IsNullOrEmpty(txt_username.Text) Then
            MessageBox.Show("Username tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt_username.Focus()
            Return
        End If

        If String.IsNullOrEmpty(txt_password.Text) Then
            MessageBox.Show("Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt_password.Focus()
            Return
        End If

        If ValidateLogin(txt_username.Text, txt_password.Text) Then
            MessageBox.Show("Login berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim Admin As New AdminDashboard()
            Admin.Show()
            Me.Hide()
        Else
            MessageBox.Show("Username atau password salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txt_password.Clear()
            txt_password.Focus()
        End If
    End Sub
    Private Function ValidateLogin(username As String, password As String) As Boolean
        Dim isValid As Boolean = False

        Try
            Using connection As New SqlConnection(connectionString)
                Dim query As String = "SELECT COUNT(*) FROM TB_User WHERE Username = @Username AND Password = @Password"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Username", username)
                    command.Parameters.AddWithValue("@Password", password)

                    connection.Open()

                    Dim count As Integer = CInt(command.ExecuteScalar())

                    isValid = (count > 0)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return isValid
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btn_reset.Click
        txt_username.Clear()
        txt_password.Clear()
        txt_username.Focus()
    End Sub

    Private Sub btn_close_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Dashboard.Show()
        Me.Hide()
    End Sub
End Class