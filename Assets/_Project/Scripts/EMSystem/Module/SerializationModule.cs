using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class SerializableModuleDto
    {
        [JsonProperty("modules")] private List<AbstractBehaviourModule> m_BehaviourModules;

        public SerializableModuleDto(List<AbstractBehaviourModule> modules)
        {
            m_BehaviourModules = modules;
        }
    }

    [Serializable]
    public class SerializationModule : AbstractBehaviourModule
    {
        private SerializableModuleDto m_SerializableModuleDto;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            CreateJsonInAssets();
        }

        [Button]
        private void CreateJsonInAssets()
        {
            var modules = m_AbstractEntity.GetBehaviorModulesCollectionByType<AbstractBehaviourModule>();

            m_SerializableModuleDto = new SerializableModuleDto(modules);

            JsonSerializerSettings jsonSerializerSettings = new()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(m_SerializableModuleDto, jsonSerializerSettings);

            File.WriteAllText($"{Application.dataPath}/sertest.json", json);
        }
    }
}