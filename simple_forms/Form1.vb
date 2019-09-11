Imports System.Net.Sockets
Imports System.Text

Public Class Form1
    Dim clientSocket As New TcpClient()
    Dim serverStream As NetworkStream


    ' Verbinden Event
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        connect("127.0.0.1", 8888)
    End Sub

    ' Senden Event
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Text einlesen
        Dim text As String = TextBox1.Text
        ' Leere Nachricht senden, falls nichts eingegeben wird
        If text.Length = 0 Then
            text = "Nachricht vom Client"
        End If
        ' Nachricht senden
        send(text)
        ' Nachricht bekommen
        recieve()
    End Sub

    ' Verbinden
    Sub connect(ip As String, port As Integer)
        msg("Client Gestartet")
        clientSocket.Close()
        clientSocket = New TcpClient()
        clientSocket.Connect(ip, port)
    End Sub

    ' Nachricht senden
    Sub send(mesg As String)
        Try
            ' Verbindung
            Dim serverStream As NetworkStream = clientSocket.GetStream()
            ' Nachricht in Bytes umwandeln
            Dim outStream As Byte() = Encoding.ASCII.GetBytes(mesg & "$")
            ' senden
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
            msg("Client: " & mesg)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    ' Nachrichten bekommen
    Sub recieve()
        Try
            Dim serverStream As NetworkStream = clientSocket.GetStream()
            Dim inStream(65537) As Byte
            ' Nachrichten einlesen
            serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize)
            ' In String umwandeln
            msg("Daten vom Server : " + Encoding.ASCII.GetString(inStream))
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    ' Gibt Nachricht aus
    Sub msg(ByVal msg As String)
        ListBox1.Items.Add(msg)
    End Sub

End Class