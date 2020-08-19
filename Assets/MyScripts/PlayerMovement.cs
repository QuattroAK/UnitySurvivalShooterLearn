using UnityEngine;

namespace MyGame
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;
        [SerializeField] private Animator anim;  
        [SerializeField] private Rigidbody playerRigidbody;

        private PlayerHealth playerHealth;
        private Vector3 movementVector;  
        private int floorMask; // слой (уровень)
        private float camRayLength = 100f;

        public void Init(PlayerHealth playerHealth) 
        {
            this.playerHealth = playerHealth;
            floorMask = LayerMask.GetMask("Floor"); 
        }

        public void RefreshMovement()
        {
            // TODO: вынести в UserInput
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (!playerHealth.IsDead)
            {
                Move(h, v);
                Turning();
                Animating(h, v);
            }
        }

        private void Move(float h, float v) // Перемещение игрока
        {
            movementVector.Set(h, 0f, v); // устанавливаем вектор ( он постоянно меняется в зависмости от нажатия клавиш)
            movementVector = movementVector.normalized * speed * Time.deltaTime; // умножаем вектор на скорость и время
            playerRigidbody.MovePosition(transform.position + movementVector);
        }

        private void Turning() // вращение игроком  в нужном направлении 
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); // пускаем луч из камеры
            RaycastHit floorHit; // создаем выходной параметр для хранения объекто на который упал луч

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) // если луч во что то попал 
            {
                Vector3 playerToMouse = floorHit.point - transform.position; // вектор направления взгляда игрока 
                playerToMouse.y = 0f; // что бы не заваливался по Y. Устанавливаем ноль

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse); // преобразуем направление взгляда игрока во вращение 
                playerRigidbody.MoveRotation(newRotation); // применяем вращение к игроку
            }
        }

        private void Animating(float h, float v) // анимация игрока при движении, смерти или простое
        {
            bool walking = h != 0f || v != 0f; // если есть нажатие клавиш в переменной h или v  то вернет true
            anim.SetBool("IsWalking", walking); // анимируем ходьбу, если есть нажатие на клавиши 
        }
    }
}
