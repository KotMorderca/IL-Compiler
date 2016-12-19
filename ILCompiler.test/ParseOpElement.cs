namespace ILCompiler.test
{
    class ParseOpElement
    {
        public string op;
        public int expected;
        public op_type typ;
        public ParseOpElement(string o, int e, op_type t)
        {
            op = o;
            expected = e;
            typ = t;
        }
    }
}
