using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Application.Contexts.ProjectContext.Mediators
{
    [RequireComponent(typeof(Button))]
    public class ButtonSoundMediator : MonoBehaviour, ISelectHandler
    {
        [Inject] private readonly SignalBus _signalBus;
        
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PlayUISound);
        }

        private void PlayUISound()
        {
            _signalBus.Fire(new CorvetProjectSignals.PlaySoundSignal(CorvetAudioClip.SoundType.OnButtonClick));
        }

        public void OnSelect(BaseEventData eventData)
        {
            _signalBus.Fire(new CorvetProjectSignals.PlaySoundSignal(CorvetAudioClip.SoundType.OnButtonSelect));
        }
    }
}
