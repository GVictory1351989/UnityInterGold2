using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static partial class TGameObjectExtensions
{

    /// <summary>
    /// Gets the interface in children.
    /// </summary>
    /// <returns>The interface in children.</returns>
    /// <param name="gObj">G object.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T GetInterfaceInChildren<T>(this GameObject gObj)
	{
		if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
		return gObj.GetInterfacesInChildren<T>().FirstOrDefault();
	}

    /// <summary>
    /// Gets the interfaces in children.
    /// </summary>
    /// <returns>The interfaces in children.</returns>
    /// <param name="gObj">G object.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T[] GetInterfacesInChildren<T>(this GameObject gObj)
	{
		if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
		var mObjs = gObj.GetComponentsInChildren<MonoBehaviour>();
        return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T) ) select (T)(object)a).ToArray();
	}

    /// <summary>
    /// Randoms the enum value.
    /// </summary>
    /// <returns>The enum value.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(new System.Random().Next(v.Length));
    }

}
