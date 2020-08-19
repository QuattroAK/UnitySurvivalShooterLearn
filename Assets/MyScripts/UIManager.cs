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
        // TODO Вынести в GameController. Очки не должны считаться в UIManager. UIManager только обновляет значение, а не хранит его.
        public static int score;

        private PlayerController playerController;


        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
            score = 0;
        }
        // TODO Подписать каждый метод на соответствующий callback. Refresh в Update у UIController это конечно простое и быстрое решение, но лучше делать через события и подписки.
        public void Refresh()
        {
            UpdateScore();  // TODO Создать событие OnEnemyDie в EnemiesController и на него подписываться через EnemyManager. Однако т.к. объектов EnemiesController много, надо создать одно событие в EmenyManager и передать на него ссылку во все объекты EnemiesManager.
            ShowGameOver();
            ShowHitEffect();    // TODO Эту штуку можно сделать через DOTWEEEN. damageImage.DOFade(...)
        }

        public void UpdateScore()
        {
            text.text = "Score: " + score; // TODO Заменить на интерполяцию
        }

        public void ShowHitEffect()
        {
            healthSlider.value = playerController.CurrentHealth; // TODO Здесь нужно доработать. Брать относительные доли. Сейчас тебе повезло т.к. здоровья 100, а если не 100 будет, то value будет не правильно.

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
