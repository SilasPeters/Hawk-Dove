using System.Diagnostics;
using Hawk_Dove;

const int iterations = 1000;
var output = new Output();

var resource = new DefaultResource(1f);
var agents   = new Agent[] { new NeutralAgent(), new AggressiveAgent() };
var hawkDoveScenario = new HawkDoveScenario(resource, agents, 1.2f);

var seed   = Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing

var outcomes = new float[iterations];
var t        = new Stopwatch();

for (var i = 0; i < iterations; i++)
{
	t.Start();
	var outcome = hawkDoveScenario.Run(random);
	t.Stop();
	outcomes[i] = outcome;
}

// Print summaries
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine();
output.WriteLine($"{iterations} iterations took {t.Elapsed.Milliseconds}ms");
output.WriteLine("Seed used: " + seed);
output.WriteLine("Average: " + outcomes.Average());
output.WriteLine("    Min: " + outcomes.Min());
output.WriteLine("    Max: " + outcomes.Max());
output.WriteLine();
foreach(var outcome in outcomes)
	output.WriteLine(outcome.ToString());

Console.Write("Wrote output to " + output.OutputLocation);
