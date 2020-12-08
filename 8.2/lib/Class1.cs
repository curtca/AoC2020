using System;
using System.Collections.Generic;
using System.Linq;

public class Prog
{
    enum OpCode {uninit, acc, jmp, nop}
    Tuple<OpCode, int>[] program;

    public Prog(string input)
    {
        var lines = input.Split("\r\n");
        program = new Tuple<OpCode, int>[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(' ');
            OpCode oc = OpCode.uninit;
            switch (parts[0])
            {
                case "acc": oc = OpCode.acc; break;
                case "jmp": oc = OpCode.jmp; break;
                case "nop": oc = OpCode.nop; break;
            }
            program[i] = new Tuple<OpCode, int>(oc, int.Parse(parts[1]));
        }
    }

    public long RunWithOneChange()
    {
        long acc = 0;
        for (int i = 0; i < program.Length; i++)
        {
            Tuple<OpCode, int>[] p2 = (Tuple<OpCode, int>[]) program.Clone();
            OpCode oc = p2[i].Item1;
            int arg = p2[i].Item2;
            switch (oc)
            {
                case OpCode.nop:
                    p2[i] = new Tuple<OpCode, int>(OpCode.jmp, arg);
                    break;
                case OpCode.jmp:
                    p2[i] = new Tuple<OpCode, int>(OpCode.nop, arg);
                    break;
                default: continue;
            }

            if (Run(p2, out acc))
                return acc;
        }
        return -1;
    }

    private bool Run(Tuple<OpCode, int>[] prog, out long acc)
    {
        acc = 0;
        int ip = 0;
        bool[] executed = new bool[prog.Length];
        while (ip < prog.Length && !executed[ip])
        {
            executed[ip] = true;
            switch (prog[ip].Item1)
            {
                case OpCode.acc: 
                    acc += prog[ip].Item2; 
                    break;
                case OpCode.jmp: 
                    ip += prog[ip].Item2 - 1; 
                    break;
            }
            ip++;
        }
        return ip == prog.Length;
    }
}
