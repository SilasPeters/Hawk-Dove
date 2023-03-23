namespace Hawk_Dove;

internal class HawkDoveScenario
{
	public IResource Resource      { get; }
	public Tuple<HistoryBasedAgent, HistoryBasedAgent>   Agents        { get; }
	public double     ConflictCosts { get; }

	public HawkDoveScenario(IResource resource, Tuple<HistoryBasedAgent, HistoryBasedAgent> agents, double conflictCosts)
	{
		Resource      = resource;
		Agents        = agents;
		ConflictCosts = conflictCosts;
	}

	public Tuple<double,double> Run(Random random)
	{
		Agents.Item1.SetNewStance(random);
		Agents.Item2.SetNewStance(random);
		return GenerateOutcome(Agents.Item1,Agents.Item2, Resource,ConflictCosts);
	}

	private static IResource Interact(Agent a, Agent b, IResource resource, double conflictCosts)
		=> a.Stance == Stance.Dove
			? b.Stance == Stance.Dove
				? resource.WasShared(a, b)
				: resource.WasConquered(b, a)
			: b.Stance == Stance.Hawk
				? resource.WasConquered(a, b)
				: resource.WasConflicted(a, b, conflictCosts);

	private Tuple<double,double> GenerateOutcome(Agent a, Agent b, IResource resource, double conflictCosts)
	{
		return 
		a.Stance == Stance.Dove
            ? b.Stance == Stance.Dove
                ? Tuple.Create(0.5d * resource.GetValue(), 0.5d * resource.GetValue())
                : Tuple.Create(0d, resource.GetValue())
            : b.Stance == Stance.Hawk
				? Tuple.Create(0.5d * resource.GetValue() - conflictCosts, 0.5d * resource.GetValue() - conflictCosts)
                : Tuple.Create(resource.GetValue(),0d);
	}

}