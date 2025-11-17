using UnityEngine;
using System.Collections;

/// <summary>
/// Viewer.
/// </summary>
public abstract class Viewer : MonoBehaviour {
	/// <summary>
	/// The hidedobject.
	/// </summary>
	public GameObject hidedobject;
	/// <summary>
	/// The is target hide.
	/// </summary>
	[SerializeField] protected bool IsTargetHide;
	/// <summary>
	/// Awake this instance.
	/// </summary>
	protected virtual void Awake(){
		CheckToHide();
	}
	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	protected virtual void OnDestroy()
	{
//		Debug.Log ("Destroy JJ ");
	}
	/// <summary>
	/// Checks to hide.
	/// </summary>
	protected void CheckToHide ()
		{
		if(hidedobject==null && gameObject!=null)
		    hidedobject=gameObject;
		}
	}
