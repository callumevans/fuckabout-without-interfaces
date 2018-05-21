using System.Threading;

namespace Main
{
    public class Foo
    {
        public int DoSomeIo()
        {
            Thread.Sleep(1000);
            return 123;
        }
    }
}