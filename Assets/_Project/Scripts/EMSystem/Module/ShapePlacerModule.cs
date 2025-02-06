using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapePlacerModule : AbstractBehaviourModule
    {
        [SerializeField] private Camera m_Camera;

        public event Action<ShapeEntity, Vector2> ShapePlaced = delegate { };

        private List<ShapeEntity> m_Shapes;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_Shapes = new();
        }

        public void AddShape(ShapeEntity shapeEntity)
        {
            m_Shapes.Add(shapeEntity);
        }

        public void RemoveShape(ShapeEntity shapeEntity)
        {
            m_Shapes.Remove(shapeEntity);
        }

        public void PlaceShapeAlignedToTopShape(ShapeEntity shapeEntity)
        {
            if (m_Shapes.Where(x => x != shapeEntity).ToList().Count == 0)
            {
                shapeEntity.GetBehaviorModuleByType<ShapePositionModule>().ShapePosition.Position =
                    shapeEntity.transform.position;
                return;
            }

            var topShape =
                m_Shapes.Where(x => x != shapeEntity)
                    .Aggregate((x, y) => x.transform.position.y > y.transform.position.y ? x : y);


            var shapePositionModule = shapeEntity.GetBehaviorModuleByType<ShapePositionModule>().ShapePosition;
            var topShapePositionModule = topShape.GetBehaviorModuleByType<ShapePositionModule>().ShapePosition;
            var alignedPosition = topShapePositionModule.Position;

            var rightBorder = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, 0));
            var centerBorder = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, 0f));
            alignedPosition.y = topShapePositionModule.Top + shapePositionModule.HalfSize.y;
            float sizeX = Mathf.Min(topShapePositionModule.HalfSize.x, shapePositionModule.HalfSize.x);
            float rnd = sizeX * 0.5f;
            if (topShape is FloorEntity)
            {
                alignedPosition.x = shapePositionModule.Position.x;
            }
            else
            {
                alignedPosition.x += Random.Range(-rnd, rnd);
            }

            alignedPosition.x = Mathf.Clamp(alignedPosition.x, centerBorder.x + sizeX, rightBorder.x - sizeX);
            shapeEntity.GetBehaviorModuleByType<ShapePositionModule>().ShapePosition.Position = alignedPosition;

            ShapePlaced(shapeEntity, alignedPosition);
        }
    }
}