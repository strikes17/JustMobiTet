﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public abstract class AbstractEntity : MonoBehaviour
    {
        public event Action<GameObject> Destroyed = delegate { };

        public IEnumerable<AbstractBehaviourModule> BehaviourModules => m_BehaviourModules;

        [SerializeReference] protected List<AbstractBehaviourModule> m_BehaviourModules = new();
        [SerializeReference] protected List<BehaviourModuleConnector> m_Connectors = new();

        public IEnumerable<AbstractEntity> ChildEntities => m_ChildEntities;

        [SerializeField, ReadOnly] private List<AbstractEntity> m_ChildEntities;

        public IEnumerable<BehaviourModuleConnector> Connectors => m_Connectors;

        protected virtual void OnValidate()
        {
        }

        public List<T> GetBehaviorModulesCollectionByType<T>() where T : AbstractBehaviourModule
        {
            return m_BehaviourModules.Where(x =>
                x.GetType().IsSubclassOf(typeof(T)) ||
                x.GetType() == typeof(T)).Select(x => x as T).ToList();
        }

        public T GetBehaviorModuleByType<T>() where T : AbstractBehaviourModule
        {
            return m_BehaviourModules.FirstOrDefault(x =>
                x.GetType().IsSubclassOf(typeof(T)) ||
                x.GetType() == typeof(T)) as T;
        }

        public AbstractBehaviourModule GetBehaviorModuleByType(Type behaviourModuleType)
        {
            return m_BehaviourModules.FirstOrDefault(x =>
                x.GetType().IsSubclassOf(behaviourModuleType) ||
                x.GetType() == behaviourModuleType);
        }

        protected virtual void Initialize(AbstractEntity abstractEntity)
        {
            m_ChildEntities = transform.GetComponentsInChildren<AbstractEntity>(true).ToList();
            m_ChildEntities.Remove(this);
            Moroutine.Run(InitializeCoroutine());
        }

        private IEnumerator InitializeCoroutine()
        {
            m_BehaviourModules.ForEach(x => x.Initialize(this));
            yield return null;
            m_Connectors.ForEach(connector =>
            {
                if (connector.IsResolved)
                {
                    connector.Init(this);
                }
                else
                {
                    connector.Resolved += ConnectorOnResolved;
                    Debug.LogError($"Connector: {connector.GetType()} of {name} is not resolved!");
                }
            });
        }

        private void ConnectorOnResolved(IResolveTarget resolveTarget)
        {
            var connector = resolveTarget as BehaviourModuleConnector;
            connector.Init(this);
            connector.Resolved -= ConnectorOnResolved;
        }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            Initialize(this);
        }

        private void OnDestroy()
        {
            Destroyed(gameObject);
        }
    }
}