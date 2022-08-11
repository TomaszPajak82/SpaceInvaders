using SpaceInvaders.Settings;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.Initialization
{
    // just sets some settings when the program starts running
    public class InitializationObject
    {
        [Inject]
        public void Construct(IGameSettings gameSettings) {
            Application.targetFrameRate = gameSettings.TargetFramerate;
        }

    }
}
