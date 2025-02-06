using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapePhysicsAnimationConnector : BehaviourModuleConnector
    {
        [Inject] private ShapePhysicsModule m_ShapePhysicsModule;
        
        [SelfInject] private TransformAnimationModule m_TransformAnimationModule;

        protected override void Initialize()
        {
            m_ShapePhysicsModule.ShapePhysicsSolved += ShapePhysicsModuleOnShapePhysicsSolved;
        }

        private void ShapePhysicsModuleOnShapePhysicsSolved(ShapeEntity solvedEntity, ShapePosition shapePosition)
        {
            m_TransformAnimationModule.GetAnimation<PlacementTransformAnimation>().Stop();
            if (solvedEntity == m_AbstractEntity)
            {
                var animation = m_TransformAnimationModule.GetAnimation<PhysicsFallTransformAnimation>();
                animation.Position = shapePosition.Position;
                animation.Play();
            }
        }
    }
}