using System;
using System.Windows.Forms;
using GCMessaging;

namespace GemCarryClient
{
    public partial class MainForm : Form
    {
        private SocketManager mSocketManager;

        public MainForm()
        {
            mSocketManager = new SocketManager();
            InitializeComponent();

            EventManager.GetInstance().ConnectedDispatcher += new EventManager.ConnectEvent(Connected);
            EventManager.GetInstance().ChatDispatcher += new EventManager.ChatEvent(ReceiveChat);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            mSocketManager.EndServerConnection();
        }

        private void connect_button_Click(object sender, EventArgs e)
        {
            if (false == mSocketManager.IsConnected())
            {
                mSocketManager.StartServerConnection();
                connect_button.Text = "Connected";
            }
            else
            {
                mSocketManager.EndServerConnection();
                connect_button.Text = "Connect";
            }
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            if (true == mSocketManager.IsConnected())
            {
                LoginRequest msg = new LoginRequest()
                {
                    username = username_text.Text,
                    password = password_text.Text,
                };

                mSocketManager.DispatchMessage(msg);
            }
        }

        private void create_user_button_Click(object sender, EventArgs e)
        {
            CreateUserForm createUserForm = new CreateUserForm(mSocketManager);
            createUserForm.Show();
        }

        private void sendChat_button_Click(object sender, EventArgs e)
        {
            if(true == mSocketManager.IsConnected())
            {
                ChatMessage msg = new ChatMessage()
                {
                    sender = username_text.Text,
                    message = chatbox_text.Text,
                };

                mSocketManager.DispatchMessage(msg);
            }

            chatbox_text.Text = "";
        }

        public void Connected(bool success)
        {
            object[] p = null;

            if(success)
            {
                p = GetMessageParameters("Successfully connected to server.\n");
            }
            else
            {
                p = GetMessageParameters("Failed to connect.\n");
            }

            BeginInvoke(new InvokeTextDelegate(UpdateConsoleText), p);
        }

        public void ReceiveChat(string sender, string message)
        {
            object[] p = null;

            p = GetMessageParameters( sender + ": " + message + "\n" );

            BeginInvoke(new InvokeTextDelegate(UpdateConsoleText), p);
        }


        public delegate void InvokeTextDelegate(string text);

        public void UpdateConsoleText(string text)
        {
            console_text.AppendText(text);
        }

        private object[] GetMessageParameters( string message )
        {
            // We have to create an object array as this is the only way our
            // UpdateLabelText method can receive the parameters
            object[] delegateParameter = new object[1];

            delegateParameter[0] = message;

            return delegateParameter;
        }

    }
}
