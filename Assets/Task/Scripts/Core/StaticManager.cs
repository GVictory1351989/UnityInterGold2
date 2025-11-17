using System;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    private static StaticManager _instance;

    public static StaticManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject managerObj = new GameObject("StaticManager");
                _instance = managerObj.AddComponent<StaticManager>();
                DontDestroyOnLoad(managerObj); // Prevent it from being destroyed across scenes
            }
            return _instance;
        }
    }

    // Dictionary to store singleton services by their type
    private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    // Register a singleton service
    public void Register<T>(T instance) where T : class
    {
        var type = typeof(T);
        if (!services.ContainsKey(type))
        {
            services[type] = instance;
        }
        else
        {
            Debug.LogWarning($"{type.Name} is already registered.");
        }
    }

    // Retrieve a registered service by type
    public T Get<T>() where T : class
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            return services[type] as T;
        }
        else
        {
            Debug.LogError($"{type.Name} is not registered.");
            return null;
        }
    }

    // Unregister a service by type
    public void Unregister<T>() where T : class
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            services.Remove(type);
        }
        else
        {
            Debug.LogWarning($"{type.Name} is not registered, so it can't be unregistered.");
        }
    }

    // Clear all registered services (optional)
    public void ClearAllServices()
    {
        services.Clear();
    }
}
