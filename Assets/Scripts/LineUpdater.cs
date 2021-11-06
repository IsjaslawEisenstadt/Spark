using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineUpdater : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Transform source;
    private Transform sink;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        Vector3[] positions = { source.position, sink.position };
        lineRenderer.SetPositions(positions);
    }

    public void SetLineSourceAndSink(Transform source, Transform sink)
    {
        this.source = source;
        this.sink = sink;
    }
}
