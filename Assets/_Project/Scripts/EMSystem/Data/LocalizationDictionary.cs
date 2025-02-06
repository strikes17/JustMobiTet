using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]

    public class LocalizationDictionary
    {
        [JsonProperty("key_pairs")] private List<LocalizedKeyPair> m_LocalizedKeyPairs = new();

        public void Add(string key, string locale, string value)
        {
            var localizedKeyPair = m_LocalizedKeyPairs.FirstOrDefault(x => x.Key == key);
            if (localizedKeyPair == null)
            {
                localizedKeyPair = new LocalizedKeyPair(key);
                m_LocalizedKeyPairs.Add(localizedKeyPair);
            }

            localizedKeyPair.Add(locale, value);

        }

        public string Get(string key, string locale)
        {
            var localizedKeyPair = m_LocalizedKeyPairs.FirstOrDefault(x => x.Key == key);
            return localizedKeyPair.Get(locale);
        }
    }
}