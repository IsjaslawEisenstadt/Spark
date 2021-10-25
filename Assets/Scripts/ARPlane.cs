using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlane : MonoBehaviour
{
	void Start()
	{
		EventBus.onSetShowPlanes += OnSetShowPlanes;
		EventBus.InvokeOnPlaneSpawned(gameObject);
	}

	void OnDestroy()
	{
		EventBus.onSetShowPlanes -= OnSetShowPlanes;
	}

	void OnSetShowPlanes(bool showPlanes)
	{
		gameObject.SetActive(showPlanes);
	}
}
