using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Fsm;
using Application.Contexts.ProjectContext.Signals;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Application.Contexts.MainMenuContext
{
    public class MainMenuManager : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly GameFsm _gameFsm;
        
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Image _loadingFill;
        [SerializeField] private float _duration = 0.4f;
        [SerializeField] private List<UIPanel> _panels;

        private string _activePanel;

        private void Awake()
        {
            DontDestroyOnLoad(_loadingScreen);
        }

        [UsedImplicitly]
        public void OnBack(InputValue value)
        {
            _signalBus.Fire(new CorvetProjectSignals.PlaySoundSignal(CorvetAudioClip.SoundType.OnButtonBack));
            HidePanel(_activePanel);
        }

        public void ShowPanel(string panelName)
        {
            _panels.First(_ => _.PanelName == panelName).Show();
            _activePanel = panelName;
        }
        
        public void HidePanel(string panelName)
        {
            _panels.First(_ => _.PanelName == panelName).Hide();
        }
        
        public void LoadScene(string sceneName)
        {
            _loadingScreen.SetActive(true);
            _loadingScreen.transform.DOLocalMoveY(0f, _duration).OnComplete(() => 
                StartCoroutine(LoadSceneAsync(sceneName)));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            _gameFsm.TransitionToState(GameState.Gameplay);
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
