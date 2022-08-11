
using System;

namespace SpaceInvaders.Settings
{

    [Serializable]
    public class HighScoreSettings : IHighScoreSettings
    {

        public string displayFormat = "{0}: {1}";
        public string DisplayFormat => displayFormat;


        public string displayFullFormat = "{0}: {1} - {2}";
        public string DisplayFullFormat => displayFullFormat;


        public int displayCount = 10;
        public int DisplayCount => displayCount;


        public int storedCount = 20;
        public int StoredCount => storedCount;


    }
}