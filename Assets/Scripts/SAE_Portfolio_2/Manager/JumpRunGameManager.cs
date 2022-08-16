using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SAE_Portfolio_2.Manager
{
    using SAE_Portfolio_2.Data;
    using SAE_Portfolio_2.Currency;
    using SAE_Portfolio_2.Currency.Data;
    using SAE_Portfolio_2.UI;
    using SAE_Portfolio_2.Player;

    public sealed class JumpRunGameManager : MonoBehaviour
    {
        [SerializeField] private GameParameters gameParameters;
        [SerializeField] private Transform remainingCoinsParent;
        [SerializeField] private Coins coins;
        [SerializeField] private Player player;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private LoadingPanel loadingPanel;
        [SerializeField] private PausePanel pausePanel;

        private string persistentPath = "";
        private bool isPaused = false;

        private void Awake()
        {
            persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
            if (!gameParameters.IsNewGame)
            {
                LoadData();
            }
            else
            {
                coins.SetBalance(0);
            }
            Time.timeScale = 1.0f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
                pausePanel.ToggleVisibility();
            }
        }

        private GameData GetGameData()
        {
            Vector3[] remainingCoins = new Vector3[remainingCoinsParent.childCount];
            for (int i = 0; i < remainingCoinsParent.childCount; i++)
            {
                remainingCoins[i] = remainingCoinsParent.GetChild(i).transform.position;
            }
            return new GameData(remainingCoins, coins.GetBalance(), player.transform.position);
        }

        private void SetGameData(GameData gameData)
        {
            Coin[] oldCoins = FindObjectsOfType<Coin>();
            foreach (Coin oldCoin in oldCoins)
            {
                Destroy(oldCoin.gameObject);
            }
            foreach (Vector3 remainingCoin in gameData.remainingCoins)
            {
                Instantiate(coinPrefab, remainingCoin, Quaternion.identity, remainingCoinsParent);
            }

            coins.SetBalance(gameData.coinBalance);

            player.transform.position = gameData.playerPosition;
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(GetGameData());
            using StreamWriter writer = new StreamWriter(persistentPath);
            writer.Write(json);
            Debug.Log($"Saved data at {persistentPath}");
        }

        public void LoadData()
        {
            if (File.Exists(persistentPath))
            {
                using StreamReader reader = new StreamReader(persistentPath);
                string json = reader.ReadToEnd();
                SetGameData(JsonUtility.FromJson<GameData>(json));
                Debug.Log($"Loaded data from {persistentPath}");
            }
            else
            {
                Debug.Log("Could not load data: File does not exist!");
            }
        }

        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1.0f;
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
