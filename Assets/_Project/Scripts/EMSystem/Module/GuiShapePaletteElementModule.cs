using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiShapePaletteElementModule : GuiAbstractBehaviourModule
    {
        [SerializeField] private Image m_Image;

        private ShapeDataDto m_ShapeDataDto;

        public ShapeDataDto ShapeDataDto => m_ShapeDataDto;

        public void Set(ShapeDataDto shapeDataDto)
        {
            m_ShapeDataDto = shapeDataDto;
            m_Image.sprite = shapeDataDto.UiSprite;
            m_Image.color = shapeDataDto.Color;
        }
    }
}