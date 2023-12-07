using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadingScreenMediator : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;

    private void Start()
    {
        /*public void LoadScene(string sceneName)
        {
            _loadingScreen.SetActive(true);
            _loadingScreen.transform.DOLocalMoveY(0f, _duration).OnComplete(() => 
                StartCoroutine(LoadSceneAsync(sceneName)));
        }

        private IEnumerator<> LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);
                _loadingFill.fillAmount = progress;
                yield return null;
            }
        }*/
    }
}
