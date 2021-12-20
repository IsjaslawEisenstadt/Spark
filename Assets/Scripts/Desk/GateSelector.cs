using UnityEngine;
using UnityEngine.EventSystems;

public class GateSelector : MonoBehaviour
{
	public GameObject CurrentSelectedObject { get; private set; }

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
				CurrentSelectedObject = hitObject;
				UIManager.Instance.OnGateSelected();
			}
			else
				UIManager.Instance.OnNoGateSelected();
		}
	}
}
