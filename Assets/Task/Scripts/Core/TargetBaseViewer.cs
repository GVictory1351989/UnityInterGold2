using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Target base viewer.
/// </summary>
public abstract class TargetBaseViewer<TTarget> : Viewer , ITargetViewer where TTarget : class
{
	/// <summary>
	/// Gets or sets the target.
	/// </summary>
	/// <value>The target.</value>
	protected TTarget target{set;get;}

	#region ITargetViewer implementation

	void ITargetViewer.Show (object target)
	{
		Show(target as TTarget);
	}

	void ITargetViewer.Hide ()
	{
		Hide();
	}

	Type ITargetViewer.GetType {
		get {
			return typeof(TTarget);
		}
	}

	bool ITargetViewer.isHideObject {
		get {
			return IsTargetHide;
		}
	}

	#endregion

	protected virtual void Show (TTarget tTarget)
	{
		
		target =tTarget;
		CheckToHide();
		hidedobject.SetActive(true);
		ScaleUpScaleDown();
#if UNITY_EDITOR
		//		Debug.Log (tTarget.GetType().ToString());
#endif
	}

	protected void ScaleUpScaleDown()
    {
		
	}
	protected virtual void Hide ()
	{
		target=null;
		CheckToHide();
		hidedobject.SetActive(false);
	}
	Animation currentAnimation;
	protected void PlayAnimation(GameObject obj, Action<bool> isend)
	{
		if (obj.GetComponent<Animation> () != null) {
			currentAnimation = obj.GetComponent<Animation> ();				
		}
		else
			currentAnimation=null;
		if(currentAnimation!=null)
		{
			currentAnimation.Stop ();
			currentAnimation.Play ();
		}
		StartCoroutine (WaitAndPlayEvent (currentAnimation,isend));
	}

	IEnumerator WaitAndPlayEvent(Animation anim , Action<bool> isend)
	{
		yield return new WaitForSeconds (0.5f);
		if (anim != null)
			anim.Stop ();
		EventSender.SendEvent (isend, true);
	}


	protected void PlayAnimationDuration(GameObject obj, Action<bool> isend )
	{
		float duration = 1.5f;
		if (obj.GetComponent<Animation> () != null) {
			currentAnimation = obj.GetComponent<Animation> ();				
		}
		else
			currentAnimation=null;
		if(currentAnimation!=null)
		{
			currentAnimation.Stop ();
			currentAnimation.Play ();
			if(currentAnimation.clip!=null)
			duration = currentAnimation.clip.length;
		}
		StartCoroutine (WaitAndPlayEvent (currentAnimation,isend,duration));
	}

	IEnumerator WaitAndPlayEvent(Animation anim , Action<bool> isend,float duration )
	{
		yield return new WaitForSeconds (duration);
		if (anim != null)
			anim.Stop ();
		EventSender.SendEvent (isend, true);
	}

	public void BackOperation(Action callback)
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
			callback?.Invoke();
        }
    }
	public void MoveGameObjectsSequentially(Transform from, Transform target, GameObject deactivePrefab, Transform parent, int count, float duration, Action complete)
	{
		StartCoroutine(MoveObjectsSequentiallyCoroutine(from.position, target.position, deactivePrefab, parent, count, duration, complete));
	}

	private IEnumerator MoveObjectsSequentiallyCoroutine(Vector3 fromPosition, Vector3 targetPosition, GameObject deactivePrefab, Transform parent, int count, float duration, Action complete)
	{
		for (int i = 0; i < count; i++)
		{
			// Instantiate the object and set its parent
			GameObject newObject = Instantiate(deactivePrefab, fromPosition, Quaternion.identity, parent);
			newObject.SetActive(true);
			yield return StartCoroutine(MoveObject(newObject.transform, fromPosition, targetPosition, duration));
			Destroy(newObject); // Destroy the object after reaching the target
		}

		// Invoke the complete action after all objects have been moved
		complete?.Invoke();
	}

	// Parallel movement
	public void MoveGameObjectsInParallel(Transform from, Transform target, GameObject deactivePrefab, Transform parent, int count, float duration, Action complete)
	{
		StartCoroutine(MoveObjectsInParallelCoroutine(from.position, target.position, deactivePrefab, parent, count, duration, complete));
	}

	private IEnumerator MoveObjectsInParallelCoroutine(Vector3 fromPosition, Vector3 targetPosition, GameObject deactivePrefab, Transform parent, int count, float duration, Action complete)
	{
		List<Coroutine> moveCoroutines = new List<Coroutine>();
		List<GameObject> instantiatedObjects = new List<GameObject>(); // List 
		for (int i = 0; i < count; i++)
		{
			GameObject newObject = Instantiate(deactivePrefab, fromPosition, Quaternion.identity, parent);
			newObject.SetActive(true);
			Coroutine moveCoroutine = StartCoroutine(MoveObject(newObject.transform, fromPosition, targetPosition, duration));
			moveCoroutines.Add(moveCoroutine);
			instantiatedObjects.Add(newObject);
		}

		yield return new WaitUntil(() => AreAllCoroutinesCompleted(moveCoroutines));
		foreach (var obj in instantiatedObjects)
		{
			Destroy(obj); // Destroy each object
		}
		// Invoke the complete action after all objects have been moved
		complete?.Invoke();
	}

	private IEnumerator MoveObject(Transform objTransform, Vector3 fromPosition, Vector3 targetPosition, float time)
	{
		float elapsedTime = 0f;
		while (elapsedTime < time)
		{
			objTransform.position = Vector3.Lerp(fromPosition, targetPosition, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null; // Wait for the next frame
		}

		objTransform.position = targetPosition;
	}

	private bool AreAllCoroutinesCompleted(List<Coroutine> coroutines)
	{
		foreach (var coroutine in coroutines)
		{
			if (coroutine != null) // If the coroutine is still running
				return false;
		}
		return true;
	}
}
