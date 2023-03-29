using System.Diagnostics;
using Hawk_Dove;

// Simulation Constants
const int  iterations   = 10000;
const int  historyRange = 30;
const bool debug        = true;

// Conflict Constants
const int conflictCosts = 200;
const int resourceValue = 100;

// Initial Aggressiveness
const int agent1Aggressiveness = 30;
const int agent2Aggressiveness = 2;

// Create scenario
Agent agent1 = new(agent1Aggressiveness, historyRange);
Agent agent2 = new(agent2Aggressiveness, historyRange);
var hawkDoveScenario = new HawkDoveScenario(agent1, agent2, resourceValue, conflictCosts);


var seed = 4694968;// Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing

// Boilerplate
using var output = new Output(seed);



output.WriteLine("sep=;");  
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine("Iterations:", iterations.ToString());
output.WriteLine("historyRange: ", historyRange.ToString());
output.WriteLine("Agent1: ", agent1Aggressiveness.ToString());
output.WriteLine("Agent2: ", agent2Aggressiveness.ToString());
output.WriteLine("Conflict costs: ", conflictCosts.ToString());
output.WriteLine("Resource value: ", resourceValue.ToString());


output.WriteLine("Seed used:", seed.ToString());

output.WriteLine();
output.WriteLine("Iteration", "Agent 1 Score", "Agent 2 Score","","","Iteration","Agent 1 Aggression","Agent 2 Aggression");

int[] aggressionOutcomesA1 = new int[iterations];
int[] aggressionOutcomesA2 = new int[iterations];
int[] scoreOutcomesA1      = new int[iterations];
int[] scoreOutcomesA2      = new int[iterations];

// Algorithm
for (int i = 0; i < iterations; i++)
{
    int historyIndex = i % historyRange;

    (int a1Outcome, int a2Outcome) = hawkDoveScenario.Run(random,i);
    agent1.history[historyIndex] = a1Outcome;
    agent2.history[historyIndex] = a2Outcome;
    aggressionOutcomesA1[i] = agent1.aggressiveness;
    aggressionOutcomesA2[i] = agent2.aggressiveness;
    scoreOutcomesA1[i] = a1Outcome;
    scoreOutcomesA2[i] = a2Outcome;
    
    // Log every 1000 runs
    if (i % 1000 == 0)
    { 
        Console.Clear(); 
        Console.WriteLine("runs: " + i.ToString());
        Console.WriteLine("run score: " + a1Outcome + ", " + a2Outcome);
    }

    if (!debug)
        return;

    // Output
    output.WriteLine(
           i.ToString(),
           agent1.history[historyIndex].ToString(),
           agent2.history[historyIndex].ToString(),
           "",
           "",
           i.ToString(),
           agent1.aggressiveness.ToString(),
           agent2.aggressiveness.ToString()
           );
}

int[] partialAggressionOutcomesA1 = new int[iterations-1000];
int[] partialAggressionOutcomesA2 = new int[iterations - 1000];
for (int i = 1000;i < iterations; i++)
{
    partialAggressionOutcomesA1[i-1000] = aggressionOutcomesA1[i];
    partialAggressionOutcomesA2[i-1000] = aggressionOutcomesA2[i];
}


output.WriteLine("");
if (aggressionOutcomesA1.Skip(iterations-1000).Average() == 0 && aggressionOutcomesA2.Skip(iterations - 1000).Average() == 0) 
{
    output.WriteLine("Aggression ended in zero!");
}
else
{
    output.WriteLine("average aggresion a1: " , partialAggressionOutcomesA1.Average().ToString());
    output.WriteLine("average aggresion a2: " , partialAggressionOutcomesA2.Average().ToString());
}