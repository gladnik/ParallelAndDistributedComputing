//Parallel and Distributed Computing - Assignment (Todai 2014)
//By Nikolai Gladkov

using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        const int m = 5000, n = 5000, q = 5000;
        float[,] matrixA = new float[m, n];
        float[,] matrixB = new float[n, q];
        float[,] matrixC = new float[m, q];
        Stopwatch stopwatch = new Stopwatch();
        Random rand = new Random();

        //Initialization of matrices A and B
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrixA[i, j] = (float)rand.NextDouble() * 100;
            }
        }
        for (int j = 0; j < n; j++)
        {
            for (int k = 0; k < q; k++)
            {
                matrixB[j, k] = (float)rand.NextDouble() * 100;
            }
        }

        //Sequential matrix multiplication
        stopwatch.Start();
        for (int i = 0; i < m; i++)
        {
            for (int k = 0; k < q; k++)
            {
                float sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += matrixA[i, j] * matrixB[j, k];
                }
                matrixC[i, k] = sum;
            }
        }
        stopwatch.Stop();
        float timeSequential = (float)stopwatch.ElapsedMilliseconds / 1000;
        Console.WriteLine("Sequential loop: {0} seconds", timeSequential);
        stopwatch.Reset();
        matrixC = new float[m, q];

        //Parallel matrix multiplication
        stopwatch.Start();
        ParallelOptions options = new ParallelOptions();
        options.MaxDegreeOfParallelism = 2;
        Parallel.For(0, m, options, i =>
        {
            for (int k = 0; k < q; k++)
            {
                float sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += matrixA[i, j] * matrixB[j, k];
                }
                matrixC[i, k] = sum;
            }
        });
        stopwatch.Stop();
        float timeParallel = (float)stopwatch.ElapsedMilliseconds / 1000;
        Console.WriteLine("Parallel loop: {0} seconds", timeParallel);
        Console.WriteLine("Speedup - Parallel to Sequential: {0}", timeSequential / timeParallel);

        Console.ReadKey();
    }
}