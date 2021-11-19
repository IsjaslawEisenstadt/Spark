using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateInformation : MonoBehaviour
{
    Camera camera;

    TMP_Text[] gateNames;
    public float offsetValue;

    void Awake()
    {
        gateNames = GetComponentsInChildren<TMP_Text>();
    }

    void Start()
    {
        camera = Camera.main;
        UpdatePosition(false);
    }

    void Update() => UpdatePosition(true);

    public void ShowGateInformation(bool show, string gateName)
    {
        gameObject.SetActive(show);
        SetGateName(gateName);
    }

    void SetGateName(string gateName)
    {
        // This is the table heading.
        gateNames[1].SetText(gateName);
    }

    private void UpdatePosition(bool lerpPosition)
    {
        Vector3 offset = Vector3.Cross(camera.transform.right, (camera.transform.position - transform.position)).normalized * offsetValue;
        transform.position = lerpPosition ? Vector3.Lerp(transform.position, transform.parent.position + offset, 0.5f) : transform.parent.position + offset;
        transform.LookAt(camera.transform.position);
    }
}
