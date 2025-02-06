using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiShapesPaletteModule : GuiAbstractBehaviourModule
    {
        public event Action<GuiPaletteShapeEntity, ShapeDataDto> PaletteShapeAdded = delegate { };

        [SerializeField] private Transform m_FillRoot;
        [SerializeField] private GuiPaletteShapeEntity m_GuiPaletteShapeEntityPrefab;

        public void CreateShapesPalette(GameShapesDataDto gameShapesDataDto)
        {
            foreach (var shapeDataDto in gameShapesDataDto.ShapeDataDtos)
            {
                var instance = Object.Instantiate(m_GuiPaletteShapeEntityPrefab, m_FillRoot);
                var shapePaletteModule = instance.GetBehaviorModuleByType<GuiShapePaletteElementModule>();
                if (shapePaletteModule != null)
                {
                    shapePaletteModule.Set(shapeDataDto);
                    PaletteShapeAdded(instance, shapeDataDto);
                }
                else
                {
                    instance.gameObject.SetActive(false);
                    Object.Destroy(instance);
                    Debug.LogError($"Failed to create palette element!");
                }
            }
        }
    }
}