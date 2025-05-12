using UnityEngine;

namespace UnityServiceLocator.ExampleServices
{
    public class MonoBehaviourService : MonoBehaviour
    {
        void Awake()
        {
            Debug.Log("[MonoBehaviourService] Awaken.");
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(12, 12, 12);
        }
    }
}