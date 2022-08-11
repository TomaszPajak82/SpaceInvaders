using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects.Components
{

    [CreateAssetMenu(fileName = "WaveGameplayStateObjectSO", menuName = "GameplayStateObject/Wave", order = 1)]
    public class WaveGameplayStateObjectProviderScriptableObject : GameplayStateObjectProviderScriptableObject
    {

        public GameObject[] prefabs;
        public int columnsCount = 10;
        public int rowsCount = 3;
        public float horizontalSpacing = 2.5f;
        public float verticalSpacing = 2.5f;

        [Header("Bbehavior")]
        public float movementSpeed = 2;
        public float columnFiringPerSecondPropability = 0.1f;

        WaveGameplayStateObject GameplayStateObject { get; set; }

        protected override IGameplayStateObject OnGetGameplayStateObject() {
            if (GameplayStateObject == null) {
                GameplayStateObject = new WaveGameplayStateObject();

                GameplayStateObject.Prefabs = prefabs;
                GameplayStateObject.ColumnsCount = columnsCount;
                GameplayStateObject.RowsCount = rowsCount;
                GameplayStateObject.HorizontalSpacing = horizontalSpacing;
                GameplayStateObject.VerticalSpacing = verticalSpacing;

                GameplayStateObject.MovementSpeed = movementSpeed;
                GameplayStateObject.ColumnFiringPerSecondPropability = columnFiringPerSecondPropability;
            }
            return GameplayStateObject;
        }

    }
}