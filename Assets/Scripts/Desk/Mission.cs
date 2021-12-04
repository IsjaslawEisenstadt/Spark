using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Mission", order = 1)]
public class Mission : ScriptableObject
{
	public AbstractGate gateScript;

	[field: SerializeField] List<MissionEntry> GateRestrictionEntries { get; set; }

	Dictionary<GateType, int> gateRestriction;

	public Dictionary<GateType, int> GateRestriction
	{
		get
		{
			if (gateRestriction == null)
			{
				gateRestriction = new Dictionary<GateType, int>(GateRestrictionEntries.Count);
				foreach (MissionEntry entry in GateRestrictionEntries)
				{
					gateRestriction.Add(entry.gateType, entry.count);
				}
			}

			return gateRestriction;
		}
		private set => gateRestriction = value;
	}
}

[Serializable]
public struct MissionEntry
{
	public GateType gateType;
	public int count;
}
