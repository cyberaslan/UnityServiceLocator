using System;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace UnityServiceLocator.ExampleServices
{
    public class BrokenExampleService 
    {
        public BrokenExampleService()
        {
            Debug.LogError("[BrokenExampleService] Error here would not break the startup.");
        }
    }
}