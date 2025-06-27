using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    public class SelectLevelSlot : MonoBehaviour
    {
        [SerializeField] private Button _levelClickedButton;

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _levelLockedSprite;
        [SerializeField] private List<Image> _stars = new List<Image>();
        [SerializeField] private Sprite _filledStarSprite;
        [SerializeField] private Sprite _emptyStarSprite;

        public void Init(LevelData levelData)
        {
            int levelId = levelData.LevelId;

            bool isUnlocked = IsLevelUnlocked(levelId);
            bool isCompleted = IsLevelCompleted(levelId);
            int earnedStars = GetEarnedStars(levelId);

            if (!isUnlocked)
            {
                _levelText.gameObject.SetActive(false);
                SetStarsVisible(false);
                _levelLockedSprite.gameObject.SetActive(true);
                _levelClickedButton.enabled = false;
                return;
            }

            _levelLockedSprite.gameObject.SetActive(false);
            _levelText.text = levelId.ToString();
            _levelText.gameObject.SetActive(true);
            _levelClickedButton.enabled = true;

            if (isCompleted)
            {
                SetStarsVisible(true);
                DisplayEarnedStars(earnedStars);
            }
            else
            {
                SetStarsVisible(false);
            }

            _levelClickedButton.onClick.AddListener(() =>
            {

                PlayerPrefs.SetInt(StringConstants.CURRENT_LEVEL, levelId);
                SceneManager.LoadScene(StringConstants.GAME_SCENE);
            });
        }

        private bool IsLevelUnlocked(int levelId)
        {
            if (levelId == 1) return true;

            int prevLevelStars = PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_STARS + (levelId - 1), 0);
            return prevLevelStars > 0;
        }

        private bool IsLevelCompleted(int levelId)
        {
            int stars = GetEarnedStars(levelId);
            return stars > 0;
        }

        private int GetEarnedStars(int levelId)
        {
            return PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_STARS + levelId, 0);
        }

        private void SetStarsVisible(bool visible)
        {
            foreach (var star in _stars)
                star.gameObject.SetActive(visible);
        }

        private void DisplayEarnedStars(int count)
        {
            for (int i = 0; i < _stars.Count; i++)
            {
                _stars[i].sprite = i < count ? _filledStarSprite : _emptyStarSprite;
            }
        }

        private void OnDestroy()
        {
            _levelClickedButton.onClick.RemoveAllListeners();
        }
    }
}
