using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatAppSchool
{
    class Server
    {
        private TcpClient tcpClient;
        private TcpListener listener;
        private NetworkStream networkStream;
        private Thread thread;
        private Form1 form;

        public Server(Form1 form)
        {
            this.form = form;
        }

        public void Connect(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            ConnectClient();
            //listener.Start();
            //form.UpdateLstChat("Listening for a client.");

            //tcpClient = listener.AcceptTcpClient();
            //form.UpdateLstChat("Connecting...");
            //thread = new Thread(new ThreadStart(ReceiveData));
            //thread.Start();
        }

        public void Connect(string ip, int port)
        {
            long newIp = 0;
            try
            {
                newIp = Convert.ToInt64(ip);
                listener = new TcpListener(new IPEndPoint(newIp, port));
                ConnectClient();
                //listener.Start();
                //tcpClient = listener.AcceptTcpClient();
                //form.UpdateLstChat("Connecting...");
                //thread = new Thread(new ThreadStart(ReceiveData));
                //thread.Start();

            }
            catch(FormatException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void ConnectClient()
        {
            listener.Start();
            form.UpdateLstChat("Listening for a client.");

            tcpClient = listener.AcceptTcpClient();
            form.UpdateLstChat("Connecting...");
            thread = new Thread(new ThreadStart(ReceiveData));
            thread.IsBackground = true;
            thread.Start();
        }

        public void WriteMessage(string message)
        {
            try
            {
                Byte[] sendingMessage = new Byte[1024];
                sendingMessage = Encoding.ASCII.GetBytes(message);
                networkStream.Write(sendingMessage, 0, sendingMessage.Length);
                form.UpdateLstChat(">> \t" + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred: " + ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                WriteMessage("Server disconnected.");
                form.UpdateLstChat("Stopped listening!");
                tcpClient.Close();
                networkStream.Close();
                listener.Stop();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("An error has occurred: " + ex.Message);
            }
        }

        public void ReceiveData()
        {
            string text;
            StringBuilder incomingMessage = new StringBuilder();
            int numberOfBytes = 0;

            Byte[] byteSize = new Byte[1024];

            try
            {
                networkStream = tcpClient.GetStream();

                WriteMessage("Connected!");

                while (true)
                {
                    numberOfBytes = networkStream.Read(byteSize, 0, byteSize.Length);
                    incomingMessage.AppendFormat("{0}", Encoding.ASCII.GetString(byteSize, 0, numberOfBytes));
                    text = incomingMessage.ToString();
                    if (text.Equals("bye"))
                    {
                        form.UpdateLstChat("Disconnecting chat.");
                        break;
                    }
                    if (!text.Equals(""))
                    {
                        form.UpdateLstChat("<< \t" + text);

                        //Clears message and byte array for new messages.
                        incomingMessage.Clear();
                        byteSize = new Byte[1024];
                    }
                }

                byteSize = Encoding.ASCII.GetBytes("bye");
                networkStream.Write(byteSize, 0, byteSize.Length);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred: " + ex.Message);
            }
            finally
            {
                networkStream.Close();
                tcpClient.Close();

                form.UpdateLstChat("Connection closed.");
            }
        }
    }
}

