namespace Hawk_Dove;

internal class HawkDoveScenario
{
	public IResource Resource      { get; }
	public Agent[]   Agents        { get; }
	public float     ConflictCosts { get; }
	
	private readonly int _agentCount;

	public HawkDoveScenario(IResource resource, Agent[] agents, float conflictCosts)
	{
		Resource      = resource;
		Agents        = agents;
		ConflictCosts = conflictCosts;
		_agentCount   = agents.Length;
	}

	public float Run(Random random)
	{
		foreach (var agent in Agents)
			agent.SetNewStance(random);

		float outcome = 0; // 0 means no change in value
		for (int j = 0; j < _agentCount - 1; j++)
			for (int i = j + 1; i < _agentCount; i++)
				outcome += Interact(Agents[i], Agents[j], Resource, ConflictCosts).GetValue();
				

		return outcome;
	}

	private static IResource Interact(Agent a, Agent b, IResource resource, float conflictCosts)
		=> a.Stance == Stance.Dove
			? b.Stance == Stance.Dove
				? resource.WasShared(a, b)
				: resource.WasConquered(b, a)
			: b.Stance == Stance.Hawk
				? resource.WasConquered(a, b)
				: resource.WasConflicted(a, b, conflictCosts);
}