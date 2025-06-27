using System;
using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ShopWindow : Window
    {
        [SerializeField] private GridLayoutGroup _elementsGrid;
        [SerializeField] private ShopSlot _shopSlotPrefab;
        [SerializeField] private Button _goMenuButton;
        [SerializeField] private TextMeshProUGUI _coinValue;

        [SerializeField] private Button _leftArrowButton;
        [SerializeField] private Button _rightArrowButton;

        [SerializeField] private RectTransform _scrollContent;

        private EventBus _eventBus;

        private int _totalSlots;
        private int _slotsPerPage = 4;
        private int _totalPages;
        private int _currentPage = 0;

        private float _pageWidth;

        private void Start()
        {
            InitShopSlots();

            _goMenuButton.onClick.AddListener(OnGoMenuButtonClick);
            _leftArrowButton.onClick.AddListener(ScrollLeft);
            _rightArrowButton.onClick.AddListener(ScrollRight);

            var coin = ServiceLocator.Current.Get<CoinController>().Coin;
            _coinValue.text = coin.ToString();

            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<CoinChangedSignal>(RedrawCoin);
        }

        private void InitShopSlots()
        {
            var balloonDataLoader = ServiceLocator.Current.Get<IBalloonLoader>();
            var balloonsData = balloonDataLoader.GetBalloonsData();
            balloonsData = balloonsData.OrderBy(x => x.BalloonId);

            foreach (var balloonData in balloonsData)
            {
                var balloonSlot = Instantiate(_shopSlotPrefab, _elementsGrid.transform);
                bool purchased = PlayerPrefs.GetInt(StringConstants.BALLOON_PURCHASED + balloonData.BalloonId, 0) == 1 || balloonData.PurchasePrice == 0;
                balloonSlot.Init(balloonData.BalloonId, balloonData.BalloonSprite, balloonData.PurchasePrice, purchased);
            }

            _totalSlots = balloonsData.Count();
            _totalPages = Mathf.CeilToInt(_totalSlots / (float)_slotsPerPage);
            _pageWidth = _elementsGrid.cellSize.x * 2 + _elementsGrid.spacing.x;
        }

        private void RedrawCoin(CoinChangedSignal signal)
        {
            _coinValue.text = signal.Coin.ToString();
        }

        private void ScrollLeft()
        {
            if (_currentPage <= 0)
                return;

            _currentPage--;
            UpdateScrollPosition();
        }

        private void ScrollRight()
        {
            if (_currentPage >= _totalPages - 1)
                return;

            _currentPage++;
            UpdateScrollPosition();
        }

        private void UpdateScrollPosition()
        {
            Vector2 pos = _scrollContent.anchoredPosition;
            pos.x = -_currentPage * _pageWidth;
            _scrollContent.anchoredPosition = pos;
        }

        private void OnGoMenuButtonClick()
        {
            WindowManager.ShowWindow<MenuWindow>();
            Hide();
        }

        private void OnDestroy()
        {
            _goMenuButton.onClick.RemoveAllListeners();
            _leftArrowButton.onClick.RemoveAllListeners();
            _rightArrowButton.onClick.RemoveAllListeners();
            _eventBus.Unsubscribe<CoinChangedSignal>(RedrawCoin);
        }
    }
}
