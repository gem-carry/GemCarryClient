using GCMessaging;

namespace GemCarryClient
{
    public class EventManager
    {
        public static EventManager sInstance = null;

        public static EventManager GetInstance()
        {
            if(null == sInstance)
            {
                sInstance = new EventManager();
            }
            return sInstance;
        }

        EventManager()
        {
        }

        // Delegate definitions
        public delegate void ConnectResponse(bool isConnected);
        public delegate void LoginResponse(bool success);
        public delegate void ChatResponse(string sender, string message);
        public delegate void ServerResponseCodeResponse(int response);

        // Event Dispatchers
        public event ConnectResponse ConnectedDispatcher;
        public event LoginResponse LoginDispatcher;
        public event ChatResponse ChatDispatcher;
        public event ServerResponseCodeResponse ServerResponseCodeDispatcher;

        // Message Handlers
        public void HandleMessage(MessageBase message)
        {
            ConnectedDispatcher(true);
        }

        public void HandleMessage(LoginMessage message)
        {
            LoginDispatcher(true);
        }

        public void HandleMessage(ChatMessage message)
        {
            ChatDispatcher(message.mSender, message.mMessage);
        }

        public void HandleMessage(ServerResponseCodeMessage message)
        {
            ServerResponseCodeDispatcher(message.mResponseCode);
        }

    }
}
