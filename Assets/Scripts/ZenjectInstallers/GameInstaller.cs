using SoftwareCore.Storage;
using SoftwareCore.Signals.CollisionSignal;
using SoftwareCore.States;
using SoftwareCore.Time;
using SpaceInvaders.States;
using SpaceInvaders.HighScore;
using SpaceInvaders.Initialization;
using SpaceInvaders.LastGameData;
using SpaceInvaders.PlayerInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SpaceInvaders.States.GameplaySubStates;
using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using SoftwareCore.Audio;

namespace SpaceInvaders.ZenjectInstallers
{


    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings() {
            //base.InstallBindings();

            SignalBusInstaller.Install(Container);

            Container.Bind<InitializationObject>().ToSelf().AsSingle().NonLazy();

            Container.BindInterfacesTo<TouchesPlayerInput>().AsSingle();

            Container.Bind<ITime>().To<ProxyTime>().AsSingle();

            Container.Bind<IAudioManager>().To<AudioManager>().AsSingle();

            Container.BindInterfacesTo<StateManager>().AsSingle();

            Container.Bind<ISubStateManager>().To<SubStateManager>().AsTransient();

            Container.Bind<GameplaySubStatesSet>().ToSelf().AsTransient();
            Container.Bind<InitializationGameplaySubState>().ToSelf().AsTransient();
            Container.Bind<GameplayStateObjectInitializationGameplaySubState>().ToSelf().AsTransient();
            Container.Bind<PrePlayGameplaySubState>().ToSelf().AsTransient();
            Container.Bind<PlayGameplaySubState>().ToSelf().AsTransient();
            Container.Bind<CleanupGameplaySubState>().ToSelf().AsTransient();
            Container.Bind<RegisterHighScoreGameplaySubState>().ToSelf().AsTransient();

            Container.Bind<IGameplayStateObjectManager>().To<GameplayStateObjectManager>().AsSingle();

            Container.Bind<IPersistentStorage>().To<PlayerPrefsPersistentStorage>().AsSingle();

            Container.Bind<IHighScoreRepository>().To<HighScore.HighScoreRepository>().AsSingle();

            Container.Bind<LastGameDataInfo>().ToSelf().AsSingle();


            Container.BindInterfacesTo<CollisionSignalBus>().AsSingle();
            Container.DeclareSignal<ICollisionSignal>();


        }

    }
}
