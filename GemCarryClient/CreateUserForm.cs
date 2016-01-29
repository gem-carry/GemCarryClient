using System;
using System.Windows.Forms;
using GCMessaging;


namespace GemCarryClient
{
    public partial class CreateUserForm : Form
    {
        private SocketManager mSocketManager;

        public CreateUserForm(SocketManager socket)
        {
            mSocketManager = socket;

            InitializeComponent();

            EventManager.GetInstance().ServerResponseCodeDispatcher += new EventManager.ServerResponseCodeResponse(ServerResponseCode);
        }

        private void submit_user_button_Click(object sender, EventArgs e)
        {
            // Early return if we are not connected
            if(false == mSocketManager.IsConnected())
            {
                return;
            }

            if (password_text.Text != repassword_text.Text)
            {
                diagnostic_message_label.ForeColor = System.Drawing.Color.Red;
                //diagnostic_message_label.Text = "Passwords do not match.";
            }
            else
            {
                diagnostic_message_label.ForeColor = System.Drawing.Color.Black;
                //diagnostic_message_label.Text = "Submitting request...";

                CreateUserMessage msg = new CreateUserMessage();
                msg.mUsername = username_text.Text;
                msg.mPassword = password_text.Text;
                msg.mPasswordValidate = password_text.Text;

                mSocketManager.DispatchMessage(msg);
            }            
        }

        public delegate void InvokeNumberDelegate(int code);

        public void ServerResponseCode(int response)
        {
            object[] p = new object[1];


            p[0] = response;

            BeginInvoke(new InvokeNumberDelegate(UpdateDiagnosticMessage), p);
        }

        private void UpdateDiagnosticMessage(int response)
        {
            if (0 == response)
            {
                diagnostic_message_label.ForeColor = System.Drawing.Color.Green;
                diagnostic_message_label.Text = "User Created Successfully.";
            }
            else
            {
                diagnostic_message_label.ForeColor = System.Drawing.Color.Red;
                diagnostic_message_label.Text = "User already exists, please try another name.";
            }
        }

    }
}
