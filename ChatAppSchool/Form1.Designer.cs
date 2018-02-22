using System.Net.Sockets;
using System.Threading;

namespace ChatAppSchool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtMessage = new System.Windows.Forms.TextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.BtnListen = new System.Windows.Forms.Button();
            this.TxtServerIP = new System.Windows.Forms.TextBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.ConnectServerLabel = new System.Windows.Forms.Label();
            this.ChatServerIPLabel = new System.Windows.Forms.Label();
            this.lstChat = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // TxtMessage
            // 
            this.TxtMessage.Location = new System.Drawing.Point(12, 345);
            this.TxtMessage.Name = "TxtMessage";
            this.TxtMessage.Size = new System.Drawing.Size(442, 20);
            this.TxtMessage.TabIndex = 0;
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(461, 345);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(107, 20);
            this.BtnSend.TabIndex = 1;
            this.BtnSend.Text = "btnSend";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // BtnListen
            // 
            this.BtnListen.Location = new System.Drawing.Point(574, 13);
            this.BtnListen.Name = "BtnListen";
            this.BtnListen.Size = new System.Drawing.Size(144, 45);
            this.BtnListen.TabIndex = 2;
            this.BtnListen.Text = "btnListen";
            this.BtnListen.UseVisualStyleBackColor = true;
            this.BtnListen.Click += new System.EventHandler(this.BtnListen_Click);
            // 
            // TxtServerIP
            // 
            this.TxtServerIP.Location = new System.Drawing.Point(587, 133);
            this.TxtServerIP.Name = "TxtServerIP";
            this.TxtServerIP.Size = new System.Drawing.Size(120, 20);
            this.TxtServerIP.TabIndex = 3;
            this.TxtServerIP.Text = "txtServerIP";
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(587, 164);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(120, 23);
            this.BtnConnect.TabIndex = 4;
            this.BtnConnect.Text = "btnConnect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // ConnectServerLabel
            // 
            this.ConnectServerLabel.AutoSize = true;
            this.ConnectServerLabel.Location = new System.Drawing.Point(584, 117);
            this.ConnectServerLabel.Name = "ConnectServerLabel";
            this.ConnectServerLabel.Size = new System.Drawing.Size(74, 13);
            this.ConnectServerLabel.TabIndex = 5;
            this.ConnectServerLabel.Text = "Chatserver IP:";
            this.ConnectServerLabel.Click += new System.EventHandler(this.ConnectServerLabel_Click);
            // 
            // ChatServerIPLabel
            // 
            this.ChatServerIPLabel.AutoSize = true;
            this.ChatServerIPLabel.Location = new System.Drawing.Point(584, 83);
            this.ChatServerIPLabel.Name = "ChatServerIPLabel";
            this.ChatServerIPLabel.Size = new System.Drawing.Size(96, 13);
            this.ChatServerIPLabel.TabIndex = 6;
            this.ChatServerIPLabel.Text = "Connect to Server.";
            // 
            // lstChat
            // 
            this.lstChat.FormattingEnabled = true;
            this.lstChat.Location = new System.Drawing.Point(12, 13);
            this.lstChat.Name = "lstChat";
            this.lstChat.Size = new System.Drawing.Size(556, 316);
            this.lstChat.TabIndex = 8;
            // 
            // Form1
            // 
            this.AcceptButton = this.BtnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 377);
            this.Controls.Add(this.lstChat);
            this.Controls.Add(this.ChatServerIPLabel);
            this.Controls.Add(this.ConnectServerLabel);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.TxtServerIP);
            this.Controls.Add(this.BtnListen);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.TxtMessage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtMessage;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.Button BtnListen;
        private System.Windows.Forms.TextBox TxtServerIP;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Label ConnectServerLabel;
        private System.Windows.Forms.Label ChatServerIPLabel;

        delegate void InputDelegate(string input);

        public void UpdateLstChat(string input)
        {
            if(this.lstChat.InvokeRequired)
            {
                InputDelegate inputDelegate = new InputDelegate(UpdateLstChat);
                this.Invoke(inputDelegate, new object[] { input });
            }
            else
            {
                this.lstChat.Items.Add(input);
                this.lstChat.SelectedIndex = this.lstChat.Items.Count - 1;

                this.TxtMessage.Text = "";
                this.TxtMessage.Focus();
            }
        }

        private System.Windows.Forms.ListBox lstChat;
    }
}