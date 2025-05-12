using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityServiceLocator
{
    public interface IInitializable
    {
        public Action Ready { get; set; }
        public bool IsReady { get; set; }
        public bool DontAutoInit { get; } // mark true if you want service to finish init by itself
        Task Init(); // replace with UniTask for WebGL
        void FinishInit()
        {
            if (IsReady)
            {
                throw new Exception($"{GetType().Name} has already initialized.");
            }
            IsReady = true;
            Ready?.Invoke();
            Ready = null;
            
            Debug.Log($"[{GetType().Name}] initialized successfully");
        }

    }
}