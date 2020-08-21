using System;
using System.Collections;
using UnityEngine;

namespace MyGame
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 100;
        [SerializeField] private Animator anim;

        public static Action OnDamage;
        public static Action OnGameOver;
        
        private PlayerMovement playerMovement;
        private PlayerShooting playerShooting;
        private bool isDead;
        private bool damaged;
        private int currentHealth;

        public bool IsDead
        {
            get
            {
                return isDead;
            }
        }

        public bool Damage
        {
            get
            {
                return damaged;
            }
        }

        public int CurrentHealth
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

        public void Init(PlayerMovement playerMovement, PlayerShooting playerShooting)
        {
            this.playerMovement = playerMovement;
            this.playerShooting = playerShooting;
            currentHealth = startingHealth;
        }

        public void RefreshHealth()
        {
            
        }

        public void TakeDamage(int amount)
        {
            damaged = true;
            currentHealth -= amount;
            OnDamage?.Invoke();
            SoundController.Instance.PlayAudio(TypeAudio.PlayerDamage);

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }

        }

        private IEnumerator Flashing()
        {
            yield return new WaitForSeconds(0.02f);
            damaged = false;
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