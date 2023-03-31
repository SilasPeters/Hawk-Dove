using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawk_Dove
{
    internal class RandomLoop
    {
        // random that creates seeds for each random run
        // makes sure that it is re-creatable
        public readonly int masterSeed;
        readonly Random masterRandom;

        const int antiRandomIterations = 100;


        // fixed values
        public readonly int initialAggresionAgent1;
        public readonly int initialAggresionAgent2;

        public RandomLoop(int seed, int ia1, int ia2)
        {
            masterSeed = seed;
            masterRandom = new Random(masterSeed);
            initialAggresionAgent1 = ia1;
            initialAggresionAgent2 = ia2;
        }

        public double RandomFlatLineMean()
        {
            // algorithm
            int[] resultIndexes = new int[antiRandomIterations];

            // Collection of runs
            InnerLoop[] runs = new InnerLoop[antiRandomIterations];
            for (int i = 0; i < antiRandomIterations; i++)
            {
                int innerLoopSeed = masterRandom.Next(0, int.MaxValue);
                InnerLoop run = new(initialAggresionAgent1
                                  , initialAggresionAgent2
                                  , innerLoopSeed
                                  , i);
                runs[i] = run;
            }
            foreach (var currentElement in runs)
            {
                int i = currentElement.runNum;
                int result = currentElement.FlatLineIndex();
                resultIndexes[i] = result;
            }
            return resultIndexes.Average();
        }
    }
}
