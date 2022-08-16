using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SAE_Portfolio_2.UI
{
    public sealed class LoadingPanel : Panel
    {
        [SerializeField] private UnityEngine.UI.Slider loadingBar;
        private bool loading = false;

        public void LoadScene(string sceneIndex)
        {
            if (!loading)
            {
                loading = true;
                SetVisibility(true);
                StartCoroutine(LoadAsynchronously(sceneIndex));
            }
        }

        private IEnumerator LoadAsynchronously(string sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingBar.value = progress;
                yield return null;
            }
        }
    } 
}
