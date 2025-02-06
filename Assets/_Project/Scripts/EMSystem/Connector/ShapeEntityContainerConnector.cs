using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Moroutines;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeEntityContainerConnector : BehaviourModuleConnector
    {
        [Inject] private ShapeContainerModule m_ShapeContainerModule;

        protected override void Initialize()
        {
            Moroutine.Run(Shape());
        }

        private IEnumerator Shape()
        {
            yield return null;
            m_ShapeContainerModule.AddElement(m_AbstractEntity as ShapeEntity);
        }
    }
}