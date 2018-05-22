using System.Threading.Tasks;

namespace Main
{
    public class Bar
    {
        private readonly Foo fooService;
        
        public Bar(Foo fooService)
        {
            this.fooService = fooService;
        }

        public async Task<string> SomeAsyncStuff()
        {
            await Task.Delay(1200);
            return "I am a string!";
        }

        public int DoSomethingWithFoo()
        {
            return fooService.DoSomeIo();
        }
    }
}