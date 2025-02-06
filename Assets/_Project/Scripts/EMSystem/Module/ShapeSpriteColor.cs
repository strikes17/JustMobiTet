using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeSpriteColor : AbstractShapeColorModule
    {
        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        
        public override void SetColor(Color color)
        {
            m_SpriteRenderer.color = color;
        }
    }
}