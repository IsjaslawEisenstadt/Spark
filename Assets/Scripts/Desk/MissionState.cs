using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class MissionState : MonoBehaviour
{
    public static MissionState Instance { get; private set; }

    public event Action<Dictionary<GateType, GateAvailability>> OnAvailabilityChanged;
    public Dictionary<GateType, GateAvailability> GateAvailability {get; private set;}

    private Dictionary<BaseGate, GateType> currentGateTypes;

	void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

    void Start()
    {
        Dictionary<GateType, int> restrictions = CurrentMission.missions[CurrentMission.currentMissionIndex].GateRestriction;
        foreach (GateType type in restrictions.Keys)
        {
            GateAvailability.Add(type, new GateAvailability(0, restrictions[type]));
        }

        UIManager.Instance.GetElement(PopupType.GateDrawer).GetComponent<GateDrawer>().onGateTypeChanged += refreshMissionState;
    }

    public void refreshMissionState(BaseGate baseGate)
    {
        GateType baseGateType = baseGate.ActiveGateScript.GateType;
        
        if (!GateAvailability[baseGateType].available)
        {
            Debug.LogError("Selected unallowed gate!");
            return;
        }

        GateAvailability gateAvailability;

        if (currentGateTypes.ContainsKey(baseGate))
        {
            gateAvailability = GateAvailability[currentGateTypes[baseGate]];
            gateAvailability.currentCount--;
            gateAvailability.available = true;
        }

        currentGateTypes[baseGate] = baseGateType;
        gateAvailability = GateAvailability[baseGateType];
        gateAvailability.currentCount++;
        gateAvailability.available = gateAvailability.currentCount < gateAvailability.max;

        OnAvailabilityChanged?.Invoke(GateAvailability);
    }
}

public class GateAvailability
{
    public int currentCount;
    public int max;
    public bool available = true;

    public GateAvailability(int currentCount, int max)
    {
        this.currentCount = currentCount;
        this.max = max;
    }
}
