using CustomEventBus;
using CustomEventBus.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _levelProgressBar;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _exitButton;

        private EventBus _eventBus;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<SetLevelSignal>(RedrawLevel);
            _eventBus.Subscribe<ScoreChangedSignal>(RedrawScore);
            _eventBus.Subscribe<LevelProgressChangedSignal>(RedrawLevelProgress);

            _exitButton.onClick.AddListener(GoToMenu);
        }

        private void RedrawLevel(SetLevelSignal signal)
        {
            _levelText.text = "Level: " + (signal.LevelData.LevelId + 1).ToString();
        }

        private void RedrawScore(ScoreChangedSignal signal)
        {
            _scoreText.text = "Score: " + signal.Score;
        }

        private void RedrawLevelProgress(LevelProgressChangedSignal signal)
        {
            _levelProgressBar.fillAmount = signal.Progress;
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene(StringConstants.MENU_SCENE);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<SetLevelSignal>(RedrawLevel);
            _eventBus.Unsubscribe<ScoreChangedSignal>(RedrawScore);

            _eventBus.Unsubscribe<LevelProgressChangedSignal>(RedrawLevelProgress);
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}
