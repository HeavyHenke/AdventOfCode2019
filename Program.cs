using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode2019
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day2().CalcA();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    class Day2
    {
        private static string _iput = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,6,19,1,19,6,23,2,23,6,27,2,6,27,31,2,13,31,35,1,9,35,39,2,10,39,43,1,6,43,47,1,13,47,51,2,6,51,55,2,55,6,59,1,59,5,63,2,9,63,67,1,5,67,71,2,10,71,75,1,6,75,79,1,79,5,83,2,83,10,87,1,9,87,91,1,5,91,95,1,95,6,99,2,10,99,103,1,5,103,107,1,107,6,111,1,5,111,115,2,115,6,119,1,119,6,123,1,123,10,127,1,127,13,131,1,131,2,135,1,135,5,0,99,2,14,0,0";

        public string CalcA()
        {
            var intArr = _iput.Split(',').Select(int.Parse).ToArray();
            int ip = 0;

            intArr[1] = 12;
            intArr[2] = 2;

            do
            {
                if (intArr[ip] == 1)
                {
                    intArr[intArr[ip + 3]] = intArr[intArr[ip + 1]] + intArr[intArr[ip + 2]];
                    ip += 4;
                }
                else if (intArr[ip] == 2)
                {
                    intArr[intArr[ip + 3]] = intArr[intArr[ip + 1]] * intArr[intArr[ip + 2]];
                    ip += 4;
                }
                else
                    throw new Exception("Invalid op code " + intArr[ip]);
            } while (intArr[ip] != 99);

            return intArr[0].ToString();
        }
    }
}