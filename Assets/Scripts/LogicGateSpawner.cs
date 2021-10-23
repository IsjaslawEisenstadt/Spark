using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LogicGateSpawner : MonoBehaviour
{
	public ARTrackedImageManager imageManager;

	public List<LogicGateEntry> logicGatePrefabs;

	public void OnEnable()
	{
		imageManager.trackedImagesChanged += OnImageChanged;
	}

	public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
	{
		foreach (var trackedImage in args.added)
		{
			var go = Instantiate(logicGatePrefabs.Find(x => x.key.Equals(trackedImage.referenceImage.name)).value,
				trackedImage.transform, true);

			Debug.Log("found marker, position: " + go.transform.position + ", image pos: " +
			          trackedImage.transform.position);
		}
	}
}

[Serializable]
public class LogicGateEntry
{
	public string key;
	public GameObject value;
}