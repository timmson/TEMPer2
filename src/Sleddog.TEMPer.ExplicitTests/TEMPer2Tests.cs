using System;
using System.Reactive.Linq;
using System.Threading;
using Xunit;

namespace Sleddog.TEMPer.Tests
{
    public class TEMPer2Tests
    {
        [Fact]
        public void Read()
        {
            {
                TEMPer2.Main(new string[0]);
            }
        }
    }
}