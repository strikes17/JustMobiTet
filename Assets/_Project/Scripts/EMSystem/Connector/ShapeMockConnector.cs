using System;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeMockConnector : BehaviourModuleConnector
    {
        [SelfInject] private TransformAnimationModule m_TransformAnimationModule;
        [SelfInject] private DestroyModule m_ShapeDestroyModule;

        protected override void Initialize()
        {
            var expandTransformAnimation =
                m_TransformAnimationModule.GetAnimation<ExplodeTransformAnimation>();

            expandTransformAnimation.Finished += ExpandTransformAnimationOnFinished;
            m_ShapeDestroyModule.MarkAsDestroyable();

            expandTransformAnimation.Play();
        }

        private void ExpandTransformAnimationOnFinished()
        {
            m_ShapeDestroyModule.RequestToDestroy();
        }
    }
}