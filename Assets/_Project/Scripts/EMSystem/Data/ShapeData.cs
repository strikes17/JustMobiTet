using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeData
    {
        [SerializeField] private GameObject m_ShapePrefab;
        [SerializeField] private GameObject m_ShapeMockPrefab;
        [SerializeField] private Sprite m_UiSprite;

        public Sprite UiSprite => m_UiSprite;

        public GameObject ShapePrefab => m_ShapePrefab;

        public GameObject ShapeMockPrefab => m_ShapeMockPrefab;
    }
}