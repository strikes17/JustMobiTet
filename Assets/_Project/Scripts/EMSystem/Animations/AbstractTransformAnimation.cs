using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractTransformAnimation
    {
        public abstract event Action Finished;

        [SerializeField] protected float m_AnimationSpeed;
        [SerializeField] protected Transform m_Transform;

        protected Moroutine m_Moroutine;

        public void Play()
        {
            Stop();
            m_Moroutine = Moroutine.Run(PlayCoroutine());
        }

        public virtual void Stop()
        {
            if (m_Moroutine != null)
            {
                m_Moroutine.Stop();
            }
        }

        protected abstract IEnumerator PlayCoroutine();
    }
}