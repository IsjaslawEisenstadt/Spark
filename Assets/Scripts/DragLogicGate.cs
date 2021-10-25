using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class DragLogicGate : MonoBehaviour
{
	GameObject spawnedObject;
	ARRaycastManager arRaycastManager;

	static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

	void Awake()
	{
		arRaycastManager = GetComponent<ARRaycastManager>();
		enabled = false;
	}

	void Update()
	{
		if (Input.touchCount <= 0)
		{
			if (!spawnedObject.activeSelf)
				Destroy(spawnedObject);
			spawnedObject = null;
			enabled = false;
			return;
		}

		var touchPosition = Input.GetTouch(0).position;
		var hit = arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon);

		spawnedObject.SetActive(hit);
		if (hit)
			spawnedObject.transform.position = hits[0].pose.position;
	}

	public void InstantiateGate(GameObject gate)
	{
		spawnedObject = Instantiate(gate);
		spawnedObject.SetActive(false);
		enabled = true;
	}
}
