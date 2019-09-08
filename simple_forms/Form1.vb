Imports System.Net.Sockets
Imports System.Text
Public Class Form1
    Dim clientSocket As New TcpClient()
    Dim serverStream As NetworkStream

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim text As String = TextBox1.Text
        If text.Length = 0 Then
            text = "Nachricht vom Client"
        End If
        Try
            Dim serverStream As NetworkStream = clientSocket.GetStream()
            Dim outStream As Byte() = Encoding.ASCII.GetBytes(text & "$")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()

            Dim inStream(65537) As Byte
            serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize)
            Dim returndata As String = Encoding.ASCII.GetString(inStream)
            msg("Daten vom Server : " + returndata)

        Catch err As Exception
            MsgBox(err.ToString)
        End Try
    End Sub

    Sub msg(ByVal msg As String)
        ListBox1.Items.Add(msg)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        msg("Client Gestartet")
        clientSocket.Close()
        clientSocket = New TcpClient()
        clientSocket.Connect("127.0.0.1", 8888)
    End Sub
End Class