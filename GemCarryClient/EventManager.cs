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
        public delegate void ConnectEvent(bool isConnected);
        public delegate void LoginEvent(bool success);
        public delegate void ChatEvent(string sender, string message);
        public delegate void CreateUserEvent(bool success);

        // Event Dispatchers
        public event ConnectEvent ConnectedDispatcher;
        public event LoginEvent LoginDispatcher;
        public event ChatEvent ChatDispatcher;
        public event CreateUserEvent CreateUserDispatcher;

        // Message Handlers
        public void HandleMessage(ConnectResponse message)
        {
            if (null != ConnectedDispatcher)
            {
                ConnectedDispatcher(message.success);
            }
        }

        public void HandleMessage(LoginResponse message)
        {
            if (null != LoginDispatcher)
            {
                LoginDispatcher(message.success);
            }

            if (true == message.success && null != ChatDispatcher)
            {
                ChatDispatcher("Server", "Successfully logged in!");
            }
            else
            {
                ChatDispatcher("Server", "Username or password is invalid.");
            }
        }

        public void HandleMessage(ChatMessage message)
        {
            if (null != ChatDispatcher)
            {
                ChatDispatcher(message.sender, message.message);
            }
        }

        public void HandleMessage(CreateUserResponse message)
        {
            if (null != CreateUserDispatcher)
            {
                CreateUserDispatcher(message.success);
            }

            if(true == message.success && null != ChatDispatcher)
            {
                ChatDispatcher("Server", "Successfully created user!");
            }
            else
            {
                ChatDispatcher("Server", "Username already exists.");
            }
        }

    }
}
