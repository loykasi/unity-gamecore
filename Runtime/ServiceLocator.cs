using System;
using System.Collections.Generic;

namespace GameCore
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void Register<T>(object service)
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                _services.Add(typeof(T), service);
            }
        }

        public void Register(object service)
        {
            if (!_services.ContainsKey(service.GetType()))
            {
                _services.Add(service.GetType(), service);
            }
        }

        public T Get<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out object service))
            {
                return (T) service;
            }
            return null;
        }

        public bool TryGet<T>(out T service) where T : class
        {
            if (_services.TryGetValue(typeof(T), out object obj))
            {
                service = obj as T;
                return true;
            }
            service = null;
            return false;
        }
    }
}