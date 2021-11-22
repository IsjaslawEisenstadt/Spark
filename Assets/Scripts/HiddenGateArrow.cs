using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Viewport
{
    FullScreen,
    Left,
    UpperLeft
}

public class HiddenGateArrow : MonoBehaviour
{
    public static HiddenGateArrow Instance { get; private set; }

    public GameObject arrow;
    public float borderSizeInPercent;

    GameObject target;
    Camera camera;
    RectTransform viewportRectTransform;
    RectTransform arrowRectTransform;

    float yBorder;
    float xBorder;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            viewportRectTransform = gameObject.GetComponent<RectTransform>();
            arrowRectTransform = arrow.GetComponent<RectTransform>();
            camera = Camera.main;
        }
    }

    void Update()
    {
        Vector3 targetScreenPosition = camera.WorldToScreenPoint(target.transform.position);

        bool isOffScreen = IsOffScreen(targetScreenPosition);
        Debug.Log("isOffScreen: " + isOffScreen);
        if (isOffScreen)
        {
            RotatePointer(targetScreenPosition);
        }

        arrow.SetActive(isOffScreen);
    }

    public void Activate(GameObject baseGate)
    {
        target = baseGate;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    //Is not used yet -> should be done in new UIManager
    public void SetViewport(Viewport viewport)
    {
        switch (viewport)
        {
            case (Viewport.FullScreen):
                {
                    viewportRectTransform.offsetMin = new Vector2(0, 0);
                    break;
                }
            case (Viewport.Left):
                {
                    viewportRectTransform.offsetMin = new Vector2(550, 0);
                    break;
                }
            case (Viewport.UpperLeft):
                {
                    viewportRectTransform.offsetMin = new Vector2(550, 300);
                    break;
                }
        }

        viewportRectTransform.offsetMax = new Vector2(0, 0);

        xBorder = viewportRectTransform.rect.width * borderSizeInPercent;
        yBorder = viewportRectTransform.rect.height * borderSizeInPercent;
    }

    private bool IsOffScreen(Vector3 targetScreenPosition)
    {
        Debug.Log("targetScreenPosition: " + targetScreenPosition);
        Debug.Log("viewportRectTransform.position: " + viewportRectTransform.position);

        float left = viewportRectTransform.position.x - viewportRectTransform.rect.width / 2;
        float right = viewportRectTransform.position.x + viewportRectTransform.rect.width / 2;
        float top = viewportRectTransform.position.y + viewportRectTransform.rect.height / 2;
        float bottom = viewportRectTransform.position.y - viewportRectTransform.rect.height / 2;
        Debug.Log("left: " + left);
        Debug.Log("right: " + right);
        Debug.Log("top: " + top);
        Debug.Log("bottom: " + bottom);
        return  left > targetScreenPosition.x ||
                right < targetScreenPosition.x ||
                top < targetScreenPosition.y ||
                bottom > targetScreenPosition.y;
    }

    //Does not rotet correctly yet
    private void RotatePointer(Vector3 targetScreenPosition)
    {
        Vector3 direction = (targetScreenPosition - viewportRectTransform.position).normalized;
        float rad = Mathf.Acos(Vector3.Dot(direction, new Vector3(0, 1, 0)));
        arrowRectTransform.localEulerAngles = new Vector3(0, 0, (rad / 2 * Mathf.PI) * 360);
    }
}
