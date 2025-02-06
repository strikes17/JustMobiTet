using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class LocalizationModule : AbstractBehaviourModule
    {
        [SerializeField] private string m_Locale = "Ru";
        [SerializeField] private TextAsset m_TextAsset;
        [SerializeField] private LocalizationData m_LocalizationData;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            var text = m_TextAsset.text;
            JsonSerializerSettings jsonSerializerSettings = new()
            {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.DeserializeObject<LocalizationData>(text, jsonSerializerSettings);
            m_LocalizationData = json;
        }

        public string GetValue(string key)
        {
            return m_LocalizationData.LocalizationDictionary.Get(key, m_Locale);
        }
    }
}