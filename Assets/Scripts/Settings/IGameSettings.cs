using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.Settings
{
    public interface IGameSettings
    {

        int TargetFramerate { get; }

        float IncomingWaveTripDistance { get; }

        float IncomingWaveTripDistancePerRow { get; }

        float IncomingWaveTripDuration { get; }



        int PlayerSpeed { get; }

        int LivesCount { get; }

        float PlayerInvulnerabilityDuration { get; }

        float PlayerInvulnerabilityBlinkingFrequency { get; }

        float ProjectileSpeed { get; }

        float FiringCooldown { get; }
    }

}