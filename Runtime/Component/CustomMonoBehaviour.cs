using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamecore.Component
{
    public abstract class CustomMonobehaviour : MonoBehaviour
    {
        private bool _isStarted = false;
        private bool _isLoaded = false;

        protected virtual void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (_isStarted)
            {
                OnEnableOrStart();
            }
            if (_isLoaded)
            {
                OnEnableOrLoaded();
            }
        }

        protected virtual void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            _isLoaded = true;
            OnSceneLoaded();
            OnEnableOrLoaded();
        }

        protected virtual void OnSceneLoaded()
        {

        }

        protected virtual void OnEnableOrStart()
        {

        }

        protected virtual void OnEnableOrLoaded()
        {

        }


        protected virtual void Start()
        {
            _isStarted = true;
            OnEnableOrStart();
        }

        protected virtual void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}