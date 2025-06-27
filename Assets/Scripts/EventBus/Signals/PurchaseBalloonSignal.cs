namespace CustomEventBus.Signals
{
    public class PurchaseBalloonSignal
    {
        public readonly int BalloonId;
        public PurchaseBalloonSignal(int balloonId)
        {
            BalloonId = balloonId;
        }
    }
}