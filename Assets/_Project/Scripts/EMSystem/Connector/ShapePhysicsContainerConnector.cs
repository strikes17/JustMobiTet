using System;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapePhysicsContainerConnector : BehaviourModuleConnector
    {
        [Inject] private ShapePhysicsModule m_ShapePhysicsModule;
        [Inject] private ShapeContainerModule m_ShapeContainerModule;

        protected override void Initialize()
        {
            m_ShapeContainerModule.ElementAdded += ShapeContainerModuleOnElementAdded;
            m_ShapeContainerModule.ElementRemoved += ShapeContainerModuleOnElementRemoved;

            foreach (var abstractEntity in m_ShapeContainerModule.ContainerCollection)
            {
                m_ShapePhysicsModule.AddShape(abstractEntity as ShapeEntity);
            }
        }

        private void ShapeContainerModuleOnElementAdded(AbstractEntity abstractEntity)
        {
            m_ShapePhysicsModule.AddShape(abstractEntity as ShapeEntity);
        }

        private void ShapeContainerModuleOnElementRemoved(AbstractEntity abstractEntity)
        {
            m_ShapePhysicsModule.Remove(abstractEntity as ShapeEntity);
            m_ShapePhysicsModule.Solve();
        }
    }
}