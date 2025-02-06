using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractShapeColorModule : AbstractBehaviourModule
    {
        public abstract void SetColor(Color color);
    }
}