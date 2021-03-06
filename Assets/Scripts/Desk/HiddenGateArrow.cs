using UnityEngine;

public enum Viewport
{
	PinSettingOn,
	PinSettingsOff
}

public class HiddenGateArrow : MonoBehaviour, IUIElement
{
	public GateSelector gateSelector;

	public GameObject arrow;
	public float borderSize;

	GameObject target;

	Camera mainCamera;
	RectTransform viewportRectTransform;
	RectTransform arrowRectTransform;

	(float left, float right, float top, float bottom) viewportBorders;

	void Awake()
	{
		viewportRectTransform = gameObject.GetComponent<RectTransform>();
		arrowRectTransform = arrow.GetComponent<RectTransform>();
		mainCamera = Camera.main;
	}

	void Start() => SetViewport(Viewport.PinSettingsOff);

	void Update()
	{
		Vector3 targetScreenPosition = mainCamera.WorldToScreenPoint(target.transform.position);

		bool isOffScreen = IsOffScreen(targetScreenPosition);
		if (isOffScreen)
		{
			CalculateArrowPosition(targetScreenPosition);
			RotatePointer(targetScreenPosition);
		}

		arrow.SetActive(isOffScreen);
	}

	public void OnOpen()
	{
		target = gateSelector.CurrentSelectedObject.transform.parent.parent.gameObject;
		gameObject.SetActive(true);
	}


	public void OnClose() => gameObject.SetActive(false);

	public void SetViewport(Viewport viewport)
	{
		switch (viewport)
		{
			case Viewport.PinSettingOn: viewportRectTransform.offsetMin = new Vector2(viewportRectTransform.offsetMin.x, 350); break;
			case Viewport.PinSettingsOff: viewportRectTransform.offsetMin = new Vector2(viewportRectTransform.offsetMin.x, 0); break;
		}

		viewportBorders = CalculateViewportBorders();
	}

	bool IsOffScreen(Vector3 targetScreenPosition)
	{
		return 	viewportBorders.left > targetScreenPosition.x ||
				viewportBorders.right < targetScreenPosition.x ||
				viewportBorders.top < targetScreenPosition.y ||
				viewportBorders.bottom > targetScreenPosition.y;
	}

	void RotatePointer(Vector3 targetScreenPosition)
	{
		Vector3 direction = (targetScreenPosition - arrowRectTransform.position).normalized;
		float rad = Mathf.Acos(Vector3.Dot(direction, new Vector3(1, 0, 0)));

		if (direction.y < 0)
		{
			rad *= -1;
		}
		else if (direction.y == 0)
		{
			rad = direction.x > 0 ? 0 : Mathf.PI;
		}

		arrowRectTransform.localEulerAngles = new Vector3(0, 0, (rad / (2 * Mathf.PI)) * 360);
	}

	(float left, float right, float top, float bottom) CalculateViewportBorders()
	{
		Vector3 position = viewportRectTransform.position;
		Rect rect = viewportRectTransform.rect;
		float left = position.x - rect.width / 2;
		float right = position.x + rect.width / 2;
		float top = position.y + rect.height / 2;
		float bottom = position.y - rect.height / 2;
		return (left, right, top, bottom);
	}

	void CalculateArrowPosition(Vector3 targetScreenPosition)
	{
		Vector3 arrowScreenPosition = targetScreenPosition;
		if (arrowScreenPosition.x <= viewportBorders.left + borderSize)
			arrowScreenPosition.x = viewportBorders.left + borderSize;
		if (arrowScreenPosition.x >= viewportBorders.right - borderSize)
			arrowScreenPosition.x = viewportBorders.right - borderSize;
		if (arrowScreenPosition.y <= viewportBorders.bottom + borderSize)
			arrowScreenPosition.y = viewportBorders.bottom + borderSize;
		if (arrowScreenPosition.y >= viewportBorders.top - borderSize)
			arrowScreenPosition.y = viewportBorders.top - borderSize;

		arrowRectTransform.position = arrowScreenPosition;
	}
}
