using UnityEngine;

namespace Player
{
    internal interface IInputSource
    {
        Vector2 GetDirection();
    }
}