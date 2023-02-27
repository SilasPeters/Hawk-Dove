namespace Hawk_Dove;

internal interface IResource
{
	public float GetValue();
	public IResource WasShared(Agent a, Agent b);
	public IResource WasConflicted(Agent a, Agent b, float conflictCosts);
	public IResource WasConquered(Agent winner, Agent loser);
}

internal class DefaultResource : IResource
{
	private float _value;

	public DefaultResource(float value)
		=> _value = value;

	public float GetValue()
		=> _value;

	// TODO: a IResource does not yet represent who gets what value.
	public IResource WasShared(Agent a, Agent b)
		=> new DefaultResource(_value * 0.5f);

	public IResource WasConflicted(Agent a, Agent b, float conflictCosts)
		=> new DefaultResource(_value * 0.5f - conflictCosts);

	public IResource WasConquered(Agent winner, Agent loser)
		=> new DefaultResource(_value);
}