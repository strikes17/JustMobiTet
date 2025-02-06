using UnityEngine;

namespace _Project.Scripts
{
    public class ShapePositionScreenFillerConnector : BehaviourModuleConnector
    {
        [SelfInject] private ShapePositionModule m_ShapePositionModule;
        [SelfInject] private AdaptiveTransformModule m_HalfScreenFillerModule;
        
        protected override void Initialize()
        {
            m_HalfScreenFillerModule.Updated += HalfScreenFillerModuleOnUpdated;
        }

        private void HalfScreenFillerModuleOnUpdated(Transform obj)
        {
            m_ShapePositionModule.ShapePosition.Position = obj.transform.position;
            m_ShapePositionModule.ShapePosition.Size = obj.transform.localScale;
        }
    }
}