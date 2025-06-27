using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using CustomEventBus.Signals;
using TMPro;

namespace UI.Windows
{
    public class PurchaseWindow : Window
    {
        [SerializeField] private Image _balloonImage;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Sprite _canNotBuyButtonSprite;
        [SerializeField] private Sprite _defaultBuyButtonSprite;

        private EventBus _eventBus;
        private int _price;
        private int _balloonId;

        private bool _canBuy;

        private void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();

            _cancelButton.onClick.AddListener(Hide);
            _buyButton.onClick.AddListener(OnBuyButtonClick);
        }

        public void Init(int balloonId, Sprite balloonSprite, int price)
        {
            _balloonId = balloonId;
            _balloonImage.sprite = balloonSprite;
            _price = price;

            var coinController = ServiceLocator.Current.Get<CoinController>();

            _canBuy = coinController.HaveEnoughCoin(_price);
            
            _buyButton.enabled = _canBuy;
            _buyButton.image.sprite = _canBuy ? _defaultBuyButtonSprite : _canNotBuyButtonSprite;
        }

        private void OnBuyButtonClick()
        {
            if (!_canBuy)
                return;

            _eventBus.Invoke(new SpendCoinSignal(_price));
            _eventBus.Invoke(new PurchaseBalloonSignal(_balloonId));
            Hide();
        }

        private void OnDestroy()
        {
            _cancelButton.onClick.RemoveAllListeners();
            _buyButton.onClick.RemoveAllListeners();
        }
    }
}
