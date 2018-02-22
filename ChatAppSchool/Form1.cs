using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatAppSchool
{
    public partial class Form1 : Form
    {
        private Thread thread;
        private Client client;
        private Server server;
        private bool connected = false;
        private bool listening = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ConnectServerLabel_Click(object sender, EventArgs e)
        {

        }

        private void lstChat_TextChanged(object sender, EventArgs e)
        {
            
        }

        //Opens a new thread to listen to new clients.
        private void BtnListen_Click(object sender, EventArgs e)
        {
            if(!listening)
            {
                listening = true;
                this.BtnListen.Text = "Stop Listening";
                Thread threadListen = new Thread(new ThreadStart(ListenCheck));
                threadListen.IsBackground = true;
                threadListen.Start();
                
            } else
            {
                listening = false;
                this.BtnListen.Text = "Start Listening";
                DisconnectServer();
            }
            
        }

        //Listens for a new tcpclient. Starts a new thread once it finds one.
        private void ListenCheck()
        {
            server = new Server(this);
            server.Connect(8080);
        }

        //Starts a new thread to set up a connection to a server.
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if(!connected)
            {
                Thread threadConnect = new Thread(new ThreadStart(CheckConnect));
                threadConnect.IsBackground = true;
                threadConnect.Start();
                this.BtnConnect.Text = "Disconnect";
                connected = true;
            } else
            {
                DisconnectClient();
                this.BtnConnect.Text = "Connect";
                connected = false;
            } 
        }

        private void DisconnectClient()
        {
            client.Disconnect();
        }

        private void DisconnectServer()
        {
            server.Disconnect();
        }

        //Sets up a new client to begin chatting with.
        private void CheckConnect()
        {
            client = new Client(this);
            client.Connect("127.0.0.1", 8080);
        }

        //Sends the message to a listening client/server. Clears and focusses the messagebox 
        //to type more messages.
        private void BtnSend_Click(object sender, EventArgs e)
        {
            if(client != null)
            {
                client.WriteMessage(this.TxtMessage.Text);
            }
            if(server != null)
            {
                server.WriteMessage(this.TxtMessage.Text);
            }
            
        }
    }
}
