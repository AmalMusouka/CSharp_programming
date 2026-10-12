using System;
using System.IO;

class Program
{
    static long CountWords(string path)
    {
        long count = 0;
        bool inWord = false;

        using (StreamReader reader = new StreamReader(path))
        {
            int ch;
            while ((ch = reader.Read()) != -1)
            {
                if (ch == ' ' || ch == '\t' || ch == '\n')
                {
                    inWord = false;
                }
                else if (!inWord)
                {
                    inWord = true;
                    count++;
                }
            }
        }
        return count;
    }

    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Argument Error");
            return;
        }

        try
        {
            long result = CountWords(args[0]);
            Console.WriteLine(result);
        }
        catch (Exception)
        {
            Console.WriteLine("File Error");
        }
    }
}