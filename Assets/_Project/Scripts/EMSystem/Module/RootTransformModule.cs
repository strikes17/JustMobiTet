using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class RootTransformModule : AbstractBehaviourModule
    {
        [SerializeField] private Transform m_Transform;

        public Transform Transform => m_Transform;
    }
}