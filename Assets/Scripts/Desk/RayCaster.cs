using UnityEngine;

public class RayCaster : MonoBehaviour
{
	public static RayCaster Instance { get; private set; }

	Camera mainCamera;
	GameObject hitObject;
	bool isCached = false;

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			mainCamera = Camera.main;
		}
	}

	(bool successful, GameObject hitObject) CalculateHitObject()
	{
		Vector2 touchPosition = Input.GetTouch(0).position;

		Ray ray = mainCamera.ScreenPointToRay(touchPosition);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			hitObject = hit.collider.gameObject;
			isCached = true;
		}

		return (isCached, hitObject);
	}

	public (bool successful, GameObject hitObject) GetHitObject() => isCached ? (true, hitObject) : CalculateHitObject();

	void LateUpdate()
	{
		if (isCached)
		{
			hitObject = null;
			isCached = false;
		}
	}
}
