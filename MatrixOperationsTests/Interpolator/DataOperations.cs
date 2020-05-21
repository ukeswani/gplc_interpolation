using System;

namespace Interpolator
{
    class DataOperations
    {
        public static double[,] ConvertAndValidateInput(string[] input)
        {
            var inputAsDoubles = ConvertInputToDoubles(input);
            ValidateInput(inputAsDoubles);
            var inputMatrix = ConvertToTwoDimensionalArray(inputAsDoubles);
            return inputMatrix;
        }

        private static double[][] ConvertInputToDoubles(string[] allLines)
        {
            if (allLines.Length == 0)
            {
                return new double[][]{};
            }

            var values = new double[allLines.Length][];
            var lineCounter = 0;

            foreach (var line in allLines)
            {
                values[lineCounter++] = 
                    Array.ConvertAll<string, double>(line.Split(',', StringSplitOptions.None), ConvertStringToDouble());
            }

            return values;
        }

        private static void ValidateInput(double[][] inputAsDoubles)
        {
            var numberOfColumns = inputAsDoubles[0].Length;

            if (numberOfColumns == 0)
            {
                throw new ArgumentException($"Number of columns cannot be zero");
            }

            foreach (var array in inputAsDoubles)
            {
                if (array.Length != numberOfColumns)
                {
                    throw new ArgumentException($"All rows must have same number of columns");

                }
            }
        }

        private static double[,] ConvertToTwoDimensionalArray(double[][] jaggedArray)
        {
            var results = new double[jaggedArray.Length, jaggedArray[0].Length];

            for (var i = 0; i <= results.GetUpperBound(0); i++)
            {
                for (var j = 0; j <= results.GetUpperBound(1); j++)
                {
                    results[i, j] = jaggedArray[i][j];
                }
            }

            return results;
        }

        private static Converter<string, double> ConvertStringToDouble()
        {
            return (s) =>
            {
                if (!double.TryParse(s, out var value))
                {
                    value = double.NaN;
                }

                return value;
            };
        }
    }
}