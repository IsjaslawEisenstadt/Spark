using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineCollider : MonoBehaviour
{
    LineRenderer lineRenderer;
    CapsuleCollider collider;

    public float LineWidth;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        collider = gameObject.AddComponent<CapsuleCollider>();
    }

    void Start()
    {
        collider.radius = LineWidth / 2;
        collider.center = Vector3.zero;
        collider.direction = 2;

        collider.enabled = false;
    }

    void LateUpdate()
    {
        Vector3[] positions = new Vector3[2];
        lineRenderer.GetPositions(positions);

        collider.transform.position = positions[0] + (positions[1] - positions[0]) / 2;
        collider.transform.LookAt(positions[0]);
        collider.height = (positions[1] - positions[0]).magnitude;
    }

    public void EnableCollider(bool enable)
    {
        collider.enabled = enable;
    }
}
