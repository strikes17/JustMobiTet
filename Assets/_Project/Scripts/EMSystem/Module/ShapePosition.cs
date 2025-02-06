using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public struct ShapePosition
    {
        public Vector2 Size;
        
        public Vector2 Position;

        public Vector2 HalfSize => Size / 2f;

        public float Right => Position.x + HalfSize.x;
        public float Top => Position.y + HalfSize.y;

        public float Left => Position.x - HalfSize.x;
        public float Bottom => Position.y - HalfSize.y;
    }
}