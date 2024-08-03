using System.Collections.Generic;

namespace GameCore.Events
{
    public class EventFactory : Singleton<EventFactory>
    {
        private readonly Dictionary<string, BaseGameAction> _actions = new Dictionary<string, BaseGameAction>();

        public void Add(string key, BaseGameAction action)
        {
            if (!_actions.ContainsKey(key))
            {
                _actions.Add(key, action);
            }
        }

        public BaseGameAction Get(string key)
        {
            if (_actions.TryGetValue(key, out BaseGameAction action))
            {
                return action;
            }
            return null;
        }

        public T Get<T>(string key) where T : BaseGameAction
        {
            if (_actions.TryGetValue(key, out BaseGameAction action))
            {
                return action as T;
            }
            return null;
        }


        public bool TryGet(string key, out BaseGameAction action)
        {
            if (_actions.TryGetValue(key, out action))
            {
                return true;
            }
            return false;
        }

        public bool TryGet<T>(string key, out T action) where T : BaseGameAction
        {
            if (_actions.TryGetValue(key, out BaseGameAction baseEvent))
            {
                action = baseEvent as T;
                return true;
            }
            action = null;
            return false;
        }
    }
}