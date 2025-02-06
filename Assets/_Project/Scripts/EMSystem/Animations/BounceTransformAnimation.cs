using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class BounceTransformAnimation : AbstractTransformAnimation
    {
        [SerializeField] private int m_BouncesCount;
        [SerializeField] private AnimationCurve m_AnimationCurve;

        [HideInInspector] public Vector2 Position;

        public override event Action Finished = delegate { };

        protected override IEnumerator PlayCoroutine()
        {
            Vector3 startPosition = m_Transform.position;
            float time = 0f;
            float bounceDampingCoeff = 1f;

            for (int i = 0; i < m_BouncesCount; i++)
            {
                bounceDampingCoeff = 1f - m_AnimationCurve.Evaluate((float)i / m_BouncesCount);
                while (time < 1f)
                {
                    time += Time.deltaTime * m_AnimationSpeed / 1f;

                    float heightOffset = Mathf.Sin(time * Mathf.PI) * 0.5f * bounceDampingCoeff;
                    m_Transform.position = startPosition + Vector3.up * heightOffset;
                    yield return null;
                }

                time = 0f;
            }

            m_Transform.position = startPosition;

            Finished();
        }
    }
}