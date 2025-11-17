using UnityEngine;
using System.Collections;
using System;

public class EventSender 
{
	/// <summary>
	/// Sends the event.
	/// </summary>
	/// <param name="action">Action.</param>
 public static void SendEvent(Action action)
	{
		var handler =action;
		if(handler!=null)
		{
			handler();
		}
	}
	/// <summary>
	/// Sends the event.
	/// </summary>
	/// <param name="action">Action.</param>
	/// <param name="data">Data.</param>
	public static void SendEvent<T>(Action<T> action, T data)
	{
		var handler =action;
		if(handler!=null)
		{
			handler(data);
		}
	}
	/// <summary>
	/// Sends the event.
	/// </summary>
	/// <param name="action">Action.</param>
	/// <param name="data1">Data1.</param>
	/// <param name="data2">Data2.</param>
	public static void SendEvent<T1,T2>(Action<T1,T2> action, T1 data1,T2 data2)
	{
		var handler =action;
		if(handler!=null)
		{
			handler(data1,data2);
		}
	}
	/// <summary>
	/// Sends the event.
	/// </summary>
	/// <param name="action">Action.</param>
	/// <param name="data1">Data1.</param>
	/// <param name="data2">Data2.</param>
	/// <param name="data3">Data3.</param>
	public static void SendEvent<T1,T2,T3>(Action<T1,T2,T3> action, T1 data1,T2 data2,T3 data3)
	{
		var handler =action;
		if(handler!=null)
		{
			handler(data1,data2,data3);
		}
	}

	/// <summary>
	/// Sends the event.
	/// </summary>
	/// <param name="action">Action.</param>
	/// <param name="data1">Data1.</param>
	/// <param name="data2">Data2.</param>
	/// <param name="data3">Data3.</param>
	public static void SendEvent<T1,T2,T3,T4>(Action<T1,T2,T3,T4> action, T1 data1,T2 data2,T3 data3,T4 data4)
	{
		var handler =action;
		if(handler!=null)
		{
			handler(data1,data2,data3,data4);
		}
	}


}
