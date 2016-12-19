using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ILCompiler
{
    public delegate void textOutput(string text);
    public enum radix { RADIX2, RADIX8, RADIX10, RADIX16 }
    public enum op_type { OP_CONB = 0, OP_CONW = 1, OP_MEMB = 2, OP_MEMW = 3, OP_OV = 4, OP_REAL = 5, OP_NONE = 6, OP_UNKNOWN = 7}
    class Compiler
    {
        StreamReader fileCode;
        StreamWriter fileOut;
        public event textOutput txtOutput;
        string codePath = null;
        bool isGood = true;
        public Compiler(string pathToCode)
        {
            codePath = pathToCode;
            fileCode = new StreamReader(codePath);
            fileOut = new StreamWriter(codePath.Substring(0, codePath.Length - 2) + "txt", false);
        }

        public void beginCompilation()
        {
            string firstWord = null, wholeCode = null;
            string[] wholeCodeArray = null;
            List<List<string>> program = new List<List<string>>();
            List<UInt64> programHex = new List<UInt64>();
            int index;
            StringBuilder sb = new StringBuilder();
            txtOutput("Rozpoczęto proces kompilacji");

            txtOutput("Rozpoczęto pracę preprocesora");
            fileCode.BaseStream.Seek(0, SeekOrigin.Begin);
            wholeCodeArray = File.ReadAllLines(codePath);
            for (int i = 0; i < wholeCodeArray.Length; i++)
            {
                wholeCodeArray[i] = wholeCodeArray[i].Replace("_", "");
                index = wholeCodeArray[i].IndexOf("//");
                if (index != -1)
                {
                    wholeCodeArray[i] = wholeCodeArray[i].Substring(0, index).TrimEnd(new char[] {' ','\t'});
                }

                if (!string.IsNullOrWhiteSpace(wholeCodeArray[i]))
                {
                    firstWord = (string)wholeCodeArray[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).GetValue(0);
                    if (wholeCodeArray[i].Split().Length == 1 && firstWord.EndsWith(":"))
                    {
                        sb.Append(wholeCodeArray[i] + " ");
                    }
                    else
                    {
                        sb.AppendLine(wholeCodeArray[i]);
                    }
                }
            }
            wholeCode = sb.ToString();
            removeMultiLineComments(ref wholeCode, 0, "/*", "*/");
            removeMultiLineComments(ref wholeCode, 0, "(*", "*)");
            wholeCodeArray = wholeCode.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string st in wholeCodeArray)
            {
                program.Add(new List<string>(st.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)));
            }
            txtOutput("Zakończono pracę preprocesora");
            wholeCode = null;
            wholeCodeArray = null;
            txtOutput("Rozpoczęcie pracy kompilatora");

            //Przetworzenie etykiet
            for(int i = 0; i < program.Count; i++)
            {
                if (program[i][0].EndsWith(":"))
                {
                    for (int j = 0; j < program.Count; j++)
                    {
                        for (int k = 0; k < program[j].Count; k++)
                        {
                            if(program[j][k] == program[i][0].TrimEnd(new char[] { ':' }))
                            {
                                program[j][k] = i.ToString();
                            }
                        }
                    }
                    program[i].RemoveAt(0);
                }
            }
            //Przetworzenie operacji nawiasowych
            processParenthesis(ref program, 0);
            //Kompilacja do postaci kodu maszynowego
            processCode(ref program, ref programHex);
            for (int i = 0; i < programHex.Count; i++)
            {
                if ((programHex[i] >> 32) == 254)
                    isGood = false;
            }
            if (isGood)
            {
#if false
                /* Plik wynikowy w formacie tablicy C */
                for (int i = 0; i < programHex.Count; i++)
                {
                    fileOut.WriteLine("0x" + programHex[i].ToString("X10") + ",");
                }
#else
                /* Plik wynikowy w formacie wsadu binarnego */
                fileOut.BaseStream.Write(BitConverter.GetBytes((ulong)programHex.Count), 0, 8);
                for (int i = 0; i < programHex.Count; i++)
                {
                    fileOut.BaseStream.Write(BitConverter.GetBytes(programHex[i]), 0, 8);
                }
#endif
                txtOutput("Zakończono kompilację");
            }
            else
            {
                txtOutput("Kompilacja nie powiodła się!");
            }
          
            fileCode.Close();
            fileOut.Close();
        }

        void processParenthesis(ref List<List<string>> lista, int position)
        {
            string mnemonic = null;
            for(int i = position; i < lista.Count - 1; i++)
            {
                if (lista[i][0].EndsWith("("))
                {
                    mnemonic = lista[i][0].TrimEnd(new char[] { ':' });
                    for (int j = i + 1; j < lista.Count; j++)
                    {
                        if(lista[j][0] == ")")
                        {
                            lista[j][0] += mnemonic.Substring(0,mnemonic.Length-1);
                            lista[i][0] = "(";
                            break;
                        }
                        else if (lista[j][0].EndsWith("("))
                        {
                            processParenthesis(ref lista, i + 1);
                        }
                    }
                }
            }
        }

        void removeMultiLineComments(ref string code, int position, string ss, string es)
        {
            int startIndex;
            while ((startIndex = code.IndexOf(ss, position)) != -1)
            {
                if ((code.IndexOf(ss, startIndex + 1) != -1) && (code.IndexOf(ss, startIndex + 1) < (code.IndexOf(es, startIndex))))
                {
                    removeMultiLineComments(ref code, code.IndexOf(ss, startIndex + 1), ss, es);
                }
                else
                {
                    code = code.Remove(code.IndexOf(ss, startIndex), code.IndexOf(es, startIndex) - code.IndexOf(ss, startIndex) + 2);
                }
            }
        }

        void processCode(ref List<List<string>> input, ref List<UInt64> output)
        {
            MachineCode mc = new MachineCode();
            for(int i = 0; i < input.Count; i++)
            {
                output.Add(mc.decodeLine(input[i]));
            }
            output.Add((ulong)0xFF << 32);
        }
    }
}
