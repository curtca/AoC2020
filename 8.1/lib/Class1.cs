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

    public long ExecUntil1stDupedLine()
    {
        long acc = 0;
        int ip = 0;
        bool[] executed = new bool[program.Length];
        while (!executed[ip])
        {
            executed[ip] = true;
            switch (program[ip].Item1)
            {
                case OpCode.acc: 
                    acc += program[ip].Item2; 
                    break;
                case OpCode.jmp: 
                    ip += program[ip].Item2 - 1; 
                    break;
            }
            ip++;
        }
        return acc;
    }
}
