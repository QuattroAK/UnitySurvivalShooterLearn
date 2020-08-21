using System;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(MyGame.PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        public event Action OnDamage;
        public event Action OnGameOver;

        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerShooting playerShooting;

        #region Properties

        public Transform Transform
        {
            get
            {
                return gameObject.transform;
            }
        }

        public bool IsAlive
        {
            get
            {
                return playerHealth.IsAlive;
            }
        }

        public float CurrentHealth
        {
            get
            {
                return playerHealth.CurrentHealth;
            }
        }

        public float StartingHealth
        {
            get
            {
                return playerHealth.StartingHealth;
            }
        }

        #endregion

        public void Init()
        {
            playerMovement.Init(playerHealth);
            playerHealth.Init(playerMovement, playerShooting, OnDamageHandler, OnGameOverHandler);
            playerShooting.Init(playerHealth);
        }

        public void Refresh()
        {
            playerMovement.RefreshMovement();
            playerShooting.RefreshShooting();
        }

        public void TakeDamage(int amount)
        {
            playerHealth.TakeDamage(amount);
        }

        private void OnDamageHandler()
        {
            OnDamage?.Invoke();
        }

        private void OnGameOverHandler()
        {
            OnGameOver?.Invoke();
        }
    }
}
