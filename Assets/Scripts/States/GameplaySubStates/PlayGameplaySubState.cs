using SoftwareCore.Pool;
using SoftwareCore.Signals.CollisionSignal;
using SoftwareCore.States;
using SpaceInvaders.Settings;
using SpaceInvaders.PlayerInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using SpaceInvaders.EntityRoot.Components;
using SpaceInvaders.Projectiles.Components;
using SpaceInvaders.Health.Components;
using SpaceInvaders.Weapons.Components;
using SpaceInvaders.Updatable.Components;
using Cysharp.Threading.Tasks;
using SpaceInvaders.Kill.Components;

namespace SpaceInvaders.States.GameplaySubStates
{
    public class PlayGameplaySubState : GameplaySubState
    {
        ICollisionSignalReceiver CollisionSignalReceiver { get; set; }

        IPlayerInput PlayerInput { get; set; }

        IGameSettings Settings { get; set; }

        public IState WinState { get; set; }

        public IState LostState { get; set; }


        List<GameObject> tmpGameObjectslist = new List<GameObject>();

        float PlayerFiringCooldownTimeLeft { get; set; }

        HashSet<GameObject> PendingObjectsToRemove { get; set; } = new HashSet<GameObject>();

        float PlayerInvulnerabilityTimeLeft { get; set; }

        IGameplayStateObjectSession GameplayStateObjectSession { get; set; }

        public PlayGameplaySubState(IGameSettings settings, ICollisionSignalReceiver collisionSignalReceiver, IPlayerInput playerinput) {

            Settings = settings;
            CollisionSignalReceiver = collisionSignalReceiver;
            PlayerInput = playerinput;

        }

        protected override void OnOnEnter(IStateManager stateManager) {
            base.OnOnEnter(stateManager);

            GameplayStateObjectSession = Data.GameplayStateObject.StartPlaySession();

            PendingObjectsToRemove.Clear();


            PlayerFiringCooldownTimeLeft = 0;
            PlayerInvulnerabilityTimeLeft = 0;

            CollisionSignalReceiver.Received += CollisionSignalReceiver_Received;

            Data.GameplayStateObject.StartPlaySession();
        }

        protected override void OnOnExit() {
            base.OnOnExit();

            ResolvePendingObjects();

            CollisionSignalReceiver.Received -= CollisionSignalReceiver_Received;
        }

        private void CollisionSignalReceiver_Received(object sender, ICollisionSignal e) {

            EntityRootComponent entityRootComponent = e.PrimaryCollider.transform.GetComponentInParent<EntityRootComponent>();
            if (entityRootComponent == null) {
                return;
            }

            ProjectileComponent projectile = entityRootComponent.GetComponent<ProjectileComponent>();
            if (projectile != null) {
                PendingObjectsToRemove.Add(entityRootComponent.gameObject);
                Data.Projectiles.Remove(entityRootComponent.gameObject);
                return;
            }


            GameObject go = entityRootComponent.gameObject;



            bool playerIsHit = false;
            if (go == Data.PlayerGO) {
                playerIsHit = true;
            }

            bool isAlive = true;

            if (playerIsHit && PlayerInvulnerabilityTimeLeft > 0) {

            } else {

                if (playerIsHit) {
                    if (PlayerInvulnerabilityTimeLeft <= 0) {
                        PlayerInvulnerabilityTimeLeft = Settings.PlayerInvulnerabilityDuration;
                    }
                }

                HealthComponent health = go.GetComponent<HealthComponent>();
                if (health != null) {

                    health.Health = Mathf.Max(0, health.Health - 1);

                    isAlive = health.Health > 0;

                }

            }

            if (!isAlive) {

                if (!playerIsHit) {

                    KillComponent killComponent = go.GetComponent<KillComponent>();
                    if (killComponent != null) {
                        if (!killComponent.IsKilling()) {
                            Data.ObjectsInProcessOfKilling.Enqueue((killComponent.Kill(), killComponent.gameObject));
                            killComponent.Kill();
                        }
                    } else {
                        PendingObjectsToRemove.Add(go);
                    }
                }

                Data.CurrentScore = Data.CurrentScore + 1;
            }

        }


        

