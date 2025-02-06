using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class AdaptiveTransformModule : AbstractBehaviourModule
    {
        public event Action<Transform> Updated = delegate { };

        [SerializeField] private bool m_IsRightSide;
        [SerializeField] private Transform m_Transform;
        [SerializeField] private Camera m_Camera;
        [SerializeField] private bool m_StretchX;
        [SerializeField] private bool m_StretchY;
        [SerializeField] private Vector2 m_Scale;
        [SerializeField] private Vector2 m_Offset;

        public override void OnUpdate()
        {
            float cameraHeight = m_Camera.orthographicSize * 2;
            float cameraWidth = cameraHeight * m_Camera.aspect;

            var xScale = m_StretchX ? cameraWidth * 0.5f : m_Transform.localScale.x;
            var yScale = m_StretchY ? cameraHeight : m_Transform.localScale.y;

            xScale *= m_Scale.x;
            yScale *= m_Scale.y;

            m_Transform.localScale = new Vector3(xScale, yScale);

            int dir = m_IsRightSide ? 1 : -1;
            m_Transform.position = new Vector3(m_Transform.localScale.x / 2f * dir / m_Scale.x + m_Offset.x,
                m_Offset.y / m_Scale.y);

            Updated(m_Transform);
        }
    }
}