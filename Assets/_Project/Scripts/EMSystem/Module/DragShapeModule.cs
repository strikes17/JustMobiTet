using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [Serializable]
    public class DragShapeModule : GuiAbstractBehaviourModule
    {
        public event Action<GuiDefaultEntity, Vector2> DragFinished = delegate { };
        public event Action<GuiDefaultEntity, Vector2> DragStarted = delegate { };
        public event Action<GuiDefaultEntity, Vector2> DragUpdate = delegate { };

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            DragStarted(m_GuiDefaultEntity, eventData.position);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            DragUpdate(m_GuiDefaultEntity, eventData.position);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            DragFinished(m_GuiDefaultEntity, eventData.position);
        }
    }
}