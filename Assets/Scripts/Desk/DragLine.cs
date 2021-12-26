using UnityEngine;

public class DragLine : MonoBehaviour
{
	Line currentLine;
	Pin lineStart;
	Pin lineEnd;
	Line oldLine;

	public GameObject line;

	void Update()
	{
		if (SparkInput.GetTouchCount() <= 0)
		{
			if (!currentLine)
			{
				return;
			}

			lineStart.SetOutline(false);

			if (lineEnd)
			{
				lineEnd.SetOutline(false);
				currentLine.SetPins(lineStart, lineEnd);
			}
			else
			{
				Destroy(currentLine.gameObject);
			}

			if (oldLine)
			{
				if (oldLine.LineStart)
				{
					oldLine.LineStart.Lines.Remove(oldLine);
				}
				if (oldLine.LineEnd)
				{
					oldLine.LineEnd.Lines.Remove(oldLine);
				}

				Destroy(oldLine.gameObject);
			}

			currentLine = null;
			lineStart = null;
			lineEnd = null;

			return;
		}

		(bool successful, GameObject hitObject) rayCast = RayCaster.Instance.GetHitObject();

		if (!currentLine)
		{
			if (!rayCast.successful)
			{
				return;
			}

			if (rayCast.hitObject.CompareTag("LogicGateOutput"))
			{
				lineStart = rayCast.hitObject.GetComponent<Pin>();
				currentLine = Instantiate(line, lineStart.transform).GetComponent<Line>();

				lineStart.SetOutline(true);
			}
			else if (rayCast.hitObject.CompareTag("LogicGateInput"))
			{
				Pin pin = rayCast.hitObject.GetComponent<Pin>();

				if (pin.Lines.Count <= 0)
					return;

				currentLine = pin.Lines[0];
				lineStart = currentLine.LineStart;
				lineStart.Lines.Remove(currentLine);
				pin.ClearLines();
				currentLine.Disconnect();
			}
		}

		if (currentLine)
		{
			if (rayCast.successful &&
			    rayCast.hitObject != lineStart.gameObject &&
			    rayCast.hitObject.CompareTag("LogicGateInput"))
			{
				Pin pin = rayCast.hitObject.GetComponent<Pin>();

				if (pin != lineEnd && lineStart.CanConnect(pin))
				{
					oldLine = pin.Lines.Count > 0 ? pin.Lines[0] : null;

					lineEnd = pin;
					lineEnd.SetOutline(true);
				}
			}
			else if (lineEnd)
			{
				lineEnd.SetOutline(false);
				lineEnd = null;
			}

			Vector3 projectedTouchPosition;
			if (!lineEnd)
			{
				Vector2 touchPosition = SparkInput.GetTouchPosition();
				projectedTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y,
					(Camera.main.transform.position - gameObject.transform.position).magnitude +
					Camera.main.nearClipPlane));
			}
			else
			{
				projectedTouchPosition = lineEnd.transform.position;
			}

			Vector3[] points = { lineStart.transform.position, projectedTouchPosition };
			currentLine.SetPositions(points);
		}
	}
}
