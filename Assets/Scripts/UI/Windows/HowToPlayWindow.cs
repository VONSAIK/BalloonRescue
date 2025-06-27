using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class HowToPlayWindow : Window
    {
        [SerializeField] private Button _goMenuButton;
        [SerializeField] private Button _goLevelsButton;


        private void Start()
        {
            _goMenuButton.onClick.AddListener(OnGoMenuButtonClick);
            _goLevelsButton.onClick.AddListener(OnGoLevelsButtonClick);
        }

        private void OnGoLevelsButtonClick()
        {
            Hide();
        }

        private void OnGoMenuButtonClick()
        {
            WindowManager.ShowWindow<MenuWindow>();
            Hide();
        }

        private void OnDestroy()
        {
            _goMenuButton.onClick.RemoveAllListeners();
            _goLevelsButton.onClick.RemoveAllListeners();
        }
    }
}
