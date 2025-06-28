using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UI.Windows.LeaderboardWindow;

public class LeaderboardSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Sprite _defaultAvatar;

    public void Init(LeaderboardPlayerData data, bool isMainPlayer)
    {
        _nameText.text = data.Name;
        _scoreText.text = data.Score.ToString();

        if (isMainPlayer)
        {
            string avatarPath = PlayerPrefs.GetString(StringConstants.ACCOUNT_AVATAR_PATH, "");
            if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
            {
                byte[] imageData = File.ReadAllBytes(avatarPath);
                Texture2D texture = new Texture2D(2, 2);
                if (texture.LoadImage(imageData))
                {
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    _avatarImage.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                }
                else
                {
                    _avatarImage.sprite = _defaultAvatar;
                }
            }
            else
            {
                _avatarImage.sprite = _defaultAvatar;
            }
        }
        else
        {
            _avatarImage.sprite = _defaultAvatar;
        }
    }
}
