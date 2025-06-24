
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private List<Image> _hearts;

        private EventBus _eventBus;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<HealthChangedSignal>(DisplayHealth);
            _eventBus.Subscribe<AllDataLoadedSignal>(OnAllDataLoaded);
        }

        private void OnAllDataLoaded(AllDataLoadedSignal signal)
        {
            var balloonDataLoader = ServiceLocator.Current.Get<IBalloonLoader>();
            var curBalloonData = balloonDataLoader.GetCurrentBalloonData();
            var sprite = curBalloonData.BalloonSprite;
            foreach (var heartImage in _hearts)
            {
                heartImage.sprite = sprite;
            }
        }

        private void DisplayHealth(HealthChangedSignal signal)
        {
            for (int i = 0; i < _hearts.Count; i++)
            {
                bool isHeartActive = i <= (signal.Health - 1);
                _hearts[i].gameObject.SetActive(isHeartActive);
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<HealthChangedSignal>(DisplayHealth);
        }
    }
}