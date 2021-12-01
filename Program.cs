using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MoreLinq;

namespace AdventOfCode2019
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day16().CalcBTest()?.ToString() ?? " ";
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    class Day16
    {
        public object CalcA()
        {
            var inpString = "59728839950345262750652573835965979939888018102191625099946787791682326347549309844135638586166731548034760365897189592233753445638181247676324660686068855684292956604998590827637221627543512414238407861211421936232231340691500214827820904991045564597324533808990098343557895760522104140762068572528148690396033860391137697751034053950225418906057288850192115676834742394553585487838826710005579833289943702498162546384263561449255093278108677331969126402467573596116021040898708023407842928838817237736084235431065576909382323833184591099600309974914741618495832080930442596854495321267401706790270027803358798899922938307821234896434934824289476011";
            var inp2 = inpString.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
            var inp = new int[inp2.Length * 10000];
            for(int i = 0; i < 10000; i++)
                Array.Copy(inp2, 0, inp, i*inp2.Length, inp2.Length);

            var offset = int.Parse(inpString.Substring(0, 7));
            
            for (int phase = 0; phase < 100; phase++)
            {
                Console.WriteLine("Phase " + phase);
                var outp = new int[inp.Length];
                for (int i = 0; i < inp.Length; i++)
                {
                    using var fftVect = GetFftVector(i + 1).GetEnumerator();
                    fftVect.MoveNext();
                    fftVect.MoveNext();

                    int temp = 0;

                    foreach (var t in inp)
                    {
                        var cur = fftVect.Current;
                        if (cur == 1)
                            temp += t;
                        else if(cur == -1)
                            temp -= t;
                        fftVect.MoveNext();
                    }

                    outp[i] = Math.Abs(temp) % 10;
                }

                inp = outp;
            }
 

            var result = new StringBuilder(inp.Length);
            for(int i = 0; i < 8; i++)
                result.Append(inp[i+offset]);
            
            return result.ToString();
        }

        public object CalcB()
        {
            var inpString = "03036732577212944063491565474664";
            var inp2 = inpString.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

            var inp = new int[inp2.Length * 10000];
            for(int i = 0; i < 10000; i++)
                Array.Copy(inp2, 0, inp, i*inp2.Length, inp2.Length);
            
            var offset = int.Parse(inpString.Substring(0, 7));
            
            
            var result = new StringBuilder(8);
            for (int i = 0; i < 8; i++)
            {
                var value = GetAt(inp, i + offset, 3);
                Console.Write(value);
                result.Append(value);
            }
            
            Console.WriteLine();
            return result.ToString();
        }
        public object CalcBTest()
        {
            var e1 = GetFftVector(350000).Skip(1).GetEnumerator();
            var e2 = GetFftVector(350001).Skip(1).GetEnumerator();

            int diff = 0;
            for (int i = 0; i < 650000; i++)
            {
                e1.MoveNext();
                e2.MoveNext();

                if (e1.Current != e2.Current)
                    diff++;
            }
            
            
            //var inpString = "12345678";
            var inpString = "59728839950345262750652573835965979939888018102191625099946787791682326347549309844135638586166731548034760365897189592233753445638181247676324660686068855684292956604998590827637221627543512414238407861211421936232231340691500214827820904991045564597324533808990098343557895760522104140762068572528148690396033860391137697751034053950225418906057288850192115676834742394553585487838826710005579833289943702498162546384263561449255093278108677331969126402467573596116021040898708023407842928838817237736084235431065576909382323833184591099600309974914741618495832080930442596854495321267401706790270027803358798899922938307821234896434934824289476011";
            var inp2 = inpString.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

            var inp = new int[inp2.Length * 10000];
            for(int i = 0; i < 10000; i++)
                Array.Copy(inp2, 0, inp, i*inp2.Length, inp2.Length);

            var val0 = GetAt(inp, 0, 100);

            return val0;
        }


        private int GetAt2(int[] vect, int ix,  int lastValue)
        {
            int chunkLength = ix + 1;
            int firstNonZero = chunkLength - 1;
            int ix2 = firstNonZero;

            var result = lastValue;
            int numInter = 1;
            while (ix2 < vect.Length)
            {
                
            }

            return result;
        }
        
        
        private int GetAt(int[] vect, int ix, int phase)
        {
            if (phase == 0)
                return vect[ix];
            
            int result = 0;

            int chunkLength = ix + 1;
            int firstNonZero = chunkLength - 1;
            int ix2 = firstNonZero;

            var maxLength = vect.Length;
            while (ix2 < maxLength)
            {
                for (int i = 0; i < chunkLength && ix2 < maxLength; i++, ix2++)
                {
                    result += GetAt(vect, ix2, phase - 1);
                }

                ix2 += chunkLength;
                for (int i = 0; i < chunkLength && ix2 < maxLength; i++, ix2++)
                {
                    result -= GetAt(vect, ix2, phase - 1);
                }

                ix2 += chunkLength;
            }

            result = Math.Abs(result % 10);
            return result;
        }
        
        private IEnumerable<int> GetFftVector(int numRep)
        {
            var vector = new[] {0, 1, 0, -1};

            int ix = 0;

            while (true)
            {
                for (int r = 0; r < numRep; r++)
                {
                    yield return vector[ix];
                }

                ix++;
                ix &= 3;
            }
        }
    }
}