using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiTextModule : GuiAbstractBehaviourModule
    {
        public event Action<string> TextChanged = delegate { };

        [SerializeField] private TMP_Text m_TMPText;

        public string Text
        {
            set
            {
                if (m_TMPText.text != value)
                {
                    m_TMPText.text = value;
                    TextChanged(value);
                }
            }
        }
    }
}