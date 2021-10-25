using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePlanes : MonoBehaviour
{
	private Toggle _toggle;

	private void Start()
	{
		_toggle = GetComponent<Toggle>();

		EventBus.onPlaneSpawned += OnPlaneSpawned;
	}

	private void OnPlaneSpawned(GameObject go)
	{
		go.SetActive(_toggle.isOn);
	}

	public void VisualizePlanes()
	{
		EventBus.InvokeSetShowPlanes(_toggle.isOn);
	}
}
