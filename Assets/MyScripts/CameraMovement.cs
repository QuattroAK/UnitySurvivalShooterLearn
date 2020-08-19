using UnityEngine;

namespace MyGame
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float movementSmoothing = 5f;

        private Transform playerTarget;
        private Vector3 cameraOffset;

        public void Init(Transform playerTarget)
        {
            this.playerTarget = playerTarget;
            cameraOffset = transform.position - playerTarget.position; // вектор смещения
        }

        public void RefreshPosition()
        {
            Vector3 targetCamPos = playerTarget.position + cameraOffset; // позиция камеры это расположение игрока плюс вектор смещения камеры 
            transform.position = Vector3.Lerp(transform.position, targetCamPos, movementSmoothing * Time.deltaTime); // задаем движение камеры со сглаживанием 
        }
    }
}
