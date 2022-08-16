using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SAE_Portfolio_2.Manager
{
    using SAE_Portfolio_2.UI;
    using SAE_Portfolio_2.Data;

    public sealed class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameParameters gameParameters;
        [SerializeField] private LoadingPanel loadingPanel;
        [SerializeField] private Button loadGameButton;
        [SerializeField] private Toggle vSyncToggle;

        private void Awake()
        {
            if (!File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
            {
                loadGameButton.interactable = false;
            }
            if (PlayerPrefs.HasKey("VSync"))
            {
                QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSync");
                if (PlayerPrefs.GetInt("VSync") == 0)
                {
                    vSyncToggle.isOn = false;
                }
                else
                {
                    vSyncToggle.isOn = true;
                }
            }
            vSyncToggle.onValueChanged.AddListener((isOn) => 
            {
                PlayerPrefs.SetInt("VSync", isOn ? 1 : 0);
                QualitySettings.vSyncCount = isOn ? 1 : 0;
            });
            Time.timeScale = 1.0f;
        }

        public void LoadGame()
        {
            gameParameters.IsNewGame = false;
            LoadScene("JumpRunGame");
        }

        public void NewGame()
        {
            gameParameters.IsNewGame = true;
            LoadScene("JumpRunGame");
        }

        public void FPSTest()
        {
            LoadScene("FPSGame");
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
