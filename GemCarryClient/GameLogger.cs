namespace GemCarryClient
{
    public class GameLogger
    {

        public static GameLogger sInstance = null;

        private GameLogger() { }
        ~GameLogger() { }

        public static GameLogger GetInstance()
        {
            if (null == sInstance)
            {
                sInstance = new GameLogger();
            }
            return sInstance;
        }

        public void WriteWarning()
        {

        }

        public void WriteError()
        {

        }

        public void WriteDebug()
        {
        }
    }
}
