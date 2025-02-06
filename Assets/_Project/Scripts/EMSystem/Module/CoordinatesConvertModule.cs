using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class CoordinatesConvertModule : AbstractBehaviourModule
    {
        [SerializeField] private UnityEngine.Camera m_Camera;

        public Vector2 GetWorldFromScreen(Vector2 screenPosition)
        {
            return m_Camera.ScreenToWorldPoint(screenPosition);
        }
    }
}