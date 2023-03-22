namespace Hawk_Dove;

internal class HawkDoveScenario
{
	public IResource Resource      { get; }
	public Tuple<HistoryBasedAgent, HistoryBasedAgent>   Agents        { get; }
	public float     ConflictCosts { get; }

	public HawkDoveScenario(IResource resource, Tuple<HistoryBasedAgent, HistoryBasedAgent> agents, float conflictCosts)
	{
		Resource      = resource;
		Agents        = agents;
		ConflictCosts = conflictCosts;
	}

	public Tuple<float,float> Run(Random random)
	{
		Agents.Item1.SetNewStance(random);
		Agents.Item2.SetNewStance(random);
		return GenerateOutcome(Agents.Item1,Agents.Item2, Resource,ConflictCosts);
	}

	private static IResource Interact(Agent a, Agent b, IResource resource, float conflictCosts)
		=> a.Stance == Stance.Dove
			? b.Stance == Stance.Dove
				? resource.WasShared(a, b)
				: resource.WasConquered(b, a)
			: b.Stance == Stance.Hawk
				? resource.WasConquered(a, b)
				: resource.WasConflicted(a, b, conflictCosts);

	private Tuple<float,float> GenerateOutcome(Agent a, Agent b, IResource resource, float conflictCosts)
	{
		return 
		a.Stance == Stance.Dove
            ? b.Stance == Stance.Dove
                ? Tuple.Create(0.5f * resource.GetValue(), 0.5f * resource.GetValue())
                : Tuple.Create(0f, resource.GetValue())
            : b.Stance == Stance.Hawk
				? Tuple.Create(0.5f * resource.GetValue() - conflictCosts, 0.5f * resource.GetValue() - conflictCosts)
                : Tuple.Create(resource.GetValue(),0f);
	}

}