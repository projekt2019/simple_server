Imports System.Net.Sockets
Imports System.Text
Module Module1
    Sub Main()
        Dim serverSocket As New TcpListener(8888)
        Dim requestCount As Integer = 0
        Dim clientSocket As TcpClient
        serverSocket.Start()
        msg("Server Gestartet")
        clientSocket = serverSocket.AcceptTcpClient()

        While (True)
            Try
                requestCount = requestCount + 1
                Dim networkStream As NetworkStream = clientSocket.GetStream()
                Dim bytesFrom(65537) As Byte
                msg(clientSocket.ReceiveBufferSize)
                networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize)
                Dim dataFromClient As String = System.Text.Encoding.ASCII.GetString(bytesFrom)
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"))
                msg("Daten vom Client -  " + dataFromClient)

                Dim serverResponse As String = "Server response " + Convert.ToString(requestCount)
                Dim sendBytes As Byte() = Encoding.ASCII.GetBytes(serverResponse)

                networkStream.Write(sendBytes, 0, sendBytes.Length)
                networkStream.Flush()
                msg(serverResponse)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End While


        clientSocket.Close()
        serverSocket.Stop()
        Console.ReadLine()
    End Sub

    Sub msg(ByVal mesg As String)
        mesg.Trim()
        Console.WriteLine(" >> " + mesg)
    End Sub
End Module