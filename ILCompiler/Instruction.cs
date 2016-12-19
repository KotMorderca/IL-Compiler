namespace ILCompiler
{
    class Instruction
    {
        public string mnemonic = null;
        public byte[] codes = new byte[7];
        public Instruction(string m, byte argb, byte argw, byte datab, byte dataw, byte ov, byte real, byte none)
        {
            mnemonic = m;
            codes[0] = argb;
            codes[1] = argw;
            codes[2] = datab;
            codes[3] = dataw;
            codes[4] = ov;
            codes[5] = real;
            codes[6] = none;
        }
    }
}
