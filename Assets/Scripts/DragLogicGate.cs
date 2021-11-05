using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class DragLogicGate : MonoBehaviour
{
	public GameObject GateDrawer;

	void Update()
	{
		if (Input.touchCount <= 0)
		{
			return;
		}

		var touchPosition = Input.GetTouch(0).position;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(touchPosition);

		if (Physics.Raycast(ray, out hit) && hit.rigidbody.gameObject.tag == "Gate")
		{
			GateDrawer.SetActive(true);
		}
	}
}
