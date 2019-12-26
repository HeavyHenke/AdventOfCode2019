using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    class Day15
    {
        public object CalcA()
        {
            var visited = new Dictionary<(int x, int y), (int command, int steps)>();

            IList<int> GetCommands((int x, int y) pos)
            {
                var cmds = new List<int>();

                while (pos != (0, 0))
                {
                    var command = visited[pos].command;
                    cmds.Add(command);
                    pos = MoveInverse(pos, command);
                }
                cmds.Reverse();
                return cmds;
            }

            static (int x, int y) Move((int x, int y) pos, int direction)
            {
                if (direction == 1)
                    return (pos.x, pos.y - 1);
                if(direction == 2)
                    return (pos.x, pos.y + 1);
                if (direction == 3)
                    return (pos.x - 1, pos.y);
                if(direction == 4)
                    return (pos.x + 1, pos.y);
                throw new Exception("invalid command");
            }

            static (int x, int y) MoveInverse((int x, int y) pos, int direction)
            {
                if (direction == 1)
                    return (pos.x, pos.y + 1);
                if(direction == 2)
                    return (pos.x, pos.y - 1);
                if (direction == 3)
                    return (pos.x + 1, pos.y);
                if(direction == 4)
                    return (pos.x - 1, pos.y);
                throw new Exception("invalid command");            }

            static int GetResultFromCommandSequence(IList<int> seq)
            {
                int i = 0;
                Func<long> inputFunc = () => seq[i];
                
                var machine = new IntMachineClass(_machineCode, inputFunc);
                long? output = null;
                for (i = 0; i < seq.Count; i++)
                {
                    output = machine.RunUntil(true);
                }

                return (int) output;
            }
            
            
            var searchQueue = new Queue<(int x, int y)>();
            searchQueue.Enqueue((0, 0));
            
            while (searchQueue.Count > 0)
            {
                var pos = searchQueue.Dequeue();

                for (int direction = 1; direction < 5; direction++)
                {
                    var pos2 = Move(pos, direction);
                    if(pos2 == (0,0))
                        continue;
                    
                    if (visited.ContainsKey(pos2) == false)
                    {
                        var steps = (pos == (0,0))? 1 : visited[pos].steps + 1;
                        visited[pos2] = (direction, steps);

                        var result = GetResultFromCommandSequence(GetCommands(pos2));
                        if (result == 2)
                            return visited[pos].steps + 1;
                        if (result == 1)
                            searchQueue.Enqueue(pos2);
                    }
                }
            }
            
            
            return null;
        }
        
        public object CalcB()
        {
            var visited = new Dictionary<(int x, int y), (int command, int steps)>();
            var walls = new HashSet<(int x, int y)>();

            IList<int> GetCommands((int x, int y) pos)
            {
                var cmds = new List<int>();

                while (pos != (0, 0))
                {
                    var command = visited[pos].command;
                    cmds.Add(command);
                    pos = MoveInverse(pos, command);
                }
                cmds.Reverse();
                return cmds;
            }

            static (int x, int y) Move((int x, int y) pos, int direction)
            {
                if (direction == 1)
                    return (pos.x, pos.y - 1);
                if(direction == 2)
                    return (pos.x, pos.y + 1);
                if (direction == 3)
                    return (pos.x - 1, pos.y);
                if(direction == 4)
                    return (pos.x + 1, pos.y);
                throw new Exception("invalid command");
            }

            static (int x, int y) MoveInverse((int x, int y) pos, int direction)
            {
                if (direction == 1)
                    return (pos.x, pos.y + 1);
                if(direction == 2)
                    return (pos.x, pos.y - 1);
                if (direction == 3)
                    return (pos.x + 1, pos.y);
                if(direction == 4)
                    return (pos.x - 1, pos.y);
                throw new Exception("invalid command");            }

            static int GetResultFromCommandSequence(IList<int> seq)
            {
                int i = 0;
                Func<long> inputFunc = () => seq[i];
                
                var machine = new IntMachineClass(_machineCode, inputFunc);
                long? output = null;
                for (i = 0; i < seq.Count; i++)
                {
                    output = machine.RunUntil(true);
                }

                return (int) output;
            }
            
            
            var searchQueue = new Queue<(int x, int y)>();
            searchQueue.Enqueue((0, 0));
            (int x, int y) oxygenPos = (0, 0);
            
            while (searchQueue.Count > 0)
            {
                var pos = searchQueue.Dequeue();

                for (int direction = 1; direction < 5; direction++)
                {
                    var pos2 = Move(pos, direction);
                    if(pos2 == (0,0))
                        continue;
                    
                    if (visited.ContainsKey(pos2) == false)
                    {
                        var steps = (pos == (0,0))? 1 : visited[pos].steps + 1;
                        visited[pos2] = (direction, steps);

                        var result = GetResultFromCommandSequence(GetCommands(pos2));
                        if (result == 2)
                            oxygenPos = pos2;
                        
                        if (result != 0)
                            searchQueue.Enqueue(pos2);
                        else
                            walls.Add(pos2);
                    }
                }
            }
            
            // Search from oxygen and back
            var oxyVisited = new Dictionary<(int x, int y), int>();
            oxyVisited.Add(oxygenPos, 0);
            searchQueue.Enqueue(oxygenPos);
            int max = 0;
            
            while (searchQueue.Count > 0)
            {
                var pos = searchQueue.Dequeue();
                for (int direction = 1; direction < 5; direction++)
                {
                    var pos2 = Move(pos, direction);
                    if(oxyVisited.ContainsKey(pos2) || walls.Contains(pos2))
                        continue;

                    var steps = oxyVisited[pos] + 1;
                    if (steps > max)
                        max = steps;
                    oxyVisited.Add(pos2, steps);
                    searchQueue.Enqueue(pos2);
                }
            }
            
            return max;
        }

        
        private class IntMachineClass
        {
            private long[] _machineCode;
            private int _ip = 0;
            private int _relativeBase = 0;

            public bool IsEnded;
            private Func<long> _input;

            public IntMachineClass(long[] machineCode, Func<long> input)
            {
                _input = input;
                _machineCode = (long[]) machineCode.Clone();
            }

            private void EnsureMemory(long address)
            {
                if (_machineCode.Length <= address)
                {
                    Array.Resize(ref _machineCode, (int) address + 100);
                }
            }

            public long? RunUntil(bool haltOnOutput)
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
                            _machineCode[ix] = _input();
                            _ip += 2;
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
                            IsEnded = true;
                            return null;
                        default:
                            throw new Exception("Invalid op code " + _machineCode[_ip]);
                    }
                } while (_machineCode[_ip] != 99);

                return null;
            }
        }
        
        private static readonly long[] _machineCode = {3,1033,1008,1033,1,1032,1005,1032,31,1008,1033,2,1032,1005,1032,58,1008,1033,3,1032,1005,1032,81,1008,1033,4,1032,1005,1032,104,99,1002,1034,1,1039,102,1,1036,1041,1001,1035,-1,1040,1008,1038,0,1043,102,-1,1043,1032,1,1037,1032,1042,1106,0,124,102,1,1034,1039,101,0,1036,1041,1001,1035,1,1040,1008,1038,0,1043,1,1037,1038,1042,1106,0,124,1001,1034,-1,1039,1008,1036,0,1041,1001,1035,0,1040,1002,1038,1,1043,101,0,1037,1042,1106,0,124,1001,1034,1,1039,1008,1036,0,1041,102,1,1035,1040,101,0,1038,1043,1001,1037,0,1042,1006,1039,217,1006,1040,217,1008,1039,40,1032,1005,1032,217,1008,1040,40,1032,1005,1032,217,1008,1039,7,1032,1006,1032,165,1008,1040,37,1032,1006,1032,165,1102,1,2,1044,1106,0,224,2,1041,1043,1032,1006,1032,179,1102,1,1,1044,1106,0,224,1,1041,1043,1032,1006,1032,217,1,1042,1043,1032,1001,1032,-1,1032,1002,1032,39,1032,1,1032,1039,1032,101,-1,1032,1032,101,252,1032,211,1007,0,39,1044,1105,1,224,1101,0,0,1044,1105,1,224,1006,1044,247,102,1,1039,1034,102,1,1040,1035,101,0,1041,1036,102,1,1043,1038,102,1,1042,1037,4,1044,1106,0,0,35,37,2,26,91,30,85,34,87,18,47,29,50,23,7,46,94,2,26,42,36,23,3,32,65,21,63,18,54,31,52,75,4,35,24,24,74,33,81,89,75,50,36,43,7,20,45,9,23,10,70,12,81,62,12,51,3,5,96,7,93,90,12,41,5,52,30,91,12,62,34,44,92,68,9,81,9,6,30,38,63,27,51,3,44,47,27,86,41,1,73,78,15,34,98,9,63,66,21,89,96,5,9,36,21,97,6,26,75,14,86,16,82,21,23,91,25,15,66,33,2,50,26,18,61,73,17,49,15,99,19,68,96,33,23,12,81,11,51,19,30,56,74,27,40,76,15,49,11,24,50,27,50,36,77,36,16,22,80,86,11,85,20,87,24,26,6,64,35,27,65,32,86,42,99,30,78,68,24,67,82,4,76,63,36,4,46,21,72,68,17,21,69,71,36,82,22,57,1,29,95,59,18,48,40,91,7,44,22,64,5,52,20,20,86,34,9,67,74,22,13,31,97,23,19,78,19,12,80,19,82,83,11,26,5,10,74,2,42,5,94,26,79,51,33,15,47,9,12,84,20,37,85,63,27,92,16,10,82,64,15,50,75,12,68,51,37,87,10,51,18,11,13,99,97,30,33,48,2,45,29,22,45,20,49,14,78,33,41,89,4,67,21,40,42,20,4,34,64,98,32,77,28,79,9,51,91,58,19,45,56,4,10,3,52,47,65,11,21,53,25,57,78,33,16,70,88,34,56,37,86,30,4,84,91,86,90,37,37,25,59,2,96,25,19,69,6,11,67,83,38,8,49,18,17,21,56,20,43,89,8,78,30,80,52,29,9,65,1,1,65,27,84,23,8,33,99,71,28,38,45,14,40,31,45,44,12,94,12,65,23,96,5,93,50,35,84,10,34,81,2,51,15,11,92,69,20,65,27,68,86,76,36,49,38,79,92,38,72,8,32,80,29,41,7,15,78,38,5,10,61,24,44,38,19,80,9,60,95,95,33,48,13,51,32,57,84,97,1,51,36,6,51,96,16,62,32,13,93,4,79,40,2,68,74,38,4,30,82,17,67,51,68,29,3,85,13,5,2,30,71,36,77,35,78,23,87,22,7,78,5,60,2,11,42,15,68,89,66,93,31,38,31,81,8,65,22,7,27,83,59,21,12,73,64,72,40,38,59,20,29,92,20,7,65,16,86,81,12,44,77,97,30,19,49,61,24,29,24,31,87,89,31,42,80,17,91,23,18,91,10,53,5,17,53,30,96,96,34,83,34,18,68,79,97,18,4,56,37,33,62,31,79,99,32,14,99,87,83,53,34,26,17,70,59,31,12,42,91,32,93,5,54,8,10,83,20,58,92,30,71,24,34,60,3,9,64,72,12,70,14,22,69,38,27,77,31,84,8,54,44,58,9,30,95,22,12,61,95,21,81,71,5,64,44,7,71,4,17,41,2,89,16,20,93,88,20,31,45,28,49,91,15,72,43,6,21,82,15,25,99,8,11,34,18,93,50,15,15,98,27,34,44,38,15,29,79,42,14,86,68,56,7,3,97,21,58,11,33,67,6,53,23,71,16,58,74,17,92,17,14,98,23,35,60,32,70,54,1,82,2,41,32,68,91,27,80,6,25,55,93,23,52,91,3,95,44,3,42,70,23,16,54,36,36,59,5,63,27,40,11,73,34,48,29,73,36,74,77,58,25,55,25,45,7,58,53,49,8,95,13,84,23,58,37,42,6,70,36,58,73,55,14,51,5,99,95,61,20,65,0,0,21,21,1,10,1,0,0,0,0,0,0};
    }
}