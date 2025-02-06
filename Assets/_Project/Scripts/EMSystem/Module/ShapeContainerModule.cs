using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeContainerModule : EntityContainerModule
    {
        [SerializeField] private Transform m_Root;
        private int m_ShapesCount;

        public ShapeEntity SpawnShape(ShapeDataDto shapeDataDto, Vector2 position)
        {
            var prefab = shapeDataDto.ShapePrefab;
            var instance = Object.Instantiate(prefab, position, Quaternion.identity, m_Root)
                .GetComponent<ShapeEntity>();
            instance.name += ++m_ShapesCount;

            var shapeColorModule = instance.GetBehaviorModuleByType<AbstractShapeColorModule>();
            shapeColorModule.SetColor(shapeDataDto.Color);

            AddElement(instance);
            return instance;
        }

        public void DestroyShape(ShapeEntity shapeEntity)
        {
            RemoveElement(shapeEntity);
            Object.Destroy(shapeEntity.gameObject);
        }
    }
}