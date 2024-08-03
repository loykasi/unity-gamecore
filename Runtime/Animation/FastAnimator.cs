using UnityEngine;

namespace Gamecore.Animation
{
    public class FastAnimator : MonoBehaviour
    {
        [SerializeField] private bool _isUsingDefaultAnimation;
        [SerializeField] private string _defaultAnimationState;
        [SerializeField] private float _crossFadeTime;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (_isUsingDefaultAnimation)
            {
                _animator.CrossFade(_defaultAnimationState, _crossFadeTime);
            }
        }

        public void PlayAnimation(string stateName)
        {
            _animator.CrossFade(stateName, _crossFadeTime);
        }
    }   
}