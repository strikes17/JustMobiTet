using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "SO/New Shape Data Object", fileName = "Shape Data Object")]
    public class GameShapesDataScriptableReference : ScriptableObject
    {
        [SerializeField] private ShapeData shapeData;
        [SerializeReference] private IGameConfiguration m_GameConfiguration;

        public GameShapesDataDto GameShapesDataDto => new GameShapesDataDto(shapeData, m_GameConfiguration);
    }
}