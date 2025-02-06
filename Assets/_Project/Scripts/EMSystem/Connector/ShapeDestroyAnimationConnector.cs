using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeDestroyAnimationConnector : BehaviourModuleConnector
    {
        [SelfInject] private TransformAnimationModule m_TransformAnimationModule;
        [SelfInject] private DestroyModule m_ShapeDestroyModule;
        
        [Inject(typeof(HoleTransformEntity))] private RootTransformModule m_HoleRootTransformModule;

        protected override void Initialize()
        {
            m_ShapeDestroyModule.DestroyRequested += CalledDestroy;
            var dropInHoleAnimation =
                m_TransformAnimationModule.GetAnimation<ShapeDropInHoleAnimation>();
            
            dropInHoleAnimation.Finished += DropInHoleAnimationOnFinished;
        }

        private void DropInHoleAnimationOnFinished()
        {
            m_ShapeDestroyModule.MarkAsDestroyable();
        }
        
        private void CalledDestroy(AbstractEntity obj)
        {
            var anim = m_TransformAnimationModule.GetAnimation<ShapeDropInHoleAnimation>();
            anim.HolePosition = m_HoleRootTransformModule.Transform.position;
            anim.Play();
        }
    }
}