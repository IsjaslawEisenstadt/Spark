using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateInformation : MonoBehaviour
{
    Camera camera;

    public float offsetValue;

    void Start()
    {
        camera = Camera.main;
        UpdatePosition(false);
    }

    void Update() => UpdatePosition(true);

    public void ShowGateInformation(bool show) => gameObject.SetActive(show);

    private void UpdatePosition(bool lerpPosition)
    {
        Vector3 offset = Vector3.Cross(camera.transform.right, (camera.transform.position - transform.position)).normalized * offsetValue;
        transform.position = lerpPosition ? Vector3.Lerp(transform.position, transform.parent.position + offset, 0.5f) : transform.parent.position + offset;
        transform.LookAt(camera.transform.position, Vector3.up);
    }
}
