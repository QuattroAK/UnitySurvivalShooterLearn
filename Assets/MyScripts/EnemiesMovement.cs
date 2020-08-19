using UnityEngine;

namespace MyGame
{
    public class EnemiesMovement : MonoBehaviour
    {
        [SerializeField] private UnityEngine.AI.NavMeshAgent nav;

        private PlayerController playerController;
        private EnemiesHealth enemyHealth;

        public void Init(PlayerController playerController , EnemiesHealth enemiesHealth)
        {
            this.playerController = playerController;
            this.enemyHealth = enemiesHealth;
        }

        public void Refresh()
        {
            if (enemyHealth.currentHealth > 0 && playerController.IsAlive)
            {
                nav.SetDestination(playerController.transform.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
    }
}
