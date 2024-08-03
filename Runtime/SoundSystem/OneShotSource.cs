using UnityEngine;
using GameCore.ObjectPool;

namespace GameCore.SoundSystem
{
    public class OneShotSource : MonoBehaviour
    {
        public IObjectPool<OneShotSource> OneShotSourcePool
        {
            set => _oneShotSourcePool = value;
        }

        public AudioSource AudioSource
        {
            get => _audioSource;
            set => _audioSource = value;
        }

        private IObjectPool<OneShotSource> _oneShotSourcePool;
        private AudioSource _audioSource;

        public void Play(float delay)
        {
            _audioSource.Play();
            Deactivate(delay);
        }

        public void SetClip(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }

        public void Deactivate(float delay)
        {
            time = delay;
            isStart = true;
            // StartCoroutine(WaitToDeactivate(delay));
        }

        float time;
        bool isStart;
        Coroutine coroutine;
        private void Update()
        {
            if (!isStart) return;
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = 0;
                _oneShotSourcePool.Return(this);
                isStart = false;
            }
        }
        
        // private IEnumerator WaitToDeactivate(float delay)
        // {
        //     yield return CoroutineHelpers.GetWaitForSeconds(delay);
        //     _oneShotSourcePool.Return(this);
        // }
    }
}