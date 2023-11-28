using System.Collections.Generic;
using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Signals;
using UnityEngine;
using Zenject;

namespace Application.Contexts.ProjectContext
{
    public class SoundService
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly SoundConfig _soundConfig;

        private Dictionary<CorvetAudioClip.SoundType, AudioClip> _dictionary;

        private AudioSource _audioSource;
        
        [Inject]
        private void Construct()
        {
            _dictionary = new Dictionary<CorvetAudioClip.SoundType, AudioClip>();
            _audioSource = Object.Instantiate(new GameObject()).AddComponent<AudioSource>();
            _audioSource.gameObject.name = "AudioSource";
            _signalBus.Subscribe<CorvetProjectSignals.PlaySoundSignal>(OnPlaySoundSignal);
            foreach (var corvetAudioClip in _soundConfig.AudioClips)
            {
                if (!_dictionary.ContainsKey(corvetAudioClip.Type))
                {
                    _dictionary[corvetAudioClip.Type] = corvetAudioClip.Clip;
                }
            }
        }

        private void OnPlaySoundSignal(CorvetProjectSignals.PlaySoundSignal signal)
        {
            Debug.Log($"Will play sound: {signal.Type}");
            var clip = _dictionary[signal.Type];
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}