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
			(bool successful, GameObject hitObject) = RayCaster.Instance.GetHitObject();

			if (successful && (hitObject.CompareTag("LogicGate") || hitObject.CompareTag("Source") ||
			                   hitObject.CompareTag("Sink")))
			{
				// A raycast will hit the child Box of an AbstractGate, hence why we need to get its parent and then
				// the parent of the AbstractGate to get the BaseGate
				GameObject baseGate = hitObject.transform.parent.parent.gameObject;
				gateDrawer.Open(baseGate);
				HiddenGateArrow.Instance.Activate(baseGate);
				PinSettings.Instance.SetVisualizationState(hitObject);
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
