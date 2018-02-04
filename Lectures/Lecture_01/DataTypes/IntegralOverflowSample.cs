using System;
using Xunit;

namespace DataTypes
{
  public class IntegralOverflowSample
  {
    [Fact]
    public void OverflowTest()
    {
      int a = int.MinValue;
      a--;
      Assert.Equal(int.MaxValue, a);
    }

    [Fact]
    public void CheckedOverflowTest()
    {
      int a = int.MinValue;
      Assert.Throws<OverflowException>(() => checked(a--));
    }
  }
}