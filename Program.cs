using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Hawk_Dove;
using System.Text;

// random that creates seeds for each random run
// makes sure that it is re-creatable
int masterSeed = Environment.TickCount;
Random masterRandom = new(masterSeed);


Output output = new(masterSeed);

const int antiRandomIterations = 1000;

// cartisian product
const int minAggression = 0;
const int maxAggression = 100;
int[] up = Enumerable.Range(minAggression, maxAggression).ToArray();
int[] down = Enumerable.Range(minAggression, maxAggression).Reverse().ToArray();
List<(int,int)> cartesian = new List<(int,int)>();

for (int i = 0; i < up.Length; i++)
{
    for (int j = 0; j < down.Length; j++)
    {
        (int,int) var = (up[i], down[j]);
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


// file head
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine("Cartisian results");
output.WriteLine("historyRange: ", InnerLoop.historyRange.ToString());
output.WriteLine("Conflict costs: ", InnerLoop.conflictCosts.ToString());
output.WriteLine("Resource value: ", InnerLoop.resourceValue.ToString());
output.WriteLine("MasterSeed: ", masterSeed.ToString());
output.WriteLine("");
output.WriteLine("Initial Aggression A1","Initial Aggression A2", "Mean of flatline");


// Algorithm
List<double> randomFlatLineMeans = new();
int toGo = cartesians.Count();
Parallel.ForEach(cartesians,
    currentElement =>
    {
        double result = currentElement.RandomFlatLineMean();
        randomFlatLineMeans.Add(result);
        output.WriteLine(currentElement.initialAggresionAgent1.ToString()
                       , currentElement.initialAggresionAgent2.ToString()
                       , result.ToString());
        toGo--;
        if (toGo%100 == 0) Console.WriteLine("toGo: " + toGo.ToString());
    });


