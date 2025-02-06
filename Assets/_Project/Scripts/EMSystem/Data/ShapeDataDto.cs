using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeDataDto
    {
        private GameObject m_ShapePrefab;
        private GameObject m_ShapeMockPrefab;
        private Sprite m_UiSprite;
        private Color m_Color;

        public Color Color => m_Color;

        public Sprite UiSprite => m_UiSprite;

        public GameObject ShapePrefab => m_ShapePrefab;

        public GameObject ShapeMockPrefab => m_ShapeMockPrefab;

        public ShapeDataDto(ShapeData shapeData, Color color)
        {
            m_ShapePrefab = shapeData.ShapePrefab;
            m_ShapeMockPrefab = shapeData.ShapeMockPrefab;
            m_UiSprite = shapeData.UiSprite;
            m_Color = color;
        }
    }
}