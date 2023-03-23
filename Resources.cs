namespace Hawk_Dove;

internal interface IResource
{
	public double GetValue();
	public IResource WasShared(Agent a, Agent b);
	public IResource WasConflicted(Agent a, Agent b, double conflictCosts);
	public IResource WasConquered(Agent winner, Agent loser);
}

internal class DefaultResource : IResource
{
	private double _value;

	public DefaultResource(double value)
		=> _value = value;

	public double GetValue()
		=> _value;

	// TODO: a IResource does not yet represent who gets what value.
	public IResource WasShared(Agent a, Agent b)
		=> new DefaultResource(_value * 0.5f);

	public IResource WasConflicted(Agent a, Agent b, double conflictCosts)
		=> new DefaultResource(_value * 0.5f - conflictCosts);

	public IResource WasConquered(Agent winner, Agent loser)
		=> new DefaultResource(_value);
}