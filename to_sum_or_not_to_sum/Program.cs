using System;
using System.Globalization;
using System.IO;

class Program
{
    static string? CheckColumn(string input, string output, string column)
    {
        char[] separators = { ' ', '\t' };
        long sum = 0;

        using (StreamReader reader = new StreamReader(input))
        {
            string? line = reader.ReadLine();
            if (line == null)
            {
                return "Invalid File Format";
            }

            string[] columnNames = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int width = columnNames.Length;
            if (width == 0)
            {
                return "Invalid File Format";
            } 

            int columnIndex = Array.IndexOf(columnNames, column);
            if (columnIndex < 0)
            {
                return "Non-existent Column Name";
            }

            while ((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length != width)
                {
                    return "Invalid File Format";
                }

                int value;
                if (!int.TryParse(words[columnIndex], out value))
                {
                    return "Invalid Integer Value";
                }

                sum += value;
            }
        }

        using (StreamWriter writer = new StreamWriter(output))
        {
            writer.Write(column + "\n");
            writer.Write(new string('-', column.Length) + "\n");
            writer.Write(sum + "\n");
        }

        return null;
    }

    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Argument Error");
            return;
        }

        try
        {
            string? error = CheckColumn(args[0], args[1], args[2]);
            if (error != null)
            {
                Console.WriteLine(error);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("File Error");
        }
    }
}