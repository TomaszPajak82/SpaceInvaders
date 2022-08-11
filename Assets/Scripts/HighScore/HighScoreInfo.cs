using System;

namespace SpaceInvaders.HighScore
{
    public readonly struct HighScoreInfo
    {
        public readonly int value;

        public readonly int wavesDefeated;

        public readonly DateTime dataTime;

        public HighScoreInfo(int value, int wavesDefeated, DateTime dataTime) {
            this.value = value;
            this.wavesDefeated = wavesDefeated;
            this.dataTime = dataTime;
        }

    }
}
