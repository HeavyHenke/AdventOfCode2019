using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoreLinq;

namespace AdventOfCode2019
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day3().CalcA();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    class Day3
    {
        public string CalcA()
        {
            return " ";
        }
    }
}