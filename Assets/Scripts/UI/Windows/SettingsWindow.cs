using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using CustomEventBus.Signals;

namespace UI.Windows
{
    public class SettingsWindow : Window
    {
        [SerializeField] private Image _musicFillImage;
        [SerializeField] private Button _musicPlusButton;
        [SerializeField] private Button _musicMinusButton;

        [SerializeField] private Image _soundFillImage;
        [SerializeField] private Button _soundPlusButton;
        [SerializeField] private Button _soundMinusButton;

        [SerializeField] private Button _goMenuButton;

        private const float Step = 0.1f;
        private const float MinVolume = 0f;
        private const float MaxVolume = 1f;

        private EventBus _eventBus;

        private void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();

            _musicPlusButton.onClick.AddListener(() => ChangeVolume(StringConstants.MUSIC_VOLUME, Step));
            _musicMinusButton.onClick.AddListener(() => ChangeVolume(StringConstants.MUSIC_VOLUME, -Step));
            _soundPlusButton.onClick.AddListener(() => ChangeVolume(StringConstants.SOUND_VOLUME, Step));
            _soundMinusButton.onClick.AddListener(() => ChangeVolume(StringConstants.SOUND_VOLUME, -Step));

            _goMenuButton.onClick.AddListener(OnGoMenuButtonClick);

            RedrawUI();
        }

        private void ChangeVolume(string key, float delta)
        {
            float volume = PlayerPrefs.GetFloat(key, 1f);
            volume = Mathf.Clamp(volume + delta, MinVolume, MaxVolume);
            PlayerPrefs.SetFloat(key, volume);
            PlayerPrefs.Save();

            _eventBus.Invoke(new VolumeChangedSignal());

            RedrawUI();
        }

        private void RedrawUI()
        {
            float musicVolume = PlayerPrefs.GetFloat(StringConstants.MUSIC_VOLUME, 1f);
            float soundVolume = PlayerPrefs.GetFloat(StringConstants.SOUND_VOLUME, 1f);

            _musicFillImage.fillAmount = musicVolume;
            _soundFillImage.fillAmount = soundVolume;
        }

        private void OnGoMenuButtonClick()
        {
            WindowManager.ShowWindow<MenuWindow>();
            Hide();
        }

        private void OnDestroy()
        {
            _musicPlusButton.onClick.RemoveAllListeners();
            _musicMinusButton.onClick.RemoveAllListeners();
            _soundPlusButton.onClick.RemoveAllListeners();
            _soundMinusButton.onClick.RemoveAllListeners();
        }
    }
}
