using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
	LineRenderer lineRenderer;
	LineCollider lineCollider;

	Transform start;
	Transform end;

	GameObject deletionCanvas;
	Camera camera;

	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineCollider = GetComponent<LineCollider>();
		deletionCanvas = transform.GetChild(0).gameObject;
	}

	void Start()
	{
		camera = Camera.main;
		deletionCanvas.GetComponent<Canvas>().worldCamera = camera;
		enabled = false;
	}

	void Update()
	{
		lineRenderer.SetPositions(new[] { start.position, end.position });
		CanvasUpdate();
	}

	public void SetLine(Transform lineStart, Transform lineEnd)
	{
		start = lineStart;
		end = lineEnd;
		enabled = true;

		StartCoroutine(SetCollider());
	}

	public void SetPositions(Vector3[] positions) => lineRenderer.SetPositions(positions);

	public void ActivateDeletionOption(bool activate)
	{
		CanvasUpdate();
		deletionCanvas.SetActive(activate);
	}

	public void DeleteLine()
    {
		//unsubscribe everywhere
		Destroy(gameObject);
    }

	IEnumerator SetCollider()
    {
		yield return new WaitForEndOfFrame();
		lineCollider.EnableCollider(true);
    }

	void CanvasUpdate()
    {
		Vector3 lineMid = start.position + (end.position - start.position) / 2;
		Vector3 offset = Vector3.Cross((end.position - start.position), lineMid).normalized * 0.1f;

		if (offset.y < 0)
			offset *= -1;

		deletionCanvas.transform.position = Vector3.Lerp(deletionCanvas.transform.position, lineMid + offset, 0.5f);
		deletionCanvas.transform.LookAt(camera.transform.position, -Vector3.up);
	}
}
