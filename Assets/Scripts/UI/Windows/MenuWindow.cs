using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    public class MenuWindow : Window
    {
        [SerializeField] private Button _profileButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _startButton;

        protected void Awake()
        {
            _profileButton.onClick.AddListener(OnProfileButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _shopButton.onClick.AddListener(OnShopButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _startButton.onClick.AddListener(OnStartButtonClick);
        }
        private void OnProfileButtonClick()
        {
            WindowManager.ShowWindow<ProfileWindow>();
            Hide();
        }
        private void OnSettingsButtonClick()
        {
            WindowManager.ShowWindow<SettingsWindow>();
            Hide();
        }
        private void OnShopButtonClick()
        {
            WindowManager.ShowWindow<ShopWindow>();
            Hide();
        }
        private void OnExitButtonClick()
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        private void OnStartButtonClick()
        {
            WindowManager.ShowWindow<HowToPlayWindow>();
        }
    }
}
