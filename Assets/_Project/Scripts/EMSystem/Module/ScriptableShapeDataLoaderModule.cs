using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ScriptableShapeDataLoaderModule : AbstractShapeDataLoaderModule
    {
        [SerializeField] private GameShapesDataScriptableReference m_ShapesDataScriptableReference;

        public override GameShapesDataDto GameShapesDataDto => m_ShapesDataScriptableReference.GameShapesDataDto;
    }
}