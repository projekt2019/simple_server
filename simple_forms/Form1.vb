Imports System.Net.Sockets
Imports System.Text
Imports klassen

Public Class Form1
    Private WithEvents handler As ClientMessageHandler = New ClientMessageHandler()


    ' Verbinden Event2
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        handler.connect("127.0.0.1", 8888)
        msg("Connected!")
    End Sub

    Private Sub HandleRecieveMessage(str As String) Handles handler.RecievedMessage
        msg(str)
    End Sub

    ' Senden Event
    Private Async Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Text einlesen
        Dim text As String = TextBox1.Text
        ' Leere Nachricht senden, falls nichts eingegeben wird
        If text.Length = 0 Then
            text = "Nachricht vom Client"
        End If
        ' Nachricht senden
        handler.send(text)
    End Sub

    ' Gibt Nachricht aus
    Sub msg(ByVal msg As String)
        ListBox1.Items.Add(msg)
    End Sub

End Class