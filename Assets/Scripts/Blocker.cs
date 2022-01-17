using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocker : MonoBehaviour
{
	private Image imageComponent;

	void Awake()
	{
		imageComponent = GetComponent<Image>();
		((TutorialInfo)UIManager.Instance.GetPopup(PopupType.TutorialInfo).script).OnVisibilityChanged += (visible) => imageComponent.enabled = !visible;
	}
}
