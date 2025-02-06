using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ExplodeTransformAnimation : AbstractTransformAnimation
    {
        [SerializeField] private float m_TickTime;
        [SerializeField] private int m_TicksCount;

        public override event Action Finished = delegate { };

        protected override IEnumerator PlayCoroutine()
        {
            for (int i = 0; i < m_TicksCount; i++)
            {
                yield return Moroutine.Run(ScaleCoroutine(1.25f)).WaitForComplete();
                yield return Moroutine.Run(ScaleCoroutine(0.75f)).WaitForComplete();
            }
            
            yield return Moroutine.Run(ScaleCoroutine(1.5f)).WaitForComplete();
            
            Finished();
        }

        protected IEnumerator ScaleCoroutine(float scale)
        {
            Vector2 startScale = m_Transform.localScale;
            float progress = 0f;
            while (progress < 1f)
            {
                progress += Time.deltaTime / m_TickTime;
                m_Transform.localScale = Vector2.Lerp(startScale, new Vector2(scale, scale), progress);
                yield return null;
            }
        }
    }
}