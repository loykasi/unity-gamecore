namespace GameCore.SoundSystem
{
    [System.Serializable]
    public struct SoundSettings
    {
        public bool IsRandomPitch;
        public float PitchMin;
        public float PitchMax;

        public SoundSettings(bool isRandomPitch, float pitchMin, float pitchMax)
        {
            IsRandomPitch = isRandomPitch;
            PitchMin = pitchMin;
            PitchMax = pitchMax;
        }
    }
}