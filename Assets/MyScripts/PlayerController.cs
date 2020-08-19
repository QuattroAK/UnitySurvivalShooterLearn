using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(MyGame.PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerShooting playerShooting;

        public bool Damage
        {
            get
            {
                return playerHealth.Damage;
            }
        }
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

        public int CurrentHealth
        {
            get
            {
                return playerHealth.CurrentHealth;
            }
        }

        public void Init()
        {
            playerMovement.Init(playerHealth);
            playerHealth.Init(playerMovement, playerShooting);
            playerShooting.Init(playerHealth);
        }

        public void Refresh()
        {
            playerMovement.RefreshMovement();
            playerHealth.RefreshHealth();
            playerShooting.RefreshShooting();
        }

        public void TakeDamage(int amount)
        {
            playerHealth.TakeDamage(amount);
        }
    }
}
