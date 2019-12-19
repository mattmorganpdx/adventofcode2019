using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class Computer
    {
        private readonly long[] _input;
        private long _pc;

        public Computer(long[] aInput)
        {
            _input = new long[500000000];
            aInput.CopyTo(_input, 0);
            var Halted = false;
            long Output = 0;
            long _pc = 0;
            long RelativeBase = 0;
            UserInput = new List<long>();
        }

        public List<long> UserInput { get; set; }

        public long Output { get; set; }

        public bool Halted { get; set; }

        public long RelativeBase { get; set; }

        public long RunComputer()
        {
            Tuple<long, List<long>> opCode;
            while ((opCode = ProcessOpCode(_input[_pc])).Item1 != 99)
            {
                try
                {
                    switch (opCode.Item1)
                    {
                        case 1:
                            _input[GetValueFromMode(opCode, 2)] =
                                _input[GetValueFromMode(opCode, 0)] + _input[GetValueFromMode(opCode, 1)];
                            _pc += 4;
                            break;
                        case 2:
                            _input[GetValueFromMode(opCode, 2)] =
                                _input[GetValueFromMode(opCode, 0)] * _input[GetValueFromMode(opCode, 1)];
                            _pc += 4;
                            break;
                        case 3:
                            _input[GetValueFromMode(opCode, 0)] = UserInput.First();
                            UserInput = UserInput.Skip(1).ToList();
                            _pc += 2;
                            break;
                        case 4:
                            Output = _input[GetValueFromMode(opCode, 0)];
                            if (Output == 99) Halted = true;
                            _pc += 2;
                            return Output;
                        case 5:
                            _pc = _input[GetValueFromMode(opCode, 0)] > 0
                                ? _input[GetValueFromMode(opCode, 1)]
                                : _pc + 3;
                            break;
                        case 6:
                            _pc = _input[GetValueFromMode(opCode, 0)] == 0
                                ? _input[GetValueFromMode(opCode, 1)]
                                : _pc + 3;
                            break;
                        case 7:
                            _input[GetValueFromMode(opCode, 2)] =
                                _input[GetValueFromMode(opCode, 0)] < _input[GetValueFromMode(opCode, 1)] ? 1 : 0;
                            _pc += 4;
                            break;
                        case 8:
                            _input[GetValueFromMode(opCode, 2)] =
                                _input[GetValueFromMode(opCode, 0)] == _input[GetValueFromMode(opCode, 1)] ? 1 : 0;
                            _pc += 4;
                            break;
                        case 9:
                            RelativeBase += _input[GetValueFromMode(opCode, 0)];
                            _pc += 2;
                            break;
                        default:
                            Console.WriteLine($"Found unknown OP Code {opCode.Item1}");
                            return -1;
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    var argEx = new ArgumentException("Index is out of range", "index", ex);
                    throw argEx;
                }
            }

            Halted = true;
            return Output;
        }

        private long GetValueFromMode(Tuple<long, List<long>> opCode, int offset)
        {
            return opCode.Item2[0 + offset] switch
            {
                0 => _input[_pc + 1 + offset],
                1 => (_pc + 1 + offset),
                2 => RelativeBase + _input[_pc + 1 + offset],
                _ => -1
            };
        }

        private static Tuple<long, List<long>> ProcessOpCode(long opCode)
        {
            var digits = opCode.ToString("D5");
            var op = Convert.ToInt64(digits.Substring(digits.Length - 2, 2));
            var modes = digits.Substring(0, digits.Length - 2)
                .Select(x => long.Parse(x.ToString()))
                .Reverse()
                .ToList();
            return Tuple.Create(op, modes);
        }
    }
}