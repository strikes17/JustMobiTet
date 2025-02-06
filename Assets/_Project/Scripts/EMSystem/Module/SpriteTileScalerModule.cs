using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class SpriteTileScalerModule : AbstractBehaviourModule
    {
        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private float m_ScaleMultiply;

        public override void OnUpdate()
        {
            m_SpriteRenderer.material.SetVector("_Tiling", m_AbstractEntity.transform.localScale * m_ScaleMultiply);
        }
    }
}