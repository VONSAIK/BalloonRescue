using System;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class SoundController : MonoBehaviour, IService
{
    [SerializeField] private AudioSource _soundAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    [SerializeField] private AudioClip _menuMusic;

    private EventBus _eventBus;

    private AudioClip _levelMusic;
    private Dictionary<string, AudioClip> _interactableSounds = new Dictionary<string, AudioClip>();
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<SetLevelSignal>(SetLevel);
        _eventBus.Subscribe<InteractablePlaySoundSignal>(OnPlaySound);
        _eventBus.Subscribe<LevelPlayMusicSignal>(OnPlayMusic);
        _eventBus.Subscribe<MenuPlayMusicSignal>(OnPlayMusic);
        _eventBus.Subscribe<VolumeChangedSignal>(OnChangedVolume);

        _soundAudioSource.volume = PlayerPrefs.GetFloat(StringConstants.SOUND_VOLUME, 1f);
        _musicAudioSource.volume = PlayerPrefs.GetFloat(StringConstants.MUSIC_VOLUME, 1f);
    }

    private void SetLevel(SetLevelSignal signal)
    {
        foreach (var interactableData in signal.LevelData.InteractableData)
        {
            if (interactableData.InteractSound != null)
            {
                var objectTypeStr = interactableData.Prefab.GetType().ToString();
                if (!_interactableSounds.ContainsKey(objectTypeStr))
                {
                    _interactableSounds.Add(objectTypeStr, interactableData.InteractSound);
                }
            }
        }

        _levelMusic = signal.LevelData.LevelMusic;
    }

    private void OnPlaySound(InteractablePlaySoundSignal signal)
    {
        var objectTypeStr = signal.Interactable.GetType().ToString();

        if (_interactableSounds.TryGetValue(objectTypeStr, out var clip) && clip != null)
        {
            _soundAudioSource.PlayOneShot(clip);
        }
    }

    private void OnPlayMusic(LevelPlayMusicSignal signal)
    {
        _musicAudioSource.clip = _levelMusic;
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    private void OnPlayMusic(MenuPlayMusicSignal signal)
    {
        _musicAudioSource.clip = _menuMusic;
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    private void OnChangedVolume(VolumeChangedSignal signal)
    {
        _soundAudioSource.volume = PlayerPrefs.GetFloat(StringConstants.SOUND_VOLUME, 1f);
        _musicAudioSource.volume = PlayerPrefs.GetFloat(StringConstants.MUSIC_VOLUME, 1f);
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<SetLevelSignal>(SetLevel);
        _eventBus.Unsubscribe<InteractablePlaySoundSignal>(OnPlaySound);
        _eventBus.Unsubscribe<VolumeChangedSignal>(OnChangedVolume);
    }
}
