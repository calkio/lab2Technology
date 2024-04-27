using Emgu.CV.Features2D;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab2.ParallelAlgorithm
{
    internal class NotSafeParallelAlgorithm
    {
        private int _groupSize;
        private double _threshold;

        public NotSafeParallelAlgorithm(int groupSize, double threshold)
        {
            _groupSize = groupSize;
            _threshold = threshold;
        }

        public BigInteger CalculeteTimeParalellAlgorithm(double[] pixcelData)
        {
            Stopwatch watch = new Stopwatch();
            watch.Restart();

            int[] indexDeviation = FindIndexDeviation(pixcelData);
            watch.Stop();
            BigInteger sumIndexDeviation = SumIndexDeviationParallelAlgorithm(indexDeviation);

            Console.WriteLine($"Скорость небезопасного параллельного кода - {watch.Elapsed}, количество индексов - {indexDeviation.Length}, сумма индексов - {sumIndexDeviation}");
            return sumIndexDeviation;
        }


        private int[] FindIndexDeviation(double[] series)
        {
            double[] standardDeviations = CreateStandartDeviationSeries(series);
            int[] indexDeviationByThreshold = FindIndexDeviationByThreshold(standardDeviations);
            return indexDeviationByThreshold;
        }
        private double[] CreateStandartDeviationSeries(double[] series)
        {
            double[] standardDeviations = new double[series.Length - (_groupSize - 1)];

            Parallel.For(0, series.Length - (_groupSize - 1), i =>
            {
                double[] group = series.Skip(i).Take(_groupSize).ToArray();
                standardDeviations[i] = Statistics.StandardDeviation(group);
            });

            return standardDeviations;
        }
        private int[] FindIndexDeviationByThreshold(double[] seriesDeviation)
        {
            int[] indexDeviationByThreshold = seriesDeviation.AsParallel()
                                                             .Select((value, index) => new { Value = value, Index = index })
                                                             .Where(item => item.Value > _threshold)
                                                             .Select(item => item.Index)
                                                             .ToArray();

            return indexDeviationByThreshold;
        }


        private BigInteger SumIndexDeviationParallelAlgorithm(int[] indexDeviation)
        {
            BigInteger sum = 0;
            for (int i = 0; i < indexDeviation.Length; i++)
            {
                sum += indexDeviation[i] * i;
            }
            return sum;
        }
    }
}
