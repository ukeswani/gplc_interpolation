using MatrixOperations;
using System;

namespace Interpolator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var (inputFile, outputFile) = GetArgumentValues(args);
                
                var input = FileOperations.ReadInput(inputFile);

                var inputMatrix = DataOperations.ConvertAndValidateInput(input);

                var interpolationEngine = new InterpolationEngine(new FourNonDiagonalNeighboursNoAdjacentNansStrategy());

                var interpolatedMatrix = interpolationEngine.Interpolate(inputMatrix);

                FileOperations.WriteOutput(outputFile, interpolatedMatrix.AsString());
            }
            catch (Exception e)
            {
                Display(e.Message);
            }
        }

        private static (string inputFile, string outputFile) GetArgumentValues(string[] args)
        {
            ArgumentsGuard(args);

            var inputFile = string.Empty;
            var outputFile = string.Empty;

            for(var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-i":
                        inputFile = args[++i];
                        break;
                    case "-o":
                        outputFile = args[++i];
                        break;
                    default:
                        throw new ArgumentException(Usage());
                }   
            }

            return (inputFile, outputFile);
        }

        private static void ArgumentsGuard(string[] args)
        {
            if (args.Length < 4)
            {
                throw new ArgumentException(Usage());
            }
        }


        private static string Usage()
        {
            return "Usage:\n" +
                   " Interpolator.exe\n" +
                   "  -i <input filename with complete path>\n" +
                   "  -o <output filename with complete path> if path not specified, file created in same folder as executable\n";
        }

        private static void Display(string messageToDisplay)
        {
            Console.WriteLine($"\n {messageToDisplay} \n");
        }
    }
}
