using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// I target viewer.
/// </summary>
public interface ITargetViewer 
{
	/// <summary>
	/// Type of the class
	/// </summary>
	/// <value>The type of the get.</value>
	Type GetType{get;}
	/// <summary>
	/// Gets a value indicating whether this <see cref="ITargetViewer"/> is hide object.
	/// </summary>
	/// <value><c>true</c> if is hide object; otherwise, <c>false</c>.</value>
	bool isHideObject{get;}
	/// <summary>
	/// Show the specified target.
	/// </summary>
	/// <param name="target">Target.</param>
	void Show(object target);
	/// <summary>
	/// Hide the object 
	/// </summary>
	void Hide();
}
