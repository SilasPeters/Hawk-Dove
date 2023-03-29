using System.Diagnostics;
using Hawk_Dove;

// Simulation Constants
const int iterations    = 150;
const int historyRange  = 100;

// Conflict Constants
const int resourceValue = 100;
const int conflictCosts = 200;

// Initial Aggressiveness
const int agent1Aggressiveness = 10;
const int agent2Aggressiveness = 0;

// Create scenario
Agent agent1 = new(agent1Aggressiveness, historyRange);
Agent agent2 = new(agent2Aggressiveness, historyRange);
var hawkDoveScenario = new HawkDoveScenario(agent1, agent2, resourceValue, conflictCosts);

// Boilerplate
using var output = new Output();
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine("Iterations:", iterations.ToString());
output.WriteLine("historyRange: ", historyRange.ToString());
output.WriteLine("Agent1: ", agent1Aggressiveness.ToString());
output.WriteLine("Agent2: ", agent2Aggressiveness.ToString());

var seed = Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing
output.WriteLine("Seed used:", seed.ToString());

// Algorithm
for (int i = 0; i < iterations; i++)
{
    (int a1Outcome, int a2Outcome) = hawkDoveScenario.Run(random);
    agent1.history.Add(new Historic(agent1.aggressiveness, a1Outcome));
    agent2.history.Add(new Historic(agent2.aggressiveness, a2Outcome));
    
    // Log every 1000 runs
    if (i % 1000 == 0)
    { 
        Console.Clear(); 
        Console.WriteLine("runs: " + i.ToString());
        Console.WriteLine("run score: " + a1Outcome + ", " + a2Outcome);
    }
}

// Ouput
output.WriteLine();
output.WriteLine("iteration","agentOneScore","agentTwoScore");
for (var i = 0; i < iterations; ++i)
{
    output.WriteLine(
           i.ToString(),
           agent1.history[i].Score.ToString(),
           agent2.history[i].Score.ToString(),
           "",
           "",
           i.ToString(),
           agent1.history[i].Aggresion.ToString(),
           agent2.history[i].Aggresion.ToString()
           ); ;
}
