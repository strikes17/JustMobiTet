using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public enum ValidationResult
    {
        Success = 0,
        Missed,
        OutOfBounds,
        LowPosition
    }

    [Serializable]
    public class ShapePlaceValidationModule : AbstractBehaviourModule
    {
        [SerializeField] private Camera m_Camera;

        /// <summary>
        /// Если поставить 1, то кубик можно будет поставить только если его нижняя грань
        /// поверх верхней грани кубика который снизу
        /// 0 - кубик можно будет поставить если его нижняя грань
        /// поверх середины кубика который снизу
        /// </summary>
        [SerializeField, Range(0f, 1f)] private float m_ShapeValidationPrecision;

        private List<ShapeEntity> m_ValidationShapes;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_ValidationShapes = new();
        }

        public void AddShapeToValidation(ShapeEntity shapeEntity)
        {
            m_ValidationShapes.Add(shapeEntity);
        }

        public void RemoveShapeFromValidation(ShapeEntity shapeEntity)
        {
            m_ValidationShapes.Remove(shapeEntity);
        }

        public ValidationResult IsValidPlaceForShape(ShapePosition currentShapePosition)
        {
            if (m_ValidationShapes.Count == 0)
            {
                return ValidationResult.Success;
            }

            var topShapePosition =
                m_ValidationShapes.Select(x => x.GetBehaviorModuleByType<ShapePositionModule>())
                    .Aggregate((x, y) => x.ShapePosition.Position.y > y.ShapePosition.Position.y ? x : y);
            float interpolatedOffset = Mathf.Lerp(0f, topShapePosition.ShapePosition.HalfSize.y,
                1f - m_ShapeValidationPrecision);

            var topBorder = m_Camera.ScreenToWorldPoint(new Vector3(0f, Screen.height));

            var actualShapePosition = currentShapePosition;
            actualShapePosition.Position.y = topShapePosition.ShapePosition.Top + actualShapePosition.Size.y;

            if (actualShapePosition.Top > topBorder.y)
            {
                return ValidationResult.OutOfBounds;
            }

            if (currentShapePosition.Bottom > topShapePosition.ShapePosition.Top - interpolatedOffset)
            {
                if (actualShapePosition.Right > topShapePosition.ShapePosition.Left &&
                    actualShapePosition.Left < topShapePosition.ShapePosition.Right)
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.LowPosition;
            }

            return ValidationResult.Missed;
        }
    }
}