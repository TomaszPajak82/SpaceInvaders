using UnityEngine;
using Zenject;

namespace SpaceInvaders.Settings
{

    [CreateAssetMenu(fileName = "GameSettingsInstallerScriptableObjectInstaller", menuName = "Installers/GameSettingsInstallerScriptableObjectInstaller")]
    public class GameSettingsScriptableObjectInstaller : ScriptableObjectInstaller<GameSettingsScriptableObjectInstaller>
    {

        public GameSettings gameSettings;

        [Space]
        public HighScoreSettings highScoreSettings;



        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<GameSettings>().FromInstance(gameSettings).AsSingle();

            Container.Bind<IHighScoreSettings>().FromInstance(highScoreSettings).AsSingle();
        }
    }
}