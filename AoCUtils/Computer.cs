using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class Computer
    {
        private int[] _input;
        private int _pc;
        public Computer(int[] aInput)
        {
            _input = aInput;
            var Halted = false;
            var Output = 0;
            var _pc = 0;
        }
        
        public List<int> UserInput { get; set; }
        
        public int Output { get; set; }
        
        public bool Halted { get; set; }

        public int RunComputer()
        {
            Tuple<int, List<int>> opCode;
            while ((opCode = ProcessOpCode(_input[_pc])).Item1 != 99)
            {
                try
                {
                    var first = 0;
                    var second = 0;
                    var third = 0;
                    switch (opCode.Item1)
                    {
                        case 1:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : this._input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : _input[_pc + 2];
                            third = _input[_pc + 3];
                            _input[third] = _input[first] + _input[second];
                            _pc += 4;
                            break;
                        case 2:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : _input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : _input[_pc + 2];
                            third = _input[_pc + 3];
                            _input[third] = _input[first] * _input[second];
                            _pc += 4;
                            break;
                        case 3:
                            _input[_input[_pc + 1]] = UserInput.First();
                            UserInput = UserInput.Skip(1).ToList();
                            _pc += 2;
                            break;
                        case 4:
                            Output = _input[_input[_pc + 1]];
                            _pc += 2;
                            return Output;
                        case 5:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : _input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : _input[_pc + 2];
                            if (_input[first] > 0)
                            {
                                _pc = _input[second];
                            }
                            else
                            {
                                _pc += 3;
                            }

                            break;
                        case 6:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : _input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : _input[_pc + 2];
                            if (_input[first] == 0)
                            {
                                _pc = _input[second];
                            }
                            else
                            {
                                _pc += 3;
                            }

                            break;
                        case 7:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : _input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : _input[_pc + 2];
                            third = _input[_pc + 3];
                            _input[third] = _input[first] < _input[second] ? 1 : 0;
                            _pc += 4;
                            break;
                        case 8:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : _input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : _input[_pc + 2];
                            third = _input[_pc + 3];
                            _input[third] = _input[first] == _input[second] ? 1 : 0;
                            _pc += 4;
                            break;
                        default:
                            Console.WriteLine($"Found unknown OP Code {opCode.Item1}");
                            return -1;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    return -1;
                }
            }

            Halted = true;
            return Output;
        }
        
        public Tuple<int, int[], bool> Part01(int[] input, int initialInput, int secondInput, bool continuousMode)
        {
            // Why does this return a Tuple? ... see day 7
            var firstInput = true;
            var output = 0;
            var _pc = 0;
            Tuple<int, List<int>> opCode;
            while ((opCode = ProcessOpCode(input[_pc])).Item1 != 99)
            {
                try
                {
                    var first = 0;
                    var second = 0;
                    var third = 0;
                    switch (opCode.Item1)
                    {
                        case 1:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : input[_pc + 2];
                            third = input[_pc + 3];
                            input[third] = input[first] + input[second];
                            _pc += 4;
                            break;
                        case 2:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : input[_pc + 2];
                            third = input[_pc + 3];
                            input[third] = input[first] * input[second];
                            _pc += 4;
                            break;
                        case 3:
                            input[input[_pc + 1]] = firstInput ? initialInput : secondInput;
                            // Note all the test cases from Day07 passed this with secondInput 0 so WTF!?!
                            firstInput = false;
                            _pc += 2;
                            break;
                        case 4:
                            output = input[input[_pc + 1]];
                            if (continuousMode) return Tuple.Create(output, input, true);
                            _pc += 2;
                            break;
                        case 5:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : input[_pc + 2];
                            if (input[first] > 0)
                            {
                                _pc = input[second];
                            }
                            else
                            {
                                _pc += 3;
                            }

                            break;
                        case 6:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : input[_pc + 2];
                            if (input[first] == 0)
                            {
                                _pc = input[second];
                            }
                            else
                            {
                                _pc += 3;
                            }

                            break;
                        case 7:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : input[_pc + 2];
                            third = input[_pc + 3];
                            input[third] = input[first] < input[second] ? 1 : 0;
                            _pc += 4;
                            break;
                        case 8:
                            first = Convert.ToBoolean(opCode.Item2[0]) ? _pc + 1 : input[_pc + 1];
                            second = Convert.ToBoolean(opCode.Item2[1]) ? _pc + 2 : input[_pc + 2];
                            third = input[_pc + 3];
                            input[third] = input[first] == input[second] ? 1 : 0;
                            _pc += 4;
                            break;
                        default:
                            Console.WriteLine($"Found unknown OP Code {opCode.Item1}");
                            return Tuple.Create(-1, input, false);
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    return Tuple.Create(-1, input, false);
                }
            }

            return Tuple.Create(output, input, false);
        }

        private static Tuple<int, List<int>> ProcessOpCode(int opCode)
        {
            var digits = opCode.ToString("D5");
            var op = Convert.ToInt32(digits.Substring(digits.Length - 2, 2));
            var modes = digits.Substring(0, digits.Length - 2)
                .Select(x => int.Parse(x.ToString()))
                .Reverse()
                .ToList();
            return Tuple.Create(op, modes);
        }
    }
}