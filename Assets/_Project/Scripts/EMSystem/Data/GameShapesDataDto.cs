using System;
using System.Collections.Generic;

namespace _Project.Scripts
{
    [Serializable]
    public class GameShapesDataDto
    {
        private IGameConfiguration m_GameConfiguration;
        private List<ShapeDataDto> m_ShapeDataDtos;

        public IGameConfiguration GameConfiguration => m_GameConfiguration;

        public List<ShapeDataDto> ShapeDataDtos => m_ShapeDataDtos;

        public GameShapesDataDto(ShapeData shapeData, IGameConfiguration gameConfiguration)
        {
            m_ShapeDataDtos = new();
            foreach (var color in gameConfiguration.Colors)
            {
                ShapeDataDto shapeDataDto = new ShapeDataDto(shapeData, color);
                m_ShapeDataDtos.Add(shapeDataDto);
            }

            m_GameConfiguration = gameConfiguration;
        }
    }
}