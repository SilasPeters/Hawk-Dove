namespace Hawk_Dove;

public class Agent
{
    public Stance Stance { get; protected set; }

    public virtual void SetNewStance(Random r) => 
		Stance = r.Next(0, 100) < ChanceHawk(r) ? Stance.Hawk : Stance.Dove;

    public int aggressiveness;
	public int[] history;
	// private int historyIndex = 0;
	public int historyRange;
	const int aggressivenessIncrease = 1;

	public Agent(int a, int hr) 
	{
		aggressiveness = a;
		historyRange = hr; 
		history = new int[hr]; 
	}

	// private void PushToHistory(int a)
	// {
	// 	historyIndex = (historyIndex + 1) % 10;
	// 	history[historyIndex] = a;
	// }

	public int ChanceHawk(Random r, int iteration)
	{
		history[iteration % historyRange] = 4;
		
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
