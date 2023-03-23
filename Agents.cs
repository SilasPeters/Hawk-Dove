using System.Drawing;

namespace Hawk_Dove;

public abstract class Agent
{
	public          Stance Stance { get; protected set; }
	public abstract double  ChanceHawk(Random r);
	
	public virtual  void   SetNewStance(Random r)
		=> Stance = r.NextSingle() < ChanceHawk(r)
			? Stance.Hawk : Stance.Dove;
}

internal class NeutralAgent : Agent
{
    public override double ChanceHawk(Random r) => .50f;
}
internal class AggressiveAgent : Agent
{
	public override double ChanceHawk(Random r) => .75f;
}
internal class PeacefulAgent : Agent
{
	public override double ChanceHawk(Random r) => .25f;
}

internal class FlexibleAgent : Agent
{
	public double chanceRate;
    public override double ChanceHawk(Random r) => chanceRate;
}

internal class ResponseAgent : Agent
{
    public override double ChanceHawk(Random r)
    {
        throw new NotImplementedException();
    }
}

internal class HistoryBasedAgent : Agent
{
	public double aggressiveness;
	public List<Tuple<double,double>> history;
	public int historyRange;
	const double aggressivenessIncrease = 0.01f;

	public HistoryBasedAgent(double aggression, int hr) { aggressiveness = aggression; historyRange = hr; history = new(); }
	public override double ChanceHawk(Random r)
	{

		List<double> increasedScores = new();
		List<double> decreasedScores = new();
		
		// Count the scores that have increased or decreased
		if (history.Count >= historyRange)
		{
            for (int i = Math.Max(0, history.Count - historyRange - 1); i < history.Count; i++) // For the last X samples, where X = historyRange
            {
                if (i > 0)
                {
	                var prev = history[i - 1].Item1;
	                var curr = history[i].Item1;
                    if (prev < curr)
                        increasedScores.Add(history[i].Item2);
                    else if (curr < prev)
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
		double averageIncreasedScore = increasedScores.Any() ? increasedScores.Average() : 0;
		double averageDecreasedScore = decreasedScores.Any() ? decreasedScores.Average() : 0;
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
