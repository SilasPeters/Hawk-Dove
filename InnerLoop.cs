﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawk_Dove
{
    internal class InnerLoop
    {
        readonly int runNum;

        // Simulation Constants
        const int iterations = 100_000;
        public static int historyRange = 30;

        // Conflict Constants
        public static int conflictCosts = 200;
        public static int resourceValue = 100;

        // Initial Aggressiveness
        readonly int agent1Aggressiveness;
        readonly int agent2Aggressiveness;

        // Create scenario
        readonly Agent agent1;
        readonly Agent agent2;
        readonly HawkDoveScenario hawkDoveScenario;
        
        readonly Random random;
        public readonly int seed;

        public InnerLoop(int a1a, int a2a, int seed, int runNum)
        {
            agent1Aggressiveness = a1a;
            agent2Aggressiveness = a2a;
            this.seed = seed;
            random = new Random(this.seed);
            this.runNum = runNum;

            // Create scenario
            agent1 = new(agent1Aggressiveness, historyRange);
            agent2 = new(agent2Aggressiveness, historyRange);
            hawkDoveScenario = new HawkDoveScenario(agent1, agent2, resourceValue, conflictCosts);
        }

        public int FlatLineIndex() 
        {


            int[] aggressionOutcomesA1 = new int[iterations];
            int[] aggressionOutcomesA2 = new int[iterations];
            int[] scoreOutcomesA1 = new int[iterations];
            int[] scoreOutcomesA2 = new int[iterations];

            // Algorithm
            for (int i = 0; i < iterations; i++)
            {
                int historyIndex = i % historyRange;

                (int a1Outcome, int a2Outcome) = hawkDoveScenario.Run(random, i);
                agent1.history[historyIndex] = a1Outcome;
                agent2.history[historyIndex] = a2Outcome;
                aggressionOutcomesA1[i] = agent1.aggressiveness;
                aggressionOutcomesA2[i] = agent2.aggressiveness;
                scoreOutcomesA1[i] = a1Outcome;
                scoreOutcomesA2[i] = a2Outcome;

                // Log every 1000 runs
                if (i % 1000 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("run: " + runNum.ToString());
                    Console.WriteLine("runs: " + i.ToString());
                }
            }
            for (int i = iterations - 1; i >= 0; i--)
            {
                if (!(aggressionOutcomesA1[i] == 0 && aggressionOutcomesA2[i] == 0))
                    return i;
            }
            return iterations*2;
        }
    }
}