using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.PlayerInput
{
    public interface IPlayerInput
    {
        float HorizontalInputValue { get; }

        bool Fire { get; }

    }
}
