ParallelAndDistributedComputing
===============================

Assignment for Parallel and Distributed Computing 2014 course at Todai.

**Implement a program that multiplies matrices in sequential and parallel way, compare performance of 1, 2, 4 threads.**

1. C++ source code is [here](https://github.com/gladnik/ParallelAndDistributedComputing/blob/master/MatrixMultiplicationCPP/MatrixMultiplicationCPP/MatrixMultiplication.cpp).

 ```C++
int i, j, k;
omp_set_num_threads(4);
#pragma omp parallel for shared (matrixA, matrixB, matrixC) private (i, j, k)
for (i = 0; i < m; i++)
{
	for (k = 0; k < q; k++)
	{
		float sum = 0;
		for (j = 0; j < n; j++)
		{
			sum += matrixA[i][j] * matrixB[j][k]; 
		}
		matrixC[i][k] = sum;
	}
}
    ```
 
2. C# source code is [here](https://github.com/gladnik/ParallelAndDistributedComputing/blob/master/MatrixMultiplicationCS/MatrixMultiplicationCS/MatrixMultiplication.cs).
 
 ```C#
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
 ```
