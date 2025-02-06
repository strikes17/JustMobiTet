using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class LocalizedKeyPair
    {
        [JsonProperty("key")] private string m_Key;
        [JsonProperty("localized_pairs")] private List<LocalizedPair> m_LocalizedPairs = new();

        public string Key => m_Key;

        public LocalizedKeyPair(string key)
        {
            m_Key = key;
        }

        public LocalizedKeyPair()
        {
        }

        public void Add(string locale, string value)
        {
            var localizedPair = new LocalizedPair(locale, value);
            m_LocalizedPairs.Add(localizedPair);
        }

        public string Get(string locale)
        {
            var localizedPair = m_LocalizedPairs.FirstOrDefault(x => x.Locale == locale);
            if (localizedPair != null)
            {
                return localizedPair.Value;
            }

            return string.Empty;
        }
    }
}