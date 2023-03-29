namespace Hawk_Dove;

internal class HawkDoveScenario
{
	public Agent Agent1        { get; }
    public Agent Agent2        { get; }
    public int   Resource      { get; }
	public int   ConflictCosts { get; }

	public HawkDoveScenario(Agent a1, Agent a2, int r, int c)
	{
		Agent1 = a1;
		Agent2 = a2;
		Resource = r;
		ConflictCosts = c;
	}

	public (int, int) Run(Random random, int i)
	{
		Agent1.SetNewStance(random,Agent2, i);
		Agent2.SetNewStance(random,Agent1, i);

        return
        Agent1.Stance == Stance.Dove
            ? Agent2.Stance == Stance.Dove
                ? (Resource / 2, Resource / 2)
                : (0, Resource)
            : Agent2.Stance == Stance.Hawk
                ? ((Resource - ConflictCosts) / 2, (Resource - ConflictCosts) / 2)
                : (Resource, 0);
    }
}