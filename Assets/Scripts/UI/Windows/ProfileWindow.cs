using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using TMPro;


namespace UI.Windows
{
    public class ProfileWindow : Window
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private Image _avatarImage;
        [SerializeField] private Sprite _defaultAvatar;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _selectAvatarButton;
        [SerializeField] private Button _goMenuButton;

        private string _avatarPath = "";

        private void Start()
        {
            LoadProfile();

            _saveButton.onClick.AddListener(OnSaveProfileButtonClick);
            _selectAvatarButton.onClick.AddListener(OnSelectAvatarButtonClick);
            _goMenuButton.onClick.AddListener(OnGoMenuButtonClick);
        }

        private void LoadProfile()
        {
            _nameText.text = PlayerPrefs.GetString(StringConstants.ACCOUNT_NAME, "Player");
            _avatarPath = PlayerPrefs.GetString(StringConstants.ACCOUNT_AVATAR_PATH, "");

            if (!string.IsNullOrEmpty(_avatarPath) && File.Exists(_avatarPath))
            {
                LoadAvatarFromPath(_avatarPath);
            }
            else
            {
                _avatarImage.sprite = _defaultAvatar;
            }
        }

        private void OnSaveProfileButtonClick()
        {
            PlayerPrefs.SetString(StringConstants.ACCOUNT_NAME, _nameInput.text);
            PlayerPrefs.SetString(StringConstants.ACCOUNT_AVATAR_PATH, _avatarPath);
            LoadProfile();
        }

        private void OnSelectAvatarButtonClick()
        {
#if UNITY_ANDROID || UNITY_IOS
            NativeGallery.GetImageFromGallery((path) =>
            {
                if (path != null)
                {
                    _avatarPath = path;
                    LoadAvatarFromPath(_avatarPath);
                }
            }, "Choose an avatar", "image/*");

#elif UNITY_EDITOR
            string path = EditorUtility.OpenFilePanel("Choose an avatar", "", "png,jpg,jpeg");
            if (!string.IsNullOrEmpty(path))
            {
                _avatarPath = path;
                LoadAvatarFromPath(_avatarPath);
            }
#endif
        }

        private void LoadAvatarFromPath(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogWarning("Avatar file not found at path: " + path);
                return;
            }

            byte[] imageData = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageData))
            {
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                _avatarImage.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogError("Unable to load image from file: " + path);
            }
        }

        private void OnGoMenuButtonClick()
        {
            WindowManager.ShowWindow<MenuWindow>();
            Hide();
        }
    }
}
