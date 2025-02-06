using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace _Project.Scripts
{
    public static class Utility
    {
        public static bool IsOverridingVirtualMethod(Type derivedType, MethodInfo virtualMethod)
        {
            MethodInfo derivedMethod = derivedType.GetMethod(
                virtualMethod.Name,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                virtualMethod.GetParameters().Select(p => p.ParameterType).ToArray(),
                null
            );

            if (derivedMethod == null) return false; // If the method is not present, then it is not overridden
            if (derivedMethod.DeclaringType == virtualMethod.DeclaringType)
                return false;

            return true;
        }
    }
}