using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TargetsViewer : MonoBehaviour {

	private readonly Dictionary<Type,object> targetviewers = new Dictionary<Type,object>();
	private List<string> viewers= new List<string>();
	void Awake()
	{
		ITargetViewer[] viewersa = gameObject.GetViews ();
		Displayer.Instance.displayviewers = viewersa;
		var obj = gameObject.GetInterfacesInChildren<ITargetViewer>();
		foreach(ITargetViewer viewer in obj)
		{
			if(!targetviewers.ContainsKey(viewer.GetType))			
				targetviewers.Add(viewer.GetType,(object)viewer);
			if(viewer.isHideObject)
				viewer.Hide();

			viewers.Add(viewer.GetType.ToString());
		}

		Displayer.Instance.targetshowevent+=ShowTargetEvent;
		Displayer.Instance.targethidedevent+=TargetHideEvent;
		Displayer.Instance.targetshowbytype+=TargetShowByType;
		Displayer.Instance.getTargetTypeEvent+=TargtTypeGet;
	}

    public void ReCreate()
    {
        targetviewers.Clear();
        ITargetViewer[] viewersa = gameObject.GetViews();
        Displayer.Instance.displayviewers = viewersa;
        var obj = gameObject.GetInterfacesInChildren<ITargetViewer>();
        foreach (ITargetViewer viewer in obj)
        {
            if (!targetviewers.ContainsKey(viewer.GetType))
                targetviewers.Add(viewer.GetType, (object)viewer);
            if (viewer.isHideObject)
                viewer.Hide();

            viewers.Add(viewer.GetType.ToString());
        }

        Displayer.Instance.targetshowevent += ShowTargetEvent;
        Displayer.Instance.targethidedevent += TargetHideEvent;
        Displayer.Instance.targetshowbytype += TargetShowByType;
        Displayer.Instance.getTargetTypeEvent += TargtTypeGet;
    }

	void ShowTargetEvent (object arg1, ITargetViewer arg2)
	{
		Type type =null;
		if(arg1!=null){
			type=arg1.GetType();
			arg2 = arg1 as ITargetViewer;
		}
		else
			type = arg2.GetType;
		object targetviewerobject=null;
		if(targetviewers.TryGetValue(type,out targetviewerobject)){
			ITargetViewer viewera = targetviewerobject as ITargetViewer;
			viewera.Show(arg1);
		}

	}

	void TargetHideEvent (Type obj)
	{
		object targetviewerobject=null;
		if(targetviewers.TryGetValue(obj,out targetviewerobject))
		{
			ITargetViewer viewer = targetviewerobject as ITargetViewer;
			viewer.Hide();
		}
	}

	void TargetShowByType (Type obj)
	{
		object targetviewerobject=null;
		if(targetviewers.TryGetValue(obj,out targetviewerobject))
		{
			ITargetViewer viewer = targetviewerobject as ITargetViewer;
			viewer.Show(targetviewerobject);
		}
	}
	/// <summary>
	/// Gets the type.
	/// </summary>
	/// <returns>The type.</returns>
	/// <param name="typenam">Typenam.</param>
	public  ITargetViewer getType(string typenam)
	{
		Type type_ax= Type.GetType(typenam);
		object targetviewerobject=null;
		if(targetviewers.TryGetValue(type_ax,out targetviewerobject))
		{
			ITargetViewer viewer = targetviewerobject as ITargetViewer;
			return viewer;
		}
		return null;
	}
    void OnDisable()
    {
        targetviewers.Clear();
    }
	void TargtTypeGet (string obj)
	{
		EventSender.SendEvent(Displayer.Instance.targetvieweraction,  getType(obj));
	}
}
