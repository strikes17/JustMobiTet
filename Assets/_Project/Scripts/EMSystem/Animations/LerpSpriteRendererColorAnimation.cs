using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class LerpSpriteRendererColorAnimation : AbstractTransformAnimation
    {
        public override event Action Finished = delegate { };

        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private Color m_TargetColor;
        [SerializeField] private float m_Time;

        protected override IEnumerator PlayCoroutine()
        {
            float progress = 0f;
            Color color = m_SpriteRenderer.color;
            while (progress < 1f)
            {
                progress += Time.deltaTime / m_Time;
                m_SpriteRenderer.color = Color.Lerp(color, m_TargetColor, progress);
                yield return null;
            }

            Finished();
        }
    }
}