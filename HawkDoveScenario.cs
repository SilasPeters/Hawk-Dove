namespace Hawk_Dove;

internal class HawkDoveScenario
{
	public IResource Resource      { get; }
	public Tuple<HistoryBasedAgent, HistoryBasedAgent>   Agents        { get; }
	public int     ConflictCosts { get; }

	public HawkDoveScenario(IResource resource, Tuple<HistoryBasedAgent, HistoryBasedAgent> agents, int conflictCosts)
	{
		Resource      = resource;
		Agents        = agents;
		ConflictCosts = conflictCosts;
	}

	public Tuple<int,int> Run(Random random)
	{
		Agents.Item1.SetNewStance(random);
		Agents.Item2.SetNewStance(random);
		return GenerateOutcome(Agents.Item1,Agents.Item2, Resource,ConflictCosts);
	}

	private static IResource Interact(Agent a, Agent b, IResource resource, int conflictCosts)
		=> a.Stance == Stance.Dove
			? b.Stance == Stance.Dove
				? resource.WasShared(a, b)
				: resource.WasConquered(b, a)
			: b.Stance == Stance.Hawk
				? resource.WasConquered(a, b)
				: resource.WasConflicted(a, b, conflictCosts);

	private Tuple<int,int> GenerateOutcome(Agent a, Agent b, IResource resource, int conflictCosts)
	{
		return 
		a.Stance == Stance.Dove
            ? b.Stance == Stance.Dove
                ? Tuple.Create(resource.GetValue()/2, resource.GetValue() / 2)
                : Tuple.Create(0, resource.GetValue())
            : b.Stance == Stance.Hawk
				? Tuple.Create((resource.GetValue()/2) - conflictCosts, (resource.GetValue() / 2) * conflictCosts)
                : Tuple.Create(resource.GetValue(),0);
	}

}