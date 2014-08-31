//Parallel and Distributed Computing - Assignment (Todai 2014)
//By Nikolai Gladkov

#include "stdafx.h"
#include <omp.h>
#include <ctime>
#include <iostream>

const int m = 1000, n = 1000, q = 1000;
float matrixA[m][n], matrixB[n][q], matrixC[m][q];

int main()
{
	//Initialization of matrices A and B
	for (int i = 0; i < m; i++)
	{
		for (int j = 0; j < n; j++)
		{
			matrixA[i][j] = (float)rand() / RAND_MAX * 100; 
		}
	}
	for (int j = 0; j < n; j++)
	{
		for (int k = 0; k < q; k++)
		{
			matrixB[j][k] = (float)rand() / RAND_MAX * 100;
		}
	}

	//Sequential matrix multiplication
	const clock_t startSequential = clock();
	for (int i = 0; i < m; i++)
	{
		for (int k = 0; k < q; k++)
		{
			float sum = 0;
			for (int j = 0; j < n; j++)
			{
				sum += matrixA[i][j] * matrixB[j][k]; 
			}
			matrixC[i][k] = sum;
		}
	}
	const double timeSequential = static_cast<double>(clock() - startSequential) / CLOCKS_PER_SEC;
	std::cout << "Sequential loop: " << timeSequential << " seconds" << std::endl;
	memset(matrixC, 0, sizeof(matrixC));

	//Parallel matrix multiplication via OpenMP
	const clock_t startOMP = clock();
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
	const double timeOMP = static_cast<double>(clock() - startOMP) / CLOCKS_PER_SEC;
	std::cout << "Parallel loop: " << timeOMP << " seconds" << std::endl;
	std::cout << "Speedup - OpenMP to Sequential: " << timeSequential/timeOMP << std::endl;

	return 0;
}