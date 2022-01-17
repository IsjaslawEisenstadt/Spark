using UnityEngine;
using UnityEngine.UI;

public class Blocker : MonoBehaviour
{
	Image imageComponent;

	void Awake()
	{
		imageComponent = GetComponent<Image>();
		((TutorialInfo)UIManager.Instance.GetPopup(PopupType.TutorialInfo).script).OnVisibilityChanged += (visible) => imageComponent.enabled = !visible;
	}
}
