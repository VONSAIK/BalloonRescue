using System;
using UnityEngine;

[Serializable]
public struct BalloonData
{
    [SerializeField] private int _balloonId;
    [SerializeField] private int _purchasePrice;
    [SerializeField] private Sprite _balloonSprite;

    public int BalloonId => _balloonId;
    public int PurchasePrice => _purchasePrice;
    public Sprite BalloonSprite => _balloonSprite;

    public BalloonData(int balloonId, int purchasePrice, Sprite balloonSprite)
    {
        _balloonId = balloonId;
        _purchasePrice = purchasePrice;
        _balloonSprite = balloonSprite;
    }
}