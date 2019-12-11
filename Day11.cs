using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    class Day11
    {
        enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        };

        public string CalcA()
        {
            var machine = new IntMachineClass(new[]
            {
                3, 8, 1005, 8, 301, 1106, 0, 11, 0, 0, 0, 104, 1, 104, 0, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10,
                1008, 8, 0, 10, 4, 10, 1002, 8, 1, 29, 1, 1103, 7, 10, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10, 4, 10, 108,
                1, 8, 10, 4, 10, 1002, 8, 1, 54, 2, 103, 3, 10, 2, 1008, 6, 10, 1006, 0, 38, 2, 1108, 7, 10, 3, 8, 102,
                -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 1, 8, 10, 4, 10, 1001, 8, 0, 91, 3, 8, 1002, 8, -1, 10, 1001,
                10, 1, 10, 4, 10, 1008, 8, 0, 10, 4, 10, 101, 0, 8, 114, 3, 8, 1002, 8, -1, 10, 101, 1, 10, 10, 4, 10,
                1008, 8, 1, 10, 4, 10, 1001, 8, 0, 136, 3, 8, 1002, 8, -1, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 1, 10,
                4, 10, 1002, 8, 1, 158, 1, 1009, 0, 10, 2, 1002, 18, 10, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10, 4, 10,
                108, 0, 8, 10, 4, 10, 1002, 8, 1, 187, 2, 1108, 6, 10, 3, 8, 1002, 8, -1, 10, 1001, 10, 1, 10, 4, 10,
                108, 0, 8, 10, 4, 10, 1002, 8, 1, 213, 3, 8, 1002, 8, -1, 10, 101, 1, 10, 10, 4, 10, 1008, 8, 1, 10, 4,
                10, 1001, 8, 0, 236, 1, 104, 10, 10, 1, 1002, 20, 10, 2, 1008, 9, 10, 3, 8, 102, -1, 8, 10, 101, 1, 10,
                10, 4, 10, 108, 0, 8, 10, 4, 10, 101, 0, 8, 269, 1, 102, 15, 10, 1006, 0, 55, 2, 1107, 15, 10, 101, 1,
                9, 9, 1007, 9, 979, 10, 1005, 10, 15, 99, 109, 623, 104, 0, 104, 1, 21102, 1, 932700598932, 1, 21102,
                318, 1, 0, 1105, 1, 422, 21102, 1, 937150489384, 1, 21102, 329, 1, 0, 1105, 1, 422, 3, 10, 104, 0, 104,
                1, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 0, 3, 10,
                104, 0, 104, 1, 21101, 46325083227, 0, 1, 21102, 376, 1, 0, 1106, 0, 422, 21102, 3263269927, 1, 1,
                21101, 387, 0, 0, 1105, 1, 422, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 0, 21102, 988225102184, 1, 1,
                21101, 410, 0, 0, 1105, 1, 422, 21101, 868410356500, 0, 1, 21102, 1, 421, 0, 1106, 0, 422, 99, 109, 2,
                21202, -1, 1, 1, 21102, 1, 40, 2, 21102, 1, 453, 3, 21102, 1, 443, 0, 1105, 1, 486, 109, -2, 2106, 0, 0,
                0, 1, 0, 0, 1, 109, 2, 3, 10, 204, -1, 1001, 448, 449, 464, 4, 0, 1001, 448, 1, 448, 108, 4, 448, 10,
                1006, 10, 480, 1102, 1, 0, 448, 109, -2, 2106, 0, 0, 0, 109, 4, 1201, -1, 0, 485, 1207, -3, 0, 10, 1006,
                10, 503, 21101, 0, 0, -3, 22101, 0, -3, 1, 21201, -2, 0, 2, 21102, 1, 1, 3, 21101, 0, 522, 0, 1105, 1,
                527, 109, -4, 2106, 0, 0, 109, 5, 1207, -3, 1, 10, 1006, 10, 550, 2207, -4, -2, 10, 1006, 10, 550,
                22102, 1, -4, -4, 1105, 1, 618, 21201, -4, 0, 1, 21201, -3, -1, 2, 21202, -2, 2, 3, 21102, 569, 1, 0,
                1106, 0, 527, 22101, 0, 1, -4, 21101, 0, 1, -1, 2207, -4, -2, 10, 1006, 10, 588, 21102, 1, 0, -1, 22202,
                -2, -1, -2, 2107, 0, -3, 10, 1006, 10, 610, 21201, -1, 0, 1, 21101, 610, 0, 0, 105, 1, 485, 21202, -2,
                -1, -2, 22201, -4, -2, -4, 109, -5, 2105, 1, 0
            });
            var visited = new Dictionary<(int x, int y), bool>();
            var pos = (x: 0, y: 0);
            var dir = Direction.Up;

            visited[(0, 0)] = true;

            while (true)
            {
                if (visited.TryGetValue(pos, out var val) == false)
                {
                    val = false;
                }

                var out1 = machine.RunUntil(val ? 1 : 0, true, false);
                if (out1.HasValue == false)
                    break;
                visited[pos] = (out1 == 1);

                var out2 = machine.RunUntil(val ? 1 : 0, true, false);
                if (out2 == 0)
                {
                    dir--;
                    if ((int) dir == -1)
                        dir = Direction.Left;
                }
                else
                {
                    dir++;
                    if ((int) dir == 4)
                        dir = Direction.Up;
                }

                switch (dir)
                {
                    case Direction.Up:
                        pos = (pos.x, pos.y - 1);
                        break;
                    case Direction.Right:
                        pos = (pos.x + 1, pos.y);
                        break;
                    case Direction.Down:
                        pos = (pos.x, pos.y + 1);
                        break;
                    case Direction.Left:
                        pos = (pos.x - 1, pos.y);
                        break;
                }
            }

            int minX = visited.Min(d => d.Key.x);
            int maxX = visited.Max(d => d.Key.x);
            int minY = visited.Min(d => d.Key.y);
            int maxY = visited.Max(d => d.Key.y);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    bool val;
                    if (visited.TryGetValue((x, y), out val) == false)
                        val = false;
                    Console.Write(val? '#' : ' ');
                }
                Console.WriteLine();
            }

            return visited.Count.ToString();
        }

        private class IntMachineClass
        {
            private long[] _machineCode;
            private int _ip = 0;
            private int _relativeBase = 0;

            public IntMachineClass(long[] machineCode)
            {
                _machineCode = (long[]) machineCode.Clone();
            }

            private void EnsureMemory(long address)
            {
                if (_machineCode.Length <= address)
                {
                    Array.Resize(ref _machineCode, (int) address + 100);
                }
            }

            public long? RunUntil(long input, bool haltOnOutput, bool haltOnInput)
            {
                long GetIx(int mode, int lip)
                {
                    mode %= 10;
                    if (mode == 0)
                    {
                        return _machineCode[lip];
                    }

                    if (mode == 1)
                    {
                        return lip;
                    }

                    if (mode == 2)
                    {
                        return _machineCode[lip] + _relativeBase;
                    }

                    throw new Exception("Unknown operational mode: " + mode);
                }

                long GetOperand(int mode, int lip)
                {
                    var ix = GetIx(mode, lip);
                    EnsureMemory(ix);
                    return _machineCode[ix];
                }

                do
                {
                    int mode = (int) (_machineCode[_ip] / 100);
                    int op = (int) (_machineCode[_ip] % 100);

                    switch (op)
                    {
                        case 1: // add
                        {
                            var val1 = GetOperand(mode, _ip + 1);
                            var val2 = GetOperand(mode / 10, _ip + 2);
                            var ix = GetIx(mode / 100, _ip + 3);
                            EnsureMemory(ix);
                            _machineCode[ix] = val1 + val2;
                            _ip += 4;
                            break;
                        }
                        case 2: // mul
                        {
                            var val1 = GetOperand(mode, _ip + 1);
                            var val2 = GetOperand(mode / 10, _ip + 2);
                            var ix = GetIx(mode / 100, _ip + 3);
                            EnsureMemory(ix);
                            _machineCode[ix] = val1 * val2;
                            _ip += 4;
                            break;
                        }
                        case 3: // input 
                        {
                            var ix = GetIx(mode, _ip + 1);
                            EnsureMemory(ix);
                            _machineCode[ix] = input;
                            _ip += 2;
                            if (haltOnInput)
                                return null;
                            break;
                        }
                        case 4: // output
                        {
                            var output = GetOperand(mode, _ip + 1);
                            _ip += 2;
                            if (haltOnOutput)
                                return output;
                            break;
                        }
                        case 5: // jump-if-true
                        {
                            var val1 = GetOperand(mode, _ip + 1);
                            if (val1 != 0)
                                _ip = (int) GetOperand(mode / 10, _ip + 2);
                            else
                                _ip += 3;
                            break;
                        }
                        case 6: // jump-if-false
                        {
                            var val1 = GetOperand(mode, _ip + 1);
                            if (val1 == 0)
                                _ip = (int) GetOperand(mode / 10, _ip + 2);
                            else
                                _ip += 3;
                            break;
                        }
                        case 7: // less than
                        {
                            var val1 = GetOperand(mode, _ip + 1);
                            var val2 = GetOperand(mode / 10, _ip + 2);
                            var ix = GetIx(mode / 100, _ip + 3);
                            EnsureMemory(ix);
                            _machineCode[ix] = val1 < val2 ? 1 : 0;
                            _ip += 4;
                            break;
                        }
                        case 8: // equals
                        {
                            var val1 = GetOperand(mode, _ip + 1);
                            var val2 = GetOperand(mode / 10, _ip + 2);
                            var ix = GetIx(mode / 100, _ip + 3);
                            EnsureMemory(ix);
                            _machineCode[ix] = (val1 == val2) ? 1 : 0;
                            _ip += 4;
                            break;
                        }
                        case 9: // Adjust relative base
                        {
                            var val1 = GetOperand(mode, _ip + 1);
                            _relativeBase += (int) val1;
                            _ip += 2;
                            break;
                        }

                        case 99:
                            return null;
                        default:
                            throw new Exception("Invalid op code " + _machineCode[_ip]);
                    }
                } while (_machineCode[_ip] != 99);

                return null;
            }
        }
    }
}