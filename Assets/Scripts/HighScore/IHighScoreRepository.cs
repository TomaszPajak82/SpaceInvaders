using SoftwareCore.Enumerators;
using SoftwareCore.Specification;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.HighScore
{
    public interface IHighScoreRepository: IEnumerable<HighScoreInfo>,IEnumerable
    {
        new VersionedIndexedEnumerator<HighScoreInfo> GetEnumerator();

        IEnumerable<HighScoreInfo> Get(ISpecification<HighScoreInfo> specification);

        void Add(HighScoreInfo highScore);
        void Clear();

    }
}