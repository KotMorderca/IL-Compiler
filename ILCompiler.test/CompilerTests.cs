using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILCompiler.test
{
    [TestClass]
    public class CompilerTests
    {
        [TestMethod]
        public void TestMemoryMap()
        {
            MemoryMap map = new MemoryMap();
            Assert.AreEqual(map.cells.Length, 256, "Mapa pamięci nie składa się z 256 elementów");
            for (int i = 0; i < map.cells.Length - 1; i++)
            {
                for (int j = i + 1; j < map.cells.Length; j++)
                {
                    Assert.AreNotEqual(map.cells[j].address, map.cells[i].address, string.Format("Adres {0} powtarza się w mapie pamięci", map.cells[i].address));
                    Assert.AreNotEqual(map.cells[j].name, map.cells[i].name, string.Format("Mnemonik {0} powtarza się  w mapie pamięci", map.cells[i].name));
                }
            }
        }

        [TestMethod]
        public void TestDecodeOp()
        {
            MemoryMap map = new MemoryMap();
            MachineCode mc = new MachineCode();
            for(int i = 0; i < 256; i++)
            {
                if(map.cells[i].address > 0x7F)
                {
                    Assert.AreEqual(op_type.OP_MEMB, mc.decodeOpType(map.cells[i].name), string.Format("Błąd dekodowania operandu {0}", map.cells[i].name));
                }
                else
                {
                    Assert.AreEqual(op_type.OP_MEMW, mc.decodeOpType(map.cells[i].name), string.Format("Błąd dekodowania operandu {0}", map.cells[i].name));
                }
            }
            Assert.AreEqual(op_type.OP_OV, mc.decodeOpType("OV"), "Błąd dekodowania operandu OV");
        }

        [TestMethod]
        public void TestParseOp()
        {
            MachineCode mc = new MachineCode();
            ParseOpElement[] ops = new ParseOpElement[]
            {
                new ParseOpElement("2#0", 0, op_type.OP_CONB),
                new ParseOpElement("2#1", 1, op_type.OP_CONB),
                new ParseOpElement("false", 0, op_type.OP_CONB),
                new ParseOpElement("fAlSe", 0, op_type.OP_CONB),
                new ParseOpElement("true", 1, op_type.OP_CONB),
                new ParseOpElement("tRuE", 1, op_type.OP_CONB),
                new ParseOpElement("-5", -5, op_type.OP_CONW),
                new ParseOpElement("2#0110", 6, op_type.OP_CONW),
                new ParseOpElement("8#770", 504, op_type.OP_CONW),
                new ParseOpElement("16#AEAE", 44718, op_type.OP_CONW),
                new ParseOpElement("2", 2, op_type.OP_CONW),
                new ParseOpElement("0", 0, op_type.OP_CONW),
                new ParseOpElement("8#0", 0, op_type.OP_CONW),
                new ParseOpElement("16#0", 0, op_type.OP_CONW),
            };
            for(int i = 0; i < ops.Length; i++)
            {
                Assert.AreEqual(ops[i].typ, mc.decodeOpType(ops[i].op));
                Assert.AreEqual(ops[i].expected, mc.parseOperand(ops[i].typ, ops[i].op));
            }
            
            


        }

        [TestMethod]
        public void TestCompileLines()
        {
            MachineCode mc = new MachineCode();
            CodeList[] lines = new CodeList[]
            {
                new CodeList("RTRIG","MW0.0", 0x2400000080),
            };
            for (int i = 0; i < lines.Length; i++)
            {
                Assert.AreEqual(lines[i].excepted, mc.decodeLine(lines[i].linia), string.Format("Błąd kompilacji, oczekiwano: {0}, \notrzymano: {1}", lines[i].excepted.ToString("X"), mc.decodeLine(lines[i].linia).ToString("X")));
            }
            
        }
    }
}
