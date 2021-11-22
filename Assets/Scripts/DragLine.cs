using cakeslice;
using UnityEngine;

public class DragLine : MonoBehaviour
{
	GameObject currentLine;
	Line currentLineScript;
	GameObject lineStart;
	GameObject lineEnd;

	public GameObject line;

	void Update()
	{
		if (Input.touchCount <= 0)
		{
			if (!currentLine)
			{
				return;
			}

			if (lineEnd)
			{
				//tell gates about new connection
				currentLineScript.SetLine(lineStart.transform, lineEnd.transform);
				lineStart.GetComponent<Pin>().Line = currentLineScript;
				lineEnd.GetComponent<Pin>().Line = currentLineScript;
			}
			else
			{
				Destroy(currentLine);
				lineStart.GetComponent<Pin>().Line = null;
				if (lineEnd)
					lineEnd.GetComponent<Pin>().Line = null;
			}

			SetEndOutline(false);
			SetStartOutline(false);

			currentLine = null;
			currentLineScript = null;
			lineStart = null;
			lineEnd = null;

			return;
		}

		var rayCast = RayCaster.Instance.GetHitObject();

		if (!currentLine)
		{
			if (!rayCast.successful)
				return;

			if (rayCast.hitObject.CompareTag("LogicGateOutput"))
			{
				lineStart = rayCast.hitObject;
				currentLine = Instantiate(line, lineStart.transform);
				currentLineScript = currentLine.GetComponent<Line>();

				SetStartOutline(true);
			}
			else if (rayCast.hitObject.CompareTag("LogicGateInput"))
			{
				Pin pin = rayCast.hitObject.GetComponent<Pin>();

				if (!pin.Line)
					return;

				currentLine = pin.Line.gameObject;
				currentLineScript = currentLine.GetComponent<Line>();
				lineStart = currentLineScript.GetStart();

				currentLineScript.Disconnect();
			}
		}

		if (currentLine)
		{
			Vector3 projectedTouchPosition;

			if (rayCast.successful &&
			    rayCast.hitObject.CompareTag("LogicGateInput") &&
			    rayCast.hitObject != lineStart)
			{
				if (rayCast.hitObject != lineEnd)
				{
					lineEnd = rayCast.hitObject;
					SetEndOutline(true);
				}

				projectedTouchPosition = lineEnd.transform.position;
			}
			else
			{
				Vector2 touchPosition = Input.GetTouch(0).position;
				projectedTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y,
					(Camera.main.transform.position - gameObject.transform.position).magnitude +
					Camera.main.nearClipPlane));

				SetEndOutline(false);
				lineEnd = null;
			}

			Vector3[] points = { lineStart.transform.position, projectedTouchPosition };

			currentLineScript.SetPositions(points);
		}
	}

	void SetStartOutline(bool outlineEnabled)
	{
		if (lineStart)
			lineStart.GetComponent<Outline>().eraseRenderer = !outlineEnabled;
	}

	void SetEndOutline(bool outlineEnabled)
	{
		if (lineEnd)
			lineEnd.GetComponent<Outline>().eraseRenderer = !outlineEnabled;
	}
}
