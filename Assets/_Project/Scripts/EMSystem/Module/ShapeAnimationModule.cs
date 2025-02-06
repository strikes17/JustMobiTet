using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeAnimationModule : AbstractBehaviourModule
    {
        [SerializeField] private Transform m_Transform;

        [SerializeField] private float m_PlacementAnimationSpeed;

        [SerializeField] private float m_BounceAnimationSpeed;
        [SerializeField] private AnimationCurve m_BounceAnimationCurve;
        [SerializeField] private int m_BouncesCount = 3;

        [SerializeField] private float m_ExplosiveDestroyScale = 1.5f;
        [SerializeField] private float m_ExplodeAnimationSpeed;

        [SerializeField] private float m_HoleDisappearAnimationSpeed = 2f;

        public event Action DestroyAnimationFinished = delegate { };
        public event Action PlacementAnimationFinished = delegate { };

        private Transform m_HoleTransform;

        private Moroutine m_PlacementAnimation;
        private Moroutine m_BounceAnimation;

        public void StopAllAnimations()
        {
            if (m_BounceAnimation != null)
            {
                m_BounceAnimation.Stop();
            }

            if (m_PlacementAnimation != null)
            {
                m_PlacementAnimation.Stop();
            }
        }

        public void PlayPlacementAnimation(Vector2 position, bool useBounceEffect = true)
        {
            StopAllAnimations();

            m_PlacementAnimation = Moroutine.Run(PlayPlacementAnimationCoroutine(position, useBounceEffect));
        }

        public void PlayHoleDisappearAnimation(Transform holeTransform)
        {
            m_HoleTransform = holeTransform;
            Moroutine.Run(PlayHoleDisappearAnimationCoroutine());
        }

        public void PlayExplosiveDestroyAnimation()
        {
            Moroutine.Run(PlayExplosiveDestroyAnimationCoroutine());
        }

        private IEnumerator PlayExplosiveDestroyAnimationCoroutine()
        {
            Vector2 startScale = m_Transform.localScale;
            float progress = 0f;
            float diff = Mathf.Abs(m_ExplosiveDestroyScale - startScale.magnitude);
            float time = diff / m_ExplodeAnimationSpeed;
            while (progress < 1f)
            {
                progress += Time.deltaTime / time;
                Vector2 scale = new Vector2(m_ExplosiveDestroyScale, m_ExplosiveDestroyScale);
                m_Transform.localScale = Vector2.Lerp(startScale, scale, progress);
                yield return null;
            }

            DestroyAnimationFinished();
        }

        private IEnumerator PlayShrinkAnimationCoroutine(float speed)
        {
            Vector2 startScale = m_Transform.localScale;
            float progress = 0f;
            float time = startScale.magnitude / speed;
            while (progress < 1f)
            {
                progress += Time.deltaTime / time;
                Vector2 scale = new Vector2(0f, 0f);
                m_Transform.localScale = Vector2.Lerp(startScale, scale, progress);
                yield return null;
            }

            DestroyAnimationFinished();
        }

        private IEnumerator PlayPlacementAnimationCoroutine(Vector2 position, bool useBounceEffect = true)
        {
            Vector2 startPosition = m_Transform.position;
            float distance = Vector2.Distance(startPosition, position);
            float time = distance / m_PlacementAnimationSpeed;
            float progress = 0f;
            while (progress < 1f)
            {
                progress += Time.deltaTime / time;
                m_Transform.position = Vector2.Lerp(startPosition, position, progress);
                yield return null;
            }

            PlacementAnimationFinished();

            if (useBounceEffect)
            {
                m_BounceAnimation = Moroutine.Run(Bounce());
            }
        }

        private IEnumerator Bounce()
        {
            Vector3 startPosition = m_Transform.position;
            float time = 0f;
            float bounceDampingCoeff = 1f;

            for (int i = 0; i < m_BouncesCount; i++)
            {
                bounceDampingCoeff = 1f - m_BounceAnimationCurve.Evaluate((float)i / m_BouncesCount);
                while (time < 1f)
                {
                    time += Time.deltaTime * m_BounceAnimationSpeed / 1f;

                    float heightOffset = Mathf.Sin(time * Mathf.PI) * 0.5f * bounceDampingCoeff;
                    m_Transform.position = startPosition + Vector3.up * heightOffset;
                    yield return null;
                }

                time = 0f;
            }

            m_Transform.position = startPosition;
        }

        private IEnumerator PlayHoleDisappearAnimationCoroutine()
        {
            PlayPlacementAnimation(m_HoleTransform.position, false);
            yield return Moroutine.Run(PlayShrinkAnimationCoroutine(m_HoleDisappearAnimationSpeed)).WaitForComplete();
            StopAllAnimations();
            DestroyAnimationFinished();
        }
    }
}