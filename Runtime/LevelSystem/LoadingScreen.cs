using System;
using UnityEngine;

namespace GameCore.LevelSystem
{
    public abstract class LoadingScreen : MonoBehaviour, ILevelLoadingScreen
    {
        public abstract void Show(Action callback);
        public abstract void UpdateProgress(float progress);
        public abstract void Close();
    }
}