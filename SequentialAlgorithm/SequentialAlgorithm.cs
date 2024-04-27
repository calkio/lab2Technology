using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab2.SequentialAlgorithm
{
    internal class SequentialAlgorithm
    {
        private int _groupSize; 
        private double _threshold;

        public SequentialAlgorithm(int groupSize, double threshold)
        {
            _groupSize = groupSize;
            _threshold = threshold;
        }


        public BigInteger CalculeteTimeSequentialAlgorithm(double[] pixcelData)
        {
            Stopwatch watch = new Stopwatch();
            watch.Restart();

            int[] indexDeviation = FindIndexDeviation(pixcelData);
            watch.Stop();
            BigInteger sumIndexDeviation = SumIndexDeviationSequentialAlgorithm(indexDeviation);

            Console.WriteLine($"Скорость последовательного кода - {watch.Elapsed}, количество индексов - {indexDeviation.Length}, сумма индексов - {sumIndexDeviation}");

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
            double[] standardDeviations = Enumerable.Range(0, series.Length - (_groupSize - 1))
                                                    .Select(i => series.Skip(i).Take(_groupSize))
                                                    .Select(group => Statistics.StandardDeviation(group.ToArray()))
                                                    .ToArray();
            return standardDeviations;
        }
        private int[] FindIndexDeviationByThreshold(double[] seriesDeviation)
        {
            int[] indexDeviationByThreshold = seriesDeviation.Select((value, index) => new { Value = value, Index = index })
                                                             .Where(item => item.Value > _threshold)
                                                             .Select(item => item.Index)
                                                             .ToArray();
            return indexDeviationByThreshold;
        }


        private BigInteger SumIndexDeviationSequentialAlgorithm(int[] indexDeviation)
        {
            BigInteger sum = indexDeviation.Max();
            for (int i = 0; i < indexDeviation.Length; i++)
            {
                sum += indexDeviation[i];
            }
            return sum;
        }
    }
}
