using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

namespace UI.Windows
{
    public class LeaderboardWindow : Window
    {
        [SerializeField] private Button _goMenuButton;

        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TextMeshProUGUI _playerScoreText;
        [SerializeField] private List<Image> _stars;
        [SerializeField] private Image _playerAvatar;

        [SerializeField] private LeaderboardSlot _leaderboardSlotPrefab;
        [SerializeField] private RectTransform _listContainer;

        private void Start()
        {
            _goMenuButton.onClick.AddListener(OnGoMenuButtonClick);

            LoadPlayerData();
            LoadLeaderboard();
        }

        private void LoadPlayerData()
        {
            string playerName = PlayerPrefs.GetString(StringConstants.ACCOUNT_NAME, "Player");
            float playerScore = ServiceLocator.Current.Get<ScoreController>().Score;

            _playerNameText.text = playerName;
            _playerScoreText.text = playerScore.ToString();

            int stars = ServiceLocator.Current.Get<StarController>().EarnedStars;
            for (int i = 0; i < _stars.Count; i++)
            {
                _stars[i].enabled = i < stars;
            }

            string avatarPath = PlayerPrefs.GetString(StringConstants.ACCOUNT_AVATAR_PATH, "");
            if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
            {
                LoadAvatarFromPath(avatarPath);
            }
        }

        private void LoadLeaderboard()
        {
            int levelId = PlayerPrefs.GetInt(StringConstants.CURRENT_LEVEL, 1);
            string playerName = PlayerPrefs.GetString(StringConstants.ACCOUNT_NAME, "Player");
            int maxScore = PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_SCORE + levelId, 0);

            List<LeaderboardPlayerData> leaderboard = new List<LeaderboardPlayerData>();

            leaderboard.Add(new LeaderboardPlayerData
            {
                Name = playerName,
                Score = maxScore
            });

            for (int i = 1; i <= 4; i++)
            {
                string nameKey = $"Leaderboard_Name_{i}_{levelId}";
                string scoreKey = $"Leaderboard_Score_{i}_{levelId}";

                if (PlayerPrefs.HasKey(nameKey) && PlayerPrefs.HasKey(scoreKey))
                {
                    string name = PlayerPrefs.GetString(nameKey);
                    int score = PlayerPrefs.GetInt(scoreKey);
                    leaderboard.Add(new LeaderboardPlayerData { Name = name, Score = score });
                }
                else
                {
                    string generatedName = $"Player {i + 1}";
                    int generatedScore = Random.Range(100, 200) + levelId * 50;

                    PlayerPrefs.SetString(nameKey, generatedName);
                    PlayerPrefs.SetInt(scoreKey, generatedScore);

                    leaderboard.Add(new LeaderboardPlayerData { Name = generatedName, Score = generatedScore });
                }
            }

            leaderboard.Sort((a, b) => b.Score.CompareTo(a.Score));

            foreach (Transform child in _listContainer)
                Destroy(child.gameObject);

            foreach (var entry in leaderboard)
            {
                var go = Instantiate(_leaderboardSlotPrefab, _listContainer);
                var slot = go.GetComponent<LeaderboardSlot>();
                bool isMain = entry.Name == playerName && Mathf.Approximately(entry.Score, maxScore);
                slot.Init(entry, isMain);
            }
        }


        private void LoadAvatarFromPath(string path)
        {
            byte[] imageData = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageData))
            {
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                _playerAvatar.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogError("LeaderboardWindow: Failed to load avatar image.");
            }
        }

        private void OnGoMenuButtonClick()
        {
            SceneManager.LoadScene(StringConstants.MENU_SCENE);
            Hide();
        }

        private void OnDestroy()
        {
            _goMenuButton.onClick.RemoveAllListeners();
        }

        public class LeaderboardPlayerData
        {
            public string Name;
            public int Score;
        }
    }
}
