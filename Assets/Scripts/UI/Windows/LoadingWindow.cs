using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class LoadingWindow : Window
    {
        [SerializeField] private Image _progressLoading;

        private EventBus _eventBus;

        private void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<LoadProgressChangedSignal>(LoadProgressChanged);
            _eventBus.Subscribe<AllDataLoadedSignal>(OnAllResourcesLoaded);
        }

        private void LoadProgressChanged(LoadProgressChangedSignal signal)
        {
            RedrawProgress(signal.Progress);
        }

        private void OnAllResourcesLoaded(AllDataLoadedSignal signal)
        {
            Hide();
        }

        private void RedrawProgress(float progress)
        {
            _progressLoading.fillAmount = progress;
        }
    }
}