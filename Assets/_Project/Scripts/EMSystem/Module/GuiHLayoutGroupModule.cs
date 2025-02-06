using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiHLayoutGroupModule : AbstractBehaviourModule
    {
        [SerializeField] private HorizontalLayoutGroup m_HorizontalLayoutGroup;

        public HorizontalLayoutGroup HorizontalLayoutGroup => m_HorizontalLayoutGroup;
    }
}