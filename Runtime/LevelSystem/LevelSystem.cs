using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.LevelSystem
{
    public class LevelSystem : Singleton<LevelSystem> 
    {
        public static event Action OnStartLoading;
        public static event Action OnFinishLoading;
        public static bool IsLoading;


        private enum LoadMethod
        {
            Name,
            BuildIndex,
        }
        
        [SerializeField] private LoadingScreen _defaultLoadingScreen;
        [SerializeField] private bool _IsUsingDefaultLoadingScreen;
        private ILevelLoadingScreen _loadingScreen;
        
        private LoadMethod _loadMethod;
        
        private string _loadName;
        private int _loadBuildIndex;

        protected override void Awake()
        {
            base.Awake();
            if (_IsUsingDefaultLoadingScreen)
            {
                SetLoadingScreen(_defaultLoadingScreen);
            }
        }

        public void SetLoadingScreen(LoadingScreen loadingScreen)
        {
            if (_defaultLoadingScreen == null)
            {
                return;
            }

            loadingScreen.transform.SetParent(transform);
            _loadingScreen = loadingScreen;
        }

        public void LoadLevel(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadLevel(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadLevelAsync(string sceneName)
        {
            _loadMethod = LoadMethod.Name;
            _loadName = sceneName;
            StartLoadingScreen();
        }

        public void LoadLevelAsync(int sceneIndex)
        {
            _loadMethod = LoadMethod.BuildIndex;
            _loadBuildIndex = sceneIndex;
            StartLoadingScreen();
        }

        private void StartLoadingScreen()
        {
            _loadingScreen.Show(LoadLevel);
            IsLoading = true;
            OnStartLoading?.Invoke();
        }

        private void LoadLevel()
        {
            StartCoroutine(LoadingLevel());
        }

        private void CloseLoadingScreen()
        {
            _loadingScreen.Close();
            IsLoading = false;
            OnFinishLoading?.Invoke();
        }
        
        private IEnumerator LoadingLevel()
        {
            AsyncOperation loadingOperation = GetLoadSceneOperation();

            while (!loadingOperation.isDone)
            {
                float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
                _loadingScreen.UpdateProgress(progress);
                yield return null;  
            }

            CloseLoadingScreen();
        }

        private AsyncOperation GetLoadSceneOperation()
        {
            switch (_loadMethod)
            {
                case LoadMethod.Name:
                    return SceneManager.LoadSceneAsync(_loadName);
                case LoadMethod.BuildIndex:
                    return SceneManager.LoadSceneAsync(_loadBuildIndex);
                default:
                    throw new Exception("Invalid load scene method");
            }
        }
    }
}
