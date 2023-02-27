using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmForFloat
{
    class FitnessForSum
    {
        /// <summary>
        /// calculate the fitness of the chromosome, by sum value close to 100f.
        /// </summary>
        /// <param name="chromosome"></param>
        /// <returns></returns>
        public static float CalcFitness(Chromosome chromosome)
        {
            float sum = chromosome.FloatingNumbers.Sum();
            if (Math.Abs(sum - 100f) < float.Epsilon)
            {
                return float.MaxValue;
            } else
            {
                return 1 / Math.Abs(sum - 100f);
            }            
        }

        public static void OutputBest(Chromosome chromosome, string caption)
        {
            string outPut = $"{caption} Fitness: {chromosome.Fitness:0.###} Sum: {chromosome.FloatingNumbers.Sum():0.###} Values:";
            foreach (var f in chromosome.FloatingNumbers)
            {
                outPut = $"{outPut} {f:0.###}";
            }
            Console.WriteLine(outPut);

            Console.WriteLine("----------------------------------------------------------------------------------------------");
        }
    }
}
