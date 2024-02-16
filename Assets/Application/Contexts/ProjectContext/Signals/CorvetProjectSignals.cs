using Application.Contexts.ProjectContext.Configs;
using Application.GameCore;

namespace Application.Contexts.ProjectContext.Signals
{
    public class CorvetProjectSignals
    {
        public class ExpChangedSignal : ISignal
        {
            private int _exp;

            public ExpChangedSignal(int exp)
            {
                _exp = exp;
            }

            public int Exp => _exp;
        }

        public class LevelUpSignal : ISignal
        {
        }

        public class PlaySoundSignal : ISignal
        {
            private CorvetAudioClip.SoundType _soundType;

            public PlaySoundSignal(CorvetAudioClip.SoundType soundType)
            {
                _soundType = soundType;
            }

            public CorvetAudioClip.SoundType Type => _soundType;
        }
    }
}