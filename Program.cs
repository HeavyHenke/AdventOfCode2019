using System;
using System.Collections;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MoreLinq;
using static System.Math;

namespace AdventOfCode2019
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day12().CalcA();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    class Day12
    {
        public string CalcA()
        {
            return " ";
        }
    }
}