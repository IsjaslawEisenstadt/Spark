using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
	private GameObject _spawnedObject;
	private ARRaycastManager _arRaycastManager;

	private static readonly List<ARRaycastHit> Hits = new List<ARRaycastHit>();

	private void Awake()
	{
		_arRaycastManager = GetComponent<ARRaycastManager>();
		enabled = false;
	}

	private void Update()
	{
		if (Input.touchCount <= 0)
		{
			if (!_spawnedObject.activeSelf)
				Destroy(_spawnedObject);
			_spawnedObject = null;
			enabled = false;
			return;
		}

		var touchPosition = Input.GetTouch(0).position;
		var hit = _arRaycastManager.Raycast(touchPosition, Hits, TrackableType.PlaneWithinPolygon);

		_spawnedObject.SetActive(hit);
		if (hit)
			_spawnedObject.transform.position = Hits[0].pose.position;
	}

	public void InstantiateGate(GameObject gate)
	{
		_spawnedObject = Instantiate(gate);
		_spawnedObject.SetActive(false);
		enabled = true;
	}
}
