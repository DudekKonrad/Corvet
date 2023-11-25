using Application.Contexts.ProjectContext.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Application.Contexts.MainMenuContext
{
    public class UIPanel : MonoBehaviour
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
    
        [SerializeField] private string _panelName;
        [SerializeField] private Ease _ease;

        public string PanelName => _panelName;

        public void Show()
        {
            transform.DOLocalMoveY(0f, _gameConfig.PanelDuration).SetEase(_ease);
        }

        public void Hide(){
            transform.DOLocalMoveY(1080f, _gameConfig.PanelDuration).SetEase(_ease);
        }
    }
}
