using UnityEngine;

namespace DefaultNamespace
{
    public interface ITrapSetter
    {
        bool TryToInstantiateTrap(Vector2 mousePos);
    }
}