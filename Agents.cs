namespace Hawk_Dove;

public abstract class Agent
{
	public          Stance Stance { get; protected set; }
	public abstract float  ChanceHawk(Random r);
	
	public virtual  void   SetNewStance(Random r)
		=> Stance = r.NextSingle() < ChanceHawk(r)
			? Stance.Hawk : Stance.Dove;
}

internal class NeutralAgent : Agent
{
    public override float ChanceHawk(Random r) => .50f;
}
internal class AggressiveAgent : Agent
{
	public override float ChanceHawk(Random r) => .75f;
}
internal class PeacefulAgent : Agent
{
	public override float ChanceHawk(Random r) => .25f;
}

internal class FlexibleAgent : Agent
{
	public float chanceRate;
    public override float ChanceHawk(Random r) => chanceRate;
}

internal abstract class ResponsiveAgent : Agent
{
	private IList<Stance> _stances;
	public override float ChanceHawk(Random r)
		=> throw new NotImplementedException("This method will return a chance" +
		                                     "based on the history of the conflicts");
}

public enum Stance
{
	Hawk,
	Dove
}