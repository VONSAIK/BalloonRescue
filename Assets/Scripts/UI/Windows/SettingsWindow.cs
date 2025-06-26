using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using CustomEventBus.Signals;

namespace UI.Windows
{
    public class SettingsWindow : Window
    {
        [SerializeField] private Button _goMenuButton;
        [SerializeField] private Button _saveButton;

        [SerializeField] private Image _soundFillImage;
        [SerializeField] private Button _soundPlusButton;
        [SerializeField] private Button _soundMinusButton;

        [SerializeField] private Image _musicFillImage;
        [SerializeField] private Button _musicPlusButton;
        [SerializeField] private Button _musicMinusButton;

        private const float Step = 0.1f;
        private const float MinVolume = 0f;
        private const float MaxVolume = 1f;

        private float _soundVolume;
        private float _musicVolume;

        private EventBus _eventBus;

        private void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();

            _soundVolume = PlayerPrefs.GetFloat(StringConstants.SOUND_VOLUME, 1f);
            _musicVolume = PlayerPrefs.GetFloat(StringConstants.MUSIC_VOLUME, 1f);

            _soundPlusButton.onClick.AddListener(() => ChangeVolume(ref _soundVolume, Step));
            _soundMinusButton.onClick.AddListener(() => ChangeVolume(ref _soundVolume, -Step));
            _musicPlusButton.onClick.AddListener(() => ChangeVolume(ref _musicVolume, Step));
            _musicMinusButton.onClick.AddListener(() => ChangeVolume(ref _musicVolume, -Step));

            _goMenuButton.onClick.AddListener(OnGoMenuButtonClick);
            _saveButton.onClick.AddListener(OnSaveSettingsButtonClick);

            RedrawUI();
        }

        private void ChangeVolume(ref float volume, float delta)
        {
            volume = Mathf.Clamp(volume + delta, MinVolume, MaxVolume);
            RedrawUI();
        }

        private void RedrawUI()
        {
            _soundFillImage.fillAmount = _soundVolume;
            _musicFillImage.fillAmount = _musicVolume;
        }

        private void OnSaveSettingsButtonClick()
        {
            PlayerPrefs.SetFloat(StringConstants.SOUND_VOLUME, _soundVolume);
            PlayerPrefs.SetFloat(StringConstants.MUSIC_VOLUME, _musicVolume);
            _eventBus.Invoke(new VolumeChangedSignal());
        }

        private void OnGoMenuButtonClick()
        {
            WindowManager.ShowWindow<MenuWindow>();
            Hide();
        }

        private void OnDestroy()
        {
            _goMenuButton.onClick.RemoveAllListeners();
            _saveButton.onClick.RemoveAllListeners();

            _musicPlusButton.onClick.RemoveAllListeners();
            _musicMinusButton.onClick.RemoveAllListeners();
            _soundPlusButton.onClick.RemoveAllListeners();
            _soundMinusButton.onClick.RemoveAllListeners();
        }
    }
}
