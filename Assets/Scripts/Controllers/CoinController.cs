using CustomEventBus;
using CustomEventBus.Signals;
using System;
using UnityEngine;

public class CoinController : IService
{
    private int _coin;
    public int Coin => _coin;

    private EventBus _eventBus;

    public void Init()
    {
        _coin = PlayerPrefs.GetInt(StringConstants.COIN, 0);

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

    public bool HaveEnoughGold(int coin)
    {
        return _coin >= coin;
    }

    private void SpendCoin(SpendCoinSignal signal)
    {
        if (HaveEnoughGold(signal.Value))
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
}