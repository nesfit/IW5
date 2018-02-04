using Xunit;
namespace DataTypes
{
  public class DefaultValuesSample
  {
    private int y;

    [Fact]
    public void DefaultValuesTest()
    {
      int x;
      //Assert.Equal(0,x); // Compile-time error
      var ints = new int[2];
      Assert.Equal(0, ints[0]);
      Assert.Equal(0, ints[1]);
      Assert.Equal(0, this.y);
    }
  }
}