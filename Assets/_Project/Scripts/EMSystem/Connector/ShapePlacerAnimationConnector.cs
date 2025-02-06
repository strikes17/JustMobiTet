using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapePlacerAnimationConnector : BehaviourModuleConnector
    {
        [Inject] private ShapePlacerModule m_ShapePlacerModule;
        
        [SelfInject] private TransformAnimationModule m_TransformAnimationModule;

        protected override void Initialize()
        {
            m_ShapePlacerModule.ShapePlaced += ShapePlacerModuleOnShapePlaced;    
            m_ShapePlacerModule.PlaceShapeAlignedToTopShape(m_AbstractEntity as ShapeEntity);
        }

        private void ShapePlacerModuleOnShapePlaced(ShapeEntity shapeEntity, Vector2 position)
        {
            if (shapeEntity == m_AbstractEntity)
            {
                var placementTransformAnimation = m_TransformAnimationModule.GetAnimation<PlacementTransformAnimation>();
                placementTransformAnimation.Position = position;
                placementTransformAnimation.Play();
            }
        }
    }
}