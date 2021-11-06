using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public List<ObjectContainer> gameObjectList;

    void Awake()
    {
        gameObjectList.ForEach(x => x.gameObject.SetActive(x.active));
    }
}

[Serializable]
public class ObjectContainer
{
    public GameObject gameObject;
    public bool active;
}
