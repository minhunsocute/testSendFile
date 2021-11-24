using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace Server1
{
    class client11
    {
        public static string curMsg_client = "Idle";
        public static void SendFile(string fileName)
        {
            try
            {
                //IPAddress[] ipAddress = Dns.GetHostAddresses("localhost");
                //IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5656);

                string IpAddressString = "192.168.1.13";
                IPEndPoint ipEnd_client = new IPEndPoint(IPAddress.Parse(IpAddressString), 5656);
                Socket clientSock_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


                string filePath = "";

                fileName = fileName.Replace("\\", "/");
                while (fileName.IndexOf("/") > -1)
                {
                    filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                }


                byte[] fileNameByte = Encoding.UTF8.GetBytes(fileName);
                if (fileNameByte.Length > 5000 * 1024)
                {
                    curMsg_client = "File size is more than 5Mb, please try with small file.";
                    return;
                }

                curMsg_client = "Buffering ...";
                string fullPath = filePath + fileName;

                byte[] fileData = File.ReadAllBytes(fullPath);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                curMsg_client = "Connection to server ...";
                clientSock_client.Connect(ipEnd_client);

                curMsg_client = "File sending...";
                clientSock_client.Send(clientData, 0, clientData.Length, 0);

                curMsg_client = "Disconnecting...";
                clientSock_client.Close();
                curMsg_client = "File [" + fullPath + "] transferred.";

            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it")
                    curMsg_client = "File Sending fail. Because server not running.";
                else
                    curMsg_client = "File Sending fail." + ex.Message;
            }

        }
    }
}
