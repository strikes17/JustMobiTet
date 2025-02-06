using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiPopupAnimatorModule : AbstractBehaviourModule
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private float m_PopupLifeTime;

        private float m_UpdateTime;
        private bool m_IsShowing;
        private static readonly int ShowName = Animator.StringToHash("Show");

        public void Show()
        {
            m_UpdateTime = 0f;
            m_IsShowing = true;
            m_Animator.SetBool(ShowName, true);
        }

        public void Hide()
        {
            m_IsShowing = false;
            m_Animator.SetBool(ShowName, false);
        }

        public override void OnUpdate()
        {
            if (!m_IsShowing)
            {
                return;
            }

            if (m_UpdateTime < m_PopupLifeTime)
            {
                m_UpdateTime += Time.deltaTime;
                return;
            }

            Hide();
        }
    }
}