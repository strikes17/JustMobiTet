using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapePhysicsModule : AbstractBehaviourModule
    {
        public event Action<ShapeEntity, ShapePosition> ShapePhysicsSolved = delegate { };

        private List<ShapeEntity> m_Shapes = new();

        public void AddShape(ShapeEntity shapeEntity)
        {
            m_Shapes.Add(shapeEntity);
        }

        public void Remove(ShapeEntity shapeEntity)
        {
            m_Shapes.Remove(shapeEntity);
        }

        public void Solve()
        {
            var orderedShapes = m_Shapes
                .OrderBy(x => x.GetBehaviorModuleByType<ShapePositionModule>().ShapePosition.Position.y)
                .ToList();
            if (orderedShapes.Count == 1) return;
            foreach (var orderedShape in orderedShapes)
            {
                Debug.Log(orderedShape.name);
            }

            for (int i = 0; i < orderedShapes.Count - 1; i++)
            {
                var shape1 = orderedShapes[i];
                var shape2 = orderedShapes[i + 1];
                var pos1 = shape1.GetBehaviorModuleByType<ShapePositionModule>();
                var pos2 = shape2.GetBehaviorModuleByType<ShapePositionModule>();
                var diff = Mathf.Abs(Mathf.Abs(pos1.ShapePosition.Top) - Math.Abs(pos2.ShapePosition.Bottom));
                if (diff > 0.01)
                {
                    var newShapePosition = new Vector2(pos2.ShapePosition.Position.x, pos1.ShapePosition.Top + pos2.ShapePosition.HalfSize.y);

                    pos2.ShapePosition.Position = newShapePosition;

                    ShapePhysicsSolved(shape2, pos2.ShapePosition);
                }
            }
        }
    }
}