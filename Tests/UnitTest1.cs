using System;
using Main;
using Xunit;
using Pose;

namespace Tests
{
    public class UnitTest1
    {        
        Shim fooShim = Shim.Replace(() => Is.A<Foo>().DoSomeIo()).With(
            delegate (Foo @this) { return 99; });
        
        [Fact]
        public void Test1()
        {
            int? result = null;
            var classToTest = new Foo();

            PoseContext.Isolate(() =>
            {                
                result = classToTest.DoSomeIo();
            }, fooShim);
            
            // Assert
            Assert.Equal(99, result);
        }
    }
}