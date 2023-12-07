using System.Collections.Generic;
using System.Linq;
using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Signals;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Application.Contexts.MainMenuContext
{
    public class MainMenuManager : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        
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
    }
}
