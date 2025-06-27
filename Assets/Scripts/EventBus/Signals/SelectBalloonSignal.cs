namespace CustomEventBus.Signals
{
    public class SelectBalloonSignal
    {
        public readonly int BalloonId;

        public SelectBalloonSignal(int balloonId)
        {
            BalloonId = balloonId;
        }
    }
}