using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class JsonGameConfigurationData : IGameConfiguration
    {
        [JsonProperty("colors"), SerializeField]
        private List<Color> m_Colors;

        [JsonIgnore] public IEnumerable<Color> Colors => m_Colors;
    }
}