using Application.Contexts.ProjectContext.Configs;

namespace Application.Contexts.ProjectContext.Signals
{
    public class CorvetProjectSignals
    {
        public class ExpChangedSignal
        {
            private int _exp;

            public ExpChangedSignal(int exp)
            {
                _exp = exp;
            }

            public int Exp => _exp;
        }
        
        public class PlaySoundSignal
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