using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ExpandTransformAnimation : AbstractTransformAnimation
    {
        [SerializeField] private float m_ExpandScale;

        [HideInInspector] public Vector2 StartCustomScale;

        public override event Action Finished = delegate { };

        protected override IEnumerator PlayCoroutine()
        {
            Vector2 startScale = StartCustomScale == Vector2.zero ? m_Transform.localScale : StartCustomScale;
            float progress = 0f;
            float time = startScale.x / m_AnimationSpeed;
            while (progress < 1f)
            {
                progress += Time.deltaTime / time;
                m_Transform.localScale = Vector2.Lerp(startScale, new Vector2(m_ExpandScale, m_ExpandScale), progress);
                yield return null;
            }

            Finished();
        }
    }
}