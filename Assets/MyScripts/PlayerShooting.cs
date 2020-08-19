using UnityEngine;

namespace MyGame
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private int damagePerShot = 20;
        [SerializeField] private float timeBetweenShot = 0.15f;
        [SerializeField] private ParticleSystem gunParticles;
        [SerializeField] private LineRenderer gunLine;
        [SerializeField] private Light gunLight;
        [SerializeField] private float range = 100f;

        private PlayerHealth playerHealth;
        private float timer;
        private Ray shootRay = new Ray();
        private RaycastHit shootHit;
        private int shootableMask;
        private float effectsDisplayTime = 0.2f;

        public void Init(PlayerHealth playerHealth)
        {
            this.playerHealth = playerHealth;
            shootableMask = LayerMask.GetMask("Shootable");
        }

        public void RefreshShooting()
        {
            timer += Time.deltaTime;

            if (Input.GetButton("Fire1") && timer >= timeBetweenShot && Time.timeScale != 0 && !playerHealth.IsDead)
            {
                Shoot();
            }

            if (timer >= timeBetweenShot * effectsDisplayTime)
            {
                DisableEffects();
            }
        }

        public void DisableEffects()
        {
            gunLine.enabled = false;
            gunLight.enabled = false;
        }

        private void Shoot()
        {
            timer = 0f;

            SoundController.Instance.PlayAudio(TypeAudio.GunShot);

            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                EnemiesHealth enemiesHealth = shootHit.collider.GetComponent<EnemiesHealth>();
                if (enemiesHealth != null)
                {
                    enemiesHealth.TakeDamage(damagePerShot, shootHit.point);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}
