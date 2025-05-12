using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityServiceLocator.ExampleServices
{
    public class AuthService : MonoBehaviour, IInitializable
    {
        // This is example service simulates game authorization (login)
        public Action Ready { get; set; }
        public bool IsReady { get; set; }
        public bool DontAutoInit => true; // set true to finish initialization manually 
        
        public async Task Init()
        {
            var isLoginSuccessful = await SimulateLogin();

            Debug.Log($"[Auth Service] Login simulated with result: {isLoginSuccessful}");
            
            if (isLoginSuccessful)
            {
                //continue initialization order
                this.FinishInit();
            }
            else
            {
                //stop initialization order, cause this service will be never initialized (DontAutoInit == true)
                throw new Exception("[Auth Service] Login is not successful");
            }
        }

        private async Task<bool> SimulateLogin()
        {
            Debug.Log("[Auth Service] Try login to server...");
            await Task.Delay(1000);
            return true;
        }
    }
}