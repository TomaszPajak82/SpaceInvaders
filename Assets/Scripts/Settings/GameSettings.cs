
using System;
using UnityEngine;

namespace SpaceInvaders.Settings
{
    [Serializable]
    public class GameSettings : IGameSettings
    {
        [SerializeField]
        int targetFramerate = 30;
        public int TargetFramerate => targetFramerate;


        [Header("Player")]
        [SerializeField]
        int playerSpeed = 2;
        public int PlayerSpeed => playerSpeed;

        [SerializeField]
        int livesCount = 3;
        public int LivesCount => livesCount;

        [SerializeField]
        float firingCooldown = 0.5f;
        public float FiringCooldown => firingCooldown;

        [SerializeField]
        float playerInvulnerabilityDuration = 2.0f;
        public float PlayerInvulnerabilityDuration => playerInvulnerabilityDuration;

        [SerializeField]
        float playerInvulnerabilityBlinkingFrequency = 5.0f;
        public float PlayerInvulnerabilityBlinkingFrequency => playerInvulnerabilityBlinkingFrequency;


        [Header("Enemy Wave")]

        [SerializeField]
        float incomingWaveTripDistance = 10;
        public float IncomingWaveTripDistance => incomingWaveTripDistance;

        [SerializeField]
        float incomingWaveTripDistancePerRow = 10;
        public float IncomingWaveTripDistancePerRow => incomingWaveTripDistancePerRow;

        [SerializeField]
        float incomingWaveTripDuration = 2;
        public float IncomingWaveTripDuration => incomingWaveTripDuration;



        [Header("Other")]
        [SerializeField]
        float projectileSpeed = 3;
        public float ProjectileSpeed => projectileSpeed;
    }

}