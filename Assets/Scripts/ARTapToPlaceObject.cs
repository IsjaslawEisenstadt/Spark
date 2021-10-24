using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    private GameObject spawnedObject;
    private ARRaycastManager arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool tryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (spawnedObject == null)
            return;

        if (!tryGetTouchPosition(out Vector2 touchPosition))
        {
            if (!spawnedObject.activeSelf)
                Destroy(spawnedObject);
            spawnedObject = null;
            return;
        }

        if(arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            if (!spawnedObject.activeSelf)
                spawnedObject.SetActive(true);

            spawnedObject.transform.position = hits[0].pose.position;
        }
        else
        {
            if(spawnedObject.activeSelf)
                spawnedObject.SetActive(false);
        }
    }

    public void instantiateGate(GameObject gate)
    {
        spawnedObject = Instantiate(gate);
        spawnedObject.SetActive(false);
    }
}
