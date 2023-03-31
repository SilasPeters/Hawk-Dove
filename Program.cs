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

List<(int,int)> cartesian = new List<(int,int)>();
for (int i = minAggression; i <= maxAggression; i++)
{
    for (int j = i; j <= maxAggression; j++)
    {
        var var = (i, j);
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
var results = new ConcurrentBag<Result>();
foreach (var currentElement in cartesians)
{
    results.Add(new Result(
        currentElement.RandomFlatLineMean(),
        currentElement.initialAggresionAgent1,
        currentElement.initialAggresionAgent2)
    );
}
        // var result = currentElement.RandomFlatLineMean();
        // results.Add(result);
        // output.WriteLine(currentElement.initialAggresionAgent1.ToString()
        //                , currentElement.initialAggresionAgent2.ToString()
        //                , result.ToString());

foreach (var result in results)
    output.WriteLine(
        result.InitAgg1.ToString(),
        result.InitAgg2.ToString(),
        result.FlatLineMean.ToString()
        );

internal record struct Result(double FlatLineMean, double InitAgg1, double InitAgg2);