using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        private PlayerController playerController;
        private int score;

        public void Init(PlayerController playerController, int score)
        {
            this.score = score;
            this.playerController = playerController;
            EnemiesHealth.OnEnemyDie += UpdateScore;
            PlayerHealth.OnDamage += ShowHitEffect;
            PlayerHealth.OnGameOver += ShowGameOver;
        }

        public void UpdateScore(int scoreValue)
        {
            score += scoreValue;
            text.text = ($"Score: {score}");
        }

        public void ShowHitEffect()
        {
            damageImage.DOFade(0.2f, 0.1f).SetLoops(2, LoopType.Yoyo);
            healthSlider.value = playerController.CurrentHealth / 100;
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
