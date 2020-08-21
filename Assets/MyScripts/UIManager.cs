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

        private float flashSpeed = 0.2f;
        private PlayerController playerController;

        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
            playerController.OnDamage += ShowHitEffect;
            playerController.OnGameOver += ShowGameOver;
        }

        public void UpdateScore(int scoreValue)
        {
            text.text = $"Score: {scoreValue}";
        }

        public void ShowHitEffect()
        {
            if (!DOTween.IsTweening(damageImage))
            {
                damageImage.DOFade(0.1f, flashSpeed).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
            }
            
            healthSlider.value = playerController.CurrentHealth / playerController.StartingHealth;
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