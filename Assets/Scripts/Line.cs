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

	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineCollider = GetComponent<LineCollider>();
		deletionCanvas = transform.GetChild(0).gameObject;
	}

	void Start()
	{
		deletionCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
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
		//some magic here
    }
}
