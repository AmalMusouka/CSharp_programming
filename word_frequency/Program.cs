using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, int> CountFrequencies(string path)
    {
        Dictionary<string, int> word_freq = new Dictionary<string, int>();

        using (StreamReader reader = new StreamReader(path))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    if (word_freq.ContainsKey(word))
                    {
                        word_freq[word]++;
                    }
                    else
                    {
                        word_freq[word] = 1;
                    }
                }
            }
        }
        return word_freq;
    }

    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Argument Error");
            return;
        }

        Dictionary<string, int> result;

        try
        {            
            result = CountFrequencies(args[0]);

            List<string> keys = new List<string>(result.Keys);
            keys.Sort();    

            foreach (string word in keys)
            {
                Console.WriteLine(word + ": " + result[word]);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("File Error");
        }
    }
}