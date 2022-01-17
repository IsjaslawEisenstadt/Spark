using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PopupType
{
	ResultView,
	GateDrawer,
	PinSettings,
	HiddenGateArrow,
	MissionInfo,
	HUD,
	TutorialReset,
	TutorialInfo
	RewardWindow
}

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }

	[SerializeField] public List<Popup> uiElementList;

	Dictionary<PopupType, Popup> typeToPopup;

	public void TypeToPopupTest()
	{
		Debug.Log("test");
	}

	void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		else
		{
			Instance = this;

			typeToPopup = new Dictionary<PopupType, Popup>();
			uiElementList.ForEach(entry =>
			{
				entry.script = entry.popup.GetComponent<IUIElement>();
				typeToPopup.Add(entry.popupType, entry);
			});
		}
	}

	public void Open(PopupType popup)
	{
		if (!typeToPopup.ContainsKey(popup))
			return;

		typeToPopup[popup].popup.SetActive(true);
	}

	public void Open(string popup) => Open(typeToPopup.Keys.FirstOrDefault(x => x.ToString() == popup));

	public Popup GetPopup(PopupType popup) => typeToPopup[popup];

	/// <summary>
	/// A raycast will hit the child Box of an AbstractGate, hence why we need to get its parent and then
	/// the parent of the AbstractGate to get the BaseGate
	/// </summary>
	public void OnGateSelected()
	{
		typeToPopup[PopupType.GateDrawer].script.OnOpen();
		typeToPopup[PopupType.HiddenGateArrow].script.OnOpen();
		typeToPopup[PopupType.PinSettings].script.OnOpen();
	}

	public void OnNoGateSelected()
	{
		typeToPopup[PopupType.GateDrawer].script.OnClose();
		typeToPopup[PopupType.HiddenGateArrow].script.OnClose();
		typeToPopup[PopupType.PinSettings].script.OnClose();
	}

	public void SetViewport(Viewport viewport) => ((HiddenGateArrow)GetPopup(PopupType.HiddenGateArrow).script).SetViewport(viewport);
}

[Serializable]
public class Popup
{
	public PopupType popupType;
	public GameObject popup;
	[HideInInspector] public IUIElement script;
}
