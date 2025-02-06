using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public interface IGameConfiguration
    {
        IEnumerable<Color> Colors { get; }
    }
}