using System.Diagnostics;
using System.Threading.Tasks;
using Hawk_Dove;

// random that creates seeds for each random run
// makes sure that it is re-creatable
int masterSeed = Environment.TickCount;
Random masterRandom = new(masterSeed);


Output output = new(masterSeed);

const int antiRandomIterations = 1000;

// fixed values
const int initialAggresionAgent1 = 30;
const int initialAggresionAgent2 = 2;

// file head
output.WriteLine("Hawk-Dove Simulation on " + DateTime.Now.ToString("MM-dd HH-mm-ss"));
output.WriteLine("InnerLoop results");
output.WriteLine("Iterations:", antiRandomIterations.ToString());
output.WriteLine("historyRange: ", InnerLoop.historyRange.ToString());
output.WriteLine("Agent1: ", initialAggresionAgent1.ToString());
output.WriteLine("Agent2: ", initialAggresionAgent2.ToString());
output.WriteLine("Conflict costs: ", InnerLoop.conflictCosts.ToString());
output.WriteLine("Resource value: ", InnerLoop.resourceValue.ToString());
output.WriteLine("MasterSeed: ", masterSeed.ToString());
output.WriteLine("");
output.WriteLine("Iteration", "flatline index", "", "seed for debug");

// algorithm
int[] resultIndexes = new int[antiRandomIterations];

// Collection of runs
InnerLoop[] runs = new InnerLoop[antiRandomIterations];
for (int i = 0; i < antiRandomIterations; i++)
{
    int innerLoopSeed = masterRandom.Next(0, int.MaxValue);
    InnerLoop run = new(initialAggresionAgent1
                      , initialAggresionAgent2
                      , innerLoopSeed, i);
    runs[i] = run;
}

//for (int i = 0; i < antiRandomIterations; i++)
//{
//    int innerLoopSeed = masterRandom.Next(0, int.MaxValue);
//    InnerLoop run = new(initialAggresionAgent1
//                      , initialAggresionAgent2
//                      , innerLoopSeed, i);
//    int result = run.FlatLineIndex();
//    resultIndexes[i] = result;
//    output.WriteLine(i.ToString(),result.ToString(),"",run.seed.ToString());
//}

Parallel.ForEach(runs,
    currentElement =>
    {
        int i = currentElement.runNum;
        int result = currentElement.FlatLineIndex();
        resultIndexes[i] = result;
        output.WriteLine(i.ToString(), result.ToString(), "", currentElement.seed.ToString());
    });