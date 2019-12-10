using System;

namespace AdventOfCode2019
{
    class Day7
    {
        public string CalcA()
        {
            var machineCode = new int[] { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 38, 63, 80, 105, 118, 199, 280, 361, 442, 99999, 3, 9, 102, 5, 9, 9, 1001, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 1001, 9, 4, 9, 102, 4, 9, 9, 101, 4, 9, 9, 102, 2, 9, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 5, 9, 102, 4, 9, 9, 1001, 9, 4, 9, 4, 9, 99, 3, 9, 101, 3, 9, 9, 1002, 9, 5, 9, 101, 3, 9, 9, 102, 5, 9, 9, 101, 3, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 1001, 9, 4, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99 };

            int maxValue = int.MinValue;
            var maxPhase = new int[5];

            var phaseSetting = new int[5];
            for (int a = 0; a < 5; a++)
            {
                phaseSetting[0] = a;
                var fromA = IntMachine(machineCode, a, 0);

                for (int b = 0; b < 5; b++)
                {
                    if (b == a) continue;
                    phaseSetting[1] = b;
                    var fromB = IntMachine(machineCode, b, fromA);

                    for (int c = 0; c < 5; c++)
                    {
                        if (c == a || c == b) continue;
                        phaseSetting[2] = c;
                        var fromC = IntMachine(machineCode, c, fromB);

                        for (int d = 0; d < 5; d++)
                        {
                            if (d == a || d == b || d == c) continue;
                            phaseSetting[3] = d;
                            var fromD = IntMachine(machineCode, d, fromC);

                            for (int e = 0; e < 5; e++)
                            {
                                if (e == a || e == b || e == c || e == d) continue;
                                phaseSetting[4] = e;
                                var output = IntMachine(machineCode, e, fromD);
                                if (output > maxValue)
                                {
                                    maxValue = output;
                                    Array.Copy(phaseSetting, maxPhase, 5);
                                }
                            }
                        }
                    }
                }
            }


            return maxValue.ToString();
        }

        private int IntMachine(int[] intArr, int firstInput, int secondInput)
        {
            int ip = 0;
            int input = firstInput;
            int output = 0;

            int GetOperand(int mode, int lip)
            {
                mode %= 10;
                if (mode == 0)
                    return intArr[intArr[lip]];
                if (mode == 1)
                    return intArr[lip];
                throw new Exception("Unknown operational mode: " + mode);
            }

            do
            {
                int mode = intArr[ip] / 100;
                int op = intArr[ip] % 100;

                switch (op)
                {
                    case 1: // add
                        {
                            var val1 = GetOperand(mode, ip + 1);
                            var val2 = GetOperand(mode / 10, ip + 2);
                            var ix = intArr[ip + 3];
                            intArr[ix] = val1 + val2;
                            ip += 4;
                            break;
                        }
                    case 2: // mul
                        {
                            var val1 = GetOperand(mode, ip + 1);
                            var val2 = GetOperand(mode / 10, ip + 2);
                            var ix = intArr[ip + 3];
                            intArr[ix] = val1 * val2;
                            ip += 4;
                            break;
                        }
                    case 3: // input 
                        {
                            var ix = intArr[ip + 1];
                            intArr[ix] = input;
                            input = secondInput;
                            ip += 2;
                            break;
                        }
                    case 4: // output
                        {
                            output = GetOperand(mode, ip + 1);
                            ip += 2;
                            return output;
                            Console.WriteLine(output);
                            break;
                        }
                    case 5: // jump-if-true
                        {
                            var val1 = GetOperand(mode, ip + 1);
                            if (val1 != 0)
                                ip = GetOperand(mode / 10, ip + 2);
                            else
                                ip += 3;
                            break;
                        }
                    case 6: // jump-if-false
                        {
                            var val1 = GetOperand(mode, ip + 1);
                            if (val1 == 0)
                                ip = GetOperand(mode / 10, ip + 2);
                            else
                                ip += 3;
                            break;
                        }
                    case 7: // less than
                        {
                            var val1 = GetOperand(mode, ip + 1);
                            var val2 = GetOperand(mode / 10, ip + 2);
                            var ix = intArr[ip + 3];
                            intArr[ix] = val1 < val2 ? 1 : 0;
                            ip += 4;
                            break;
                        }
                    case 8: // equals
                        {
                            var val1 = GetOperand(mode, ip + 1);
                            var val2 = GetOperand(mode / 10, ip + 2);
                            var ix = intArr[ip + 3];
                            intArr[ix] = (val1 == val2) ? 1 : 0;
                            ip += 4;
                            break;
                        }
                    default:
                        throw new Exception("Invalid op code " + intArr[ip]);
                }
            } while (intArr[ip] != 99);

            return output;
        }


