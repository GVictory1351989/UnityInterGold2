using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton.
/// </summary>
public class Singleton<T> where T : class,new()
{
	/// <summary>
	/// The instance.
	/// </summary>
	readonly static T instance = new T();
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance{
		get{
			return instance;
		}
	}
}

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	/// <summary>
	/// The instance.
	/// </summary>
    private static T instance;
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get
		{
			if(instance==null)
            {
				instance = new GameObject().AddComponent<T>();
				instance.transform.name = typeof(T).ToString();
				DontDestroyOnLoad(instance);
            }
			return instance;
		}
	}
}