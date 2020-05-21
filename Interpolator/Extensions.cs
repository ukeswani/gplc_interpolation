using System;
using System.Globalization;
using System.Text;

namespace Interpolator
{
    static class Extensions
    {
        public static string AsString(this double[,] matrix)
        {
            var builder = new StringBuilder();

            for (var i = 0; i <= matrix.GetUpperBound(0); i++)
            {
                var line = new string[matrix.GetUpperBound(1) + 1];

                for (var j = 0; j <= matrix.GetUpperBound(1); j++)
                { 
                    line[j] = Convert.ToString(matrix[i, j], CultureInfo.InvariantCulture);
                }

                builder.Append(string.Join(',', line));
                builder.Append("\n");
            }

            return builder.ToString();
        }
    }
}