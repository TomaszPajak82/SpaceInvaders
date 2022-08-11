using SoftwareCore.Enumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.HighScore
{
    public interface IHighScoreRepository: IEnumerable<HighScoreInfo>,IEnumerable
    {
        new VersionedIndexedEnumerator<HighScoreInfo> GetEnumerator();

        void Add(HighScoreInfo highScore);
        void Clear();

    }
}