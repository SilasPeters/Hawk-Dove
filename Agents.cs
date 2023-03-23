using System.Drawing;

namespace Hawk_Dove;

public abstract class Agent
{
	public          Stance Stance { get; protected set; }
	public abstract int  ChanceHawk(Random r);
	
	public virtual  void   SetNewStance(Random r)
		=> Stance = r.Next(0,100) < ChanceHawk(r)
			? Stance.Hawk : Stance.Dove;
}

internal class NeutralAgent : Agent
{
    public override int ChanceHawk(Random r) => 50;
}
internal class AggressiveAgent : Agent
{
	public override int ChanceHawk(Random r) => 75;
}
internal class PeacefulAgent : Agent
{
	public override int ChanceHawk(Random r) => 25;
}

internal class FlexibleAgent : Agent
{
	public int chanceRate;
    public override int ChanceHawk(Random r) => chanceRate;
}

internal class ResponseAgent : Agent
{
    public override int ChanceHawk(Random r)
    {
        throw new NotImplementedException();
    }
}

internal class HistoryBasedAgent : Agent
{
	public int aggressiveness;
	public List<Tuple<int,int>> history;
	public int historyRange;
	const int aggressivenessIncrease = 1;

	public HistoryBasedAgent(int aggression, int hr) { aggressiveness = aggression; historyRange = hr; history = new(); }
	public override int ChanceHawk(Random r)
	{

		List<int> increasedScores = new();
		List<int> decreasedScores = new();
		
		if (history.Count >= historyRange)
		{
			// Count the scores that have increased or decreased
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
		else if (history.Any())
		{
			if (history.Last().Item2 < 0) { if (aggressiveness + aggressivenessIncrease <= 1) return aggressiveness += aggressivenessIncrease; }
			else { if (aggressiveness - aggressivenessIncrease >= 0) return aggressiveness -= aggressivenessIncrease; }
		}
		
		int averageIncreasedScore = increasedScores.Any() ? (int)increasedScores.Average() : 0;
		int averageDecreasedScore = decreasedScores.Any() ? (int)decreasedScores.Average() : 0;
		if(averageIncreasedScore >= averageDecreasedScore && aggressiveness + aggressivenessIncrease <= 1f) return aggressiveness+=aggressivenessIncrease;
		if(averageDecreasedScore > averageIncreasedScore && aggressiveness - aggressivenessIncrease >= 0f) return aggressiveness-=aggressivenessIncrease;
		return aggressiveness;
    }
}

public enum Stance
{
	Hawk,
	Dove
}
