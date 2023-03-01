﻿using System.Diagnostics;
using Hawk_Dove;

const int iterations = 1000;

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
	
	Console.WriteLine("Outcome: " + outcome);
	outcomes[i] = outcome;
}

// Print summaries
Console.WriteLine();
Console.WriteLine($"{iterations} iterations took {t.Elapsed.Milliseconds}ms");
Console.WriteLine("Seed used: " + seed);
Console.WriteLine("Average: " + outcomes.Average());
Console.WriteLine("    Min: " + outcomes.Min());
Console.WriteLine("    Max: " + outcomes.Max());
