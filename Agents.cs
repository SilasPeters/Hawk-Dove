using System.ComponentModel.DataAnnotations;
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
		List<int> neutralScore = new();

		if (history.Any())
		{
			if(history.Count > 1)
			{
                for (int i = 1; i < history.Count; i++)
                {
					if (history[i].Item2 > history[i-1].Item2)
						increasedScores.Add(history[i].Item2);
					else if (history[i].Item2 < history[i-1].Item2)
						decreasedScores.Add(history[i].Item2);
					else
						neutralScore.Add(history[i].Item2);
                }
            }
		}
		
		double averageIncreasedScore = increasedScores.Any() ? increasedScores.Average() : 0d;
        double averageDecreasedScore = decreasedScores.Any() ? decreasedScores.Average() : 0d;
        double averageNeutralScore   = neutralScore.Any()	 ? neutralScore.Average()    : 0d;

		List<double> scores = new() { averageIncreasedScore, averageDecreasedScore, averageNeutralScore };

		if(history.Count >= 100)
		{
			Console.Clear();
		}

        return scores.IndexOf(scores.Max()) switch
        {
            0 => aggressiveness += aggressivenessIncrease,
            1 => aggressiveness -= aggressivenessIncrease,
            _ => averageNeutralScore < 0 ? aggressiveness -= aggressivenessIncrease : aggressiveness,
        };
    }
}

public enum Stance
{
	Hawk,
	Dove
}
