using ConfigManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TuringMachine
{
    class Program
    {
        class State
        {
            [ConfigDataSource("$0")] public string on;
            [ConfigDataSource("$1")] public string to;
            [ConfigDataSource("$2")] public string move;
            [ConfigDataSource("$3")] public int nextState;
        }

        class Machine
        {
            public List<string> input;
            public int tapeSize;

            public List<List<State>> states;

            [ConfigDataSource(null)] public int position;
            [ConfigDataSource(null)] public int state;
        }

        static void Main(string[] args)
        {
            var m = Config.LoadToClassFromFile<Machine>("prog.tm");

            var buf = new List<string>(m.tapeSize);
            buf.AddRange(Enumerable.Repeat("", buf.Capacity));

            for (int i = 0; i < m.input.Count; ++i)
                buf[i] = m.input[i];


            Console.WriteLine("Input tape:");
            Console.WriteLine($" {String.Join(" | ", buf)}\n");

            Console.WriteLine("\nActive tape:");
            while (m.state >= 0)
            {
                bool found = false;
                foreach (var c in m.states[m.state])
                {
                    if (c.on != buf[m.position])
                        continue;

                    if (c.on != c.to)
                    {
                        Console.WriteLine($" {String.Join(" | ", buf)}");
                        Console.Write(new String(' ', buf.Take(m.position).Select(v => v.Length).Sum() + m.position * 3));
                        Console.WriteLine(new String('^', buf[m.position].Length + 2));
                    }

                    buf[m.position] = c.to;
                    m.position = (buf.Count + m.position + (c.move == ">" ? 1 : 0) - (c.move == "<" ? 1 : 0)) % buf.Count;
                    m.state = c.nextState;
                    found = true;
                }

                if (!found)
                {
                    Console.Error.WriteLine("Nothing found for current state");
                    break;
                }
            }


            Console.WriteLine("\nOutput tape:");
            Console.WriteLine($" {String.Join(" | ", buf)}");
            Console.Write(new String(' ', buf.Take(m.position).Select(v => v.Length).Sum() + m.position * 3));
            Console.WriteLine(new String('^', buf[m.position].Length + 2));
        }
    }
}
