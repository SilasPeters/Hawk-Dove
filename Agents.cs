namespace Hawk_Dove;

public class Agent
{
    public Stance Stance { get; protected set; }

    public virtual void SetNewStance(Random r, Agent opponent, int i) => 
		Stance = r.Next(0, 100) < ChanceHawk(opponent, i) ? Stance.Hawk : Stance.Dove;

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

	public int ChanceHawk(Agent opponent, int iterationCount)
	{
		if (iterationCount < historyRange)
			return aggressiveness;
		
		double myAverage  = history.Average();
		double oppAverage = opponent.history.Average();
		
		if (myAverage < oppAverage)
			return aggressiveness = Math.Min(aggressiveness + aggressivenessIncrease, 100);
		if (myAverage < 0)
			return aggressiveness = Math.Max(aggressiveness - aggressivenessIncrease, 0);
        return aggressiveness = Math.Max(aggressiveness - aggressivenessIncrease, 0);
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
