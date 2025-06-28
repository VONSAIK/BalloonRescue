using CustomEventBus;
using CustomEventBus.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    public class YouWinWindow : Window
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [SerializeField] private TextMeshProUGUI _rewardText;

        private EventBus _eventBus;

        void Start()
        {
            _nextLevelButton.onClick.AddListener(NextLevel);
            _goToMenuButton.onClick.AddListener(GoToMenu);
            _leaderboardButton.onClick.AddListener(Leaderboard);

            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void Init(int currentScore, int addCoinValue)
        {
            _currentScoreText.text = currentScore.ToString();
            _rewardText.text = addCoinValue.ToString();
        }

        private void NextLevel()
        {
            _eventBus.Invoke(new NextLevelSignal());
            Hide();
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene(StringConstants.MENU_SCENE);
            Hide();
        }

        private void Leaderboard()
        {
            WindowManager.ShowWindow<LeaderboardWindow>();
            Hide();
        }

        private void OnDestroy()
        {
            _nextLevelButton.onClick.RemoveAllListeners();
            _goToMenuButton.onClick.RemoveAllListeners();
            _leaderboardButton.onClick.RemoveAllListeners();
        }
    }
}
