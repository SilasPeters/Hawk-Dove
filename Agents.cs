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
	public List<Tuple<float,float>> history;
	public int historyRange;
	const float aggressivenessIncrease = 0.01f;

	public HistoryBasedAgent(float aggression, int hr) { aggressiveness = aggression; historyRange = hr; history = new(); }
	public override float ChanceHawk(Random r)
	{

		List<float> increasedScores = new();
		List<float> decreasedScores = new();
		// do something for each known sample withing the range
		if (history.Count >= historyRange)
		{
            for (int i = Math.Max(0, history.Count - historyRange - 1); i < history.Count; i++)
            {
                if (i > 0)
                {
                    if (history[i - 1].Item1 <= history[i].Item1)
                        increasedScores.Add(history[i].Item2);
                    else
                        decreasedScores.Add(history[i].Item2);
                }
            }
        }
		else
		{
			if (history.Any())
			{
				if (history.Last().Item2 < 0f) { if (aggressiveness + aggressivenessIncrease <= 1f) return aggressiveness += aggressivenessIncrease; }
				else { if (aggressiveness - aggressivenessIncrease >= 0f) return aggressiveness -= aggressivenessIncrease; }
			}
		}
		float averageIncreasedScore = increasedScores.Any() ? increasedScores.Average() : 0;
		float averageDecreasedScore = decreasedScores.Any() ? decreasedScores.Average() : 0;
		if(averageIncreasedScore >= averageDecreasedScore) { if(aggressiveness + aggressivenessIncrease <= 1f) return aggressiveness+=aggressivenessIncrease; }
		if(averageDecreasedScore > averageIncreasedScore) { if(aggressiveness - aggressivenessIncrease >= 0f) return aggressiveness-=aggressivenessIncrease; }
		return aggressiveness;
    }
}

public enum Stance
{
	Hawk,
	Dove
}
