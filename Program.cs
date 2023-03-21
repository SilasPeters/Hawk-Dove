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
var variationVariables_2 = new float[variations];

for (var i = 0; i < variations; ++i)
{
    float variationVariable_conflictCosts = 2f;//5f * (float)i / variations;
    float variationVariable_agentChances_0  = random.NextSingle();
    float variationVariable_agentChances_1 =  random.NextSingle();
    var resource = new DefaultResource(1f);
    var agents = new Agent[] { new FlexibleAgent(), new FlexibleAgent() };
    ((FlexibleAgent)agents[0]).chanceRate = variationVariable_agentChances_0;
    ((FlexibleAgent)agents[1]).chanceRate = variationVariable_agentChances_1;
    var hawkDoveScenario = new HawkDoveScenario(resource, agents, variationVariable_conflictCosts);
    var outcomes = new float[iterations];
    for (var j = 0; j < iterations; j++)
    {
        
        t.Start();
        var outcome = hawkDoveScenario.Run(random);
        t.Stop();
        outcomes[j] = outcome;
    }
    averages[i] = outcomes.Average();
    variationVariables[i] = variationVariable_agentChances_0;
    variationVariables_2[i] = variationVariable_agentChances_1;
}

// Print summaries
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine();
output.WriteLine($"{iterations} iterations took:", t.Elapsed.Milliseconds.ToString(), "ms");
output.WriteLine("Seed used:",                     seed.ToString());
output.WriteLine("Average:",                       variationVariables.Average().ToString());
output.WriteLine("Min:",                            variationVariables.Min().ToString());
output.WriteLine("Max:",                             variationVariables.Max().ToString());
output.WriteLine();
output.WriteLine("averages,variable");
for (var i = 0; i < variations; i++)
    output.WriteLine(averages[i].ToString(),variationVariables[i].ToString(),variationVariables_2[i].ToString());

Console.Write("Wrote output to " + output.OutputLocation);
