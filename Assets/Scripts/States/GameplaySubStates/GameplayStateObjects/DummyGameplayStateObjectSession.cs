using System.Collections;
using System.Data;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    public class DummyGameplayStateObjectSession : IGameplayStateObjectSession {

        private bool isCompleted = false;

        private bool isReadOnly = false;

        public bool IsCompleted {
            get { return isCompleted; }
            set {
                if (isReadOnly) {
                    throw new ReadOnlyException(nameof(IsCompleted));
                } else {
                    isCompleted = value;
                }
            }
        }
    

        public void FixedUpdate(float dTime) {}

        public void Update(float dTime) {}

        public DummyGameplayStateObjectSession(bool isCompleted,bool isReadOnly = false) {
            this.isReadOnly = isReadOnly;
            this.isCompleted = isCompleted;
        }

        public static IGameplayStateObjectSession CompletedSession { get; private set; } = new DummyGameplayStateObjectSession(true,true);
    }
}