
namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    public interface IGameplayStateObjectManager
    {
        IGameplayStateObject GetNextGameplayStateObject(GameplaySubStatesData data);

        void SetGameplayStateObjectsSource(IGameplayStateObjectsSource gameplayStateObjectsSource);

        void Reset();
    }
}