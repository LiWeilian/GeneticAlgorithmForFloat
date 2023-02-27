using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmForFloat
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                List<Chromosome> chromosomes = new List<Chromosome>();

                float mutationRatio = 0.1f;
                float max = 50f;
                float min = 0f;

                var rnd = new Random();
                List<float> floats = new List<float>();
                for (int j = 0; j < 5; j++)
                {
                    floats.Add((float)rnd.NextDouble() * 50);
                }
                Chromosome initChromsome = new Chromosome(floats, mutationRatio, max, min);
                chromosomes.Add(initChromsome);

                int popSize = 10;
                float xoverRatio = 0.5f;

                for (int i = 1; i < popSize; i++)
                {
                    Chromosome copy = initChromsome.GetCopy();
                    copy.ChromosomeMutate();
                    chromosomes.Add(copy);
                }

                Population pop = new Population(DateTime.Now.ToString("yyyyMMddHHmmss"),
                        xoverRatio, popSize, chromosomes, mutationRatio);

                int evoloveGens = 50;
                List<Chromosome> historyChromosomes = new List<Chromosome>();

                for (int i = 0; i < evoloveGens; i++)
                {
                    pop.Generation++;
                    //Console.WriteLine($"Generation: {pop.Generation}");
                    if (i > 0)
                    {
                        pop.PopulationCrossover();
                        pop.ChromosomeMutate();
                    }

                    foreach (var chromosome in pop.Chromosomes)
                    {
                        chromosome.Fitness = FitnessForSum.CalcFitness(chromosome);
                    }

                    //Sort chromosome in population
                    pop.ChromosomeSort();

                    //update selected ratio of chromosome
                    pop.UpdateChromosomeSelectedRatio();

                    //save population to history list
                    foreach (var chrom in pop.Chromosomes)
                    {
                        historyChromosomes.Add(chrom.GetCopy());
                    }

                    FitnessForSum.OutputBest(pop.Chromosomes.FirstOrDefault(), $"Generation: {pop.Generation} Best ");
                }

                Population best_pop = new Population(DateTime.Now.ToString("yyyyMMddHHmmss"),
                            xoverRatio, popSize, historyChromosomes, mutationRatio);

                FitnessForSum.OutputBest(best_pop.Chromosomes.FirstOrDefault(), "Best of All ");

                Console.ReadKey();
            }
        }
    }
}
