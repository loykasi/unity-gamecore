using System;
using UnityEngine;

namespace GameCore.Events
{
    public class GameAction : BaseGameAction
    {
        public Action<GameObject> GameEvent;

        public void Raise(GameObject sender)
        {
            GameEvent?.Invoke(sender);
        }
    }

    public class GameAction<T1> : BaseGameAction
    {
        public Action<GameObject, T1> GameEvent;

        public void Raise(GameObject sender, T1 value_1)
        {
            GameEvent?.Invoke(sender, value_1);
        }
    }

    public class GameAction<T1, T2> : BaseGameAction
    {
        public Action<GameObject, T1, T2> GameEvent;

        public void Raise(GameObject sender, T1 value_1, T2 value_2)
        {
            GameEvent?.Invoke(sender, value_1, value_2);
        }
    }

    public class GameAction<T1, T2, T3> : BaseGameAction
    {
        public Action<GameObject, T1, T2, T3> GameEvent;

        public void Raise(GameObject sender, T1 value_1, T2 value_2, T3 value_3)
        {
            GameEvent?.Invoke(sender, value_1, value_2, value_3);
        }
    }   
}