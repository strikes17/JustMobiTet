using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class DragShapeConnector : BehaviourModuleConnector
    {
        [SelfInject] private DragShapeModule m_DragShapeModule;
        [SelfInject] private TransformAnimationModule m_TransformAnimationModule;

        [Inject] private SideValidatorModule m_SideValidatorModule;
        [Inject] private CoordinatesConvertModule m_CoordinatesConvertModule;
        [Inject] private GameLoggerModule m_GameLoggerModule;

        private int m_LayerMask;

        protected override void Initialize()
        {
            m_DragShapeModule.DragFinished += DragShapeModuleOnDragFinished;
            m_DragShapeModule.DragUpdate += DragShapeModuleOnDragUpdate;
            m_DragShapeModule.DragStarted += DragShapeModuleOnDragStarted;
            m_LayerMask = 1 << LayerMask.NameToLayer("Hole");
        }

        private void DragShapeModuleOnDragStarted(GuiDefaultEntity arg1, Vector2 arg2)
        {
            m_TransformAnimationModule.StopAllAnimations();
        }

        private void DragShapeModuleOnDragUpdate(GuiDefaultEntity guiDefaultEntity, Vector2 arg2)
        {
            var pos = m_CoordinatesConvertModule.GetWorldFromScreen(arg2);
            guiDefaultEntity.transform.position = pos;
        }

        private void DragShapeModuleOnDragFinished(GuiDefaultEntity guiDefaultEntity, Vector2 position)
        {
            var worldFromScreen = m_CoordinatesConvertModule.GetWorldFromScreen(position);
            var side = m_SideValidatorModule.DefineSide(worldFromScreen);
            if (side == Side.Left)
            {
                var shapePosition = guiDefaultEntity.GetBehaviorModuleByType<ShapePositionModule>().ShapePosition;
                var collider2D = Physics2D.OverlapBox(worldFromScreen, shapePosition.HalfSize, 0f, m_LayerMask);
                if (collider2D != null)
                {
                    if (collider2D.GetComponent<HoleTransformEntity>() != null)
                    {
                        var destroyModule = guiDefaultEntity.GetBehaviorModuleByType<DestroyModule>();
                        destroyModule.RequestToDestroy();
                        m_GameLoggerModule.LogLocalized("shape_dropped_hole");
                    }
                    else
                    {
                        var positionModule = guiDefaultEntity.GetBehaviorModuleByType<ShapePositionModule>();
                        var placementTransformAnimation =
                            m_TransformAnimationModule.GetAnimation<PlacementReturnTransformAnimation>();
                        placementTransformAnimation.Position = positionModule.ShapePosition.Position;
                        placementTransformAnimation.Play();
                        m_GameLoggerModule.LogLocalized("shape_placed");
                    }
                }
                else
                {
                    var positionModule = guiDefaultEntity.GetBehaviorModuleByType<ShapePositionModule>();
                    var placementTransformAnimation =
                        m_TransformAnimationModule.GetAnimation<PlacementReturnTransformAnimation>();
                    placementTransformAnimation.Position = positionModule.ShapePosition.Position;
                    placementTransformAnimation.Play();
                    m_GameLoggerModule.LogLocalized("shape_placed_oob");
                }
            }
            else
            {
                var positionModule = guiDefaultEntity.GetBehaviorModuleByType<ShapePositionModule>();
                var placementTransformAnimation =
                    m_TransformAnimationModule.GetAnimation<PlacementReturnTransformAnimation>();
                placementTransformAnimation.Position = positionModule.ShapePosition.Position;
                placementTransformAnimation.Play();
                m_GameLoggerModule.LogLocalized("shape_placed_oob");
            }
        }
    }
}