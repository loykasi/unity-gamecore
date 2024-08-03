using System;
using System.Collections;
using UnityEngine;

namespace GameCore.LevelSystem
{
    public class FadeLoadingScreen : LoadingScreen
    {
        [SerializeField] private GameObject _canvasObject;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeInDuration;
        [SerializeField] private float _fadeOutDuration;

        public override void Show(Action callback)
        {
            _canvasObject.SetActive(true);
            StartCoroutine(FadeIn(callback));
        }

        public override void Close()
        {
            StartCoroutine(FadeOut());
        }

        public override void UpdateProgress(float progress)
        {
            // Debug.Log($"Loading progress: {progress}");
        }

        private IEnumerator FadeIn(Action callback)
        {
            float percent = 0;
            float speed = 1 / _fadeInDuration;
            while (percent < 1f)
            {
                _canvasGroup.alpha = percent;
                percent += speed * Time.deltaTime;
                yield return null;
            }
            _canvasGroup.alpha = 1;
            // OnStarted?.Invoke();
            callback?.Invoke();
        }

        private IEnumerator FadeOut()
        {
            float percent = 1;
            float speed = 1 / _fadeOutDuration;
            while (percent > 0)
            {
                _canvasGroup.alpha = percent;
                percent -= speed * Time.deltaTime;
                yield return null;
            }
            _canvasGroup.alpha = 0;
            _canvasObject.SetActive(false);
        }
    }
}