using Xunit;
namespace DataTypes
{
  public class TruncationSample
  {
    [Fact]
    public void FloatTruncationTest()
    {
      float f1 = 0.09f * 100f;
      float f2 = 0.09f * 99.999999f;
      Assert.False(f1>f2);
    }

    [Fact]
    public void DoubleDecimalTruncationTest()
    {
      decimal m = 1M  /  6M;                          // 0.1666666666666666666666666667M
      double  d = 1.0 / 6.0;                          // 0.16666666666666666
      decimal notQuiteWholeM = m + m + m + m + m + m; // 1.0000000000000000000000000002M
      double  notQuiteWholeD = d + d + d + d + d + d; // 0.99999999999999989
      Assert.NotEqual(1M, notQuiteWholeM);
      Assert.NotEqual(1.0, notQuiteWholeD);
    }
  }
}