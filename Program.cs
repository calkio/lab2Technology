using lab2.Data.GenerateDataStrategy;
using lab2.Data.GenerateDataStrategy.Base;
using lab2.ParallelAlgorithm;
using lab2.SequentialAlgorithm;
using System.Diagnostics;
using System.Numerics;

internal class Program
{
    static private BigInteger _sumIndexSequentialAlgorithm;
    static private BigInteger _sumIndexParallelAlgorithm;

    private static void Main(string[] args)
    {
        double[] pixcelData = GenerateData(args);

        while (true)
        {
            Console.WriteLine();

            CalculeteTimeSequentialAlgorithm(pixcelData);
            CalculeteTimeParallelAlgorithm(pixcelData);
            ConclusionResultsDifference();

            Console.WriteLine();
        }
    }

    static private double[] GenerateData(string[] args)
    {
        IGenerateData generateData = new ImageData();
        double[] pixcelData = generateData.GenerateData(args[0]);
        return pixcelData;
    }

    static private void ConclusionResultsDifference()
    {
        Console.WriteLine($"Разница по сумме индексов - {_sumIndexSequentialAlgorithm - _sumIndexParallelAlgorithm}");
    }

    static private void CalculeteTimeSequentialAlgorithm(double[] pixcelData)
    {
        SequentialAlgorithm sequentialAlgorithm = new SequentialAlgorithm(2, 3);
        _sumIndexSequentialAlgorithm = sequentialAlgorithm.CalculeteTimeSequentialAlgorithm(pixcelData);
    }

    static private void CalculeteTimeParallelAlgorithm(double[] pixcelData)
    {
        ParallelAlgorithm parallelAlgorithm = new ParallelAlgorithm(2, 3);
        _sumIndexParallelAlgorithm = parallelAlgorithm.CalculeteTimeParalellAlgorithm(pixcelData);
    }
}