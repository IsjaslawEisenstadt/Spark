using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePlanes : MonoBehaviour
{
    private Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void visualizePlanes()
    {
        List<GameObject> planes = new List<GameObject>(GameObject.FindGameObjectsWithTag("DetectedPlane"));
        planes.ForEach(x => x.GetComponent<MeshRenderer>().enabled = toggle.isOn);
        //Problem lösen mit einer static class. Dort muss sich jede Plan beim erzeugen regstrieren und bei einem onDestroy austragen. Dort wird mit statischer Methode über alle 
        //Planes gegangen und enabled gesetzt, bzw neue planes können aktuellen state auslesen
    }
}
