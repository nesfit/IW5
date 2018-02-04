using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace DataTypes
{
  [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
  public class DecimalEqualitySample
  {
    [Fact]
    public void FloatTrunctationTest()
    {
      double x = 49.0;
      double y = 1 / x;

      double calculatedResult = x * y;
      double expectedResult = 1.0;

      bool areSame = calculatedResult == expectedResult;
      Assert.False(areSame);

      areSame = Math.Abs(calculatedResult - expectedResult) < 0.000000000000001;
      Assert.True(areSame);

      Assert.Equal(expectedResult, calculatedResult, 15);
    }
  }
}