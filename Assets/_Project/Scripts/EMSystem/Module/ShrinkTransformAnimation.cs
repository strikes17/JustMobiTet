using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShrinkTransformAnimation : AbstractTransformAnimation
    {
        [SerializeField] private float m_ShrinkScale;

        public override event Action Finished = delegate { };

        protected override IEnumerator PlayCoroutine()
        {
            Vector2 startScale = m_Transform.localScale;
            float progress = 0f;
            float time = startScale.x / m_AnimationSpeed;
            while (progress < 1f)
            {
                progress += Time.deltaTime / time;
                m_Transform.localScale = Vector2.Lerp(startScale, new Vector2(m_ShrinkScale, m_ShrinkScale), progress);
                yield return null;
            }

            Finished();
        }
    }
}