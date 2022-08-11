using System.Collections;
using UnityEngine;
using Zenject;
using System.Linq;
using System;
using SpaceInvaders.HighScore;
using SoftwareCore.Extensions;
using System.Collections.Generic;
using SpaceInvaders.Settings;
using SpaceInvaders.LastGameData;
using SoftwareCore.UI.Binders.Components;

namespace SpaceInvaders.UI.Presenters.Components
{
    public class LastGameScoreUIPresenterComponent : MonoBehaviour
    {

        public TextUIBinderComponent scoreText;
        public TextUIBinderComponent wavesDefeatedText;

        LastGameDataInfo LastGameDataInfo { get; set; }


        [Inject]
        public void Construct(LastGameDataInfo lastGameDataInfo) {

            LastGameDataInfo = lastGameDataInfo;
        }


        // Start is called before the first frame update
        void OnEnable() {
            UpdateDisplay();
        }

        void UpdateDisplay() {


            if (scoreText) {
                scoreText.SetText(LastGameDataInfo.HighScore.value.ToString());
            }

            if (wavesDefeatedText) {
                wavesDefeatedText.SetText(LastGameDataInfo.HighScore.wavesDefeated.ToString());
            }

        }

    }
}