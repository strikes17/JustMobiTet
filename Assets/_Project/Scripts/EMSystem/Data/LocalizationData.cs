using System;
using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class LocalizationData
    {
        [JsonProperty("category")] private string m_Category;
        [JsonProperty("localizations")] private LocalizationDictionary m_LocalizationDictionary;

        public LocalizationDictionary LocalizationDictionary => m_LocalizationDictionary;

        public string Category => m_Category;

        public LocalizationData(string category, LocalizationDictionary localizationDictionary)
        {
            m_Category = category;
            m_LocalizationDictionary = localizationDictionary;
        }

        public LocalizationData()
        {
        }

        [Button]
        private void CreateJsonInAssets()
        {
            var localizationDictionary = new LocalizationDictionary();

            localizationDictionary.Add("hello", "ru", "Привет");
            localizationDictionary.Add("hello", "en", "Hello");

            localizationDictionary.Add("bye", "ru", "Пока");
            localizationDictionary.Add("bye", "en", "Bye");

            var data = new LocalizationData("test", localizationDictionary);

            JsonSerializerSettings jsonSerializerSettings = new()
            {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(data, jsonSerializerSettings);

            File.WriteAllText($"{Application.dataPath}/{data.Category}.json", json);
        }
    }
}