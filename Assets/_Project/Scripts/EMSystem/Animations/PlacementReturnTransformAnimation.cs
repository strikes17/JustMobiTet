using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class PlacementReturnTransformAnimation : AbstractTransformAnimation
    {
        [HideInInspector] public Vector2 Position;

        public override event Action Finished = delegate { };

        protected override IEnumerator PlayCoroutine()
        {
            Vector2 startPosition = m_Transform.position;
            float distance = Vector2.Distance(startPosition, Position);
            float time = distance / m_AnimationSpeed;
            float progress = 0f;

            while (progress < 1f)
            {
                progress += Time.deltaTime / time;
                m_Transform.position = Vector2.Lerp(startPosition, Position, progress);
                yield return null;
            }
            
            Finished();
        }
    }
}