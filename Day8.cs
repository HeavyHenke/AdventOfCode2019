using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace AdventOfCode2019
{
    class Day8
    {
        public string CalcA()
        {
            var indata = File.ReadAllText("Day8Data.txt").Select(d => d - '0');
            var imageLayers = new List<int[]>();

            imageLayers.Add(new int[25*6]);
            int layerIx = 0;
            int ix = 0;

            foreach (var digit in indata)
            {
                if (ix >= 25*6)
                {
                    ix = 0;
                    layerIx++;
                    imageLayers.Add(new int[25 * 6]);
                }

                imageLayers[layerIx][ix++] = digit;
            }

            var numZero = new int[imageLayers.Count];
            for (int i = 0; i < imageLayers.Count; i++)
            {
                numZero[i] = imageLayers[i].Count(d => d == 0);
            }

            var min = numZero.Index().MinBy(d => d.Value).Key;
            var num1 = imageLayers[min].Count(d => d == 1);
            var num2 = imageLayers[min].Count(d => d == 2);


            var resultingLayer = new int[25 * 6];
            for (int i = 0; i < 25 * 6; i++)
            {
                int pix = 2;
                for (layerIx = 0; layerIx < imageLayers.Count && pix == 2; layerIx++)
                {
                    pix = imageLayers[layerIx][i];
                }

                resultingLayer[i] = pix;
            }

            ix = 0;
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    if(resultingLayer[ix++] == 1)
                        Console.Write('*');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }

            return (num1*num2).ToString();
        }
    }
}