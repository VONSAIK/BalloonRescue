namespace CustomEventBus.Signals
{
    public class CoinChangedSignal
    {
        public readonly int Coin;

        public CoinChangedSignal(int coin)
        {
            Coin = coin;
        }
    }
}