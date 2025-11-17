using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// I simple viewer.
/// </summary>
public interface ISimpleViewer{
	/// <summary>
	/// Type of the class
	/// </summary>
	/// <value>The type of the get.</value>
	Type GetType{get;}
	/// <summary>
	/// Show the specified target.
	/// </summary>
	/// <param name="target">Target.</param>
	void Show();
	/// <summary>
	/// Hide the object 
	/// </summary>
	void Hide();
}
