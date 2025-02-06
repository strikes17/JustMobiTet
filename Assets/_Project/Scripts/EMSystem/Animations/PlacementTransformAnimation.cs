using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class PlacementTransformAnimation : AbstractTransformAnimation
    {
        [SerializeField] private BounceTransformAnimation m_BounceTransformAnimation;
        [SerializeField] private ExpandTransformAnimation m_ExpandTransformAnimation;

        [HideInInspector] public Vector2 Position;

        public override event Action Finished = delegate { };

        protected override IEnumerator PlayCoroutine()
        {
            Vector2 startPosition = m_Transform.position;
            float distance = Vector2.Distance(startPosition, Position);
            float time = distance / m_AnimationSpeed;
            float progress = 0f;

            m_ExpandTransformAnimation.StartCustomScale = new Vector2(0.25f, 0.25f);
            m_ExpandTransformAnimation.Play();

            while (progress < 1f)
            {
                progress += Time.deltaTime / time;
                m_Transform.position = Vector2.Lerp(startPosition, Position, progress);
                yield return null;
            }
            
            m_BounceTransformAnimation.Play();
            
            Finished();
        }

        public override void Stop()
        {
            base.Stop();
            m_BounceTransformAnimation.Stop();
        }
    }
}