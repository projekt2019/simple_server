Imports System.Net.Sockets
Imports System.Text

Public Class ClientMessageHandler

    Private clientSocket As TcpClient
    Event RecievedMessage(ByVal str As String)

    Public Async Sub connect(ip As String, port As Integer)
        If clientSocket IsNot Nothing Then
            clientSocket.Close()
        End If

        clientSocket = New TcpClient()
        clientSocket.Connect(ip, port)
        While True
            RaiseEvent RecievedMessage(Await recieve())
        End While
    End Sub

    Public Async Sub send(msg As String)
        ' Verbindung
        Dim serverStream As NetworkStream = clientSocket.GetStream()
        ' Nachricht in Bytes umwandeln
        Dim outStream As Byte() = Encoding.ASCII.GetBytes(msg & "$")
        ' senden
        Await serverStream.WriteAsync(outStream, 0, outStream.Length)
        Await serverStream.FlushAsync()
    End Sub

    ' Nachrichten bekommen
    Private Async Function recieve() As Task(Of String)
        Dim serverStream As NetworkStream = clientSocket.GetStream()
        Dim inStream(65537) As Byte
        ' Nachrichten einlesen
        Await serverStream.ReadAsync(inStream, 0, clientSocket.ReceiveBufferSize)
        ' In String umwandeln
        Return Encoding.ASCII.GetString(inStream)
    End Function

End Class
