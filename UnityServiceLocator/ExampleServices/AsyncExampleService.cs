using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityServiceLocator.ExampleServices
{
    public class AsyncExampleService : IInitializable
    {
        public AsyncExampleService()
        {
            Debug.Log("[AsyncExampleService] Constructor.");
        }

        public Action Ready { get; set; }
        public bool IsReady { get; set; }
        public bool DontAutoInit { get; }
        public async Task Init()
        {
            Debug.Log("[AsyncExampleService] Init task with async support started. Waiting 1200ms...");
            await Task.Delay(1200);
            Debug.Log("[AsyncExampleService] 1200ms waited.");
        }
    }
}