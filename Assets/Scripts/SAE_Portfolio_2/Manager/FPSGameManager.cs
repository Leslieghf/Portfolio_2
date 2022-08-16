using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.Manager
{
    using SAE_Portfolio_2.UI;
    using SAE_Portfolio_2.Movement.FPS;

    public sealed class FPSGameManager : MonoBehaviour
    {
        [SerializeField] private LoadingPanel loadingPanel;
        [SerializeField] private PausePanel pausePanel;
        [SerializeField] private FPSMovement playerMovement;

        private bool isPaused = false;

        private void Awake()
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
                pausePanel.ToggleVisibility();
            }
        }

        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            playerMovement.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            playerMovement.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void TogglePause()
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void LoadScene(string sceneIndex)
        {
            loadingPanel.LoadScene(sceneIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    } 
}
