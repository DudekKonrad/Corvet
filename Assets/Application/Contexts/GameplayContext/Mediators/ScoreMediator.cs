using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class ScoreMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly ScoreModel _scoreModel;
        
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _signalBus.Subscribe<CorvetProjectSignals.ScoreChangedSignal>(OnScoreChangedSignal);
            _text.text = $"{_scoreModel.Score}";
        }

        private void OnScoreChangedSignal()
        {
            _text.text = $"{_scoreModel.Score}";
        }
    }
}