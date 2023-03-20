using System.Diagnostics;
using Hawk_Dove;

const int iterations = 1000;
using var output = new Output();


var seed   = Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing


var t        = new Stopwatch();



const int variations = 1000;

var averages = new float[variations];
var variationVariables = new float[variations];

for (var i = 0; i < variations; ++i)
{
    float variationVariable = 5f * (float)i / variations;
    var resource = new DefaultResource(1f);
    var agents = new Agent[] { new NeutralAgent(), new AggressiveAgent() };
    var hawkDoveScenario = new HawkDoveScenario(resource, agents, variationVariable);
    var outcomes = new float[iterations];
    for (var j = 0; j < iterations; j++)
    {
        
        t.Start();
        var outcome = hawkDoveScenario.Run(random);
        t.Stop();
        outcomes[j] = outcome;
    }
    averages[i] = outcomes.Average();
    variationVariables[i] = variationVariable;
}

// Print summaries
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine();
output.WriteLine($"{iterations} iterations took:", t.Elapsed.Milliseconds.ToString(), "ms");
output.WriteLine("Seed used:",                     seed.ToString());
output.WriteLine("Average:",                       averages.Average().ToString());
output.WriteLine("Min:",                           averages.Min().ToString());
output.WriteLine("Max:",                       averages.Max().ToString());
output.WriteLine();
for (var i = 0; i < variations; i++)
    output.WriteLine(averages[i].ToString() + "," + variationVariables[i].ToString());

Console.Write("Wrote output to " + output.OutputLocation);
