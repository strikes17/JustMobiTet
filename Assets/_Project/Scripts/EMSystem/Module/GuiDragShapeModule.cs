using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiDragShapeModule : GuiAbstractBehaviourModule
    {
        public event Action<GuiDefaultEntity, Vector2> DragFinished = delegate { };
        public event Action<GuiDefaultEntity, Vector2> DragStarted = delegate { };

        public event Action<GuiDefaultEntity, Vector2> DragUpdate = delegate { };
        
        private Transform m_OriginParent;
        private Transform m_DragParent;

        private int m_SiblingIndex;
        
        public void SetParents(Transform originParent, Transform dragParent)
        {
            m_OriginParent = originParent;
            m_DragParent = dragParent;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            m_SiblingIndex = m_GuiDefaultEntity.transform.GetSiblingIndex();
            m_GuiDefaultEntity.transform.SetParent(m_DragParent);
            DragStarted(m_GuiDefaultEntity, eventData.position);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            m_GuiDefaultEntity.RectTransform.position = eventData.position;
            DragUpdate(m_GuiDefaultEntity, eventData.position);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            m_GuiDefaultEntity.transform.SetParent(m_OriginParent);
            m_GuiDefaultEntity.transform.SetSiblingIndex(m_SiblingIndex);
            DragFinished(m_GuiDefaultEntity, eventData.position);
        }
    }
}