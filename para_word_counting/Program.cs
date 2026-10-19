using System;
using System.IO;

class Program
{
    static void CountWords(string path)
    {
        long count = 0;
        bool inWord = false;
        bool lineHasWord = false;

        using (StreamReader reader = new StreamReader(path))
        {
            int ch;
            while ((ch = reader.Read()) != -1)
            {
                if (ch == ' ' || ch == '\t')
                {
                    inWord = false;
                }
                else if (ch == '\n')
                {
                    inWord = false;
                    if (lineHasWord)
                    {
                        lineHasWord = false;
                    }
                    else
                    {
                        if (count > 0)
                        {
                            Console.WriteLine(count);
                            count = 0;
                        }
                    }
                }
                else if (!inWord)
                {
                    inWord = true;
                    lineHasWord = true;
                    count++;
                }
            }
        }
        
        if (count > 0)
        {
            Console.WriteLine(count);
        }
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
            CountWords(args[0]);
        }
        catch (Exception)
        {
            Console.WriteLine("File Error");
        }
    }
}