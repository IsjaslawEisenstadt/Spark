using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlane : MonoBehaviour
{
	private void Start()
	{
		EventBus.onSetShowPlanes += OnSetShowPlanes;
		EventBus.InvokeOnPlaneSpawned(gameObject);
	}

	private void OnDestroy()
	{
		EventBus.onSetShowPlanes -= OnSetShowPlanes;
	}

	private void OnSetShowPlanes(bool showPlanes)
	{
		gameObject.SetActive(showPlanes);
	}
}
