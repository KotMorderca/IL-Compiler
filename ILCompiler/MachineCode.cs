using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ILCompiler
{
    public class MachineCode
    {
        Instruction[] instructions;
        MemoryMap map;
        public MachineCode()
        {
            map = new MemoryMap();
            instructions = new Instruction[]
            {
                //              MNEMONIK, CONB, CONW, MEMB, MEMW, OV,   REAL, NONE
                new Instruction("LD",     0x00, 0x27, 0x01, 0x28, 0x02, 0254, 0254),
                new Instruction("LDN",    0x03, 0x29, 0x04, 0x30, 0x05, 0254, 0254),
                new Instruction("NOT",    0254, 0254, 0254, 0254, 0254, 0254, 0x06),
                new Instruction("NOTW",   0254, 0254, 0254, 0254, 0254, 0254, 0x2B),
                new Instruction("AND",    0x07, 0x2C, 0x08, 0x2D, 0254, 0254, 0254),
                new Instruction("ANDN",   0x09, 0x2E, 0x0A, 0x2F, 0254, 0254, 0254),
                new Instruction("OR",     0x0B, 0x30, 0x0C, 0x31, 0254, 0254, 0254),
                new Instruction("ORN",    0x0D, 0x32, 0x0E, 0x33, 0254, 0254, 0254),
                new Instruction("XOR",    0x0F, 0x34, 0x10, 0x35, 0254, 0254, 0254),
                new Instruction("XORN",   0x11, 0x36, 0x12, 0x37, 0254, 0254, 0254),
                new Instruction("(",      0x13, 0254, 0x14, 0254, 0254, 0254, 0x15),
                new Instruction(")AND",   0254, 0254, 0254, 0254, 0254, 0254, 0x16),
                new Instruction(")ANDN",  0254, 0254, 0254, 0254, 0254, 0254, 0x17),
                new Instruction(")OR",    0254, 0254, 0254, 0254, 0254, 0254, 0x18),
                new Instruction(")ORN",   0254, 0254, 0254, 0254, 0254, 0254, 0x19), 
                new Instruction(")XOR",   0254, 0254, 0254, 0254, 0254, 0254, 0x1A),
                new Instruction(")XORN",  0254, 0254, 0254, 0254, 0254, 0254, 0x1B),
                new Instruction("ST",     0254, 0254, 0x1C, 0x41, 0254, 0254, 0254),
                new Instruction("STN",    0254, 0254, 0x1D, 0x42, 0254, 0254, 0254),
                new Instruction("S",      0254, 0254, 0x1E, 0254, 0254, 0254, 0254),
                new Instruction("R",      0254, 0254, 0x1F, 0254, 0254, 0254, 0254),
                new Instruction("JMP",    0254, 0x20, 0254, 0254, 0254, 0254, 0254),
                new Instruction("JMPC",   0254, 0x21, 0254, 0254, 0254, 0254, 0254),
                new Instruction("JMPCN",  0254, 0x22, 0254, 0254, 0254, 0254, 0254),
                new Instruction("JOV",    0254, 0x23, 0254, 0254, 0254, 0254, 0254),
                new Instruction("RTRIG",  0254, 0254, 0x24, 0254, 0254, 0254, 0254),
                new Instruction("FTRIG",  0254, 0254, 0x25, 0254, 0254, 0254, 0254),
                new Instruction("NOP",    0254, 0254, 0254, 0254, 0254, 0254, 0x26),
                new Instruction("EQ",     0254, 0x43, 0254, 0x44, 0254, 0254, 0254),
                new Instruction("NE",     0254, 0x43, 0254, 0x44, 0254, 0254, 0254),
            };
        }
        
        public UInt64 decodeLine(List<string> input)
        {
            op_type type;
            for(int i = 0; i < instructions.Length; i++)
            {
                if(string.Equals(input[0],instructions[i].mnemonic))
                {
                    if (input.Count == 1)
                    {
                        return (UInt64)instructions[i].codes[(int)op_type.OP_NONE] << 32;
                    }
                    else
                    {
                        type = decodeOpType(input[1]);
                        if(type != op_type.OP_UNKNOWN)
                        {
                            return ((UInt64)instructions[i].codes[(int)type] << 32) | (UInt32)parseOperand(type, input[1]);
                        }
                        else
                        {
                            return (UInt64)0254 << 32; //STOP
                        }
                    }
                }
            }
            return 0254;
        }

        public int parseOperand(op_type type, string op)
        {
            switch (type)
            {
                case op_type.OP_MEMB:
                case op_type.OP_MEMW:
                    return map.getAddress(op);
                case op_type.OP_CONB:
                    return parseBin(op);
                case op_type.OP_CONW:
                    return parseNumber(op);
                case op_type.OP_NONE:
                case op_type.OP_OV:
                default:
                    return 0;
            }
        }

        public int parseNumber(string op)
        {
            Int32 retVal = 0;
            if (op.StartsWith("2#"))
            {
                retVal = Convert.ToInt32(op.Substring(2,op.Length-2), 2);
            }
            else if (op.StartsWith("8#"))
            {
                retVal = Convert.ToInt32(op.Substring(2, op.Length - 2), 8);
            }
            else if (op.StartsWith("10#"))
            {
                retVal = Convert.ToInt32(op.Substring(3, op.Length - 3), 10);
            }
            else if (op.StartsWith("16#"))
            {
                retVal = Convert.ToInt32(op.Substring(3, op.Length - 3), 16);
            }
            else
            {
                retVal = Convert.ToInt32(op);
            }
            return retVal;
        }

        public Int32 parseBin(string op)
        {
            if(string.Equals("FALSE", op, StringComparison.OrdinalIgnoreCase) | string.Equals("2#0", op))
            {
                return 0;
            }
            else if (string.Equals("true", op, StringComparison.OrdinalIgnoreCase) | string.Equals("2#1", op))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public op_type decodeOpType(string op)
        {
            string memB = "DQW(0\\.[0-9]|[0-2][0-9]|3[0-1])|DIW(0\\.[0-9]|[0-2][0-9]|3[0-1])|MW([0-1]\\.([0-9]|[0-2][0-9]|3[0-1]))";
            string memW = "AQ[0-7]|AI([0-9]|[1][0-5])|DQW([0-3])|DIW([0-3])|T([0-9]|1[0-5])|C([0-9]|1[0-5])|MW([0-9]|[0-5][0-9]|6[0-3])";
            string OV = "OV";
            string conB = "FALSE|false|False|TRUE|true|True|2\\#0\\z|2\\#1\\z";
            string conW = "2\\#[01][0-1]+|8\\#[0-7]+|((?<![0-9])[+\\-])?(?<!\\#)[0-9]([0-9]|[0-9])*(?!\\#)|16\\#[0-9a-fA-F]+";
            Regex rgx = new Regex(memB, RegexOptions.IgnoreCase);
            if (rgx.Matches(op).Count > 0)
            {
                return op_type.OP_MEMB;
            }

            rgx = new Regex(OV, RegexOptions.IgnoreCase);
            if (rgx.Matches(op).Count > 0)
            {
                return op_type.OP_OV;
            }

            rgx = new Regex(memW, RegexOptions.IgnoreCase);
            if (rgx.Matches(op).Count > 0)
            {
                return op_type.OP_MEMW;
            }

            rgx = new Regex(conB, RegexOptions.IgnoreCase);
            if (rgx.Matches(op).Count > 0)
            {
                return op_type.OP_CONB;
            }

            rgx = new Regex(conW, RegexOptions.IgnoreCase);
            if (rgx.Matches(op).Count > 0)
            {
                return op_type.OP_CONW;
            }
            return op_type.OP_UNKNOWN;
        }
    }
}
