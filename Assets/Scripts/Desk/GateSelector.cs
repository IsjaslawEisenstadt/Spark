using UnityEngine;
using UnityEngine.EventSystems;

public class GateSelector : MonoBehaviour
{
	public GateDrawer gateDrawer;

	void Update()
	{
		if (Input.touchCount <= 0 || EventSystem.current.IsPointerOverGameObject(0))
		{
			return;
		}

		Touch touchEvent = Input.GetTouch(0);

		if (touchEvent.phase == TouchPhase.Ended)
		{
			var rayCast = RayCaster.Instance.GetHitObject();

			if (rayCast.successful &&
			    (rayCast.hitObject.CompareTag("LogicGate") || rayCast.hitObject.CompareTag("Source")))
			{
				gateDrawer.Open(rayCast.hitObject.transform.parent.gameObject);
				HiddenGateArrow.Instance.Activate(rayCast.hitObject.transform.parent.gameObject);
				PinSettings.Instance.SetVisualizationState(rayCast.hitObject);
			}
			else
			{
				gateDrawer.Close();
				HiddenGateArrow.Instance.Deactivate();
				PinSettings.Instance.Close();
			}
		}
	}
}
