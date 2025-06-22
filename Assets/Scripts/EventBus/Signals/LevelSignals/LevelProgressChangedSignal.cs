namespace CustomEventBus.Signals
{
    public class LevelProgressChangedSignal
    {
        public readonly float Progress;
        public LevelProgressChangedSignal(float progress)
        {
            Progress = progress;
        }
    }
}