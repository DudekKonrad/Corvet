using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ButtonSoundMediator : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;

    [SerializeField] private CorvetAudioClip.SoundType _soundType;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayUISound);
    }

    private void PlayUISound()
    {
        _signalBus.Fire(new CorvetProjectSignals.PlaySoundSignal(_soundType));
    }
}
