using System;
using UnityEngine;

namespace MyGame
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 100;
        [SerializeField] private Animator anim;

        private event Action OnDamage;
        private event Action OnGameOver;
        
        private PlayerMovement playerMovement;
        private PlayerShooting playerShooting;
        private bool isDead;
        private float currentHealth;

        #region Properties

        public float StartingHealth
        {
            get
            {
                return startingHealth;
            }
        }

        public bool IsDead
        {
            get
            {
                return isDead;
            }
        }

        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
        }

        public bool IsAlive
        {
            get
            {
                return currentHealth > 0;
            }
        }

        #endregion

        public void Init(PlayerMovement playerMovement, PlayerShooting playerShooting, Action OnDamage, Action OnGameOver)
        {
            this.playerMovement = playerMovement;
            this.playerShooting = playerShooting;
            currentHealth = startingHealth;

            this.OnDamage += OnDamage;
            this.OnGameOver += OnGameOver;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            OnDamage?.Invoke();
            SoundController.Instance.PlayAudio(TypeAudio.PlayerDamage);

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }

        }

        private void Death()
        {
            isDead = true;
            playerShooting.DisableEffects();
            anim.SetTrigger("Die");
            SoundController.Instance.PlayAudio(TypeAudio.PlayerDeath);
            OnGameOver?.Invoke();
        }
    }
}