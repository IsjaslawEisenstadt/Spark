using System;
using System.Linq;

public class StudyGateDrawer : GateDrawer
{
    void Awake() => Enum.GetValues(typeof(GateType))
                        .Cast<GateType>()
                        .ToList()
                        .ForEach((entry) => GenerateGateButton(entry.ToString()));
}
