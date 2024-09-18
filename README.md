# Efficient-Aggregation-Engine

This repository contains two projects aimed at solving an aggregation problem efficiently for small and large datasets. The goal is to implement aggregation functions (SUM, AVERAGE, COUNT DISTINCT) across multiple columns of a dataset, with optimized performance for different data volumes.

## Projects Overview

### 1. Small Dataset Solution (Project 1)
- **Description**: This project provides an efficient solution for handling small datasets using basic data structures like `HashSet` to count distinct values. It performs aggregations in a single loop over the dataset to minimize overhead.
- **Key Aggregations**:
  - **SUM**: Calculates the sum of each column.
  - **AVERAGE**: Computes the average value for each column.
  - **COUNT DISTINCT**: Uses a `HashSet` to determine the distinct count for each column.
- **Performance**: Optimized for smaller datasets where memory and processing overhead is less of a concern. Direct use of arrays ensures fast access and minimal memory consumption.
 The code is also available on .NET Fiddle for simple testing:[ https://dotnetfiddle.net/wghhqi ].

### 2. Large Dataset Solution (Project 2)
- **Description**: This project leverages the `CardinalityEstimator` (HyperLogLog) library to efficiently estimate the count of distinct values in each column. This is critical for handling large datasets where an exact count might be too slow or resource-intensive.
- **Key Aggregations**:
  - **SUM**: Calculates the sum of each column.
  - **AVERAGE**: Computes the average value for each column.
  - **COUNT DISTINCT**: Uses HyperLogLog via `CardinalityEstimator` for an efficient, probabilistic distinct count.
- **Performance**: Designed for large datasets, this solution minimizes memory usage while maintaining accuracy for distinct counts. The HyperLogLog algorithm is highly efficient for large-scale data aggregation tasks.

## How to Use

### Small Dataset
1. Navigate to the `SmallDatasetSolution` folder.
2. Run the project in your preferred IDE (e.g., Visual Studio).
3. Input a small dataset, and the program will return the aggregated results (SUM, AVERAGE, COUNT DISTINCT).

### Large Dataset
1. Navigate to the `LargeDatasetSolution` folder.
2. Run the project using Visual Studio.
3. The solution uses the `CardinalityEstimator` library for estimating distinct values, ideal for larger datasets.
4. Ensure the required libraries are installed using `NuGet Package Manager`.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/OptimizedAggregationEngine.git
