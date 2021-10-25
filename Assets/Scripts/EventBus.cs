using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
	public static event Action<bool> onSetShowPlanes;

	public static void InvokeSetShowPlanes(bool showPlanes)
	{
		onSetShowPlanes?.Invoke(showPlanes);
	}

	public static event Action<GameObject> onPlaneSpawned;

	public static void InvokeOnPlaneSpawned(GameObject plane)
	{
		onPlaneSpawned?.Invoke(plane);
	}
}
