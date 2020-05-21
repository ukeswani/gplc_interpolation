using System;
using System.IO;

internal static class FileOperations
{
    public static bool WriteOutput(string outputFile, string output)
    {
        if (String.IsNullOrWhiteSpace(outputFile))
        {
            throw new ArgumentException($"{nameof(outputFile)} is empty. Value provided: {outputFile}.");
        }

        try
        {
            File.AppendAllLines(outputFile, new [] {"\n", output, "\n"});
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to write \n{output}\n to file {outputFile}." +
                                $"\n Exception details: {e}");
        }

        return true;
    }

    public static string[] ReadInput(string inputFile)
    {
        if (String.IsNullOrWhiteSpace(inputFile))
        {
            throw new ArgumentException($"{nameof(inputFile)} is empty. Value provided: {inputFile}.");
        }

        if (!File.Exists(inputFile))
        {
            throw new ArgumentException($"{nameof(inputFile)} does not exist. Value provided: {inputFile}.");
        }

        try
        {
            var allLines = File.ReadAllLines(inputFile);
            return allLines;
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Failed to read: {inputFile}." +
                                        $"\nException details: {e.Message}");
        }
    }
}