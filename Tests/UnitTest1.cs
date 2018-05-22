using System.Threading.Tasks;
using Main;
using Xunit;
using Pose;

namespace Tests
{
    public class UnitTest1
    {        
        Shim fooShim = Shim.Replace(() => Is.A<Foo>().DoSomeIo()).With(
            (Foo @this) => 99);

        private Shim barShim = Shim.Replace(() => Is.A<Bar>().SomeAsyncStuff()).With(
            (Bar @this) => Task.FromResult("lol I'm fake"));
        
        private Foo foo;
        private Bar bar;

        public UnitTest1()
        {
            foo = new Foo();
            bar = new Bar(foo);
        }
        
        [Fact]
        public void Test1()
        {
            // Arrange
            int? result = null;

            // Act
            PoseContext.Isolate(() =>
            {                
                result = foo.DoSomeIo();
            }, fooShim);
            
            // Assert
            Assert.Equal(99, result);
        }

        [Fact]
        public void OtherTest()
        {
            // Arrange
            string result = null;
            
            // Act
            PoseContext.Isolate(() =>
            {
                result = bar.SomeAsyncStuff().Result;
            }, barShim);
            
            // Assert
            Assert.Equal("lol I'm fake", result);
        }
        
        [Fact]
        public void TestBarFooMethod()
        {
            // Uses concerete 'Bar' class but it calls the previously shimmed 'Foo' class.
            
            // Arrange
            int? result = null;
            
            // Act
            PoseContext.Isolate(() =>
            {
                result = bar.DoSomethingWithFoo();
            }, fooShim);
            
            // Assert
            Assert.Equal(99, result);
        }        
    }
}