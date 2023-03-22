using System.Drawing;

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

internal class ResponseAgent : Agent
{
    public override float ChanceHawk(Random r)
    {
        throw new NotImplementedException();
    }
}

internal class HistoryBasedAgent : Agent
{
	public float aggressiveness;
	public List<Sample> history;
	public int historyRange;
	const float aggressivenessIncrease = 0.5f;

	public HistoryBasedAgent(float aggression) { aggressiveness = aggression; history = new(); }
	public override float ChanceHawk(Random r)
	{

		List<float> increasedScores = new();
		List<float> decreasedScores = new();
		// do something for each known sample withing the range
		for(int i = history.Count - historyRange - 1; i < history.Count; i++)
		{
			if(i > 0)
			{
				if (history[i - 1].agrressiveness <= history[i].agrressiveness)
					increasedScores.Add(history[i].score);
				else
					decreasedScores.Add(history[i].score);
			}
		}
		float averageIncreasedScore = increasedScores.Average();
		float averageDecreasedScore = decreasedScores.Average();
		if(averageIncreasedScore > averageDecreasedScore) { return aggressiveness+=aggressivenessIncrease; }
		if(averageDecreasedScore > averageIncreasedScore) { return aggressiveness-=aggressivenessIncrease; }
		return aggressiveness;
	}
}

public enum Stance
{
	Hawk,
	Dove
}

struct Sample
{
	public float agrressiveness;
	public float score;
}