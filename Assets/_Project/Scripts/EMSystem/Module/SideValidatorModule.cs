using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class SideValidatorModule : AbstractBehaviourModule
    {
        public Side DefineSide(Vector2 point)
        {
            if (point.x > 0)
            {
                return Side.Right;
            }

            return Side.Left;
        }
    }
}