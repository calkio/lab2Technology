using lab2.Data.GenerateDataStrategy;
using lab2.Data.GenerateDataStrategy.Base;
using lab2.ParallelAlgorithm;
using lab2.SequentialAlgorithm;
using System.Diagnostics;
using System.Numerics;

internal class Program
{
    static private BigInteger _sumIndexSequentialAlgorithm;
    static private BigInteger _sumIndexNotSafeParallelAlgorithm;
    static private BigInteger _sumIndexSafeParallelAlgorithm;

    private static void Main(string[] args)
    {
        double[] pixcelData = GenerateData(args);

        while (true)
        {
            Console.WriteLine();

            CalculeteTimeSequentialAlgorithm(pixcelData);
            CalculeteTimeNotSafeParallelAlgorithm(pixcelData);
            CalculeteTimeSafeParallelAlgorithm(pixcelData);
            ConclusionResultsDifferenceNotSafe();
            ConclusionResultsDifferenceSafe();

            Console.WriteLine();
        }
    }

    static private double[] GenerateData(string[] args)
    {
        IGenerateData generateData = new ImageData();
        double[] pixcelData = generateData.GenerateData(args[0]);
        return pixcelData;
    }

    static private void ConclusionResultsDifferenceNotSafe()
    {
        Console.WriteLine($"Разница по сумме индексов (не безопасный) - {_sumIndexSequentialAlgorithm - _sumIndexNotSafeParallelAlgorithm}");
    }

    static private void ConclusionResultsDifferenceSafe()
    {
        Console.WriteLine($"Разница по сумме индексов (безопасный) - {_sumIndexSequentialAlgorithm - _sumIndexSafeParallelAlgorithm}");
    }



    static private void CalculeteTimeSequentialAlgorithm(double[] pixcelData)
    {
        SequentialAlgorithm sequentialAlgorithm = new SequentialAlgorithm(2, 3);
        _sumIndexSequentialAlgorithm = sequentialAlgorithm.CalculeteTimeSequentialAlgorithm(pixcelData);
    }

    static private void CalculeteTimeNotSafeParallelAlgorithm(double[] pixcelData)
    {
        NotSafeParallelAlgorithm parallelAlgorithm = new NotSafeParallelAlgorithm(2, 3);
        _sumIndexNotSafeParallelAlgorithm = parallelAlgorithm.CalculeteTimeParalellAlgorithm(pixcelData);
    }

    static private void CalculeteTimeSafeParallelAlgorithm(double[] pixcelData)
    {
        SafeParallelAlgorithm parallelAlgorithm = new SafeParallelAlgorithm(2, 3);
        _sumIndexSafeParallelAlgorithm = parallelAlgorithm.CalculeteTimeParalellAlgorithm(pixcelData);
    }
}