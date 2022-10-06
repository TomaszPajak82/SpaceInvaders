using SpaceInvaders.HighScore;
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;
using Zenject;

namespace SpaceInvaders.Tests
{

    public class HighScoreGarbageTestComponent : MonoBehaviour
    {

        IHighScoreRepository HighScoreRepository { get; set; }

        [Inject]
        public void Construct(IHighScoreRepository highScoreRepository) {
            
            HighScoreRepository = highScoreRepository;
        }

        private void Update() {
            /*
            Profiler.enabled = true;
            UnityEngine.Profiling.Profiler.BeginSample("HighScoreRepositoryGarbageTest");

            foreach (var item in HighScoreRepository) {
                if (item.value < -1) {
              //      this.GetComponent<MeshRenderer>();
                    break;
                }
            }

            UnityEngine.Profiling.Profiler.EndSample();
            Profiler.enabled = false;
            */
        }


    }

}

