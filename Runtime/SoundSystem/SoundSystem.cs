using System.Collections;
using System.Collections.Generic;
using GameCore;
using GameCore.ObjectPool;
using UnityEngine;

namespace GameCore.SoundSystem
{
    public abstract class SoundSystem<T> : Singleton<SoundSystem<T>>
    {
        public enum AudioChannel {Master, Music, SFX}

        [Header("References")]
        [SerializeField] private SoundLibrary<T> _soundLibrary;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;
        private IObjectPool<OneShotSource> _oneShotSourcePool;

        [Header("Transition")]
        [SerializeField] private float _musicStartTime;
        [SerializeField] private float _musicStopTime;

        [Header("Volume")]
        [SerializeField, Range(0, 1)] private float _masterVolume;
        [SerializeField, Range(0, 1)] private float _musicVolume;
        [SerializeField, Range(0, 1)] private float _sfxVolume;

        private Dictionary<T, AudioClip> _audioTable;

        protected override void Awake()
        {
            base.Awake();
            InitAudioTable();
            _oneShotSourcePool = new ObjectPool<OneShotSource>(CreatePoolItem, GetFromPool, ReturnToPool);
        }

        private void InitAudioTable()
        {
            if (_soundLibrary != null)
            {
                _audioTable = _soundLibrary.AudioTable;
            }
        }

        private OneShotSource CreatePoolItem()
        {
            GameObject go = new GameObject("OneShotSource");
            go.transform.SetParent(gameObject.transform);
            AudioSource audioSource = go.AddComponent<AudioSource>();
            OneShotSource oneShotSource = go.AddComponent<OneShotSource>();

            // apply audio source setting
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            oneShotSource.AudioSource = audioSource;
            oneShotSource.OneShotSourcePool = _oneShotSourcePool;

            return oneShotSource;
        }

        private void GetFromPool(OneShotSource source)
        {
            source.gameObject.SetActive(true);
        }

        private void ReturnToPool(OneShotSource source)
        {
            source.gameObject.SetActive(false);
        }

        private void SetPitch(AudioSource source, ref SoundSettings soundSettings)
        {
            if (soundSettings.IsRandomPitch)
            {
                source.pitch = Random.Range(soundSettings.PitchMin, soundSettings.PitchMax);
            }
            else
            {
                source.pitch = 1;
            }
        }

        public void SetVolume(float volume, AudioChannel channel)
        {
            switch(channel)
            {
                case AudioChannel.Master:
                    _masterVolume = volume;
                    break;
                case AudioChannel.Music:
                    _musicVolume = volume;
                    break;
                case AudioChannel.SFX:
                    _sfxVolume = volume;
                    break;
            }

            _musicSource.volume = _musicVolume * _masterVolume;
            _sfxSource.volume = _sfxVolume * _masterVolume;
        }

        public void PlayMusic(T key)
        {
            if (_audioTable == null)
            {
                return;
            }
            if(_audioTable.TryGetValue(key, out AudioClip clip)) {
                StartCoroutine(StartMusic(clip));
            } else {
                Debug.Log($"Key {key} not found.");
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }
            StartCoroutine(StartMusic(clip));
        }

        public void PlaySFX(T key, bool _isRandomPitch = false, float _pitchMin = 0.8f, float _pitchMax = 1.2f)
        {
            if (_audioTable == null)
            {
                return;
            }
            if(_audioTable.TryGetValue(key, out AudioClip clip)) {
                PlaySFX(clip, new SoundSettings(_isRandomPitch, _pitchMin, _pitchMax));
            } else {
                Debug.Log($"Key {key} not found.");
            }
        }

        public void PlaySFX(AudioClip clip, bool _isRandomPitch = false, float _pitchMin = 0.8f, float _pitchMax = 1.2f)
        {
            if (clip == null)
            {
                return;
            }
            PlaySFX(clip, new SoundSettings(_isRandomPitch, _pitchMin, _pitchMax));
        }

        public void PlaySFX(AudioClip clip, SoundSettings soundSettings)
        {
            SetPitch(_sfxSource, ref soundSettings);
            _sfxSource.PlayOneShot(clip, _sfxVolume * _masterVolume);
        }

        public void PlaySFXAtPosition(T key, Vector3 position, bool _isRandomPitch = false, float _pitchMin = 0.8f, float _pitchMax = 1.2f)
        {
            if (_audioTable == null)
            {
                return;
            }
            if (_audioTable.TryGetValue(key, out AudioClip clip))
            {
                PlaySFXAtPosition(clip, position, new SoundSettings(_isRandomPitch, _pitchMin, _pitchMax));
            }
            else
            {
                Debug.Log($"Key {key} not found.");
            }
        }

        public void PlaySFXAtPosition(AudioClip clip, Vector3 position, bool _isRandomPitch = false, float _pitchMin = 0.8f, float _pitchMax = 1.2f)
        {
            if (clip == null)
            {
                return;
            }
            PlaySFXAtPosition(clip, position, new SoundSettings(_isRandomPitch, _pitchMin, _pitchMax));
        }

        public void PlaySFXAtPosition(AudioClip clip, Vector3 position, SoundSettings soundSettings)
        {
            OneShotSource source = _oneShotSourcePool.Get();
            source.transform.position = position;
            source.SetVolume(_sfxVolume * _masterVolume);
            SetPitch(source.AudioSource, ref soundSettings);
            source.SetClip(clip);

            float deactivateDelayTime = clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale) / Mathf.Abs(source.AudioSource.pitch);
            source.Play(deactivateDelayTime);
        }

        public void StopMusic()
        {
            StartCoroutine(EndMusic());
        }

        private IEnumerator StartMusic(AudioClip clip)
        {
            float speed = 1f / _musicStartTime;
            if (_musicSource.clip != null)
            {
                float percent = 0;
                while (percent < 1)
                {
                    percent += Time.deltaTime * speed;
                    _musicSource.volume = Mathf.Lerp(_musicVolume * _masterVolume, 0, percent);
                    yield return null;
                }
            }
            _musicSource.Stop();
            _musicSource.clip = clip;
            _musicSource.volume = _musicVolume * _masterVolume;
            _musicSource.Play();
        }

        private IEnumerator EndMusic()
        {
            if (_musicSource.clip != null)
            {
                float speed = 1f / _musicStopTime;
                float percent = 0;
                while (percent < 1)
                {
                    percent += Time.deltaTime * speed;
                    _musicSource.volume = Mathf.Lerp(_musicVolume * _masterVolume, 0, percent);
                    yield return null;
                }
                _musicSource.Stop();
            }
        }
    }


    public class SoundSystem : SoundSystem<string>
    {
        
    }
}