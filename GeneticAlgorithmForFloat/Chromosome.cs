using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmForFloat
{
    class Chromosome
    {
        private Random random = new Random(unchecked((int)DateTime.Now.Ticks));
        public float SelectedRatio { get; set; }
        public double Fitness { get; set; }
        //public FitnessParam FitnessParam { get; set; }
        public float MutationRatio { get; private set; } = 0.1f;
        public float Max { get; private set; }
        public float Min { get; private set; }
        public List<float> FloatingNumbers { get; private set; }
        public Chromosome(List<float> floats,
            float mutationRatio, float max, float min)
        {
            FloatingNumbers = new List<float>();
            foreach (var f in floats)
            {
                FloatingNumbers.Add(f);
            }

            MutationRatio = mutationRatio;
            Max = max;
            Min = min;
        }

        public void ChromosomeMutate()
        {
            for (int i = 0; i < FloatingNumbers.Count; i++)
            {
                float f = FloatingNumbers[i];
                int r = random.Next(0, 1000);
                switch (r % 2)
                {
                    case 0:
                        f = f + MutationRatio
                            * (Max - f) * r / 1000f;

                        f = f > Max ? Max : f;
                        break;
                    case 1:
                        f = f - MutationRatio
                            * (f - Min) * r / 1000f;
                        f = f < Min ? Min : f;
                        break;
                }
                FloatingNumbers[i] = f;
            }
        }

        public Chromosome GetCopy()
        {
            return new Chromosome(FloatingNumbers,
                MutationRatio, Max, Min)
            {
                Fitness = this.Fitness,
                //FitnessParam = this.FitnessParam?.Clone(),
                SelectedRatio = this.SelectedRatio
            };
        }
    }
}
