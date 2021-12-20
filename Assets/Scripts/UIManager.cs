using System;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType
{
	ResultView,
	GateDrawer,
	PinSettings,
	HiddenGateArrow
}

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }

	[SerializeField] public List<Popup> uiElementList;

	Dictionary<PopupType, GameObject> typeToObject;
	Dictionary<PopupType, IUIElement> typeToScript;

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	void Start()
	{
		typeToObject = new Dictionary<PopupType, GameObject>();
		typeToScript = new Dictionary<PopupType, IUIElement>();
		uiElementList.ForEach(entry =>
		{
			typeToObject.Add(entry.popupType, entry.popup);
			typeToScript.Add(entry.popupType, entry.script);
		});
	}

	public void Open(PopupType popup)
	{
		typeToObject[popup].SetActive(true);
	}

	public GameObject GetElement(PopupType popup)
	{
		return typeToObject[popup];
	}

	/// <summary>
	/// A raycast will hit the child Box of an AbstractGate, hence why we need to get its parent and then
	/// the parent of the AbstractGate to get the BaseGate
	/// </summary>
	public void OnGateSelected()
	{
		typeToScript[PopupType.GateDrawer].OnOpen();
		typeToScript[PopupType.HiddenGateArrow].OnOpen();
		typeToScript[PopupType.PinSettings].OnOpen();
	}


	public void OnNoGateSelected()
	{
		typeToScript[PopupType.GateDrawer].OnClose();
		typeToScript[PopupType.HiddenGateArrow].OnClose();
		typeToScript[PopupType.PinSettings].OnClose();
	}
}

[Serializable]
public class Popup
{
	public PopupType popupType;
	public GameObject popup;
	public IUIElement script;
}
