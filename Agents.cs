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
	public List<Historic> history;
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
					if (history[i].Aggresion > history[i-1].Aggresion)
						increasedScores.Add(history[i].Score);
					else if (history[i].Aggresion < history[i-1].Aggresion)
						decreasedScores.Add(history[i].Score);
					else
						neutralScore.Add(history[i].Score);
                }
            }
		}
		
		double averageIncreasedScore = increasedScores.Any() ? increasedScores.Average() : 0d;
        double averageDecreasedScore = decreasedScores.Any() ? decreasedScores.Average() : 0d;
        double averageNeutralScore   = neutralScore.Any()	 ? neutralScore.Average()    : 0d;

		List<double> scores = new() { averageIncreasedScore, averageDecreasedScore, averageNeutralScore };

		//if(history.Count >= 100)
		//{
		//	Console.Clear();
		//}

        return scores.IndexOf(scores.Max()) switch
        {
            0 => Math.Min(aggressiveness += aggressivenessIncrease, 100),
            1 => Math.Max(aggressiveness -= aggressivenessIncrease, 0),
            _ => aggressiveness,
        };
    }
}

public struct Historic
{
	public int Aggresion;
	public int Score;

    public Historic(int agg, int score)
    {
        Aggresion = agg;
		Score = score;
    }
}

public enum Stance
{
	Hawk,
	Dove
}
