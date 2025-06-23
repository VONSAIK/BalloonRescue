namespace CustomEventBus.Signals
{
    public class HealthChangedSignal
    {
        public readonly int Health;

        public HealthChangedSignal(int health)
        {
            Health = health;
        }
    }
}