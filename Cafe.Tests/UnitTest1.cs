using System;
using Xunit;
using Cafe.Data;
namespace Cafe.Tests
{
    public class DemoTest
    {
        [Theory]
        [InlineData(10,0,-1)]
        [InlineData(10,2,5)]
        public static void DivPositiveTest(double a,double b,double res)
        {
            Assert.Equal(res, Divide.DivPositive(a,b));
            Assert.Equal(res, Divide.DivPositive(a,b));
        }


    }
}
