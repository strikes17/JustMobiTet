using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class UiRaycastValidatorModule : AbstractBehaviourModule
    {
        [SerializeField] private List<GraphicRaycaster> m_GraphicRaycasters;

        private PointerEventData m_PointerEventData;
        private List<RaycastResult> m_RaycastResults;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_PointerEventData = new PointerEventData(null);
            m_RaycastResults = new List<RaycastResult>();
        }

        public bool IsPointOverUi(Vector2 screenPosition)
        {
            m_PointerEventData.position = screenPosition;
            foreach (var graphicRaycaster in m_GraphicRaycasters)
            {
                m_RaycastResults.Clear();
                graphicRaycaster.Raycast(m_PointerEventData, m_RaycastResults);
                if (m_RaycastResults.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}