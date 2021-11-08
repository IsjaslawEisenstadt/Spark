using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    static RayCaster _instance;

    public static RayCaster Instance { get { return _instance; } }

    Camera camera;
    GameObject hitObject;
    bool isCached = false;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            camera = Camera.main;
        }
    }

    (bool succesfull, GameObject hitObject) CalculateHitObject()
    {
        Vector2 touchPosition = Input.GetTouch(0).position;

        Ray ray = camera.ScreenPointToRay(touchPosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hitObject = hit.collider.gameObject;
            isCached = true;
        }

        return (isCached, hitObject);
    }

    public (bool succesfull, GameObject hitObject) GetHitObject() => isCached ? (true, hitObject) : CalculateHitObject();

    void LateUpdate()
    {
        if (isCached)
        {
            hitObject = null;
            isCached = false;
        }
    }
}
