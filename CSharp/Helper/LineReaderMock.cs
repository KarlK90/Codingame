namespace Codingame.Helper
{
    public class LineReaderMock : LineReader
    {
        public LineReaderMock(string[] input)
        {
            this.input = input;
        }

        private int calls = 0;
        private string[] input;

        public override string ReadLine()
        {
            string ret = input[calls];
            calls++;
            return ret;
        }
    }
}