namespace _Project.Scripts
{
    public class GameLoggerTextConnector : BehaviourModuleConnector
    {
        [Inject] private GameLoggerModule m_GameLoggerModule;
        [Inject] private LocalizationModule m_LocalizationModule;

        [SelfInject] private GuiTextModule m_TextModule;

        protected override void Initialize()
        {
            m_GameLoggerModule.Logged += GameLoggerModuleOnLogged;
            m_GameLoggerModule.LoggedLocalized += GameLoggerModuleOnLoggedLocalized;
        }

        private void GameLoggerModuleOnLoggedLocalized(string key)
        {
            m_TextModule.Text = m_LocalizationModule.GetValue(key);
        }

        private void GameLoggerModuleOnLogged(string message)
        {
            m_TextModule.Text = message;
        }
    }
}