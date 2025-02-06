using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class DestroyModule : AbstractBehaviourModule
    {
        public Action<AbstractEntity> DestroyRequested = delegate { };
        public Action<AbstractEntity> Destroyed = delegate { };
        private bool m_IsReadyToDestroy;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_IsReadyToDestroy = false;
        }

        public DestroyModule RequestToDestroy()
        {
            Moroutine.Run(DestroyRequestCheck());
            DestroyRequested(m_AbstractEntity as ShapeEntity);
            return this;
        }

        private IEnumerator DestroyRequestCheck()
        {
            while (!m_IsReadyToDestroy)
            {
                yield return null;
            }

            Destroyed(m_AbstractEntity);
            UnityEngine.Object.Destroy(m_AbstractEntity.gameObject);
        }

        public DestroyModule MarkAsDestroyable()
        {
            m_IsReadyToDestroy = true;
            return this;
        }
    }
}