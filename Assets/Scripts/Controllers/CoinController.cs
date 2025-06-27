using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class CoinController : IService, IDisposable
{
    private int _coin;
    public int Coin => _coin;

    private EventBus _eventBus;

    public void Init()
    {
        _coin = PlayerPrefs.GetInt(StringConstants.COIN, 1000);

        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<AddCoinSignal>(OnAddCoin);
        _eventBus.Subscribe<SpendCoinSignal>(SpendCoin);
        _eventBus.Subscribe<CoinChangedSignal>(CoinChanged);
        _eventBus.Subscribe<LevelFinishedSignal>(LevelFinished);
    }

    private void OnAddCoin(AddCoinSignal signal)
    {
        OnAddCoin(signal.Value);
    }

    private void OnAddCoin(int value)
    {
        _coin += value;
        _eventBus.Invoke(new CoinChangedSignal(_coin));
    }

    public bool HaveEnoughCoin(int coin)
    {
        return _coin >= coin;
    }

    private void SpendCoin(SpendCoinSignal signal)
    {
        if (HaveEnoughCoin(signal.Value))
        {
            _coin -= signal.Value;
            _eventBus.Invoke(new CoinChangedSignal(_coin));
        }
    }

    private void CoinChanged(CoinChangedSignal signal)
    {
        PlayerPrefs.SetInt(StringConstants.COIN, signal.Coin);
    }

    private void LevelFinished(LevelFinishedSignal signal)
    {
        OnAddCoin(signal.LevelData.CoinForPass);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<AddCoinSignal>(OnAddCoin);
        _eventBus.Unsubscribe<SpendCoinSignal>(SpendCoin);
        _eventBus.Unsubscribe<CoinChangedSignal>(CoinChanged);
        _eventBus.Unsubscribe<LevelFinishedSignal>(LevelFinished);
    }
}