using System;

namespace GameCore.LevelSystem
{
    public interface ILevelLoadingScreen
    {
        void Show(Action callback);
        void UpdateProgress(float progress);
        void Close();
    }
}