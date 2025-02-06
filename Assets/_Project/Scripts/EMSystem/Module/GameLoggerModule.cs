using System;

namespace _Project.Scripts
{
    [Serializable]
    public class GameLoggerModule : AbstractBehaviourModule
    {
        public event Action<string> Logged = delegate { };
        public event Action<string> LoggedLocalized = delegate { };

        public event Action<string> LoggedAny = delegate { };

        public void Log(string message)
        {
            Logged(message);
            LoggedAny(message);
        }

        public void LogLocalized(string key)
        {
            LoggedLocalized(key);
            LoggedAny(key);
        }
    }
}