using System.Collections.Generic;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Configs
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Configs/SoundConfig", order = 1)]
    public class SoundConfig : ScriptableObject
    {
        [SerializeField] private List<CorvetAudioClip> _audioClips;

        public List<CorvetAudioClip> AudioClips => _audioClips;
    }
}