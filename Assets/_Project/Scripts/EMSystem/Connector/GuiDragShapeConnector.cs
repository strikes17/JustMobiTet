using System;
using UnityEngine;

namespace _Project.Scripts
{
    // [Serializable]
    // public class CameraModule : AbstractBehaviourModule
    // {
    //     [SerializeField] private Camera m_Camera;
    //
    //     public Camera Camera => m_Camera;
    // }
    //
    // public class GuiDragShapeOOBLoggerConnector : BehaviourModuleConnector
    // {
    //     [SelfInject] private GuiDragShapeModule m_DragShapeModule;
    //     [Inject] private GameLoggerModule m_GameLoggerModule;
    //     [Inject] private CameraModule m_CameraModule;
    //
    //     private Camera m_Camera;
    //
    //     protected override void Initialize()
    //     {
    //         m_Camera = m_CameraModule.Camera;
    //         m_DragShapeModule.DragUpdate += DragShapeModuleOnDragUpdate;
    //     }
    //
    //     private void DragShapeModuleOnDragUpdate(GuiDefaultEntity arg1, Vector2 position)
    //     {
    //         var rightBorder = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, 0));
    //         var centerBorder = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, 0f));
    //         var topBorder = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, 0));
    //         var bottomBorder = m_Camera.ScreenToWorldPoint(new Vector3(0f, 0f));
    //
    //         if (position.x >= rightBorder.x || position.x <= centerBorder.x)
    //         {
    //             
    //         }
    //     }
    // }

    [Serializable]
    public class GuiDragShapeConnector : BehaviourModuleConnector
    {
        [SelfInject] private GuiDragShapeModule m_DragShapeModule;

        [Inject(typeof(ShapeScrollContentEntity))]
        private RootTransformModule m_ShapeScrollRootModule;

        [Inject(typeof(ShapeScrollContentEntity))]
        private GuiHLayoutGroupModule m_HorizontalLayoutGroupModule;

        [Inject(typeof(ShapeDragAreaEntity))] private RootTransformModule m_ShapeDragAreaRootModule;

        [Inject] private SideValidatorModule m_SideValidatorModule;
        [Inject] private CoordinatesConvertModule m_CoordinatesConvertModule;
        [Inject] private UiRaycastValidatorModule m_RaycastValidatorModule;

        [Inject] private ShapePlaceValidationModule m_PlaceValidationModule;
        [Inject] private ShapeContainerModule m_ShapeContainerModule;
        [Inject] private ShapePlacerModule m_ShapePlacerModule;
        [Inject] private ShapeMockContainerModule m_ShapeMockContainerModule;
        [Inject] private GameLoggerModule m_GameLoggerModule;

        protected override void Initialize()
        {
            m_DragShapeModule.SetParents(m_ShapeScrollRootModule.Transform, m_ShapeDragAreaRootModule.Transform);
            m_DragShapeModule.DragFinished += DragShapeModuleOnDragFinished;
            m_DragShapeModule.DragStarted += DragShapeModuleOnDragStarted;
            m_ShapeContainerModule.ElementAdded += ShapeContainerModuleOnElementAdded;
            m_ShapeContainerModule.ElementRemoved += ShapeContainerModuleOnElementRemoved;

            foreach (var abstractEntity in m_ShapeContainerModule.ContainerCollection)
            {
                var shapeEntity = abstractEntity as ShapeEntity;
                m_PlaceValidationModule.AddShapeToValidation(shapeEntity);
                m_ShapePlacerModule.AddShape(shapeEntity);
            }
        }

        private void ShapeContainerModuleOnElementRemoved(AbstractEntity entity)
        {
            var shapeEntity = entity as ShapeEntity;
            m_PlaceValidationModule.RemoveShapeFromValidation(shapeEntity);
            m_ShapePlacerModule.RemoveShape(shapeEntity);
        }

        private void ShapeContainerModuleOnElementAdded(AbstractEntity entity)
        {
            var shapeEntity = entity as ShapeEntity;
            m_PlaceValidationModule.AddShapeToValidation(shapeEntity);
            m_ShapePlacerModule.AddShape(shapeEntity);
        }

        private void DragShapeModuleOnDragStarted(GuiDefaultEntity guiDefaultEntity, Vector2 position)
        {
            m_HorizontalLayoutGroupModule.HorizontalLayoutGroup.enabled = false;
        }

        private void DragShapeModuleOnDragFinished(GuiDefaultEntity guiDefaultEntity, Vector2 position)
        {
            m_HorizontalLayoutGroupModule.HorizontalLayoutGroup.enabled = true;
            if (m_RaycastValidatorModule.IsPointOverUi(position))
            {
                return;
            }

            var worldFromScreen = m_CoordinatesConvertModule.GetWorldFromScreen(position);
            var side = m_SideValidatorModule.DefineSide(worldFromScreen);
            var paletteData = guiDefaultEntity.GetBehaviorModuleByType<GuiShapePaletteElementModule>();
            if (side == Side.Left)
            {
                m_ShapeMockContainerModule.SpawnShapeMock(paletteData.ShapeDataDto, worldFromScreen);
                m_GameLoggerModule.LogLocalized("tower_missed");
            }
            else
            {
                var shapePositionModule = paletteData.ShapeDataDto.ShapePrefab.GetComponent<ShapeEntity>()
                    .GetBehaviorModuleByType<ShapePositionModule>();
                shapePositionModule.ShapePosition.Position = worldFromScreen;

                var isValidPlaceForShape =
                    m_PlaceValidationModule.IsValidPlaceForShape(shapePositionModule.ShapePosition);


                if (isValidPlaceForShape == ValidationResult.Success)
                {
                    var shape = m_ShapeContainerModule.SpawnShape(paletteData.ShapeDataDto, worldFromScreen);
                    shape.GetBehaviorModuleByType<ShapePositionModule>().ShapePosition.Position.x = worldFromScreen.x;
                    m_GameLoggerModule.LogLocalized("shape_placed");
                }
                else
                {
                    if (isValidPlaceForShape == ValidationResult.Missed)
                    {
                        m_GameLoggerModule.LogLocalized("tower_missed");
                    }
                    else if (isValidPlaceForShape == ValidationResult.OutOfBounds)
                    {
                        m_GameLoggerModule.LogLocalized("shape_placed_too_high");
                    }
                    else if (isValidPlaceForShape == ValidationResult.LowPosition)
                    {
                        m_GameLoggerModule.LogLocalized("shape_placed_lower");
                    }

                    m_ShapeMockContainerModule.SpawnShapeMock(paletteData.ShapeDataDto, worldFromScreen);
                }
            }
        }
    }
}