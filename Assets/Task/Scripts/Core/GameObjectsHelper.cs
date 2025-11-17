using UnityEngine;
using System.Collections;
/// <summary>
/// Game objects helper.
/// </summary>

public static class GameObjectsHelper 
{
	public static void SetActive(this GameObject obj , bool isactive)
	{
		obj.SetActive(isactive);
	}
	public static ITargetViewer[] GetViews(this GameObject obj)
	{
		ITargetViewer[] foos = obj.GetComponentsInChildren<ITargetViewer>();
		return foos;
	}
}
