using SpaceInvaders.Settings;
using SpaceInvaders.HighScore;
using System.Text;
using UnityEngine;
using Zenject;
using SoftwareCore.UI.Binders.Components;
using System.Collections.Generic;
using SoftwareCore.Specification;

namespace SpaceInvaders.UI.Presenters.Components
{
    public class HighScoreUIPresenterComponent : MonoBehaviour
    {
        public TextUIBinderComponent highScoreTextBinder;

        public bool displayFormatFull = false;

        public bool displayAll = false;

        public MethodTriggeredButtonUIBinderComponent clearButtonBinder;

        public MethodTriggeredButtonUIBinderComponent addButtonBinder;

        public MaxResultsSpecification<HighScoreInfo> MaxResultsSpecification { get; set; } = new MaxResultsSpecification<HighScoreInfo>(0);

        IHighScoreRepository HighScoreRepository { get; set; }

        IHighScoreSettings Settings { get; set; }

        [Inject]
        public void Construct(IHighScoreSettings highScoreSettings, IHighScoreRepository highScoreRepository) {
            Settings = highScoreSettings;
            HighScoreRepository = highScoreRepository;
        }

        // Start is called before the first frame update
        void OnEnable() {

            if (clearButtonBinder != null) {
                clearButtonBinder.Clicked += ClearButtonBinder_Clicked;
            }

            if (addButtonBinder != null) {
                addButtonBinder.Clicked += AddButtonBinder_Clicked;
            }

            UpdateDisplay();
        }


        private void OnDisable() {
            if (clearButtonBinder != null) {
                clearButtonBinder.Clicked -= ClearButtonBinder_Clicked;
            }

            if (addButtonBinder != null) {
                addButtonBinder.Clicked -= AddButtonBinder_Clicked; ;
            }
        }

        private void AddButtonBinder_Clicked(object sender, System.EventArgs e) {
            HighScoreRepository.Add(new HighScoreInfo(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(101, 200), System.DateTime.Now));
            UpdateDisplay();
        }

        private void ClearButtonBinder_Clicked(object sender, System.EventArgs e) {
            HighScoreRepository.Clear();
            UpdateDisplay();
        }

        void UpdateDisplay() {

            StringBuilder strBuilder = new StringBuilder();
            int index = 0;

            IEnumerable<HighScoreInfo> highScoreItems;
            if (this.displayAll) {
                highScoreItems = HighScoreRepository;
            } else {
                this.MaxResultsSpecification.MaxResults = this.Settings.DisplayCount;
                highScoreItems = HighScoreRepository.Get(this.MaxResultsSpecification);
            }
            

            foreach (var highScore in highScoreItems) {

                string format = displayFormatFull ? Settings.DisplayFullFormat : Settings.DisplayFormat;

                strBuilder.AppendFormat(format, index + 1, highScore.value, highScore.dataTime);
                strBuilder.AppendLine();
                index++;

            }


            if (highScoreTextBinder != null) {
                highScoreTextBinder.SetText(strBuilder.ToString());
            }
        }
    }
}