using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePlanes : MonoBehaviour
{
	Toggle toggle;

	void Start()
	{
		toggle = GetComponent<Toggle>();

		EventBus.onPlaneSpawned += OnPlaneSpawned;
	}

	void OnPlaneSpawned(GameObject go)
	{
		go.SetActive(toggle.isOn);
	}

	public void VisualizePlanes()
	{
		EventBus.InvokeSetShowPlanes(toggle.isOn);
	}
}
