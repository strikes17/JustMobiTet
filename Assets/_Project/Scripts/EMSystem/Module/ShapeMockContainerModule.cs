using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class ShapeMockContainerModule : EntityContainerModule
    {
        [SerializeField] private Transform m_Root;
        private int m_ShapesCount;

        public ShapeMockEntity SpawnShapeMock(ShapeDataDto shapeDataDto, Vector2 position)
        {
            var prefab = shapeDataDto.ShapeMockPrefab;
            var instance = Object.Instantiate(prefab, position, Quaternion.identity, m_Root)
                .GetComponent<ShapeMockEntity>();
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