using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame
{
    public class EnemiesHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 100;
        [SerializeField] private float sinkSpeed = 2.5f; // скорость исчезновения после смерти 
        [SerializeField] private int scoreValue = 10;
        [SerializeField] private AudioClip deathClip; 
        [SerializeField] private Animator anim;  
        [SerializeField] private AudioSource enemyAudio; 
        [SerializeField] private ParticleSystem hitParticles; 
        [SerializeField] private CapsuleCollider colliderEnemies;
        [SerializeField] private NavMeshAgent navAgent;
        [SerializeField] private Rigidbody rbEnemy;

        private Action<int> OnEnemyDie;
        public int currentHealth;
        private bool isDead; 
        private bool isSinking; 

        public void Init(Action<int> OnEnemyDie) 
        {
            this.OnEnemyDie += OnEnemyDie;
            currentHealth = startingHealth; 
        }

        public void Refresh()
        {
            if (isSinking) // враг начинает тонуть со временем
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }

        public void TakeDamage(int amount, Vector3 hitPoint)  
        {
            if (isDead) 
                return;

            enemyAudio.Play(); 

            currentHealth -= amount;  
            hitParticles.transform.position = hitPoint; 
            hitParticles.Play(); 

            if (currentHealth <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            isDead = true;
            colliderEnemies.isTrigger = true; // делаем коллайдер как триггер, для предотвращения помех которое создает мертвый враг  
            anim.SetTrigger("Dead"); 
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
        }

        public void StartSinking() // начало потопления(отключаем отдельные компоненты объекта) ( вызываеться в анимации Death) 
        {
            navAgent.enabled = false;
            rbEnemy.isKinematic = true; // втердвое тело как кинематическое для облегчения движка и отсутсвия лишних вычислений физики 
            isSinking = true;
            OnEnemyDie?.Invoke(scoreValue);
            DOVirtual.DelayedCall(2f, ReturnPool);
        }

        public void ReturnPool()
        {
            gameObject.SetActive(false);
            anim.SetTrigger("StateDefault");
        }
    }
}
