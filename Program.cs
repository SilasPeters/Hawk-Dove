using System.Collections.Concurrent;
using Hawk_Dove;

// random that creates seeds for each random run
// makes sure that it is re-creatable
int masterSeed = Environment.TickCount;
Random masterRandom = new(masterSeed);
using Output output = new(masterSeed);

// cartesian product
const int minAggression = 0;
const int maxAggression = 100;
int[] up = Enumerable.Range(minAggression, maxAggression - minAggression).ToArray();
int[] down = Enumerable.Range(minAggression, maxAggression - minAggression).Reverse().ToArray();
List<(int,int)> cartesian = new List<(int,int)>();

for (int i = 0; i < up.Length; i++)
{
    for (int j = i; j < down.Length; j++)
    {
        var var = (up[i], down[j]);
        cartesian.Add(var);
    }
}

RandomLoop[] cartesians = new RandomLoop[cartesian.Count];
for (int i = 0; i < cartesians.Length; i++)
{
    RandomLoop loop = new(masterRandom.Next(0, int.MaxValue)
                        , cartesian[i].Item1
                        , cartesian[i].Item2);
    cartesians[i] = loop;
}


// file heaa2Outcomed
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine("Cartesian results");
output.WriteLine("historyRange: ", InnerLoop.historyRange.ToString());
output.WriteLine("Conflict costs: ", InnerLoop.conflictCosts.ToString());
output.WriteLine("Resource value: ", InnerLoop.resourceValue.ToString());
output.WriteLine("MasterSeed: ", masterSeed.ToString());
output.WriteLine("");
output.WriteLine("Initial Aggression A1","Initial Aggression A2", "Mean of flatline");

// Algorithm
var randomFlatLineMeans = new ConcurrentBag<double>();
Parallel.ForEach(cartesians, currentElement =>
    {
        var result = currentElement.RandomFlatLineMean();
        randomFlatLineMeans.Add(result);
        output.WriteLine(currentElement.initialAggresionAgent1.ToString()
                       , currentElement.initialAggresionAgent2.ToString()
                       , result.ToString());
    });

return;
