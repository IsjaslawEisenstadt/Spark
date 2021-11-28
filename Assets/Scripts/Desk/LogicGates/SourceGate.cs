public class SourceGate : AbstractGate
{
	void Awake()
	{
		outputs[0].State = Evaluate(null)[0];
	}

	protected override bool[] Evaluate(bool[] values)
	{
		return new[] { true }; // TODO: implement source tooltip toggle / mission mode source
	}
}