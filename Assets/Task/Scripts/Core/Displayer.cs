using UnityEngine;
using System.Collections;
using System;

public class Displayer : Singleton<Displayer> {

	public event Action<Type> targethidedevent = delegate{};
	public event Action<Type> targetshowbytype = delegate{};
	public event Action<object ,ITargetViewer> targetshowevent = delegate{};
	public event Action<string> getTargetTypeEvent = delegate{};
	public Action<ITargetViewer> targetvieweraction = delegate{};
	public ITargetViewer[] displayviewers = null;

	public void Display(object obj , ITargetViewer viewer)
	{
		EventSender.SendEvent(targetshowevent,obj,viewer);
	}
	public void Display(Type viewer)
	{
		EventSender.SendEvent(targetshowbytype,viewer);
	}

	public void Hide(Type viewer)
	{
		EventSender.SendEvent(targethidedevent,viewer);
	}

	public void GetTarget(string name)
	{
		EventSender.SendEvent(getTargetTypeEvent,name);
	}

	public ITargetViewer GetTargetViewer(Type type)
	{
		if(displayviewers!=null)
			foreach (ITargetViewer viewer in displayviewers)
				if (viewer.GetType.ToString().Contains(type.ToString()))
					return viewer;
		return null;
	}
}
