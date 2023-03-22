using Hawk_Dove;

const bool printContext = false;
#region Setup

// Constants
const int   iterations    = 1000;
const float conflictCosts = 1.2f;
const float resourceValue = 1f;

// Create scenario
var resource         = new DefaultResource(resourceValue);
var agents           = new Agent[] { new FlexibleAgent(), new FlexibleAgent() };
var hawkDoveScenario = new HawkDoveScenario(resource, agents, conflictCosts);

// Boilerplate
using var output = new Output();
var seed = Environment.TickCount;
var random = new Random(seed); // Seed ensures deterministic testing

if (printContext)
{
	output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
	output.WriteLine("Iterations:", iterations.ToString());
	output.WriteLine("Seed used:", seed.ToString());
	output.WriteLine();
}

#endregion

// Temporary code representing the actual simulation
var initialAggressivenessAgent1 = new[] { 0.5f, 0.7f };
var initialAggressivenessAgent2 = new[] { 0.6f, 0.4f };
var ratio                       = 0.5f;

// InitAggressiveness1, InitAggressiveness2, Score1, Score2

// InitAggressiveness1, InitAggressiveness2, Equilibrium
(float, float, float)[] outComeSimulation = {
	(.5f, .6f, .3f),
	(.7f, .4f, .6f),
	(.8f, .5f, .5f),
	(.9f, .6f, .4f),
	(.5f, .4f, .7f),
	(.7f, .6f, .4f),
	(.8f, .5f, .5f),
	(.9f, .4f, .6f),
};

// Relate the equilibrium to the initial aggressiveness
output.WriteLine("initAgg1", "initAgg2", "equilibrium");
foreach (var (initAgg1, initAgg2, equilibrium) in outComeSimulation)
	output.WriteLine(initAgg1.ToString(), initAgg2.ToString(), equilibrium.ToString());

// Export the CSV using https://chart-studio.plotly.com/create/scatter-chart/#/