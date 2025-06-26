using Cysharp.Threading.Tasks;
using CustomEventBus;
using UnityEngine;
using UnityEngine.UI;
using CustomEventBus.Signals;

namespace UI.Windows
{
    public class LoadingWindow : Window
    {
        [SerializeField] private Image _progressLoading;
        [SerializeField] private float _loadSpeed = 0.3f;

        private EventBus _eventBus;

        private void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();

            StartLoading().Forget();
        }

        private async UniTaskVoid StartLoading()
        {
            float progress = 0f;

            while (progress < 1f)
            {
                progress += Time.deltaTime * _loadSpeed;
                _progressLoading.fillAmount = progress;

                await UniTask.Yield(); 
            }

            await UniTask.Delay(500);

            _eventBus.Invoke(new MenuPlayMusicSignal());
            Hide();

            WindowManager.ShowWindow<MenuWindow>();
        }
    }
}
