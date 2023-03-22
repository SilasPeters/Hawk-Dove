using System.Diagnostics;
using Hawk_Dove;

// Constants
const int   iterations    = 1000;
const float conflictCosts = 1.2f;
const float resourceValue = 1f;

// Create scenario
var resource = new DefaultResource(resourceValue);
var agents = new Agent[] { new FlexibleAgent(), new FlexibleAgent() };
var hawkDoveScenario = new HawkDoveScenario(resource, agents, conflictCosts);

// Boilerplate
using var export = new Export();
export.AddRow("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
export.AddRow("Iterations:", iterations.ToString());

var seed = Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing
export.AddRow("Seed used:", seed.ToString());

// Algorithm
for (var i = 0; i < iterations; ++i)
{
    var outcome = hawkDoveScenario.Run(random);
    export.AddRow(outcome.ToString());
}
