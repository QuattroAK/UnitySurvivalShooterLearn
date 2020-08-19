using UnityEngine;

namespace MyGame
{
    public class EnemiesAttack : MonoBehaviour
    {
        [SerializeField] private float timeBetweenAttacks = 0.5f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private Animator anim;

        private PlayerController playerController;
        private EnemiesHealth enemiesHealth;
        private bool playerInRange;
        private float timer;

        public void Init(PlayerController playerController, EnemiesHealth enemiesHealth)
        {
            this.playerController = playerController;
            this.enemiesHealth = enemiesHealth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == playerController.gameObject)
            {
                playerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == playerController.gameObject)
            {
                playerInRange = false;
            }
        }

        public void Refresh()
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && playerInRange && enemiesHealth.currentHealth > 0) // если пришло время атаковать и если игрок в пределах досягаемости и мы не мертвы
            {
                Attack();
            }

            if (!playerController.IsAlive)
            {
                anim.SetTrigger("PlayerDead");
            }
        }

        private void Attack()
        {
            timer = 0f;

            if (playerController.IsAlive)
            {
                playerController.TakeDamage(attackDamage);
            }
        }
    }
}
