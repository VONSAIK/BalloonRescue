using CustomEventBus;
using CustomEventBus.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    public class YouLoseWindow : Window
    {
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [SerializeField] private TextMeshProUGUI _rewardText;

        private EventBus _eventBus;

        private void Start()
        {
            _tryAgainButton.onClick.AddListener(TryAgain);
            _goToMenuButton.onClick.AddListener(GoToMenu);

            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void Init(int currentScore, int addCoinValue)
        {
            _currentScoreText.text = currentScore.ToString();
            _rewardText.text = addCoinValue.ToString();
        }

        private void TryAgain()
        {
            _eventBus.Invoke(new RestartLevelSignal());
            Hide();
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene(StringConstants.MENU_SCENE);
        }
    }
}
