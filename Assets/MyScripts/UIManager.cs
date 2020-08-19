using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Animator gameOver;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image damageImage;
        [SerializeField] private Color flashColour = new Color(1f, 0f, 0f, 0.1f);
        [SerializeField] private float flashSpeed = 2f;

        public static int score;

        private PlayerController playerController;


        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
            score = 0;
        }

        public void Refresh()
        {
            UpdateScore();
            ShowGameOver();
            ShowHitEffect();
        }

        public void UpdateScore()
        {
            text.text = "Score: " + score;
        }

        public void ShowHitEffect()
        {
            healthSlider.value = playerController.CurrentHealth;

            if (playerController.Damage)
            {
                damageImage.color = flashColour;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
        }

        public void ShowGameOver()
        {
            if (!playerController.IsAlive)
            {
                gameOver.SetTrigger("GameOver");
            }
        }
    }
}
