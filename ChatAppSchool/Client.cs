using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatAppSchool
{
    class Client
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private Thread thread;
        private Form1 form;
        private bool serverDisconnected = false;
        private Stopwatch stopwatch = new Stopwatch();
        private bool firstPing = false;

        public Client(Form1 form)
        {
            this.form = form;
        }

        public void Connect(string ip, int port)
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(ip, port);
                thread = new Thread(new ThreadStart(ReceiveData));
                thread.IsBackground = true;
                thread.Start();
            } catch(Exception ex)
            {
                form.UpdateLstChat("No connection available.");
                Console.WriteLine("An error has occurred: " + ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                form.UpdateLstChat("Disconnected!");
                tcpClient.Close();
                networkStream.Close();
            } catch(NullReferenceException ex)
            {
                Console.WriteLine("An error has occurred: " + ex.Message);
            }
        }

        public void WriteMessage(string message)
        {
            try
            {
                if(!firstPing)
                {
                    Byte[] sendingMessage = new Byte[1024];
                    sendingMessage = Encoding.ASCII.GetBytes(message);
                    networkStream.Write(sendingMessage, 0, sendingMessage.Length);
                    form.UpdateLstChat(">> \t" + message);
                } else
                {
                    //Create load sending message queue.
                }
            }
            catch (Exception ex)
            {
                Disconnect();
                Console.WriteLine("An error has occurred: " + ex.Message);
            }
        }

        public void ReceiveData()
        {
            string text;
            StringBuilder incomingMessage = new StringBuilder();
            int numberOfBytes = 0;

            Byte[] byteSize = new Byte[1024];
            stopwatch.Start();

            try
            {
                networkStream = tcpClient.GetStream();

                while (true)
                {
                    numberOfBytes = networkStream.Read(byteSize, 0, byteSize.Length);
                    incomingMessage.AppendFormat("{0}", Encoding.ASCII.GetString(byteSize, 0, numberOfBytes));
                    text = incomingMessage.ToString();
                    if (text.Equals("bye"))
                    {
                        Disconnect();
                        break;
                    }
                    if (!text.Equals(""))
                    {
                        stopwatch.Restart();
                        
                        form.UpdateLstChat("<< \t" + text);
                        //Clears message and byte array for new messages.
                        incomingMessage.Clear();
                        byteSize = new Byte[1024];
                    }
                    if(PingServer())
                    {
                        Disconnect();
                        break;
                    }
                }

                byteSize = Encoding.ASCII.GetBytes("bye");
                networkStream.Write(byteSize, 0, byteSize.Length);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred: " + ex.Message);
                networkStream.Close();
                tcpClient.Close();
                form.UpdateLstChat("Connection closed.");
            }
        }

        private bool PingServer()
        {
            if (tcpClient.Client.Poll(0, SelectMode.SelectRead) && !firstPing)
            {
                byte[] buff = new byte[1];
                if (tcpClient.Client.Receive(buff, SocketFlags.Peek) == 0)
                {
                    form.UpdateLstChat("Pinging server.");
                    firstPing = true;
                }
            }

            if (stopwatch.ElapsedMilliseconds > 10000 && firstPing)
            {
                form.UpdateLstChat("Pinging server after 5 sec.");
                // Detect if client disconnected
                if (tcpClient.Client.Poll(0, SelectMode.SelectRead))
                {
                    form.UpdateLstChat("Ping failed.");
                    byte[] buff = new byte[1];
                    if (tcpClient.Client.Receive(buff, SocketFlags.Peek) == 0)
                    {
                        // Client disconnected
                        return true;
                    }
                    firstPing = false;
                }
            }
            
            return false;
        }
    }
}
