using System.Diagnostics;
using Hawk_Dove;

// Constants
const int   iterations    = 1000;
const int conflictCosts = 75;
const int resourceValue = 100;
const int  historyRange = 0;


const int agent1Init = 10;
const int agent2Init = 0;

// Create scenario
var resource = new DefaultResource(resourceValue);
HistoryBasedAgent agent1 = new(agent1Init,historyRange);
HistoryBasedAgent agent2 = new(agent2Init,historyRange);
Tuple<HistoryBasedAgent, HistoryBasedAgent> agents = Tuple.Create(agent1, agent2);
var hawkDoveScenario = new HawkDoveScenario(resource, agents, conflictCosts);

// Boilerplate
using var output = new Output();
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine("Iterations:", iterations.ToString());
output.WriteLine("historyRange: ", historyRange.ToString());
output.WriteLine("Agent1: ", agent1Init.ToString());
output.WriteLine("Agent2: ", agent2Init.ToString());

var seed = Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing
output.WriteLine("Seed used:", seed.ToString());

int[] scoresAgentOne = new int[iterations];
int[] scoresAgentTwo = new int[iterations];

// Algorithm
for (int i = 0; i < iterations; i++)
{
    var outcome = hawkDoveScenario.Run(random);
    agent1.history.Add(Tuple.Create(agent1.aggressiveness, outcome.Item1));
    agent2.history.Add(Tuple.Create(agent2.aggressiveness, outcome.Item2));
    if (i % 1000 == 0) { Console.Clear(); Console.WriteLine("runs: " + i.ToString()); Console.WriteLine(outcome.ToString()); }
}

// Ouput
output.WriteLine();
output.WriteLine("iteration","agentOneScore","agentTwoScore");
for (var i = 0; i < iterations; ++i)
{
    output.WriteLine(
           i.ToString(), 
           agent1.history[i].Item2.ToString(), 
           agent2.history[i].Item2.ToString(),
           "",
           "",
           agent1.history[i].Item1.ToString(),
           agent2.history[i].Item1.ToString()
           );
}
