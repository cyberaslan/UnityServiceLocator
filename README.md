# UnityServiceLocator
## How to use
1. Add UnityServiceLocator to project
2. Add AppEntry prefab to scene
3. Specify any types in the list

![helper](https://github.com/user-attachments/assets/b95bfd30-b82e-4d62-b780-e9bd276fb26d)
__Dont care if service is MonoBehaviour-inherited (instance will be created on scene automatically)__

4. Just use Get() to get your service:
```ServiceLocator.Get<ExampleService>();```

## Why UnityServiceLocator?
UnityServiceLocator contains an application initialization system that resolves important architectural issues:
* Transparent service initialization order allows you to control the flow of application startup
* Easier to resolve problems of service codependency  
* An exception of one of the services does not interrupt code execution and app launch!
* Auto instancing of monobehaviour services without the need to use prefabs
* Asynchronous initialization of services supported

## Why Service Locator vs. Singleton?
Service locator is a powerful and simple architectural pattern that has a number of advantages over singleton:
* Any type can be registered
* No need to write "single instance validation" boilerplate code in each class
* Any type can be unregistered, so the static object lifecycle can be temporary
