using Xunit;
namespace DataTypes
{
  public class NumericalNotationSample
  {
    [Fact]
    public void NumericalNotationTest()
    {
      Assert.IsType<float>(1f);
      Assert.IsType<double>(1d);
      Assert.IsType<decimal>(1m);
      Assert.IsType<uint>(1u);
      Assert.IsType<long>(1L);
      Assert.IsType<ulong>(1ul);
    }
  }
}