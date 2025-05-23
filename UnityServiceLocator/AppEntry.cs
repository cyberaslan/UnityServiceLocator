#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityServiceLocator.ExampleServices;

namespace UnityServiceLocator
{
    public class AppEntry : MonoBehaviour
    {
        private static bool IsLaunched;
        // Just set the initialization order for services.
        // Dont care if service is MonoBehaviour-inherited (instance will be created on scene). 
        private List<ServiceExecutionStage> _serviceRegistrationOrder = new()
        {
            // Uncomment AuthService next line to advanced practice:
            // new(typeof(AuthService), true),
            new(typeof(ExampleService)),
            new(typeof(BrokenExampleService)),
            new(typeof(AsyncExampleService)),
            new(typeof(MonoBehaviourService)),
        };

        public void Awake()
        {
            if (IsLaunched)
            {
                throw new Exception("[App Entry] Application init is already launched. Please make sure that there is only single AppEntry on the scene.");
            }

            IsLaunched = true;

            RegisterServices();
            InitServices();
        }

        void RegisterServices()
        {
            Debug.LogWarning("[App Entry] Phase #1 - registration.");
            foreach (var order in _serviceRegistrationOrder)
            {
                try
                {
                    var instance = order.CreateInstance();
                    // all services are automatically registered in the locator
                    ServiceLocator.Register(instance);
                }
                catch (Exception e)
                {
                    // exception when creating a single service doesn't stop the app initialization
                    Debug.LogError($"[App Entry] {order.ServiceType.Name} registration error: {e}");
                }
            }
        }

        async void InitServices()
        {
            Debug.LogWarning("[App Entry] Phase #2 - initialization.");
            foreach (var order in _serviceRegistrationOrder)
            {
                try
                {
                    if (order.ServiceInstance is not IInitializable initableService) continue;

                    await initableService.Init();

                    if (!initableService.DontAutoInit)
                    {
                        initableService.FinishInit();
                    }

                    while (order.WaitUntilReady && !initableService.IsReady)
                    {
                        await Task.Delay(100); // repace with UniTask for WebGL
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[App Entry] {order.ServiceType.Name} init error: {e}");
                }
            }
        }

        class ServiceExecutionStage
        {
            public readonly Type ServiceType;
            public readonly bool WaitUntilReady;
            public object ServiceInstance { get; private set; }
            public ServiceExecutionStage(Type serviceType, bool waitUntilReady = false)
            {
                ServiceType = serviceType;
                WaitUntilReady = waitUntilReady;
            }

            public object CreateInstance()
            {
                if (ServiceInstance != null)
                {
                    throw new Exception($"[App Entry] Service {ServiceType} has already instanced.");
                }

                if (ServiceType.IsSubclassOf(typeof(MonoBehaviour)))
                {
                    var container = new GameObject();
                    container.name = $"{ServiceType.Name}";
                    ServiceInstance = container.AddComponent(ServiceType);
                    DontDestroyOnLoad(container);
                }
                else
                {
                    ServiceInstance = Activator.CreateInstance(ServiceType);
                }

                return ServiceInstance;
            }
        }
#if UNITY_EDITOR
        // If you have disabled domain reload for Unity Editor isLaunched value wont reset without this fix
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void EditorDomainReloadReset()
        {
            IsLaunched = false;
        }
#endif
    }

}
