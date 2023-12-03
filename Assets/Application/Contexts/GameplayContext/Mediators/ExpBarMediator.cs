using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Signals;
using Application.Utils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class ExpBarMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly ExpModel _expModel;

        [SerializeField] private FillBar _fill;
        [SerializeField] private float _duration;
        [SerializeField] private TMP_Text _levelText;

        private int _tempExp;

        private void Start()
        {
            _signalBus.Subscribe<CorvetProjectSignals.ExpChangedSignal>(OnExpChangedSignal);
            _signalBus.Subscribe<CorvetProjectSignals.LevelUpSignal>(OnLevelUpSignal);
        }

        private void OnLevelUpSignal()
        {
            _levelText.text = $"{_expModel.Level}";
        }

        private void OnExpChangedSignal()
        {
            var toNextLevel = _gameConfig.LevelProgress[_expModel.Level] - _gameConfig.LevelProgress[_expModel.Level-1];
            var fillValue = (float) (_expModel.Exp - _gameConfig.LevelProgress[_expModel.Level - 1]) / toNextLevel;
            _fill.SetFill(fillValue, _duration, Color.green).OnComplete(() =>
            {
                if (fillValue == 1f)
                {
                    _fill.SetFill(0);
                }
            });
        }
    }
}
