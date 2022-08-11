using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Linq;
using SpaceInvaders.HighScore;
using SoftwareCore.Storage;
using SoftwareCore.Extensions;
using SpaceInvaders.Settings;

[TestFixture]
public class HighScoreRepositoryTest
{
    class FakePersistentStorage : IPersistentStorage
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        public void Clear(string key) {
            dict.Clear();
        }

        public void Store(string key, string data) {
            dict.TryAdd(key, data);
        }

        public bool TryGet(string key, out string data) {
            return dict.TryGetValue(key, out data);
        }
    }


    // A Test behaves as an ordinary method
    [TestCase( 10, 1, 1000)]
    public void Add_HighScore(int value,int waveCount,long data)
    {
        HighScoreInfo highScore = new HighScoreInfo(value, waveCount, System.DateTime.FromFileTimeUtc(data));


        var highScoreSettingsStub = Substitute.For<IHighScoreSettings>();
        highScoreSettingsStub.StoredCount.Returns(2);

        var persistentStorageFake = new FakePersistentStorage();

        IHighScoreRepository highScoreRepository = new HighScoreRepository(highScoreSettingsStub, persistentStorageFake);
        highScoreRepository.Add(highScore);

        List<HighScoreInfo> tmpList = new List<HighScoreInfo>();
        foreach(var item in highScoreRepository) {
            tmpList.Add(item);
        }

        Assert.AreEqual(1, tmpList.Count);
        Assert.AreEqual(highScore, tmpList[0]);
        
        //Assert.
        // Use the Assert class to test conditions
    }

    [TestCase(20, 2, 2000)]
    public void Load_HighScore(int value, int waveCount, long data) {

        HighScoreInfo highScore = new HighScoreInfo(value, waveCount, System.DateTime.FromFileTimeUtc(data));

        var highScoreSettingsStub = Substitute.For<IHighScoreSettings>();
        highScoreSettingsStub.StoredCount.Returns(2);

        var persistentStorageFake = new FakePersistentStorage();

        IHighScoreRepository highScoreRepository = new HighScoreRepository(highScoreSettingsStub, persistentStorageFake);
        highScoreRepository.Add(highScore);

        IHighScoreRepository highScoreRepositoryLoaded = new HighScoreRepository(highScoreSettingsStub, persistentStorageFake);

        List<HighScoreInfo> tmpList = new List<HighScoreInfo>();
        foreach (var item in highScoreRepository) {
            tmpList.Add(item);
        }

        Assert.AreEqual(1, tmpList.Count);
        Assert.AreEqual(highScore, tmpList[0]);

        //Assert.
        // Use the Assert class to test conditions
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    /*
    [UnityTest]
    public IEnumerator TestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    */
}
