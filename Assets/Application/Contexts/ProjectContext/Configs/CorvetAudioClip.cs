using System;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Configs
{
    [Serializable]
    public class CorvetAudioClip
    {
        public enum SoundType
        {
            OnClick = 0
        }

        [SerializeField] private SoundType _soundType;
        [SerializeField] private AudioClip _audioClip;

        public SoundType Type => _soundType;
        public AudioClip Clip => _audioClip;
    }
}