using System.Diagnostics;
using Hawk_Dove;

// Constants
const int   iterations    = 1000;
const float conflictCosts = 1.2f;
const float resourceValue = 1f;

// Create scenario
var resource = new DefaultResource(resourceValue);
HistoryBasedAgent agent1 = new(0.3f);
HistoryBasedAgent agent2 = new(0.4f);
Agent[] agents = {agent1, agent2};
var hawkDoveScenario = new HawkDoveScenario(resource, agents, conflictCosts);

// Boilerplate
using var output = new Output();
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine("Iterations:", iterations.ToString());

var seed = Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing
output.WriteLine("Seed used:", seed.ToString());


for (int i = 0; i < iterations; i++)
{

}

// Algorithm
for (var i = 0; i < iterations; ++i)
{
    var outcome = hawkDoveScenario.Run(random);
    output.WriteLine(outcome.ToString());
}
