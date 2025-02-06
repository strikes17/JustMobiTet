using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeDropInHoleAnimation : AbstractTransformAnimation
    {
        [SerializeField] private float m_ShrinkScale;

        [SerializeField] private LerpSpriteRendererColorAnimation m_RendererColorAnimation;
        [SerializeField] private LerpMoveTransformAnimation m_LerpMoveTransformAnimation;

        [HideInInspector] public Vector2 HolePosition;

        public override event Action Finished = delegate { };

        protected override IEnumerator PlayCoroutine()
        {
            Vector2 startScale = m_Transform.localScale;
            float progress = 0f;
            float time = startScale.x / m_AnimationSpeed;

            m_LerpMoveTransformAnimation.Position = HolePosition;
            
            m_RendererColorAnimation.Play();
            m_LerpMoveTransformAnimation.Play();
            
            while (progress < 1f)
            {
                progress += Time.deltaTime / time;
                m_Transform.localScale = Vector2.Lerp(startScale, new Vector2(m_ShrinkScale, m_ShrinkScale), progress);
                yield return null;
            }
            
            m_RendererColorAnimation.Stop();
            m_LerpMoveTransformAnimation.Stop();
            Finished();
        }

        public override void Stop()
        {
            base.Stop();
            m_LerpMoveTransformAnimation.Stop();
            m_RendererColorAnimation.Stop();
        }
    }
}