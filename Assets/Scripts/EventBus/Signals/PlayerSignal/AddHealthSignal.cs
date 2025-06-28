namespace CustomEventBus.Signals
{
    public class AddHealthSignal
    {
        public readonly int Value;
        public AddHealthSignal(int value)
        {
            Value = value;
        }
    }
}