        public string CalcB()
        {
            var machineCode = new int[] { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 38, 63, 80, 105, 118, 199, 280, 361, 442, 99999, 3, 9, 102, 5, 9, 9, 1001, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 1001, 9, 4, 9, 102, 4, 9, 9, 101, 4, 9, 9, 102, 2, 9, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 5, 9, 102, 4, 9, 9, 1001, 9, 4, 9, 4, 9, 99, 3, 9, 101, 3, 9, 9, 1002, 9, 5, 9, 101, 3, 9, 9, 102, 5, 9, 9, 101, 3, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 1001, 9, 4, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99 };

            int maxValue = int.MinValue;
            var maxPhase = new int[5];

            var phaseSetting = new int[5];
            for (int a = 5; a < 10; a++)
            {
                phaseSetting[0] = a;
                for (int b = 5; b < 10; b++)
                {
                    if (b == a) continue;
                    phaseSetting[1] = b;

                    for (int c = 5; c < 10; c++)
                    {
                        if (c == a || c == b) continue;
                        phaseSetting[2] = c;

                        for (int d = 5; d < 10; d++)
                        {
                            if (d == a || d == b || d == c) continue;
                            phaseSetting[3] = d;

                            for (int e = 5; e < 10; e++)
                            {
                                if (e == a || e == b || e == c || e == d) continue;
                                phaseSetting[4] = e;
                                var output = DoOneCalcRound(machineCode, phaseSetting);
                                if (output > maxValue)
                                {
                                    maxValue = output;
                                    Array.Copy(phaseSetting, maxPhase, 5);
                                }
                            }
                        }
                    }
                }
            }


            return maxValue.ToString();
        }


        private int DoOneCalcRound(int[] machineCode, int[] phases)
        {
            var machines = new IntMachineClass[5];
            for (int i = 0; i < 5; i++)
                machines[i] = new IntMachineClass(machineCode, phases[i]);

            int? val = 0;
            int lastVal;
            do
            {
                lastVal = val.Value;
                val = machines[0].Run(machines[1].Run(machines[2].Run(machines[3].Run(machines[4].Run(val)))));
            } while (val.HasValue);

            return lastVal;
        }


        private class IntMachineClass
        {
            private readonly int[] _machineCode;
            private int _ip = 0;

            public IntMachineClass(int[] machineCode, int firstInput)
            {
                _machineCode = (int[])machineCode.Clone();
                RunUntil(firstInput, false, true);
            }

            public int? Run(int? inp)
            {
                return RunUntil(inp, true, false);
            }

            private int? RunUntil(int? inp, bool haltOnOutput, bool haltOnInput)
            {
                if (inp.HasValue == false)
                    return null;
                int input = inp.Value;

                int GetOperand(int mode, int lip)
                {
                    mode %= 10;
                    if (mode == 0)
                        return _machineCode[_machineCode[lip]];
                    if (mode == 1)
                        return _machineCode[lip];
                    throw new Exception("Unknown operational mode: " + mode);
                }

                do
                {
                    int mode = _machineCode[_ip] / 100;
                    int op = _machineCode[_ip] % 100;

                    switch (op)
                    {
                        case 1: // add
                            {
                                var val1 = GetOperand(mode, _ip + 1);
                                var val2 = GetOperand(mode / 10, _ip + 2);
                                var ix = _machineCode[_ip + 3];
                                _machineCode[ix] = val1 + val2;
                                _ip += 4;
                                break;
                            }
                        case 2: // mul
                            {
                                var val1 = GetOperand(mode, _ip + 1);
                                var val2 = GetOperand(mode / 10, _ip + 2);
                                var ix = _machineCode[_ip + 3];
                                _machineCode[ix] = val1 * val2;
                                _ip += 4;
                                break;
                            }
                        case 3: // input 
                            {
                                var ix = _machineCode[_ip + 1];
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
                                    _ip = GetOperand(mode / 10, _ip + 2);
                                else
                                    _ip += 3;
                                break;
                            }
                        case 6: // jump-if-false
                            {
                                var val1 = GetOperand(mode, _ip + 1);
                                if (val1 == 0)
                                    _ip = GetOperand(mode / 10, _ip + 2);
                                else
                                    _ip += 3;
                                break;
                            }
                        case 7: // less than
                            {
                                var val1 = GetOperand(mode, _ip + 1);
                                var val2 = GetOperand(mode / 10, _ip + 2);
                                var ix = _machineCode[_ip + 3];
                                _machineCode[ix] = val1 < val2 ? 1 : 0;
                                _ip += 4;
                                break;
                            }
                        case 8: // equals
                            {
                                var val1 = GetOperand(mode, _ip + 1);
                                var val2 = GetOperand(mode / 10, _ip + 2);
                                var ix = _machineCode[_ip + 3];
                                _machineCode[ix] = (val1 == val2) ? 1 : 0;
                                _ip += 4;
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