        protected override void OnOnUpdate(float dTime) {
            base.OnOnUpdate(dTime);

            GameplayStateObjectSession.Update(dTime);

            if (!UpdateVictoryConditions(dTime)) {

                UpdatePlayer(dTime);

                UpdateProjectiles(dTime);

                UpdateKillingProcess(dTime);

                ResolvePendingObjects();

                UpdateData(dTime);
            }

        }

        void UpdateData(float dTime) {
            HealthComponent healthComponent = Data.PlayerGO.GetComponent<HealthComponent>();
            if (healthComponent != null) {
                Data.LivesLeft = healthComponent.Health;
            }
        }


        void UpdatePlayer(float dTime) {

            UpdatePlayerPosition(dTime);

            UpdatePlayerFiring(dTime);

            UpdatePlayerInvulunerability(dTime);
        }

        void UpdatePlayerPosition(float dTime) {

            float playerSpeed = Settings.PlayerSpeed;
            float horizontalShift = PlayerInput.HorizontalInputValue * playerSpeed * dTime;

            Transform playerTransform = Data.PlayerGO.transform;

            Vector3 newPosition = Data.PlayerArea.CorrectPosition(playerTransform.position + playerTransform.right * horizontalShift);

            playerTransform.position = newPosition;
        }

        void UpdatePlayerFiring(float dTime) {

            PlayerFiringCooldownTimeLeft -= dTime;

            if (PlayerFiringCooldownTimeLeft <= 0) {

                if (PlayerInput.Fire) {
                    WeaponsControllerComponent weaponController = Data.PlayerGO.GetComponent<WeaponsControllerComponent>();
                    if (weaponController != null) {
                        tmpGameObjectslist.Clear();
                        weaponController.Fire(this.tmpGameObjectslist);
                        tmpGameObjectslist.ForEach(x => Data.Projectiles.Add(x));

                        PlayerFiringCooldownTimeLeft = Settings.FiringCooldown;
                    }
                }
            }
        }

        void UpdatePlayerInvulunerability(float dTime) {

            if (PlayerInvulnerabilityTimeLeft > 0) {

                PlayerInvulnerabilityTimeLeft = PlayerInvulnerabilityTimeLeft - dTime;

                float blinkDuration = 1 / Settings.PlayerInvulnerabilityBlinkingFrequency;

                int oddEvenNumber = (int)(PlayerInvulnerabilityTimeLeft / blinkDuration);
                if ((oddEvenNumber & 1) == 0) {
                    Data.PlayerGO.SetActive(false);
                } else {
                    Data.PlayerGO.SetActive(true);
                }

            } else {
                Data.PlayerGO.SetActive(true);
            }
        }

        void UpdateProjectiles(float dTime) {

            float speed = Settings.ProjectileSpeed;

            foreach (var projectile in Data.Projectiles) {

                UpdatableComponent updatable = projectile.GetComponent<UpdatableComponent>();
                updatable.DoUpdate(dTime);
            }

        }

        void UpdateKillingProcess(float dTime) {

            while (Data.ObjectsInProcessOfKilling.TryPeek(out (UniTask<KillComponent> task,GameObject go) killing)) {   

                if(killing.task.Status != UniTaskStatus.Pending) {

                    killing = Data.ObjectsInProcessOfKilling.Dequeue();
                    this.PendingObjectsToRemove.Add(killing.go);
                } else {
                    break;
                }
            }

        }

        void ResolvePendingObjects() {

            foreach (var go in PendingObjectsToRemove) {
                PoolUtils.ReleaseToPoolOrDestroy(go);
            }

            PendingObjectsToRemove.Clear();

        }


        /// <returns>true when victory condition resolved and state was changed</returns>
        bool UpdateVictoryConditions(float dTime) {

            bool isLost = false;
            bool isWin = false;

            isWin = this.GameplayStateObjectSession.IsCompleted;


            if (Data.LivesLeft <= 0) {
                isLost = true;
            }

            if (isLost) {
                this.CurrentStateManager.State = LostState;
                return true;
            } else if (isWin) {
                this.CurrentStateManager.State = WinState;
                return true;
            }

            return false;
        }
    }

}