using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class SelectLevelWindow : Window
    {
        [SerializeField] private Button _goMenuButton;

        [SerializeField] private GridLayoutGroup _levelsGrid;
        [SerializeField] private SelectLevelSlot _levelSlotPrefab;

        private ILevelLoader _levelLoader;

        private void Start()
        {
            _goMenuButton.onClick.AddListener(OnGoMenuButtonClick);

            _levelLoader = ServiceLocator.Current.Get<ILevelLoader>();
            var levels = _levelLoader.GetLevels();
            levels = levels.OrderBy(x => x.LevelId);
            GenerateLevels(levels);
        }

        private void GenerateLevels(IEnumerable<LevelData> levels)
        {
            foreach (var level in levels)
            {
                var go = GameObject.Instantiate(_levelSlotPrefab, _levelsGrid.transform);
                go.Init(level);
            }
        }

        private void OnGoMenuButtonClick()
        {
            WindowManager.ShowWindow<MenuWindow>();
            Hide();
        }
    }
}