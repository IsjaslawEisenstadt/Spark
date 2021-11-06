using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineUpdater : MonoBehaviour
{
	LineRenderer lineRenderer;

	Transform source;
	Transform sink;

	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		enabled = false;
	}

	void Update()
	{
		lineRenderer.SetPositions(new[] { source.position, sink.position });
	}

	public void SetLineSourceAndSink(Transform source, Transform sink)
	{
		this.source = source;
		this.sink = sink;
		enabled = true;
	}
}
