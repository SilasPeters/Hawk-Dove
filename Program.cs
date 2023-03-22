using System.Diagnostics;
using Hawk_Dove;

// Constants
const int   iterations    = 1000;
const float conflictCosts = 75f;
const float resourceValue = 100f;
const int  historyRange = 5;


const float agent1Init = 0.2f;
const float agent2Init = 0.6f;

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

var seed = 18080093;// Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing
output.WriteLine("Seed used:", seed.ToString());

float[] scoresAgentOne = new float[iterations];
float[] scoresAgentTwo = new float[iterations];

// Algorithm
for (int i = 0; i < iterations; i++)
{
    var outcome = hawkDoveScenario.Run(random);
    agent1.history.Add(Tuple.Create(agent1.aggressiveness, outcome.Item1));
    agent2.history.Add(Tuple.Create(agent2.aggressiveness, outcome.Item2));
    if(agent1.history.Count > 4)
    {
        var last = agent1.history.Last().Item2;
        bool foo = false;
        for (int j = 1; j < 5; j++)
        {
            if (last == agent1.history[agent1.history.Count() - j].Item2)
            {
                foo = true;
            }
            else
                foo = false;
        }
        if(foo)
            random = new Random(seed);
    }
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
