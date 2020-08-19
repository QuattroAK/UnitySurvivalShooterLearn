﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MyGame
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private CameraMovement cameraMovement;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private EnemiesManager enemiesManager;
        [SerializeField] private SoundController soundController;
        [SerializeField] private UIManager uIManager;

        private float deleyRestart = 3f;

        private void Start()
        {
            uIManager.Init(playerController);
            soundController.Init();
            cameraMovement.Init(playerController.Transform);
            playerController.Init();
            enemiesManager.Init(playerController);
        }

        private void Update()
        {
            cameraMovement.RefreshPosition();
            playerController.Refresh();
            enemiesManager.Refresh();
            uIManager.Refresh();
            RestartLevel();
        }

        private void RestartLevel()
        {
            if (!playerController.IsAlive)
            {
                StartCoroutine(WaitRestart());
            }
        }

        private IEnumerator WaitRestart()
        {
            yield return new WaitForSeconds(deleyRestart);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
