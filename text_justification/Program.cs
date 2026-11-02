using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Justifier
{
    private int width;
    private TextWriter output;

    private List<string> words = new List<string>();
    private int wordLength = 0;

    private bool wroteInParagraph = false;
    private bool pendingSeparator = false;

    public Justifier(int width, TextWriter output)
    {
        this.width = width;
        this.output = output;
    }

    public void Format(TextReader input)
    {
        StringBuilder word = new StringBuilder();
        bool inLongWord = false;
        int newlines = 0;
        int c;

        while ((c = input.Read()) != -1)
        {
            char ch = (char)c;

            if (ch == ' ' || ch == '\t' || ch == '\n')
            {
                if (inLongWord)
                {
                    output.Write('\n');
                    inLongWord = false;
                    newlines = 0;
                }
                else if (word.Length > 0)
                {
                    AddWord(word.ToString());
                    word.Clear();
                    newlines = 0;
                }

                if (ch == '\n')
                {
                    newlines++;
                    if (newlines == 2)
                    {
                        EndParagraph();
                    }
                }
            }
            else if (inLongWord)
            {
                output.Write(ch);
            }
            else if (word.Length == width)
            {
                WriteJustifiedLine();
                BeginLine();
                output.Write(word.ToString());
                output.Write(ch);
                word.Clear();
                inLongWord = true;
            }
            else
            {
                word.Append(ch);
            }
        }

        if (inLongWord)
        {
            output.Write('\n');
        }
        else if (word.Length > 0)
        {
            AddWord(word.ToString());
        }
        WriteLastLine();
    }

    private void AddWord(string word)
    {
        if (words.Count > 0 && wordLength + words.Count + word.Length > width)
        {
            WriteJustifiedLine();
        }
        words.Add(word);
        wordLength += word.Length;
    }

    private void EndParagraph()
    {
        WriteLastLine();
        if (wroteInParagraph)
        {
            wroteInParagraph = false;
            pendingSeparator = true;
        }
    }

    private void BeginLine()
    {
        if (pendingSeparator)
        {
            output.Write('\n');
            pendingSeparator = false;
        }
        wroteInParagraph = true;
    }

    private void WriteJustifiedLine()
    {
        int n = words.Count;
        if (n == 0) return;

        BeginLine();
        if (n == 1)
        {
            output.Write(words[0]);
        }
        else
        {
            int gaps = n - 1;
            int spaces = width - wordLength;
            int basic = spaces / gaps;
            int extra = spaces % gaps;

            for (int i = 0; i < n; i++)
            {
                output.Write(words[i]);
                if (i < gaps)
                {
                    int count = basic;
                    if (i < extra) count++;
                    output.Write(new string(' ', count));
                }
            }
        }
        output.Write('\n');
        words.Clear();
        wordLength = 0;
    }

    private void WriteLastLine()
    {
        if (words.Count == 0) 
        {
            return;
        }

        BeginLine();
        for (int i = 0; i < words.Count; i++)
        {
            if (i > 0) output.Write(' ');
            output.Write(words[i]);
        }
        output.Write('\n');
        words.Clear();
        wordLength = 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int width;
        if (args.Length != 3 || !int.TryParse(args[2], out width) || width <= 0)
        {
            Console.WriteLine("Argument Error");
            return;
        }

        try
        {
            using (StreamReader reader = new StreamReader(args[0]))
            using (StreamWriter writer = new StreamWriter(args[1]))
            {
                Justifier justifier = new Justifier(width, writer);
                justifier.Format(reader);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("File Error");
        }
    }
}