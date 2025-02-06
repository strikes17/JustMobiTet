using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace _Project.Scripts
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class LocalizedPair
    {
        [JsonProperty("locale")] private string m_Locale;
        [JsonProperty("value")] private string m_Value;

        public string Locale => m_Locale;

        public string Value => m_Value;

        public LocalizedPair(string locale, string value)
        {
            m_Locale = locale;
            m_Value = value;
        }

        public LocalizedPair()
        {
        }
    }
}