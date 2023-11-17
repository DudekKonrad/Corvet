using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Application.Contexts.MainMenuContext
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Image _loadingFill;
        [SerializeField] private float _duration = 0.4f;

        public void LoadScene(string sceneName)
        {
            _loadingScreen.SetActive(true);
            _loadingScreen.transform.DOLocalMoveY(0f, _duration).OnComplete(() => 
                StartCoroutine(LoadSceneAsync(sceneName)));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);
                _loadingFill.fillAmount = progress;
                yield return null;
            }
        }
    }
}
