using System;
using System.Collections.Generic;

namespace GameCore.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private Stack<T> _pool = new Stack<T>();
        
        private Func<T> _funcOnCreate;
        private Action<T> _actionOnGet;
        private Action<T> _actionOnReturn;

        public ObjectPool(Func<T> funcOnCreate, Action<T> actionOnGet, Action<T> actionOnReturn)
        {
            _funcOnCreate = funcOnCreate;
            _actionOnGet = actionOnGet;
            _actionOnReturn = actionOnReturn;
        }

        public T Get()
        {
            T item;
            if (_pool.Count == 0)
            {
                item = _funcOnCreate();
            }
            else
            {
                item = _pool.Pop();
            }
            _actionOnGet?.Invoke(item);
            return item;
        }

        public void Return(T item)
        {
            _actionOnReturn(item);
            _pool.Push(item);
        }
    }
}