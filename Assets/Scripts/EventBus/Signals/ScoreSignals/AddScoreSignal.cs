namespace CustomEventBus.Signals
{
    public class AddScoreSignal
    {
        public readonly int Value;

        public AddScoreSignal(int value)
        {
            Value = value;
        }
    }
}