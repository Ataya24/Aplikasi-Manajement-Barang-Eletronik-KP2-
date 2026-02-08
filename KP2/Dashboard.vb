Public Class Dashboard
    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub AdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdminToolStripMenuItem.Click

    End Sub

    Private Sub AdminToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AdminToolStripMenuItem1.Click
        LoginAdmin.Show()
        Me.Hide()
    End Sub

    Private Sub CashierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CashierToolStripMenuItem.Click
        LoginCashier.Show()
        Me.Hide()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class