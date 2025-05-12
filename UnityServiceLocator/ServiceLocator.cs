using System.Collections.Generic;
using System;

namespace UnityServiceLocator
{
    public static class ServiceLocator
    {
        public static event Action<Type> Registered;
        private static readonly Dictionary<string, object> _services = new();
        public static bool IsRegistered<T>() => _services.ContainsKey(typeof(T).Name);

        // Register service instance as type
        public static void Register(object serviceInstance)
        {
            var type = serviceInstance.GetType();
            var key = type.Name;
            if (_services.ContainsKey(key))
            {
                throw new Exception($"[Service Locator] Service \"{key}\" has already registered.");
            }
            _services.Add(key, serviceInstance);
            Registered?.Invoke(type);
        }
        
        // Get registered service
        public static T Get<T>()
        {
            var key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                throw new Exception($"[Service Locator] Service \"{key}\" not found.");
            }

            return (T)_services[key];
        }

        // Unregister service
        public static void Unregister(System.Type type)
        {
            var key = type.Name;
            if (!_services.ContainsKey(key))
            {
                throw new Exception($"[Service Locator] Cant unregister service \"{key}\" which is not registered");
            }

            _services.Remove(key);
        }

        // extension for code usability (short method call without cast)
        public static void FinishInit(this IInitializable initializableService) => initializableService.FinishInit();
    }
}
