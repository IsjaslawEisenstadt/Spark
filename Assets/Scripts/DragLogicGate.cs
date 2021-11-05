using UnityEngine;

public class DragLogicGate : MonoBehaviour
{
	public GameObject gateDrawer;

	void Update()
	{
		if (Input.touchCount <= 0)
		{
			return;
		}
		
		Vector2 touchPosition = Input.GetTouch(0).position;

		Ray ray = Camera.main.ScreenPointToRay(touchPosition);

		if (Physics.Raycast(ray, out RaycastHit hit) && hit.rigidbody.gameObject.CompareTag("LogicGate"))
		{
			gateDrawer.SetActive(true);
		}
	}
}
