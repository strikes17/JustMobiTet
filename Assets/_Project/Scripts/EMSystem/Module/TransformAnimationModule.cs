using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TransformAnimationModule : AbstractBehaviourModule
    {
        [SerializeReference] private List<AbstractTransformAnimation> m_TransformAnimations;

        public void StopAllAnimations()
        {
            foreach (var transformAnimation in m_TransformAnimations)
            {
                transformAnimation.Stop();
            }
        }

        public T GetAnimation<T>() where T : AbstractTransformAnimation
        {
            var animation = m_TransformAnimations.FirstOrDefault(x => x.GetType() == typeof(T));
            if (animation != null)
            {
                return animation as T;
            }

            return default;
        }
    }
}