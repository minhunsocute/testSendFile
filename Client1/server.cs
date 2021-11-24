using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client1
{
    class server
    {
        IPEndPoint ipEnd_server;
        Socket sock_server;
        public server()
        {
            string IpAddressString = "192.168.1.13";
            ipEnd_server = new IPEndPoint(IPAddress.Parse(IpAddressString), 5656);
            sock_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock_server.Bind(ipEnd_server);
        }
        public static string receivedPath = "C:/Users/ASUS/OneDrive/caro/OneDrive/Desktop/";
        public static string curMsg_server = "Stopped!";


        public void StartServer()
        {
            try
            {
                curMsg_server = "Starting...";
                sock_server.Listen(100);
                curMsg_server = "Running and waiting to receive file.";

                Socket clientSock = sock_server.Accept();
                clientSock.ReceiveBufferSize = 16384;

                byte[] clientData = new byte[1024 * 50000];

                int receivedBytesLen = clientSock.Receive(clientData);
                curMsg_server = "Receiving data...";

                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.UTF8.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + "/" + fileName, FileMode.Append));
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                curMsg_server = "Saving file...";
                bWrite.Close();

                clientSock.Close();
                curMsg_server = "Received and Archived file [" + fileName + "] (" + (receivedBytesLen - 4 - fileNameLen) + " bytes received); Server stopped.";

            }
            catch (SocketException ex)
            {
                curMsg_server = "File Receving error.";
            }
        }
    }
}
