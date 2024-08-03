using System.Collections.Generic;
using UnityEngine;

namespace GameCore.SoundSystem
{
    public class SoundLibrary<T> : ScriptableObject
    {
        public Sound<T>[] Sounds;

        public Dictionary<T, AudioClip> AudioTable
        {
            get
            {
                if (_audioTable == null)
                {
                    _audioTable = new Dictionary<T, AudioClip>();
                    foreach (var item in Sounds)
                    {
                        if (_audioTable.ContainsKey(item.Key))
                        {
                            Debug.Log($"[SoundLibrary]: Key {item.Key} is duplicated");
                        }
                        else
                        {
                            _audioTable.Add(item.Key, item.Clip);
                        }
                    }
                }
                return _audioTable;
            }
        }
        private Dictionary<T, AudioClip> _audioTable;
    }


    [CreateAssetMenu(fileName = "Sound Library", menuName = "Game Core/Sound Library")]
    public class SoundLibrary : SoundLibrary<string>
    {
        
    }
}