using System;
using UnityEngine;

public class UpdateCaller : MonoBehaviour
{
    public static event Action OnUpdate;
    
    void Update()
    {
        OnUpdate?.Invoke();
    }
}
