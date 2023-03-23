namespace Hawk_Dove;

internal interface IResource
{
	public int GetValue();
	public IResource WasShared(Agent a, Agent b);
	public IResource WasConflicted(Agent a, Agent b, int conflictCosts);
	public IResource WasConquered(Agent winner, Agent loser);
}

internal class DefaultResource : IResource
{
	private int _value;

	public DefaultResource(int value)
		=> _value = value;

	public int GetValue()
		=> _value;

	// TODO: a IResource does not yet represent who gets what value.
	public IResource WasShared(Agent a, Agent b)
		=> new DefaultResource(_value/2);

	public IResource WasConflicted(Agent a, Agent b, int conflictCosts)
		=> new DefaultResource((_value / 2) - conflictCosts);

	public IResource WasConquered(Agent winner, Agent loser)
		=> new DefaultResource(_value);
}