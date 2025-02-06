namespace _Project.Scripts
{
    public class GuiPopupAnimatorLoggerConnector : BehaviourModuleConnector
    {
        [SelfInject] private GuiPopupAnimatorModule m_GuiPopupAnimatorModule;
        [Inject] private GameLoggerModule m_GameLoggerModule;

        protected override void Initialize()
        {
            m_GameLoggerModule.LoggedAny += GameLoggerModuleOnLoggedAny;
        }

        private void GameLoggerModuleOnLoggedAny(string obj)
        {
            m_GuiPopupAnimatorModule.Show();
        }
    }
}