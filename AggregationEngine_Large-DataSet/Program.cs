using System;
using System.Diagnostics;
using System.Collections.Generic;
using CardinalityEstimation;

// Solution code goes here
public class Solution
{
    public static Dictionary<string, double>[] DoAggregations(double[,] input)
    {
        int rows = input.GetLength(0);
        int cols = input.GetLength(1);

        // Initialize arrays to store the results
        double[] sums = new double[cols];
        double[] counts = new double[cols]; // to track row counts for each column
        CardinalityEstimator[] distinctEstimators = new CardinalityEstimator[cols];

        // Initialize the result array (one dictionary per column)
        Dictionary<string, double>[] result = new Dictionary<string, double>[cols];
        for (int i = 0; i < cols; i++)
        {
            result[i] = new Dictionary<string, double>();
            distinctEstimators[i] = new CardinalityEstimator();
        }

        // Perform aggregations in a single loop over the matrix
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                double value = input[i, j];

                // Sum and add value to HyperLogLog for distinct counting
                sums[j] += value;
                counts[j]++;
                distinctEstimators[j].Add((ulong)value); // Add to HyperLogLog (requires ulong conversion)
            }
        }

        // Calculate SUM, AVERAGE, and COUNT DISTINCT per column
        for (int j = 0; j < cols; j++)
        {
            result[j]["SUM"] = sums[j];
            result[j]["AVERAGE"] = sums[j] / counts[j];
            result[j]["COUNT DISTINCT"] = distinctEstimators[j].Count(); // HyperLogLog distinct count
        }

        return result;
    }
}

public class Mock
{
    public static Dictionary<string, double>[] DoAggregations(double[,] input)
    {
        return new Dictionary<string, double>[]{
            new Dictionary<string, double>{{"SUM", 9d}, {"AVERAGE", 2.25d}, {"COUNT DISTINCT", 3d}},
            new Dictionary<string, double>{{"SUM", 14d}, {"AVERAGE", 3.5d}, {"COUNT DISTINCT", 2d}},
            new Dictionary<string, double>{{"SUM", 16d}, {"AVERAGE", 4d}, {"COUNT DISTINCT", 4d}}};
    }
}

public class Program
{
    // Don't change Main
    public static void Main()
    {
        //*** SMALL INPUT ***//
        double[,] smallInput = new double[,] { { 1, 4, 3 }, { 2, 3, 4 }, { 1, 3, 7 }, { 5, 4, 2 } };

        // Mocked example of execution
        Console.WriteLine("=== Small Input - Mocked Execution ===");
        var mockResult = Mock.DoAggregations(smallInput);
        IsResultCorrect(mockResult);

        // Your code execution
        Console.WriteLine("\n=== Small Input - Solution Execution ===");
        var result = Solution.DoAggregations(smallInput);
        IsResultCorrect(result);

        //*** LARGE INPUT ***//
        Console.WriteLine("\n=== Large Input - Solution Execution ===");
        var largeInput = GenerateLargeInput();
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 5; i++)
            Solution.DoAggregations(largeInput);
        sw.Stop();
        IsExecutionFastEnough(sw.ElapsedMilliseconds / 5);
    }

    public static double[,] GenerateLargeInput()
    {
        int numRows = 15000;
        int numCols = 100;

        var input = new double[numRows, numCols];
        Random rnd = new Random(123);
        for (int col = 0; col < numCols; col++)
            for (int row = 0; row < numRows; row++)
                input[row, col] = rnd.Next(100000);

        return input;
    }

    public static void IsResultCorrect(Dictionary<string, double>[] result)
    {
        if (
            result[0]["SUM"] == 9d &&
            result[1]["SUM"] == 14d &&
            result[2]["SUM"] == 16d &&
            result[0]["AVERAGE"] == 2.25d &&
            result[1]["AVERAGE"] == 3.5d &&
            result[2]["AVERAGE"] == 4d &&
            result[0]["COUNT DISTINCT"] == 3d &&
            result[1]["COUNT DISTINCT"] == 2d &&
            result[2]["COUNT DISTINCT"] == 4d
        )
        {
            Console.WriteLine("IsResultCorrect Passed");
            return;
        }

        Console.WriteLine("IsResultCorrect Failed");
    }

    public static void IsExecutionFastEnough(double milliSeconds)
    {

        if (milliSeconds < 70)
            Console.WriteLine($"IsExecutionFastEnough DIAMOND!!!: {milliSeconds}ms");
        else if (milliSeconds < 85)
            Console.WriteLine($"IsExecutionFastEnough GOLD!: {milliSeconds}ms");
        else if (milliSeconds < 130)
            Console.WriteLine($"IsExecutionFastEnough SILVER: {milliSeconds}ms");
        else
            Console.WriteLine($"IsExecutionFastEnough BRONZE: {milliSeconds}ms");
    }
}