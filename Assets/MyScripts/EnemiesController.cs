using System;
using UnityEngine;

namespace MyGame
{
    public class EnemiesController : MonoBehaviour
    {
        [SerializeField] private EnemiesMovement enemiesMovement;
        [SerializeField] private EnemiesHealth enemiesHealth;
        [SerializeField] private EnemiesAttack enemiesAttack;
        
        public void Init(PlayerController playerController, Action<int> OnEnemyDie)
        {
            enemiesMovement.Init(playerController, enemiesHealth);
            enemiesHealth.Init(OnEnemyDie);
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
