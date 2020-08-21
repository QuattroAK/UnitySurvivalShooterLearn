using UnityEngine;

namespace MyGame
{
    public class EnemiesController : MonoBehaviour
    {
        [SerializeField] private EnemiesMovement enemiesMovement;
        [SerializeField] private EnemiesHealth enemiesHealth;
        [SerializeField] private EnemiesAttack enemiesAttack;
        
        public void Init(PlayerController playerController)
        {
            enemiesMovement.Init(playerController, enemiesHealth);
            enemiesHealth.Init();
            enemiesAttack.Init(playerController, enemiesHealth);
        }

        public void Refresh()
        {
            enemiesMovement.Refresh();
            enemiesHealth.Refresh();
            enemiesAttack.Refresh();
        }
    }
}
