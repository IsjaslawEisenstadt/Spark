public class MissionMode : PlayMode
{
	protected override void EvaluatePlay(TruthTableRow[] result)
	{
		UIManager.Instance.Open("ResultTruthtable");
	}
}

public static class CurrentMission
{
	public static Mission currentMission;
}
