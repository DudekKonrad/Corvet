using System;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Configs
{
    [Serializable]
    public class CorvetAudioClip
    {
        public enum SoundType
        {
            OnButtonClick = 0,
            OnButtonSelect = 1
        }

        [SerializeField] private SoundType _soundType;
        [SerializeField] private AudioClip _audioClip;

        public SoundType Type => _soundType;
        public AudioClip Clip => _audioClip;
    }
}