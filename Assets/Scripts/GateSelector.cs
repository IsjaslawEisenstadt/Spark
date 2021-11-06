using UnityEngine;
using UnityEngine.EventSystems;

public class GateSelector : MonoBehaviour
{
	public GameObject gateDrawer;

	GateDrawer gateDrawerScript;

	void Awake() => gateDrawerScript = gateDrawer.GetComponent<GateDrawer>();
	
	void Update()
	{
		if (Input.touchCount <= 0 || EventSystem.current.IsPointerOverGameObject(0))
		{
			return;
		}

		Touch touchEvent = Input.GetTouch(0);

		if (touchEvent.phase == TouchPhase.Ended)
		{
			Vector2 touchPosition = touchEvent.position;
			Ray ray = Camera.main.ScreenPointToRay(touchPosition);

			if (Physics.Raycast(ray, out RaycastHit hit) && hit.rigidbody.gameObject.CompareTag("LogicGate"))
			{
				gateDrawerScript.Open(hit.rigidbody.transform.parent.gameObject);
			}
			else
			{
				gateDrawerScript.Close();
			}
		}
	}
}
