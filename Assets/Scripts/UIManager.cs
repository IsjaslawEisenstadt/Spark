using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }

	public List<GameObject> uiElementList;

	Dictionary<string, GameObject> uiElementDict;

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
		uiElementDict = new Dictionary<string, GameObject>();
		uiElementList.ForEach(x => uiElementDict.Add(x.name, x));
	}

	public void Open(string uiElement)
	{
		uiElementDict[uiElement].SetActive(true);
	}

	public GameObject GetElement(string element)
	{
		return uiElementDict[element];
	}
}
