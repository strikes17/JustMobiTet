using System;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiShapesPaletteDataConnector : BehaviourModuleConnector
    {
        [SelfInject] private GuiShapesPaletteModule m_ShapesPaletteModule;
        [Inject] private AbstractShapeDataLoaderModule m_ShapeDataLoaderModule;
        [Inject] private GuiContainerModule m_GuiContainerModule;

        protected override void Initialize()
        {
            m_ShapesPaletteModule.PaletteShapeAdded += ShapesPaletteModuleOnPaletteShapeAdded;
            m_ShapesPaletteModule.CreateShapesPalette(m_ShapeDataLoaderModule.GameShapesDataDto);
        }

        private void ShapesPaletteModuleOnPaletteShapeAdded(GuiPaletteShapeEntity paletteShapeEntity,
            ShapeDataDto shapeDataDto)
        {
            m_GuiContainerModule.AddElement(paletteShapeEntity);
        }
    }
}