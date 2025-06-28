using CustomEventBus;
using CustomEventBus.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ShopSlot : MonoBehaviour
    {
        [SerializeField] private Image _balloonImage;
        [SerializeField] private Button _selectBalloonButton;
        [SerializeField] private TextMeshProUGUI _slotPrice;

        [SerializeField] private Sprite _selectedButtonSprite;
        [SerializeField] private Sprite _purchesedButtonSprite;
        [SerializeField] private Sprite _defaultButtonSprite;

        private int _price;
        private bool _isPurchased;
        private int _balloonId;

        private EventBus _eventBus;

        private void Start()
        {
            _selectBalloonButton.onClick.AddListener(OnButtonClick);

            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<SelectBalloonSignal>(OnBalloonSelected);
            _eventBus.Subscribe<PurchaseBalloonSignal>(OnBalloonPurchased);
        }

        public void Init(int id, Sprite balloonSprite, int price, bool isPurchased)
        {
            _balloonId = id;
            _balloonImage.sprite = balloonSprite;
            _price = price;
            _isPurchased = isPurchased;

            Redraw();
        }

        private void Redraw()
        {
            int selectedBalloonId = PlayerPrefs.GetInt(StringConstants.SELECTED_BALLOON, 1);

            if (!_isPurchased)
            {
                _slotPrice.text = _price.ToString();
                _selectBalloonButton.image.sprite = _defaultButtonSprite;
            }
            else
            {
                if (selectedBalloonId == _balloonId)
                {
                    _selectBalloonButton.image.sprite = _selectedButtonSprite;
                    _slotPrice.text = "Selected";
                }
                else
                {
                    _selectBalloonButton.image.sprite = _purchesedButtonSprite;
                    _slotPrice.text = "Owned";
                }
            }
        }

        private void OnButtonClick()
        {
            if (!_isPurchased)
            {
                var coinController = ServiceLocator.Current.Get<CoinController>();
                var purchaseWindow = WindowManager.ShowWindow<PurchaseWindow>();
                purchaseWindow.Init(_balloonId, _balloonImage.sprite, _price);
            }
            else
            {
                SelectBalloon();
            }
        }

        private void SelectBalloon()
        {
            PlayerPrefs.SetInt(StringConstants.SELECTED_BALLOON, _balloonId);
            _eventBus.Invoke(new SelectBalloonSignal(_balloonId));
            Redraw();
        }

        private void OnBalloonSelected(SelectBalloonSignal signal)
        {
            Redraw();
        }

        private void OnBalloonPurchased(PurchaseBalloonSignal signal)
        {
            if (signal.BalloonId != _balloonId) return;

            _isPurchased = true;
            PlayerPrefs.SetInt(StringConstants.BALLOON_PURCHASED + _balloonId, 1);

            SelectBalloon();
        }

        private void OnDestroy()
        {
            _selectBalloonButton.onClick.RemoveListener(OnButtonClick);
            _eventBus.Unsubscribe<SelectBalloonSignal>(OnBalloonSelected);
            _eventBus.Unsubscribe<PurchaseBalloonSignal>(OnBalloonPurchased);
        }
    }
}
