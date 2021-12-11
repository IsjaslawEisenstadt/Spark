using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType
{
	ResultView
}

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }

	[SerializeField] public List<Popup> uiElementList;

	Dictionary<PopupType, GameObject> uiElementDict;

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
		uiElementDict = new Dictionary<PopupType, GameObject>();
		uiElementList.ForEach(entry => uiElementDict.Add(entry.popupType, entry.popup));
	}

	public void Open(PopupType popup)
	{
		uiElementDict[popup].SetActive(true);
	}

	public GameObject GetElement(PopupType popup)
	{
		return uiElementDict[popup];
	}
}

[Serializable]
public class Popup
{
	public GameObject popup;
	public PopupType popupType;
}
