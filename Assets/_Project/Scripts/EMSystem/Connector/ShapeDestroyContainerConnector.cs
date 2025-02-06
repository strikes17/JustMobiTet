using System;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeDestroyContainerConnector : BehaviourModuleConnector
    {
        [Inject] private ShapeContainerModule m_ShapeContainerModule;
        [SelfInject] private DestroyModule m_ShapeDestroyModule;

        protected override void Initialize()
        {
            m_ShapeDestroyModule.DestroyRequested += Destroyed;
        }

        private void Destroyed(AbstractEntity obj)
        {
            m_ShapeContainerModule.RemoveElement(obj);
        }

    }
}